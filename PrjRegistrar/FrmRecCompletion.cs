using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmRecCompletion : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        private static string subjenrolledid;
        string profilepic, finalgrade, finalentrydate, completiongrade, completiondate, credits;
        readonly FrmWaitFormFunc frmwaitformfunc = new FrmWaitFormFunc();

        public FrmRecCompletion()
        {
            InitializeComponent();

            ClearDgvSubjEnrolled();
            SubjEnrolledGridFill();
        }

        private void ClearDgvSubjEnrolled()
        {
            dgvSubjEnrolled.DataSource = null;
            dgvSubjEnrolled.Rows.Clear();
            dgvSubjEnrolled.Columns.Clear();
            dgvSubjEnrolled.Refresh();
        }

        private void SubjEnrolledGridFill()
        {
            try
            {
                using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                {
                    mysqlcon.Open();

                    MySqlDataAdapter sqlDA = new MySqlDataAdapter("reg_subjenrolled_incomplete_viewbyid", mysqlcon);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.SelectCommand.Parameters.AddWithValue("_cis_fcuidno", lblfcuidno.Text);

                    DataTable dtbl = new DataTable();
                    sqlDA.Fill(dtbl);
                    dgvSubjEnrolled.DataSource = dtbl;
                    dgvSubjEnrolled.Refresh();

                    dgvSubjEnrolled.DefaultCellStyle.Font = new Font("Tahoma", 9);
                    dgvSubjEnrolled.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);

                    dgvSubjEnrolled.Columns[0].HeaderText = "Rec ID";
                    dgvSubjEnrolled.Columns[1].HeaderText = "School Yr";
                    dgvSubjEnrolled.Columns[2].HeaderText = "Semester";
                    dgvSubjEnrolled.Columns[3].HeaderText = "Course No.";

                    dgvSubjEnrolled.Columns[4].HeaderText = "Description";
                    dgvSubjEnrolled.Columns[4].Width = 385;

                    dgvSubjEnrolled.Columns[5].HeaderText = "Credit";
                    dgvSubjEnrolled.Columns[6].HeaderText = "Final Grade";
                    dgvSubjEnrolled.Columns[7].HeaderText = "Entry Date";
                    dgvSubjEnrolled.Columns[8].HeaderText = "Completion Grade";
                    dgvSubjEnrolled.Columns[9].HeaderText = "Completion Date";
                    dgvSubjEnrolled.Columns[10].HeaderText = "Remarks";
                    dgvSubjEnrolled.Columns[11].HeaderText = "Modified By";
                    dgvSubjEnrolled.Columns[12].HeaderText = "Date Last Modified";

                    DataGridViewButtonColumn CmdEdit = new DataGridViewButtonColumn();
                    {
                        CmdEdit.UseColumnTextForButtonValue = true;
                        CmdEdit.HeaderText = "Action";
                        CmdEdit.Name = "btnUpdate";
                        CmdEdit.Text = "Update";
                        CmdEdit.FlatStyle = FlatStyle.Flat;
                        CmdEdit.CellTemplate.Style.BackColor = Color.SteelBlue;
                        CmdEdit.CellTemplate.Style.ForeColor = Color.White;
                    }
                    dgvSubjEnrolled.Columns.Add(CmdEdit);
                    dgvSubjEnrolled.Columns[13].Width = 60;

                    sqlDA.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFreezeControls()
        {   
            lblcourseno.Text = "Course No";
            lbldesc.Text = "Description";
            lblschlyr.Text = "";
            lblsemester.Text = "";
            lblrecordid.Text = "";
            lblcredits.Text = "";
            lblTeacher.Text = "";

            cbofgrade.SelectedIndex = -1;
            cbofgrade.Text = "";
            cbocgrade.SelectedIndex = -1; 
            cbocgrade.Text = "";

            dtpfgdate.Value = DateTime.Now;
            dtpfgdate.CustomFormat = " ";

            dtpcgrdate.Value = DateTime.Now;
            dtpcgrdate.CustomFormat = " ";

            cbofgrade.Enabled = false;
            cbocgrade.Enabled = false;

            dtpfgdate.Enabled = false;
            dtpcgrdate.Enabled = false;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchHigherEdGradSchool frmSearchHigherEdGradSchool = new FrmSearchHigherEdGradSchool();
                frmSearchHigherEdGradSchool.ShowDialog();

                if (frmSearchHigherEdGradSchool.viewButtonClicked == true)
                {
                    lblfcuidno.Text = FrmSearchHigherEdGradSchool.selectedfcuidno;
                    lblstudno.Text = FrmSearchHigherEdGradSchool.selectedstudno;
                    lblfullname.Text = FrmSearchHigherEdGradSchool.selectedfullname;
                    lblcourse.Text = FrmSearchHigherEdGradSchool.selectedcourse;

                    frmwaitformfunc.Show(this);

                    ClearFreezeControls();
                    ClearDgvSubjEnrolled();
                    SubjEnrolledGridFill();

                    using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                    {
                        mysqlcon.Open();

                        MySqlCommand command = new MySqlCommand("SELECT cis_profilepic FROM mtbl_studprofile WHERE cis_fcuidno ='" + lblfcuidno.Text + "'", mysqlcon);
                        MySqlDataReader datareader = command.ExecuteReader();

                        if (datareader.HasRows)
                        {
                            if (datareader.Read())
                            {
                                // Load Web image in Picture Box
                                ////string webServUrl = Environment.GetEnvironmentVariable("envWebServPath");
                                ////profilepic = datareader["cis_profilepic"] as string ?? null;
                                ////if (profilepic != null)
                                ////{
                                ////    WebRequest request = WebRequest.Create(webServUrl + profilepic);
                                ////    using (var response = request.GetResponse())
                                ////    {
                                ////        using (var str = response.GetResponseStream())
                                ////        {
                                ////            cpbProfilePic.Image = Bitmap.FromStream(str);
                                ////        }
                                ////    }
                                ////}
                                ////else
                                ////{
                                ////    cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                                ////}
                            }
                            else
                            {
                                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                            }
                        }
                        else
                        {
                            cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                        }

                        frmwaitformfunc.Close();
                    }
                }

                frmSearchHigherEdGradSchool.Dispose();
            }
            catch (Exception ex)
            {
                frmwaitformfunc.Close();
                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            if (lblfcuidno.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to refresh the Completion Form? All data will be cleared.", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                    lblfcuidno.Text = "FCU ID Number";
                    lblstudno.Text = "Student ID No.";
                    lblfullname.Text = "Student's Name";
                    lblcourse.Text = "Course";
                    lblyrlevel.Text = "Year Level";
                    lblschlyr.Text = "School Year";
                    lblsemester.Text = "Semester";

                    ClearFreezeControls();
                    ClearDgvSubjEnrolled();
                    SubjEnrolledGridFill();

                    tssllastmodified.Text = "mm/dd/yyyy";
                    lblaccountability.Text = "accountability";
                }
            }
        }

        private void Lblfcuidno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblfcuidno.Text != "FCU ID Number")
                {
                    using (MySqlConnection mysqlconnection = new MySqlConnection(connectionString))
                    {
                        mysqlconnection.Open();
                        
                        using (MySqlCommand command = new MySqlCommand("SELECT * FROM mtbl_studprofile WHERE cis_fcuidno = '" + lblfcuidno.Text + "'", mysqlconnection))
                        {
                            MySqlDataReader datareader = command.ExecuteReader();

                            if (datareader.HasRows)
                            {
                                if (datareader.Read())
                                {
                                    lblcourse.Text = datareader["cis_course"].ToString();
                                    lblyrlevel.Text = datareader["cis_yrlevel"].ToString();
                                }
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

        private void Cbofgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpfgdate.Value = DateTime.Today;
        }

        private void Dtpfgdate_ValueChanged(object sender, EventArgs e)
        {
            dtpfgdate.CustomFormat = "MMM dd, yyyy";
        }

        private void Dtpfgdate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtpfgdate.CustomFormat = " ";
            }
        }

        private void Cbocgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpcgrdate.Value = DateTime.Today;
        }

        private void Dtpcgrdate_ValueChanged(object sender, EventArgs e)
        {
            dtpcgrdate.CustomFormat = "MMM dd, yyyy";
        }

        private void Dtpcgrdate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtpcgrdate.CustomFormat = " ";
            }
        }

        private void DgvSubjEnrolled_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //edit button clicked   
                if (e.ColumnIndex == dgvSubjEnrolled.Columns["btnUpdate"].Index)
                { 
                    subjenrolledid = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells["id"].Value);

                    using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                    {
                        mysqlcon.Open();
                        using (MySqlCommand Command = new MySqlCommand("SELECT id, cis_schlyr, cis_semester, cis_courseno, cis_desc, cis_credits, cis_fgrade, cis_fgdate, cis_cgrade, cis_cgrdate, cis_classid from reg_subjenrolled where id = '" + subjenrolledid + "'", mysqlcon))
                        {
                            MySqlDataReader DataReader = Command.ExecuteReader();

                            if (DataReader.HasRows)
                            {
                                if (DataReader.Read())
                                {
                                    lblschlyr.Text = DataReader["cis_schlyr"].ToString();
                                    lblsemester.Text = DataReader["cis_semester"].ToString();
                                    lblrecordid.Text = DataReader["id"].ToString();
                                    lblcourseno.Text = DataReader["cis_courseno"].ToString();
                                    lbldesc.Text = DataReader["cis_desc"].ToString();
                                    lblcredits.Text = DataReader["cis_credits"].ToString();

                                    //final grade
                                    finalgrade = DataReader["cis_fgrade"].ToString();
                                    if (finalgrade != "")
                                        cbofgrade.Text = finalgrade;
                                    else
                                        cbofgrade.SelectedIndex = -1;

                                    //final grade entry date
                                    finalentrydate = DataReader["cis_fgdate"].ToString();
                                    if (finalentrydate != "")
                                        dtpfgdate.Value = Convert.ToDateTime(finalentrydate);
                                    else
                                    {
                                        dtpfgdate.Value = DateTime.Now;
                                        dtpfgdate.CustomFormat = " ";
                                    }

                                    //completion grade
                                    completiongrade = DataReader["cis_cgrade"].ToString();
                                    if (completiongrade != "")
                                        cbocgrade.Text = completiongrade;
                                    else
                                    {
                                        cbocgrade.SelectedIndex = -1;
                                        cbocgrade.Text = "";
                                    }

                                    //completion grade entry date
                                    completiondate = DataReader["cis_cgrdate"].ToString();
                                    if (completiondate != "")
                                        dtpcgrdate.Value = Convert.ToDateTime(completiondate);
                                    else
                                    {
                                        dtpcgrdate.Value = DateTime.Now;
                                        dtpcgrdate.CustomFormat = " ";
                                    }

                                    //Enable.Disable controls
                                    if (cbofgrade.Text == "9" || cbofgrade.Text == "9.0" || cbofgrade.Text == "INC")
                                    {
                                        //final grade controls is disabled
                                        cbofgrade.Enabled = false;
                                        dtpfgdate.Enabled = false;

                                        //completion grade controls is enabled
                                        cbocgrade.Enabled = true;
                                        dtpcgrdate.Enabled = true;
                                    }
                                    else
                                    {
                                        cbofgrade.Enabled = true;
                                        dtpfgdate.Enabled = true;

                                        cbocgrade.Enabled = false;
                                        dtpcgrdate.Enabled = false;
                                    }

                                    string classid = DataReader["cis_classid"].ToString();
                                    using (MySqlConnection mysqlcon1 = new MySqlConnection(connectionString))
                                    {
                                        mysqlcon1.Open();
                                        using (MySqlCommand Command1 = new MySqlCommand("SELECT concat(C.cis_lastname, ', ', C.cis_firstname, ' ', ifnull(C.cis_midname,'')) as fullname " +
                                                                                        "FROM reg_subjschedule A, mtbl_employee C WHERE A.cis_classid = '" + classid + "' AND A.cis_empid = C.cis_empid", mysqlcon1))
                                        {
                                            MySqlDataReader DataReader1 = Command1.ExecuteReader();
                                            if (DataReader1.HasRows)
                                            {
                                                if (DataReader1.Read())
                                                {
                                                    lblTeacher.Text = DataReader1["fullname"].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblrecordid.Text == "")
                {
                    MessageBox.Show("Subject record must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    lblrecordid.Focus();
                }
                else if (cbofgrade.Enabled == true && cbofgrade.Text.Trim() == "")
                {
                    MessageBox.Show("Final Grade must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbofgrade.Focus();
                }
                else if (dtpfgdate.Enabled == true && dtpfgdate.CustomFormat == " ")
                {
                    MessageBox.Show("Final Grade Entry Date must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    dtpfgdate.Focus();
                }
                else if (cbocgrade.Enabled == true && cbocgrade.SelectedIndex == -1)
                {
                    MessageBox.Show("Completion Grade must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbocgrade.Focus();
                }
                else if (cbocgrade.SelectedIndex >= 0 && dtpcgrdate.CustomFormat == " ")
                {
                    MessageBox.Show("Completion Date must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    dtpcgrdate.Focus();
                }
                else if (cbocgrade.SelectedIndex == -1 && dtpcgrdate.CustomFormat != " ")
                {
                    MessageBox.Show("Completion Grade must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbocgrade.Focus();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to update the student's grade?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {

                        //Select cis_credits from reg_curriculum                        
                        using (MySqlConnection mysqlconnection = new MySqlConnection(connectionString))
                        {
                            mysqlconnection.Open();

                            using (MySqlCommand command = new MySqlCommand("SELECT A.cis_credits FROM reg_curriculum A, reg_subjenrolled B WHERE A.cis_coursenoid = B.cis_coursenoid and B.id  = '" + subjenrolledid + "'", mysqlconnection))
                            {
                                MySqlDataReader datareader = command.ExecuteReader();

                                if (datareader.HasRows)
                                {
                                    if (datareader.Read())
                                        credits = datareader["cis_credits"].ToString();
                                }
                                else
                                {
                                    credits = lblcredits.Text;
                                }
                            }
                        }

                        //Removes the space and succeeding texts(final grade).
                        string finalGrade = cbofgrade.Text.Trim().ToUpper();
                        if (cbofgrade.SelectedIndex >= 10)
                        {
                            int indexofspace = finalGrade.IndexOf(" ");
                            finalGrade = finalGrade.Remove(indexofspace);
                        }

                        //Removes the space and succeeding texts(completion grade).
                        string completionGrade = cbocgrade.Text.Trim().ToUpper();
                        if (cbocgrade.SelectedIndex >= 9)
                        {
                            int indexofspace = completionGrade.IndexOf(" ");
                            completionGrade = completionGrade.Remove(indexofspace);
                        }

                        using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                        {
                            mysqlcon.Open();
                            using (MySqlCommand mysqlcmd = new MySqlCommand("reg_subjenrolled_update", mysqlcon))
                            {
                                mysqlcmd.CommandType = CommandType.StoredProcedure;

                                mysqlcmd.Parameters.AddWithValue("_id", lblrecordid.Text.Trim().ToUpper());
                                mysqlcmd.Parameters.AddWithValue("_cis_credits", credits);
                                mysqlcmd.Parameters.AddWithValue("_cis_passed", "1");
                                mysqlcmd.Parameters.AddWithValue("_cis_fgrade", finalGrade);
                                mysqlcmd.Parameters.AddWithValue("_cis_fgdate", dtpfgdate.Value.Date.ToString("yyyy-MM-dd"));
                                mysqlcmd.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                mysqlcmd.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);

                                //Completion Grade value
                                if (cbocgrade.SelectedIndex == -1)
                                    mysqlcmd.Parameters.AddWithValue("_cis_cgrade", null);
                                else
                                    mysqlcmd.Parameters.AddWithValue("_cis_cgrade", completionGrade);

                                //Completion Grade Date value
                                if (dtpcgrdate.CustomFormat == " ")
                                    mysqlcmd.Parameters.AddWithValue("_cis_cgrdate", null);
                                else
                                    mysqlcmd.Parameters.AddWithValue("_cis_cgrdate", dtpcgrdate.Value.Date.ToString("yyyy-MM-dd"));

                                mysqlcmd.ExecuteNonQuery();

                                MessageBox.Show("Student's Grade updated successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                ClearFreezeControls();
                                ClearDgvSubjEnrolled();
                                SubjEnrolledGridFill();
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
    }
}