using System;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmRecGradesheetVerification : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        public static string classID;
        private static string previousfinalGradeValue, credits;
        int passed;

        public FrmRecGradesheetVerification()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchClassID();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintVerifiedClassID();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            VerifySubject();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" || txtSearch.Text == "Search for Class ID") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = "Search for Class ID";
        }

        private void ClearFields()
        {
            txtSearch.Text = "Search for Class ID";

            lblTeacherNo.Text = "Teacher ID No.";
            lblTeacherFullname.Text = "Teacher's Name";
            lblSchoolYear.Text = "School Year";
            lblSemester.Text = "Semester";
            lblYearLevel.Text = "Year Level";

            lblClassID.Text = "Class ID";
            lblDescriptiveTitle.Text = "Descriptive Title";
            lblCourseNo.Text = "Course No.";
            lblTime.Text = "Time";
            lblDay.Text = "Day";
            lblRoom.Text = "Room";

            lblCount.Text = "0";
            lblStatus.Text = "STATUS";
            lblDateVerified.Text = "DATE";

            dgvSubjEnrolled.DataSource = null;
            dgvSubjEnrolled.Rows.Clear();
            dgvSubjEnrolled.Columns.Clear();
            dgvSubjEnrolled.Refresh();
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchClassID();
            }
        }

        private void SearchClassID()
        {
            try
            {
                if (txtSearch.Text != "Search for Class ID")
                {
                    //reg_subjschedule
                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        string selectRegSubjSchedule = "SELECT A.cis_classid, B.cis_desc, A.cis_courseno, A.cis_time, A.cis_day, A.cis_room, A.cis_schlyr, A.cis_semester, A.cis_yrlevel, A.cis_empid, " +
                                                        "concat(C.cis_lastname, ', ', ucase(left(C.cis_firstname, 1)), lcase(substring(C.cis_firstname, 2)), ' ', ucase(left(ifnull(C.cis_midname, ''), 1)), lcase(substring(ifnull(C.cis_midname, ''), 2))) as fullname, " +
                                                        "A.cis_capacity - A.cis_available as actualCount, " +
                                                        "A.cis_status, " +
                                                        "CASE " +
                                                            "WHEN A.cis_dateverified IS NOT NULL THEN A.cis_dateverified ELSE '-- / -- / --' " +
                                                        "END AS cis_dateverified " +
                                                        "FROM reg_subjschedule A USE INDEX(idxClassid), reg_curriculum B, mtbl_employee C " +
                                                        "WHERE A.cis_classid = '" + txtSearch.Text.Trim() + "' " +
                                                        "AND A.cis_coursenoid = B.cis_coursenoid " +
                                                        "AND A.cis_empid = C.cis_empid";
                        using (MySqlCommand mySqlCommand = new MySqlCommand(selectRegSubjSchedule, mySqlConnection))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    //*******************************************************
                                    // Display SUBMITTED AND VERIFIED only
                                    //*******************************************************
                                    string status = mySqlDataReader["cis_status"].ToString();
                                    if (status.ToUpper() == "SUBMITTED" || status.ToUpper() == "VERIFIED")
                                    {
                                        lblClassID.Text = mySqlDataReader["cis_classid"].ToString();
                                        lblDescriptiveTitle.Text = mySqlDataReader["cis_desc"].ToString();
                                        lblCourseNo.Text = mySqlDataReader["cis_courseno"].ToString();
                                        lblTime.Text = mySqlDataReader["cis_time"].ToString();
                                        lblDay.Text = mySqlDataReader["cis_day"].ToString();
                                        lblRoom.Text = mySqlDataReader["cis_room"].ToString();
                                        lblSchoolYear.Text = mySqlDataReader["cis_schlyr"].ToString();
                                        lblSemester.Text = mySqlDataReader["cis_semester"].ToString();
                                        lblYearLevel.Text = mySqlDataReader["cis_yrlevel"].ToString();
                                        lblTeacherNo.Text = mySqlDataReader["cis_empid"].ToString();
                                        lblTeacherFullname.Text = mySqlDataReader["fullname"].ToString();
                                        lblCount.Text = mySqlDataReader["actualCount"].ToString();
                                        lblStatus.Text = mySqlDataReader["cis_status"].ToString().ToUpper();

                                        //Removes the space and succeeding texts(time).
                                        string dateNow = mySqlDataReader["cis_dateverified"].ToString();
                                        int indexofspace = dateNow.IndexOf(" ");
                                        lblDateVerified.Text = dateNow.Remove(indexofspace);

                                        //reg_subjenrolled table to dgvSubjEnrolled
                                        using (MySqlConnection mySqlConn = new MySqlConnection(connectionString))
                                        {
                                            mySqlConn.Open();

                                            MySqlDataAdapter mySqlDA = new MySqlDataAdapter("reg_subjenrolled_verification_searchby_classid", mySqlConn);
                                            mySqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                                            mySqlDA.SelectCommand.Parameters.AddWithValue("_cis_classid", txtSearch.Text.Trim());

                                            dgvSubjEnrolled.DataSource = null;
                                            dgvSubjEnrolled.Rows.Clear();
                                            dgvSubjEnrolled.Columns.Clear();

                                            DataTable table = new DataTable();
                                            mySqlDA.Fill(table);

                                            BindingSource bindingSource = new BindingSource
                                            {
                                                DataSource = table
                                            };

                                            dgvSubjEnrolled.DataSource = bindingSource;

                                            lblCount.Text = dgvSubjEnrolled.RowCount.ToString();
                                        }
                                    }
                                    else if (status.ToUpper() == "PENDING" || status == "")
                                    {
                                        MessageBox.Show("Class ID is PENDING for submission.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        ClearFields();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Class ID not found. Please try again.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        ClearFields();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Class ID not found. Please try again.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearFields();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintVerifiedClassID()
        {
            if (lblStatus.Text == "STATUS")
            {
                MessageBox.Show("Unable to Print. Please enter Class ID to be Verified.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lblStatus.Text == "SUBMITTED")
            {
                MessageBox.Show("Unable to Print. Please verify current records prior to printing.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lblStatus.Text == "VERIFIED")
            {
                classID = lblClassID.Text;

                FrmMain.rptName = "RptGradingSheet";
                FrmReportViewer frmReportViewer = new FrmReportViewer(FrmMain.paramrptUserFullname, FrmMain.paramrptUserDesignation);
                frmReportViewer.ShowDialog();
                frmReportViewer.Dispose();
            }
        }

        private void VerifySubject()
        {
            try
            {
                if (lblStatus.Text == "STATUS")
                {
                    MessageBox.Show("Please enter Class ID to be Verified.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else if (lblStatus.Text == "VERIFIED")
                {
                    MessageBox.Show("Class ID already Verified. \nPlease select another Class ID to be Verified.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (lblStatus.Text == "SUBMITTED")
                {
                    DialogResult dialogResult = MessageBox.Show("Proceed with verifying this Class ID?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        lblStatus.Text = "VERIFIED";

                        //Removes the space and succeeding texts(time).
                        string dateNow = DateTime.Now.ToString();
                        int indexofspace = dateNow.IndexOf(" ");
                        lblDateVerified.Text = dateNow.Remove(indexofspace);

                        using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                        {
                            mysqlcon.Open();
                            using (MySqlCommand mysqlcmd = new MySqlCommand("reg_subjenrolled_verification_update", mysqlcon))
                            {
                                mysqlcmd.CommandType = CommandType.StoredProcedure;

                                mysqlcmd.Parameters.AddWithValue("_cis_classid", lblClassID.Text);
                                mysqlcmd.Parameters.AddWithValue("_cis_status", lblStatus.Text);
                                mysqlcmd.Parameters.AddWithValue("_cis_dateverified", DateTime.Now);
                                mysqlcmd.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                mysqlcmd.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);

                                int isSaved = mysqlcmd.ExecuteNonQuery();
                                if (isSaved > 0)
                                    MessageBox.Show("Class ID : " + lblClassID.Text + " was successfully Verified.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Unable to verify Class ID : " + lblClassID.Text, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSubjEnrolled_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblStatus.Text == "SUBMITTED")
            {
                //Enable Final Grade Cell
                if (e.ColumnIndex == dgvSubjEnrolled.Columns[5].Index)
                {   
                    DataGridViewCell dataGridViewCell = dgvSubjEnrolled.Rows[e.RowIndex].Cells[5];
                    dgvSubjEnrolled.CurrentCell = dataGridViewCell;

                    dgvSubjEnrolled.ReadOnly = false;
                    dgvSubjEnrolled.BeginEdit(true);
                }
                else
                {
                    dgvSubjEnrolled.ReadOnly = true;
                    dgvSubjEnrolled.BeginEdit(false);
                }
            }
        }

        private void DgvSubjEnrolled_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            previousfinalGradeValue = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[5].Value);
        }

        private void DgvSubjEnrolled_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvSubjEnrolled.Rows[e.RowIndex].Cells[0].Value);
                string finalGrade = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[5].Value);

                //****************************************
                //if FinalGrade Value Changed or Updated
                //****************************************
                if (e.ColumnIndex == dgvSubjEnrolled.Columns["Final Grade"].Index)
                {
                    if (dgvSubjEnrolled.CurrentRow != null && previousfinalGradeValue != finalGrade)
                    {
                        if (finalGrade == "1.0" || finalGrade == "1.25" || finalGrade == "1.5" || finalGrade == "1.75" ||
                            finalGrade == "2.0" || finalGrade == "2.25" || finalGrade == "2.5" || finalGrade == "2.75" ||
                            finalGrade == "3.0" || finalGrade == "5.0" || finalGrade == "8" || finalGrade == "9" || finalGrade == "10" ||
                            finalGrade == "11" || finalGrade == "18" || finalGrade == "33" || finalGrade == "56" || finalGrade == "66")
                        {
                            //****************************************
                            //Query the credits from reg_curriculum TABLE
                            //****************************************
                            string selectCredits = "SELECT A.cis_credits FROM reg_curriculum A, reg_subjenrolled B " +
                                                    "WHERE A.cis_coursenoid = B.cis_coursenoid and B.id = '" + id + "'";
                            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                            {
                                mysqlcon.Open();

                                using (MySqlCommand mySqlCommand = new MySqlCommand(selectCredits, mysqlcon))
                                {
                                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                                    if (mySqlDataReader.HasRows)
                                    {
                                        if (mySqlDataReader.Read())
                                        {
                                            credits = mySqlDataReader["cis_credits"].ToString();
                                        }
                                    }
                                    else
                                        credits = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[4].Value);
                                }
                            }
                            
                            if (finalGrade == "1.0" || finalGrade == "1.25" || finalGrade == "1.5" || finalGrade == "1.75" || finalGrade == "2.0" ||
                                finalGrade == "2.25" || finalGrade == "2.5" || finalGrade == "2.75" || finalGrade == "3.0" || finalGrade == "8")
                                passed = 1;
                            else
                                passed = 0;
                            

                            //****************************************
                            //Update reg_subjenrolled Table
                            //****************************************
                            
                            string dateNow = DateTime.Now.ToString("yyyy-MM-dd");

                            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                            {
                                mysqlcon.Open();
                                using (MySqlCommand mysqlcmd = new MySqlCommand("reg_subjenrolled_verification_finalgrade_update", mysqlcon))
                                {
                                    mysqlcmd.CommandType = CommandType.StoredProcedure;
                                    
                                    mysqlcmd.Parameters.AddWithValue("_id", id);
                                    mysqlcmd.Parameters.AddWithValue("_cis_fgrade", finalGrade);
                                    mysqlcmd.Parameters.AddWithValue("_cis_fgdate", dateNow);
                                    mysqlcmd.Parameters.AddWithValue("_cis_credits", credits);
                                    mysqlcmd.Parameters.AddWithValue("_cis_passed", passed);
                                    mysqlcmd.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                    mysqlcmd.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);

                                    int isSaved = mysqlcmd.ExecuteNonQuery();
                                    if (isSaved > 0)
                                    {
                                        MessageBox.Show("Final Grade updated successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        previousfinalGradeValue = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[5].Value);

                                        //****************************************
                                        //Change Remarks Column
                                        //****************************************

                                        if (finalGrade == "1.0" || finalGrade == "1.25" || finalGrade == "1.5" || finalGrade == "1.75" || finalGrade == "2.0" ||
                                            finalGrade == "2.25" || finalGrade == "2.5" || finalGrade == "2.75" || finalGrade == "3.0" || finalGrade == "8")
                                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[7].Value = "PASSED";
                                        else if (finalGrade == "5.0" || finalGrade == "33")
                                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[7].Value = "FAILED";
                                        else if (finalGrade == "9")
                                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[7].Value = "INCOMPLETE";
                                        else if (finalGrade == "10")
                                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[7].Value = "DROPPED";
                                        else if (finalGrade == "11")
                                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[7].Value = "NO PERMIT";
                                        else if (finalGrade == "18")
                                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[7].Value = "NO GRADE";
                                        else if (finalGrade == "56")
                                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[7].Value = "WITHDRAWN";
                                        else if (finalGrade == "66")
                                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[7].Value = "IN PROGRESS";
                                    }
                                    else
                                        MessageBox.Show("Unable to update Final Grade", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Grade. Please try again.\n\nValid Grade values are the following:\n" +
                                "1.0, 1.25, 1.5, 1.75, 2.0, 2.25, 2.5, 2.75, 3.0, 5.0\n" +
                                "8 - P\n9 - INC\n10 - DRP\n11 - NP\n18 - NG\n33 - F\n56 - W\n66 - In Progress", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvSubjEnrolled.Rows[e.RowIndex].Cells[4].Value = previousfinalGradeValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSubjEnrolled_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= AllowNumbersOnly;

            if (dgvSubjEnrolled.CurrentCell.ColumnIndex == 5)
            {   
                e.Control.KeyPress += AllowNumbersOnly;
            }
        }

        private void AllowNumbersOnly(Object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    }
}