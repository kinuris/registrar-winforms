using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace PrjRegistrar
{
    public partial class FrmGeneralAverageReport : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        private string schoolYear;
        private string course;
        private string yearLevel;

        // Updated constructor to accept year level parameter
        public FrmGeneralAverageReport(string selectedSchoolYear, string selectedCourse, string selectedYearLevel = "All Year Levels")
        {
            InitializeComponent();
            schoolYear = selectedSchoolYear;
            course = selectedCourse;
            yearLevel = selectedYearLevel;
        }

        // Keep backward compatibility with old constructor
        public FrmGeneralAverageReport(string selectedSchoolYear, string selectedCourse)
        {
            InitializeComponent();
            schoolYear = selectedSchoolYear;
            course = selectedCourse;
            yearLevel = "All Year Levels";
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
                        var lblYearLevelControl = this.Controls.Find("lblYearLevel", true)[0] as Label;
                        if (lblYearLevelControl != null)
                        {
                            lblYearLevelControl.Text = $"Year Level: {(yearLevel == "All Year Levels" ? "All" : yearLevel)}";
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

                    // Query to calculate general average and ranking
                    string query = $@"
                        SELECT 
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
                        ORDER BY general_average ASC, sp.cis_lastname ASC, sp.cis_firstname ASC";

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

                        // Calculate rankings with proper tie handling
                        DataTable rankedDataTable = CalculateRankingsWithTies(dataTable);
                        
                        // Bind data to DataGridView
                        dgvGeneralAverage.DataSource = rankedDataTable;
                        
                        // Configure column headers and formatting
                        ConfigureDataGridView();
                        
                        // Update summary labels
                        lblTotalStudents.Text = $"Total Students: {rankedDataTable.Rows.Count}";
                        
                        if (rankedDataTable.Rows.Count > 0)
                        {
                            // Calculate class average
                            decimal classAverage = 0;
                            int count = 0;
                            foreach (DataRow row in rankedDataTable.Rows)
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
                            DataRow topStudent = rankedDataTable.Rows[0];
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

        private DataTable CalculateRankingsWithTies(DataTable originalDataTable)
        {
            // Create a new DataTable with rankings
            DataTable rankedTable = originalDataTable.Clone();
            rankedTable.Columns.Add("class_rank", typeof(decimal));

            // Convert to list for easier processing
            var studentData = new List<DataRow>();
            foreach (DataRow row in originalDataTable.Rows)
            {
                studentData.Add(row);
            }

            // Sort on general average (ascending - lower is better) and then by name
            studentData = studentData.OrderBy(r => Convert.ToDecimal(r["general_average"]))
                                   .ThenBy(r => r["full_name"].ToString())
                                   .ToList();

            // Calculate rankings with tie handling
            int currentRank = 1;
            decimal? previousGrade = null;
            var tiedStudents = new List<DataRow>();

            for (int i = 0; i < studentData.Count; i++)
            {
                decimal currentGrade = Convert.ToDecimal(studentData[i]["general_average"]);

                // If this is a different grade from the previous one, process any tied students
                if (previousGrade.HasValue && currentGrade != previousGrade.Value)
                {
                    // Assign tied rank to all students with the same grade
                    AssignTiedRank(tiedStudents, currentRank - tiedStudents.Count, rankedTable);
                    currentRank = i + 1; // Next rank after the tied group
                    tiedStudents.Clear();
                }

                // Add current student to tied group
                tiedStudents.Add(studentData[i]);
                previousGrade = currentGrade;
            }

            // Process the last group of tied students
            if (tiedStudents.Count > 0)
            {
                AssignTiedRank(tiedStudents, currentRank - tiedStudents.Count + 1, rankedTable);
            }

            return rankedTable;
        }

        private void AssignTiedRank(List<DataRow> tiedStudents, int startRank, DataTable rankedTable)
        {
            if (tiedStudents.Count == 1)
            {
                // No tie, assign regular rank
                DataRow newRow = rankedTable.NewRow();
                newRow.ItemArray = tiedStudents[0].ItemArray;
                newRow["class_rank"] = startRank;
                rankedTable.Rows.Add(newRow);
            }
            else
            {
                // Calculate tied rank (average of positions)
                int endRank = startRank + tiedStudents.Count - 1;
                decimal tiedRank = (startRank + endRank) / 2.0m;

                // Assign tied rank to all students
                foreach (DataRow student in tiedStudents)
                {
                    DataRow newRow = rankedTable.NewRow();
                    newRow.ItemArray = student.ItemArray;
                    newRow["class_rank"] = tiedRank;
                    rankedTable.Rows.Add(newRow);
                }
            }
        }

        private void ConfigureDataGridView()
        {
            // Set font
            dgvGeneralAverage.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvGeneralAverage.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvGeneralAverage.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);

            // Configure column headers and widths
            dgvGeneralAverage.Columns["class_rank"].HeaderText = "Rank";
            dgvGeneralAverage.Columns["class_rank"].Width = 60;
            dgvGeneralAverage.Columns["class_rank"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvGeneralAverage.Columns["cis_studno"].HeaderText = "Student No.";
            dgvGeneralAverage.Columns["cis_studno"].Width = 120;

            dgvGeneralAverage.Columns["full_name"].HeaderText = "Student Name";
            dgvGeneralAverage.Columns["full_name"].Width = 250;

            dgvGeneralAverage.Columns["cis_course"].HeaderText = "Course";
            dgvGeneralAverage.Columns["cis_course"].Width = 80;

            // Add year level column if it exists
            if (dgvGeneralAverage.Columns.Contains("cis_yrlevel"))
            {
                dgvGeneralAverage.Columns["cis_yrlevel"].HeaderText = "Year Level";
                dgvGeneralAverage.Columns["cis_yrlevel"].Width = 80;
                dgvGeneralAverage.Columns["cis_yrlevel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvGeneralAverage.Columns["total_subjects"].HeaderText = "Total Subjects";
            dgvGeneralAverage.Columns["total_subjects"].Width = 100;
            dgvGeneralAverage.Columns["total_subjects"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvGeneralAverage.Columns["general_average"].HeaderText = "General Average";
            dgvGeneralAverage.Columns["general_average"].Width = 120;
            dgvGeneralAverage.Columns["general_average"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGeneralAverage.Columns["general_average"].DefaultCellStyle.Format = "F2";

            dgvGeneralAverage.Columns["total_credits_earned"].HeaderText = "Credits Earned";
            dgvGeneralAverage.Columns["total_credits_earned"].Width = 100;
            dgvGeneralAverage.Columns["total_credits_earned"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvGeneralAverage.Columns["failed_subjects"].HeaderText = "Failed Subjects";
            dgvGeneralAverage.Columns["failed_subjects"].Width = 100;
            dgvGeneralAverage.Columns["failed_subjects"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Color coding for ranks
            dgvGeneralAverage.CellFormatting += DgvGeneralAverage_CellFormatting;

            // Auto-resize columns to fit content
            dgvGeneralAverage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void DgvGeneralAverage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvGeneralAverage.Columns[e.ColumnIndex].Name == "class_rank")
            {
                if (e.Value != null)
                {
                    int rank = Convert.ToInt32(e.Value);
                    
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
            e.Graphics.DrawString(courseYearTitle, subHeaderFont, blackBrush, leftMargin, yPos);
            yPos += 40;

            // Print run date
            string runDate = $"rundate: {DateTime.Now:dd/MM/yyyy HH:mm:ss tt}";
            e.Graphics.DrawString(runDate, smallFont, blackBrush, leftMargin, yPos);
            yPos += 25; // Reduced spacing to prevent overlap

            // Draw top line - moved down to avoid overlap
            e.Graphics.DrawLine(blackPen, leftMargin, yPos, rightMargin, yPos);
            yPos += 15; // Increased space after line

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
            e.Graphics.DrawString("CLASS", tableHeaderFont, blackBrush, rankColumnStart + 60, yPos);
            yPos += 20;

            // Second line of headers
            e.Graphics.DrawString("GRADE", tableHeaderFont, blackBrush, gradeColumnStart + 50, yPos);
            e.Graphics.DrawString("RANKING", tableHeaderFont, blackBrush, rankColumnStart + 40, yPos);
            yPos += 20; // Increased space before separator line

            // Draw header separator line
            e.Graphics.DrawLine(blackPen, leftMargin, yPos, rightMargin, yPos);
            yPos += 18; // Increased space after separator line

            // Print student data
            int rowCount = 0;
            int maxRowsPerPage = 22; // Reduced to account for better spacing

            foreach (DataGridViewRow row in dgvGeneralAverage.Rows)
            {
                if (rowCount >= maxRowsPerPage) break;
                if (row.Cells["class_rank"].Value != null)
                {
                    string studentName = row.Cells["full_name"].Value?.ToString() ?? "";
                    string averageGrade = Convert.ToDecimal(row.Cells["general_average"].Value).ToString("F2");
                    
                    // Handle tied rankings with proper decimal formatting
                    string classRank = "";
                    if (row.Cells["class_rank"].Value != null)
                    {
                        decimal rankValue = Convert.ToDecimal(row.Cells["class_rank"].Value);
                        
                        // If it's a whole number, show as integer, otherwise show with decimal
                        if (rankValue % 1 == 0)
                        {
                            classRank = ((int)rankValue).ToString();
                        }
                        else
                        {
                            classRank = rankValue.ToString("F1"); // Show one decimal place for ties
                        }
                    }

                    // Print student data
                    e.Graphics.DrawString(studentName.ToUpper(), tableDataFont, blackBrush, nameColumnStart, yPos);
                    
                    // Right-align the grade
                    SizeF gradeSize = e.Graphics.MeasureString(averageGrade, tableDataFont);
                    e.Graphics.DrawString(averageGrade, tableDataFont, blackBrush, gradeColumnStart + gradeColumnWidth - gradeSize.Width - 10, yPos);
                    
                    // Right-align the rank
                    SizeF rankSize = e.Graphics.MeasureString(classRank, tableDataFont);
                    e.Graphics.DrawString(classRank, tableDataFont, blackBrush, rankColumnStart + rankColumnWidth - rankSize.Width - 10, yPos);

                    yPos += 20; // Consistent row spacing
                    rowCount++;
                }
            }

            // Draw bottom line
            yPos += 15; // Space before bottom line
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
                saveFileDialog.FileName = $"General_Average_{course}_{yearLevelSuffix}_{schoolYear}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    
                    // Add headers
                    string yearLevelText = string.IsNullOrEmpty(yearLevel) || yearLevel == "All Year Levels" ? "All Year Levels" : $"Year Level {yearLevel}";
                    sb.AppendLine($"General Average Report - {course} - {yearLevelText} - {schoolYear}");
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
                    
                    // Add data rows
                    foreach (DataGridViewRow row in dgvGeneralAverage.Rows)
                    {
                        if (row.Cells["class_rank"].Value != null)
                        {
                            if (dgvGeneralAverage.Columns.Contains("cis_yrlevel"))
                            {
                                sb.AppendLine($"{row.Cells["class_rank"].Value},{row.Cells["cis_studno"].Value},\"{row.Cells["full_name"].Value}\",{row.Cells["cis_course"].Value},{row.Cells["cis_yrlevel"].Value},{row.Cells["total_subjects"].Value},{row.Cells["general_average"].Value:F2},{row.Cells["total_credits_earned"].Value},{row.Cells["failed_subjects"].Value}");
                            }
                            else
                            {
                                sb.AppendLine($"{row.Cells["class_rank"].Value},{row.Cells["cis_studno"].Value},\"{row.Cells["full_name"].Value}\",{row.Cells["cis_course"].Value},{row.Cells["total_subjects"].Value},{row.Cells["general_average"].Value:F2},{row.Cells["total_credits_earned"].Value},{row.Cells["failed_subjects"].Value}");
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