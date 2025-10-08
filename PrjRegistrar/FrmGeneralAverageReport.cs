using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmGeneralAverageReport : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        private string schoolYear;
        private string course;
        private string yearLevel;
        private int topStudents;

        // Updated constructor to accept year level and top students parameters
        public FrmGeneralAverageReport(string selectedSchoolYear, string selectedCourse, string selectedYearLevel = "All Year Levels", int selectedTopStudents = 0)
        {
            InitializeComponent();
            schoolYear = selectedSchoolYear;
            course = selectedCourse;
            yearLevel = selectedYearLevel;
            topStudents = selectedTopStudents;
        }

        // Keep backward compatibility with old constructor
        public FrmGeneralAverageReport(string selectedSchoolYear, string selectedCourse)
        {
            InitializeComponent();
            schoolYear = selectedSchoolYear;
            course = selectedCourse;
            yearLevel = "All Year Levels";
            topStudents = 0;
        }

        private void FrmGeneralAverageReport_Load(object sender, EventArgs e)
        {
            try
            {
                lblSchoolYear.Text = $"School Year: {schoolYear}";
                lblCourse.Text = $"Course: {course}";
                
                // Only update year level label if it exists in the designer
                try
                {
                    if (this.Controls.Find("lblYearLevel", true).Length > 0)
                    {
                        var yearLevelControl = this.Controls.Find("lblYearLevel", true)[0] as Label;
                        if (yearLevelControl != null)
                        {
                            string yearLevelDisplay = yearLevel == "All Year Levels" ? "All" : yearLevel;
                            string topStudentsDisplay = topStudents > 0 ? $" (Top {topStudents})" : "";
                            yearLevelControl.Text = $"Year Level: {yearLevelDisplay}{topStudentsDisplay}";
                        }
                    }
                }
                catch
                {
                    // Ignore if year level label doesn't exist
                }
                
                LoadGeneralAverageData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGeneralAverageData()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    // Build the year level filter condition
                    string yearLevelCondition = "";
                    if (!string.IsNullOrEmpty(yearLevel) && yearLevel != "All Year Levels")
                    {
                        yearLevelCondition = "AND sp.cis_yrlevel = @yearLevel";
                    }

                    // Build the LIMIT clause for top students
                    string limitClause = "";
                    if (topStudents > 0)
                    {
                        limitClause = $"LIMIT {topStudents}";
                    }

                    // Query to calculate general average and ranking
                    string query = $@"
                        SELECT 
                            ROW_NUMBER() OVER (ORDER BY AVG(CASE 
                                WHEN se.cis_fgrade IN ('1.0', '1.25', '1.5', '1.75', '2.0', '2.25', '2.5', '2.75', '3.0') 
                                THEN CAST(se.cis_fgrade AS DECIMAL(3,2))
                                WHEN se.cis_fgrade = '5.0' THEN 5.0
                                ELSE NULL 
                            END) ASC) AS class_rank,
                            sp.cis_studno,
                            CONCAT(sp.cis_lastname, ', ', sp.cis_firstname, 
                                   CASE 
                                       WHEN sp.cis_midname IS NOT NULL AND sp.cis_midname != '' 
                                       THEN CONCAT(' ', LEFT(sp.cis_midname, 1), '.')
                                       ELSE ''
                                   END) AS full_name,
                            sp.cis_course,
                            sp.cis_yrlevel,
                            COUNT(CASE 
                                WHEN se.cis_fgrade IN ('1.0', '1.25', '1.5', '1.75', '2.0', '2.25', '2.5', '2.75', '3.0', '5.0') 
                                THEN 1 
                                ELSE NULL 
                            END) AS total_subjects,
                            ROUND(AVG(CASE 
                                WHEN se.cis_fgrade IN ('1.0', '1.25', '1.5', '1.75', '2.0', '2.25', '2.5', '2.75', '3.0') 
                                THEN CAST(se.cis_fgrade AS DECIMAL(3,2))
                                WHEN se.cis_fgrade = '5.0' THEN 5.0
                                ELSE NULL 
                            END), 2) AS general_average,
                            SUM(CASE 
                                WHEN se.cis_fgrade IN ('1.0', '1.25', '1.5', '1.75', '2.0', '2.25', '2.5', '2.75', '3.0') 
                                THEN CAST(se.cis_credits AS DECIMAL(3,1))
                                ELSE 0 
                            END) AS total_credits_earned,
                            COUNT(CASE WHEN se.cis_fgrade = '5.0' OR se.cis_fgrade = '33' THEN 1 ELSE NULL END) AS failed_subjects
                        FROM mtbl_studprofile sp
                        INNER JOIN reg_subjenrolled se ON sp.cis_fcuidno = se.cis_fcuidno
                        WHERE se.cis_schlyr = @schoolYear 
                        AND sp.cis_course = @course
                        {yearLevelCondition}
                        AND se.cis_fgrade IS NOT NULL 
                        AND se.cis_fgrade != ''
                        AND se.cis_fgrade NOT IN ('8', '9', '10', '11', '18', '56', '66')  -- Exclude non-numerical grades
                        GROUP BY sp.cis_fcuidno, sp.cis_studno, sp.cis_lastname, sp.cis_firstname, sp.cis_midname, sp.cis_course, sp.cis_yrlevel
                        HAVING general_average IS NOT NULL
                        ORDER BY general_average ASC, sp.cis_lastname ASC, sp.cis_firstname ASC
                        {limitClause}";

                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@schoolYear", schoolYear);
                        mySqlCommand.Parameters.AddWithValue("@course", course);
                        
                        // Only add year level parameter if it's not "All Year Levels"
                        if (!string.IsNullOrEmpty(yearLevel) && yearLevel != "All Year Levels")
                        {
                            mySqlCommand.Parameters.AddWithValue("@yearLevel", yearLevel);
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            string yearLevelText = string.IsNullOrEmpty(yearLevel) || yearLevel == "All Year Levels" ? "all year levels" : $"Year Level {yearLevel}";
                            MessageBox.Show($"No student records found for School Year '{schoolYear}', Course '{course}', and {yearLevelText}.", 
                                          "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Add a display_rank column for proper tie handling
                        dataTable.Columns.Add("display_rank", typeof(string));
                        
                        // Calculate proper rankings with tie detection
                        CalculateDisplayRanks(dataTable);

                        // Bind data to DataGridView
                        dgvGeneralAverage.DataSource = dataTable;
                        
                        // Configure column headers and formatting
                        ConfigureDataGridView();
                        
                        // Update summary labels
                        string studentCountText = topStudents > 0 ? $"Showing Top {dataTable.Rows.Count} Students" : $"Total Students: {dataTable.Rows.Count}";
                        lblTotalStudents.Text = studentCountText;
                        
                        if (dataTable.Rows.Count > 0)
                        {
                            // Calculate class average
                            decimal classAverage = 0;
                            int count = 0;
                            foreach (DataRow row in dataTable.Rows)
                            {
                                if (row["general_average"] != DBNull.Value)
                                {
                                    classAverage += Convert.ToDecimal(row["general_average"]);
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                classAverage = Math.Round(classAverage / count, 2);
                                lblClassAverage.Text = $"Class Average: {classAverage:F2}";
                            }

                            // Find top student
                            DataRow topStudent = dataTable.Rows[0];
                            lblTopStudent.Text = $"Top Student: {topStudent["full_name"]} (GWA: {topStudent["general_average"]})";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading general average data: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateDisplayRanks(DataTable dataTable)
        {
            // Group students by their general average to detect ties
            var gradeGroups = new System.Collections.Generic.Dictionary<decimal, System.Collections.Generic.List<int>>();
            
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i]["general_average"] != DBNull.Value)
                {
                    decimal avg = Convert.ToDecimal(dataTable.Rows[i]["general_average"]);
                    
                    if (!gradeGroups.ContainsKey(avg))
                    {
                        gradeGroups[avg] = new System.Collections.Generic.List<int>();
                    }
                    gradeGroups[avg].Add(i);
                }
            }
            
            // Assign display ranks with T- prefix for ties
            foreach (var gradeGroup in gradeGroups)
            {
                var rowIndices = gradeGroup.Value;
                
                if (rowIndices.Count > 1)
                {
                    // This is a tie - use T- prefix
                    int baseRank = Convert.ToInt32(dataTable.Rows[rowIndices[0]]["class_rank"]);
                    string displayRank = $"T-{baseRank}";
                    
                    foreach (int rowIndex in rowIndices)
                    {
                        dataTable.Rows[rowIndex]["display_rank"] = displayRank;
                    }
                }
                else
                {
                    // No tie - use simple rank number
                    int rowIndex = rowIndices[0];
                    int rank = Convert.ToInt32(dataTable.Rows[rowIndex]["class_rank"]);
                    dataTable.Rows[rowIndex]["display_rank"] = rank.ToString();
                }
            }
        }

        private void ConfigureDataGridView()
        {
            // Set font
            dgvGeneralAverage.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvGeneralAverage.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvGeneralAverage.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);

            // Hide the original class_rank column
            dgvGeneralAverage.Columns["class_rank"].Visible = false;

            // Configure display_rank column (the one with T- prefix for ties)
            dgvGeneralAverage.Columns["display_rank"].HeaderText = "Rank";
            dgvGeneralAverage.Columns["display_rank"].Width = 80;
            dgvGeneralAverage.Columns["display_rank"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGeneralAverage.Columns["display_rank"].DisplayIndex = 0; // Show as first column

            dgvGeneralAverage.Columns["cis_studno"].HeaderText = "Student No.";
            dgvGeneralAverage.Columns["cis_studno"].Width = 120;
            dgvGeneralAverage.Columns["cis_studno"].DisplayIndex = 1;

            dgvGeneralAverage.Columns["full_name"].HeaderText = "Student Name";
            dgvGeneralAverage.Columns["full_name"].Width = 250;
            dgvGeneralAverage.Columns["full_name"].DisplayIndex = 2;

            dgvGeneralAverage.Columns["cis_course"].HeaderText = "Course";
            dgvGeneralAverage.Columns["cis_course"].Width = 80;
            dgvGeneralAverage.Columns["cis_course"].DisplayIndex = 3;

            // Add year level column if it exists
            int displayIndex = 4;
            if (dgvGeneralAverage.Columns.Contains("cis_yrlevel"))
            {
                dgvGeneralAverage.Columns["cis_yrlevel"].HeaderText = "Year Level";
                dgvGeneralAverage.Columns["cis_yrlevel"].Width = 80;
                dgvGeneralAverage.Columns["cis_yrlevel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvGeneralAverage.Columns["cis_yrlevel"].DisplayIndex = displayIndex++;
            }

            dgvGeneralAverage.Columns["total_subjects"].HeaderText = "Total Subjects";
            dgvGeneralAverage.Columns["total_subjects"].Width = 100;
            dgvGeneralAverage.Columns["total_subjects"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGeneralAverage.Columns["total_subjects"].DisplayIndex = displayIndex++;

            dgvGeneralAverage.Columns["general_average"].HeaderText = "General Average";
            dgvGeneralAverage.Columns["general_average"].Width = 120;
            dgvGeneralAverage.Columns["general_average"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGeneralAverage.Columns["general_average"].DefaultCellStyle.Format = "F2";
            dgvGeneralAverage.Columns["general_average"].DisplayIndex = displayIndex++;

            dgvGeneralAverage.Columns["total_credits_earned"].HeaderText = "Credits Earned";
            dgvGeneralAverage.Columns["total_credits_earned"].Width = 100;
            dgvGeneralAverage.Columns["total_credits_earned"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGeneralAverage.Columns["total_credits_earned"].DisplayIndex = displayIndex++;

            dgvGeneralAverage.Columns["failed_subjects"].HeaderText = "Failed Subjects";
            dgvGeneralAverage.Columns["failed_subjects"].Width = 100;
            dgvGeneralAverage.Columns["failed_subjects"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGeneralAverage.Columns["failed_subjects"].DisplayIndex = displayIndex++;

            // Color coding for ranks based on display_rank
            dgvGeneralAverage.CellFormatting += DgvGeneralAverage_CellFormatting;

            // Auto-resize columns to fit content
            dgvGeneralAverage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void DgvGeneralAverage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvGeneralAverage.Columns[e.ColumnIndex].Name == "display_rank")
            {
                if (e.Value != null)
                {
                    string rankStr = e.Value.ToString();
                    
                    // Extract numeric rank (remove T- prefix if present)
                    string numericPart = rankStr.Replace("T-", "");
                    
                    if (int.TryParse(numericPart, out int rank))
                    {
                        // Color coding for top ranks
                        if (rank == 1)
                        {
                            e.CellStyle.BackColor = Color.Gold;
                            e.CellStyle.ForeColor = Color.Black;
                            e.CellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        }
                        else if (rank == 2)
                        {
                            e.CellStyle.BackColor = Color.Silver;
                            e.CellStyle.ForeColor = Color.Black;
                            e.CellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        }
                        else if (rank == 3)
                        {
                            e.CellStyle.BackColor = Color.FromArgb(205, 127, 50); // Bronze
                            e.CellStyle.ForeColor = Color.White;
                            e.CellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        }
                    }
                }
            }
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ShowPrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing print preview: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Direct print without preview
                PrintDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowPrintPreview()
        {
            try
            {
                // Create print document
                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                printDocument.PrintPage += PrintDocument_PrintPage;

                // Create and show print preview dialog
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.WindowState = FormWindowState.Maximized;
                printPreviewDialog.Text = "General Average Report - Print Preview";
                
                // Set print preview dialog properties
                printPreviewDialog.UseAntiAlias = true;
                printPreviewDialog.AutoScrollMargin = new Size(10, 10);
                printPreviewDialog.AutoScrollMinSize = new Size(400, 400);
                
                DialogResult result = printPreviewDialog.ShowDialog();
                
                // If user wants to print from preview, show print dialog
                if (result == DialogResult.OK)
                {
                    PrintDataGridView();
                }
                
                printPreviewDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing print preview: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDataGridView()
        {
            try
            {
                // Simple print functionality
                PrintDialog printDialog = new PrintDialog();
                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();

                printDocument.PrintPage += PrintDocument_PrintPage;
                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing report: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Define fonts
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 14, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 11, FontStyle.Regular);
            Font smallFont = new Font("Arial", 10, FontStyle.Italic);
            Font tableHeaderFont = new Font("Arial", 12, FontStyle.Bold);
            Font tableDataFont = new Font("Arial", 11, FontStyle.Regular);

            // Define brushes and pens
            Brush blackBrush = Brushes.Black;
            Pen blackPen = new Pen(Color.Black, 1);

            float yPos = 80;
            float leftMargin = e.MarginBounds.Left;
            float rightMargin = e.MarginBounds.Right;
            float pageWidth = rightMargin - leftMargin;

            // Print university header
            e.Graphics.DrawString("FILAMER CHRISTIAN UNIVERSITY", headerFont, blackBrush, leftMargin, yPos);
            yPos += 25;

            e.Graphics.DrawString("Roxas City", bodyFont, blackBrush, leftMargin, yPos);
            yPos += 40;

            // Print report title
            string reportTitle = $"STUDENTS GENERAL AVERAGE AND CLASS RANKING";
            e.Graphics.DrawString(reportTitle, subHeaderFont, blackBrush, leftMargin, yPos);
            yPos += 25;

            string courseYearTitle = $"{course} {(string.IsNullOrEmpty(yearLevel) || yearLevel == "All Year Levels" ? "" : yearLevel)} {schoolYear}";
            if (topStudents > 0)
            {
                courseYearTitle += $" (TOP {topStudents} STUDENTS)";
            }
            e.Graphics.DrawString(courseYearTitle, subHeaderFont, blackBrush, leftMargin, yPos);
            yPos += 40;

            // Print run date
            string runDate = $"generated: {DateTime.Now:dd/MM/yyyy HH:mm:ss tt}";
            e.Graphics.DrawString(runDate, smallFont, blackBrush, leftMargin, yPos);
            yPos += 25;

            // Draw top line
            e.Graphics.DrawLine(blackPen, leftMargin, yPos, rightMargin, yPos);
            yPos += 15;

            // Print table headers
            float nameColumnStart = leftMargin;
            float nameColumnWidth = pageWidth * 0.6f;
            float gradeColumnStart = nameColumnStart + nameColumnWidth;
            float gradeColumnWidth = pageWidth * 0.2f;
            float rankColumnStart = gradeColumnStart + gradeColumnWidth;
            float rankColumnWidth = pageWidth * 0.2f;

            // First line of headers
            e.Graphics.DrawString("STUDENT NAME", tableHeaderFont, blackBrush, nameColumnStart, yPos);
            e.Graphics.DrawString("AVERAGE", tableHeaderFont, blackBrush, gradeColumnStart + 50, yPos);
            e.Graphics.DrawString("CLASS", tableHeaderFont, blackBrush, rankColumnStart + 50, yPos);
            yPos += 20;

            // Second line of headers
            e.Graphics.DrawString("GRADE", tableHeaderFont, blackBrush, gradeColumnStart + 60, yPos);
            e.Graphics.DrawString("RANKING", tableHeaderFont, blackBrush, rankColumnStart + 40, yPos);
            yPos += 25;

            // Draw header separator line
            e.Graphics.DrawLine(blackPen, leftMargin, yPos, rightMargin, yPos);
            yPos += 18;

            // Print student data with smart row limiting
            int rowCount = 0;
            
            // When topStudents = 0, show ALL students (no limit)
            // When topStudents > 0, show only that many students
            // For preview/print, we may need to paginate large datasets
            int maxRowsPerPage;
            if (topStudents > 0)
            {
                // Show exactly the number requested (may span multiple pages)
                maxRowsPerPage = topStudents;
            }
            else
            {
                // Show all students, but use reasonable pagination for very large datasets
                maxRowsPerPage = dgvGeneralAverage.Rows.Count; // No limit - show all
            }

            foreach (DataGridViewRow row in dgvGeneralAverage.Rows)
            {
                if (rowCount >= maxRowsPerPage) break;
                if (row.Cells["display_rank"].Value != null)
                {
                    string studentName = row.Cells["full_name"].Value?.ToString() ?? "";
                    string averageGrade = Convert.ToDecimal(row.Cells["general_average"].Value).ToString("F2");
                    
                    // Use display_rank which includes T- prefix for ties
                    string classRank = row.Cells["display_rank"].Value?.ToString() ?? "";

                    // Print student data
                    e.Graphics.DrawString(studentName.ToUpper(), tableDataFont, blackBrush, nameColumnStart, yPos);
                    
                    // Right-align the grade
                    SizeF gradeSize = e.Graphics.MeasureString(averageGrade, tableDataFont);
                    e.Graphics.DrawString(averageGrade, tableDataFont, blackBrush, gradeColumnStart + gradeColumnWidth - gradeSize.Width - 10, yPos);
                    
                    // Right-align the rank
                    SizeF rankSize = e.Graphics.MeasureString(classRank, tableDataFont);
                    e.Graphics.DrawString(classRank, tableDataFont, blackBrush, rankColumnStart + rankColumnWidth - rankSize.Width - 10, yPos);

                    yPos += 20;
                    rowCount++;
                }
            }

            // Draw bottom line
            yPos += 15;
            e.Graphics.DrawLine(blackPen, leftMargin, yPos, rightMargin, yPos);
            yPos += 20;

            // Print record count
            string recordCount = $"Record: {dgvGeneralAverage.Rows.Count}/{dgvGeneralAverage.Rows.Count}";
            e.Graphics.DrawString(recordCount, smallFont, blackBrush, leftMargin, yPos);

            // Dispose of created objects
            headerFont.Dispose();
            subHeaderFont.Dispose();
            bodyFont.Dispose();
            smallFont.Dispose();
            tableHeaderFont.Dispose();
            tableDataFont.Dispose();
            blackPen.Dispose();
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "csv";
                
                string yearLevelSuffix = string.IsNullOrEmpty(yearLevel) || yearLevel == "All Year Levels" ? "AllLevels" : $"YL{yearLevel}";
                string topStudentsSuffix = topStudents > 0 ? $"_Top{topStudents}" : "";
                saveFileDialog.FileName = $"General_Average_{course}_{yearLevelSuffix}{topStudentsSuffix}_{schoolYear}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    
                    // Add headers
                    string yearLevelText = string.IsNullOrEmpty(yearLevel) || yearLevel == "All Year Levels" ? "All Year Levels" : $"Year Level {yearLevel}";
                    string topStudentsText = topStudents > 0 ? $" (Top {topStudents} Students)" : "";
                    sb.AppendLine($"General Average Report - {course} - {yearLevelText}{topStudentsText} - {schoolYear}");
                    sb.AppendLine($"Generated on: {DateTime.Now:MMMM dd, yyyy hh:mm tt}");
                    sb.AppendLine();
                    
                    // Add column headers
                    if (dgvGeneralAverage.Columns.Contains("cis_yrlevel"))
                    {
                        sb.AppendLine("Rank,Student No.,Student Name,Course,Year Level,Total Subjects,General Average,Credits Earned,Failed Subjects");
                    }
                    else
                    {
                        sb.AppendLine("Rank,Student No.,Student Name,Course,Total Subjects,General Average,Credits Earned,Failed Subjects");
                    }
                    
                    // Add data rows using display_rank
                    foreach (DataGridViewRow row in dgvGeneralAverage.Rows)
                    {
                        if (row.Cells["display_rank"].Value != null)
                        {
                            string rank = row.Cells["display_rank"].Value?.ToString() ?? "";
                            string studentNo = row.Cells["cis_studno"].Value?.ToString() ?? "";
                            string fullName = row.Cells["full_name"].Value?.ToString() ?? "";
                            string courseStr = row.Cells["cis_course"].Value?.ToString() ?? "";
                            string totalSubjects = row.Cells["total_subjects"].Value?.ToString() ?? "";
                            string genAvg = row.Cells["general_average"].Value != null ? Convert.ToDecimal(row.Cells["general_average"].Value).ToString("F2") : "";
                            string creditsEarned = row.Cells["total_credits_earned"].Value?.ToString() ?? "";
                            string failedSubjects = row.Cells["failed_subjects"].Value?.ToString() ?? "";
                            
                            if (dgvGeneralAverage.Columns.Contains("cis_yrlevel"))
                            {
                                string yearLevelStr = row.Cells["cis_yrlevel"].Value?.ToString() ?? "";
                                sb.AppendLine($"{rank},{studentNo},\"{fullName}\",{courseStr},{yearLevelStr},{totalSubjects},{genAvg},{creditsEarned},{failedSubjects}");
                            }
                            else
                            {
                                sb.AppendLine($"{rank},{studentNo},\"{fullName}\",{courseStr},{totalSubjects},{genAvg},{creditsEarned},{failedSubjects}");
                            }
                        }
                    }
                    
                    System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                    MessageBox.Show("Export completed successfully!", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to export data: {ex.Message}");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmGeneralAverageReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }
    }
}