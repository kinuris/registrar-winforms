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

        public FrmGeneralAverageReport(string selectedSchoolYear, string selectedCourse)
        {
            InitializeComponent();
            schoolYear = selectedSchoolYear;
            course = selectedCourse;
        }

        private void FrmGeneralAverageReport_Load(object sender, EventArgs e)
        {
            try
            {
                lblSchoolYear.Text = $"School Year: {schoolYear}";
                lblCourse.Text = $"Course: {course}";
                
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

                    // Query to calculate general average and ranking
                    string query = @"
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
                        AND se.cis_fgrade IS NOT NULL 
                        AND se.cis_fgrade != ''
                        AND se.cis_fgrade NOT IN ('8', '9', '10', '11', '18', '56', '66')  -- Exclude non-numerical grades
                        GROUP BY sp.cis_fcuidno, sp.cis_studno, sp.cis_lastname, sp.cis_firstname, sp.cis_midname, sp.cis_course
                        HAVING general_average IS NOT NULL
                        ORDER BY general_average ASC, sp.cis_lastname ASC, sp.cis_firstname ASC";

                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
                    {
                        mySqlCommand.Parameters.AddWithValue("@schoolYear", schoolYear);
                        mySqlCommand.Parameters.AddWithValue("@course", course);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show($"No student records found for School Year '{schoolYear}' and Course '{course}'.", 
                                          "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Bind data to DataGridView
                        dgvGeneralAverage.DataSource = dataTable;
                        
                        // Configure column headers and formatting
                        ConfigureDataGridView();
                        
                        // Update summary labels
                        lblTotalStudents.Text = $"Total Students: {dataTable.Rows.Count}";
                        
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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a simple print preview
                PrintDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDataGridView()
        {
            try
            {
                // Simple print functionality - you can enhance this with Crystal Reports if needed
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
            // Basic print layout - you can enhance this as needed
            Font headerFont = new Font("Arial", 16, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 12, FontStyle.Regular);
            Font bodyFont = new Font("Arial", 10, FontStyle.Regular);

            float yPos = 100;
            float leftMargin = e.MarginBounds.Left;

            // Print header
            e.Graphics.DrawString("GENERAL AVERAGE REPORT", headerFont, Brushes.Black, leftMargin, yPos);
            yPos += 40;

            e.Graphics.DrawString($"School Year: {schoolYear}", subHeaderFont, Brushes.Black, leftMargin, yPos);
            yPos += 25;

            e.Graphics.DrawString($"Course: {course}", subHeaderFont, Brushes.Black, leftMargin, yPos);
            yPos += 25;

            e.Graphics.DrawString($"Generated on: {DateTime.Now:MMMM dd, yyyy}", subHeaderFont, Brushes.Black, leftMargin, yPos);
            yPos += 40;

            // Print column headers
            string header = "Rank\tStudent No.\tStudent Name\t\t\tGeneral Average";
            e.Graphics.DrawString(header, bodyFont, Brushes.Black, leftMargin, yPos);
            yPos += 25;

            // Print data rows (limited to fit on page)
            int rowCount = 0;
            foreach (DataGridViewRow row in dgvGeneralAverage.Rows)
            {
                if (rowCount >= 30) break; // Limit rows per page
                if (row.Cells["class_rank"].Value != null)
                {
                    string line = $"{row.Cells["class_rank"].Value}\t{row.Cells["cis_studno"].Value}\t{row.Cells["full_name"].Value}\t\t\t{row.Cells["general_average"].Value:F2}";
                    e.Graphics.DrawString(line, bodyFont, Brushes.Black, leftMargin, yPos);
                    yPos += 20;
                    rowCount++;
                }
            }
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
                saveFileDialog.FileName = $"General_Average_{course}_{schoolYear}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    
                    // Add headers
                    sb.AppendLine($"General Average Report - {course} - {schoolYear}");
                    sb.AppendLine($"Generated on: {DateTime.Now:MMMM dd, yyyy hh:mm tt}");
                    sb.AppendLine();
                    
                    // Add column headers
                    sb.AppendLine("Rank,Student No.,Student Name,Course,Total Subjects,General Average,Credits Earned,Failed Subjects");
                    
                    // Add data rows
                    foreach (DataGridViewRow row in dgvGeneralAverage.Rows)
                    {
                        if (row.Cells["class_rank"].Value != null)
                        {
                            sb.AppendLine($"{row.Cells["class_rank"].Value},{row.Cells["cis_studno"].Value},\"{row.Cells["full_name"].Value}\",{row.Cells["cis_course"].Value},{row.Cells["total_subjects"].Value},{row.Cells["general_average"].Value:F2},{row.Cells["total_credits_earned"].Value},{row.Cells["failed_subjects"].Value}");
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