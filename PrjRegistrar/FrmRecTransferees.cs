using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmRecTransferees : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string fcuidno, profilepic, selectStatement, dgvSubjSchoolsID, finalgrade, finalentrydate, completiongrade, completiondate, sem;
        int indexofspace, indexSem;

        public FrmRecTransferees()
        {
            InitializeComponent();

            ClearFields();
            AutoCompleteSchool();
        }

        private void BtnAddRecord_Click(object sender, EventArgs e)
        {
            FrmRecTransfereesNew frmRecTransfereesNew = new FrmRecTransfereesNew();
            frmRecTransfereesNew.ShowDialog();
            frmRecTransfereesNew.Dispose();

            if (fcuidno != "")
            {
                txtSearch.Text = " Search Course No or Description";

                mskSY.Text = "";
                cboSem.SelectedIndex = cboYrlevel.SelectedIndex = -1;

                txtCourseNo.Text = "";
                txtDesc.Text = "";
                txtCredits.Text = "";
                txtSchoolName.Text = "";

                cbofgrade.Text = cbocgrade.Text = "";
                cbofgrade.SelectedIndex = cbocgrade.SelectedIndex = -1;

                dtpfgdate.Value = dtpcgrdate.Value = DateTime.Now;
                dtpfgdate.CustomFormat = dtpcgrdate.CustomFormat = " ";

                ClearDgvSubjSchools();
                tssllastmodified.Text = "mm/dd/yyyy";
                lblaccountability.Text = "accountability";

                GridFill();
                ActionButtons();
            }            
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void BtnSearchStudent_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPersonalData frmSearchPersonalData = new FrmSearchPersonalData();
                frmSearchPersonalData.ShowDialog();

                if (frmSearchPersonalData.viewButtonClicked == true)
                {
                    ClearFields();

                    lblfullname.Text = FrmSearchPersonalData.selectedfullname;
                    lblstudno.Text = FrmSearchPersonalData.selectedstudno;
                    lblcourse.Text = FrmSearchPersonalData.selectedcourse;
                    fcuidno = FrmSearchPersonalData.selectedfcuidno;
                                   
                    StudentProfileLoad(fcuidno);                                       
                    GridFill();
                    ActionButtons();    
                }

                frmSearchPersonalData.Dispose();
            }
            catch (Exception ex)
            {        
                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StudentProfileLoad(string strfcuidno)
        {
            try
            {
                using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                {
                    mysqlcon.Open();

                    selectStatement = "SELECT cis_profilepic FROM mtbl_studprofile WHERE cis_fcuidno = '" + strfcuidno + "'";
                    using (MySqlCommand command = new MySqlCommand(selectStatement, mysqlcon))
                    {
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {        
                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            fcuidno = "";

            cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
            txtSearch.Text = " Search Course No or Description";

            lblstudno.Text = "Student ID No.";
            lblfullname.Text = "Student's Name";
            lblcourse.Text = "Course";

            mskSY.Text = "";
            cboSem.SelectedIndex = cboYrlevel.SelectedIndex = -1;

            txtCourseNo.Text = "";
            txtDesc.Text = "";
            txtCredits.Text = "";
            txtSchoolName.Text = "";

            cbofgrade.Text = cbocgrade.Text = "";
            cbofgrade.SelectedIndex = cbocgrade.SelectedIndex = -1;

            dtpfgdate.Value = dtpcgrdate.Value = DateTime.Now;
            dtpfgdate.CustomFormat = dtpcgrdate.CustomFormat = " ";

            ClearDgvSubjSchools();
            tssllastmodified.Text = "mm/dd/yyyy";
            lblaccountability.Text = "accountability";
        }

        private void ClearDgvSubjSchools()
        {
            dgvSubjSchools.DataSource = null;
            dgvSubjSchools.Rows.Clear();
            dgvSubjSchools.Columns.Clear();
            dgvSubjSchools.Refresh();
        }

        private void GridFill()
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();

                MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                selectStatement = "SELECT id, cis_schlyr, cis_semester, cis_yrlevel, cis_courseno, cis_desc, cis_credits, cis_fgrade, cis_fgdate, cis_cgrade, cis_cgrdate, cis_schlname FROM reg_subjenrolled " +
                                    "WHERE cis_fcuidno = '" + fcuidno + "' AND cis_schlname NOT LIKE 'FILAMER%'";
                mySqlDA.SelectCommand = new MySqlCommand(selectStatement, mySqlConnection);

                DataTable table = new DataTable();
                mySqlDA.Fill(table);

                BindingSource bindingSource = new BindingSource
                {
                    DataSource = table
                };

                dgvSubjSchools.DataSource = bindingSource;

                HeaderTexts();

                mySqlConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HeaderTexts()
        {
            dgvSubjSchools.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvSubjSchools.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);

            //Hide record ID
            dgvSubjSchools.Columns[0].Visible = false;
            dgvSubjSchools.Columns[0].ReadOnly = true;

            //Column Header Texts
            dgvSubjSchools.Columns[0].HeaderText = "ID";
            dgvSubjSchools.Columns[1].HeaderText = "School Year";
            dgvSubjSchools.Columns[2].HeaderText = "Semester";
            dgvSubjSchools.Columns[3].HeaderText = "Year Level";
            dgvSubjSchools.Columns[4].HeaderText = "Course No.";
            dgvSubjSchools.Columns[5].HeaderText = "Description";
            dgvSubjSchools.Columns[6].HeaderText = "Credits";
            dgvSubjSchools.Columns[7].HeaderText = "Final Grade";
            dgvSubjSchools.Columns[8].HeaderText = "Entry Date";
            dgvSubjSchools.Columns[9].HeaderText = "Completion Grade";
            dgvSubjSchools.Columns[10].HeaderText = "Completion Date";
            dgvSubjSchools.Columns[11].HeaderText = "School Name";

            dgvSubjSchools.Columns[1].Width = 100;
            dgvSubjSchools.Columns[2].Width = 70;
            dgvSubjSchools.Columns[3].Width = 70;
            dgvSubjSchools.Columns[4].Width = 100;
            dgvSubjSchools.Columns[5].Width = 350;
            dgvSubjSchools.Columns[6].Width = 70;
            dgvSubjSchools.Columns[7].Width = 100;
            dgvSubjSchools.Columns[8].Width = 100;
            dgvSubjSchools.Columns[9].Width = 100;
            dgvSubjSchools.Columns[10].Width = 100;
            dgvSubjSchools.Columns[11].Width = 350;
        }

        private void ActionButtons()
        {
            DataGridViewButtonColumn CmdEdit = new DataGridViewButtonColumn();
            {
                CmdEdit.UseColumnTextForButtonValue = true;
                CmdEdit.HeaderText = "Edit";
                CmdEdit.Name = "btnEdit";
                CmdEdit.Text = "Edit";
                CmdEdit.FlatStyle = FlatStyle.Flat;
                CmdEdit.CellTemplate.Style.BackColor = Color.SteelBlue;
                CmdEdit.CellTemplate.Style.ForeColor = Color.White;
            }
            dgvSubjSchools.Columns.Add(CmdEdit);
            dgvSubjSchools.Columns[12].Width = 58;


            DataGridViewButtonColumn CmdDelete = new DataGridViewButtonColumn();
            {
                CmdDelete.UseColumnTextForButtonValue = true;
                CmdDelete.HeaderText = "Delete";
                CmdDelete.Name = "btnDelete";
                CmdDelete.Text = "Delete";
                CmdDelete.FlatStyle = FlatStyle.Flat;
                CmdDelete.CellTemplate.Style.BackColor = Color.Red;
                CmdDelete.CellTemplate.Style.ForeColor = Color.White;
            }
            dgvSubjSchools.Columns.Add(CmdDelete);
            dgvSubjSchools.Columns[13].Width = 58;
        }

        private void AutoCompleteSchool()
        {
            try
            {
                AutoCompleteStringCollection autoCollection = new AutoCompleteStringCollection();

                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_schlname FROM reg_subjenrolled ORDER BY cis_schlname ASC", mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        while (mySqlDataReader.Read())
                        {
                            string school = mySqlDataReader["cis_schlname"].ToString();
                            autoCollection.Add(school);
                        }
                    }
                }

                txtSchoolName.AutoCompleteCustomSource = autoCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" || txtSearch.Text == " Search Course No or Description") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = " Search Course No or Description";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //If you have a search criteria in the search box
                if (txtSearch.Text != " Search Course No or Description")
                {
                    if (fcuidno != "")
                    {
                        using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                        {
                            mySqlConnection.Open();

                            MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                            selectStatement = "SELECT id, cis_schlyr, cis_semester, cis_yrlevel, cis_courseno, cis_desc, cis_credits, cis_fgrade, cis_fgdate, cis_cgrade, cis_cgrdate, cis_schlname FROM reg_subjenrolled " +
                                              "WHERE cis_fcuidno = '" + fcuidno + "' AND cis_schlname <> 'FILAMER CHRISTIAN UNIVERSITY' AND " +
                                              "(cis_courseno LIKE concat('%', '" + txtSearch.Text + "' ,'%') OR cis_desc LIKE concat('%', '" + txtSearch.Text + "' ,'%'))";
                            mySqlDA.SelectCommand = new MySqlCommand(selectStatement, mySqlConnection);

                            dgvSubjSchools.DataSource = null;
                            dgvSubjSchools.Rows.Clear();
                            dgvSubjSchools.Columns.Clear();

                            DataTable table = new DataTable();
                            mySqlDA.Fill(table);

                            BindingSource bindingSource = new BindingSource
                            {
                                DataSource = table
                            };

                            dgvSubjSchools.DataSource = bindingSource;

                            HeaderTexts();
                            ActionButtons();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetSem()
        {
            //Removes the space and succeeding texts.
            sem = cboSem.Text.ToUpper().Trim();
            if (sem != "")
            {
                indexofspace = sem.IndexOf(" ");
                sem = sem.Remove(indexofspace);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try  
            {
                if (lblfullname.Text == "Student's Name")
                {
                    MessageBox.Show("Click the Search for Student button to select student's name.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearchStudent.Focus();
                }
                else if (mskSY.Text.Trim() == "-")
                {
                    MessageBox.Show("Enter a School Year for this record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskSY.Focus();
                }
                else if (cboSem.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a Semester for this record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboSem.Focus();
                }
                else if (txtCourseNo.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Course Number for this record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCourseNo.Focus();
                }
                //else if (txtDesc.Text.Trim() == "")
                //{
                //    MessageBox.Show("Enter a Course Description for this record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtDesc.Focus();
                //}
                //else if (txtCredits.Text.Trim() == "")
                //{
                //    MessageBox.Show("Enter Credits for this record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtCredits.Focus();
                //}
                else if (txtSchoolName.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a School Name for this record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSchoolName.Focus();
                }
                else if (cbofgrade.Text.Trim() == "")
                {
                    MessageBox.Show("Final Grade must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbofgrade.Focus();
                }
                else
                {
                    using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                    {
                        mySqlConnection1.Open();

                        GetSem();                   

                        using (MySqlCommand mySqlCommand1 = new MySqlCommand("reg_subjschools_addedit", mySqlConnection1))
                        {
                            mySqlCommand1.CommandType = CommandType.StoredProcedure;

                            mySqlCommand1.Parameters.AddWithValue("_id", dgvSubjSchoolsID);
                            
                            mySqlCommand1.Parameters.AddWithValue("_cis_fcuidno", fcuidno);
                            mySqlCommand1.Parameters.AddWithValue("_cis_studno", lblstudno.Text);
                            mySqlCommand1.Parameters.AddWithValue("_cis_course", lblcourse.Text);
                            mySqlCommand1.Parameters.AddWithValue("_cis_schlyr", mskSY.Text);
                            mySqlCommand1.Parameters.AddWithValue("_cis_semester", sem);
                            mySqlCommand1.Parameters.AddWithValue("_cis_yrlevel", cboYrlevel.Text);

                            mySqlCommand1.Parameters.AddWithValue("_cis_courseno", txtCourseNo.Text.Trim());
                            mySqlCommand1.Parameters.AddWithValue("_cis_desc", txtDesc.Text.Trim());
                            mySqlCommand1.Parameters.AddWithValue("_cis_credits", String.IsNullOrEmpty(txtCredits.Text) ? DBNull.Value : (object)txtCredits.Text);
                            mySqlCommand1.Parameters.AddWithValue("_cis_remarks", DBNull.Value);
                            mySqlCommand1.Parameters.AddWithValue("_cis_schlname", txtSchoolName.Text.ToUpper());

                            mySqlCommand1.Parameters.AddWithValue("_cis_fgrade", cbofgrade.Text.Trim().ToUpper().Trim());

                            //Final Grade Date value
                            if (dtpfgdate.CustomFormat == " ")
                                mySqlCommand1.Parameters.AddWithValue("_cis_fgdate", DateTime.Now);
                            else
                                mySqlCommand1.Parameters.AddWithValue("_cis_fgdate", dtpfgdate.Value.Date.ToString("yyyy-MM-dd"));

                            //Completion Grade value
                            if (cbocgrade.Text.Trim() == "")
                                mySqlCommand1.Parameters.AddWithValue("_cis_cgrade", null);
                            else
                                mySqlCommand1.Parameters.AddWithValue("_cis_cgrade", cbocgrade.Text.ToUpper().Trim());

                            //Completion Grade Date value
                            if (dtpcgrdate.CustomFormat == " ")
                                mySqlCommand1.Parameters.AddWithValue("_cis_cgrdate", null);
                            else
                                mySqlCommand1.Parameters.AddWithValue("_cis_cgrdate", dtpcgrdate.Value.Date.ToString("yyyy-MM-dd"));

                            mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                            mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);


                            int isSaved = mySqlCommand1.ExecuteNonQuery();
                            if (isSaved > 0)
                                MessageBox.Show("GRADES - Transferees from other schools record saved successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("Unable to save record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    
                    ClearDgvSubjSchools();
                    GridFill();
                    ActionButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSubjEnrolled_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //****************************************************************************
                //edit button clicked
                //****************************************************************************  
                if (e.ColumnIndex == dgvSubjSchools.Columns["btnEdit"].Index)
                {
                    dgvSubjSchoolsID = Convert.ToString(dgvSubjSchools.Rows[e.RowIndex].Cells["id"].Value);

                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();

                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM reg_subjenrolled where id ='" + dgvSubjSchoolsID + "'", mySqlConnection))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    fcuidno = mySqlDataReader["cis_fcuidno"].ToString();
                                    lblstudno.Text = mySqlDataReader["cis_studno"].ToString();
                                    lblcourse.Text = mySqlDataReader["cis_course"].ToString();
                                    mskSY.Text = mySqlDataReader["cis_schlyr"].ToString();

                                    sem = mySqlDataReader["cis_semester"].ToString();
                                    if (sem == "" || sem == "0")
                                        cboSem.SelectedIndex = -1;
                                    else
                                    {
                                        indexSem = cboSem.FindString(sem);
                                        cboSem.SelectedIndex = indexSem;
                                    }

                                    cboYrlevel.Text = mySqlDataReader["cis_yrlevel"].ToString();

                                    txtCourseNo.Text = mySqlDataReader["cis_courseno"].ToString();
                                    txtDesc.Text = mySqlDataReader["cis_desc"].ToString();
                                    txtCredits.Text = mySqlDataReader["cis_credits"].ToString();
                                    txtSchoolName.Text = mySqlDataReader["cis_schlname"].ToString();

                                    //final grade
                                    finalgrade = mySqlDataReader["cis_fgrade"].ToString();
                                    if (finalgrade != "")
                                        cbofgrade.Text = finalgrade;
                                    else
                                        cbofgrade.SelectedIndex = -1;

                                    //final grade entry date
                                    finalentrydate = mySqlDataReader["cis_fgdate"].ToString();
                                    if (finalentrydate != "")
                                        dtpfgdate.Value = Convert.ToDateTime(finalentrydate);
                                    else
                                    {
                                        dtpfgdate.Value = DateTime.Now;
                                        dtpfgdate.CustomFormat = " ";
                                    }

                                    //completion grade
                                    completiongrade = mySqlDataReader["cis_cgrade"].ToString();
                                    if (completiongrade != "")
                                        cbocgrade.Text = completiongrade;
                                    else
                                        cbocgrade.SelectedIndex = -1;

                                    //completion grade entry date
                                    completiondate = mySqlDataReader["cis_cgrdate"].ToString();
                                    if (completiondate != "")
                                        dtpcgrdate.Value = Convert.ToDateTime(completiondate);
                                    else
                                    {
                                        dtpcgrdate.Value = DateTime.Now;
                                        dtpcgrdate.CustomFormat = " ";
                                    }

                                    //Check if lastmodified has value.
                                    string lastmodified = mySqlDataReader["cis_lastmodified"].ToString();
                                    if (lastmodified != "")
                                        tssllastmodified.Text = lastmodified;
                                    else
                                        tssllastmodified.Text = "mm/dd/yyyy";

                                    //Last Modified By: (Accountability)
                                    string accountability = mySqlDataReader["cis_accountability"].ToString();
                                    if (accountability != "")
                                        lblaccountability.Text = accountability;
                                    else
                                        lblaccountability.Text = "accountability";
                                    
                                    StudentProfileLoad(fcuidno);
                                }
                            }
                        }
                    }
                }

                //****************************************************************************
                //delete button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvSubjSchools.Columns["btnDelete"].Index)
                {
                    dgvSubjSchoolsID = Convert.ToString(dgvSubjSchools.Rows[e.RowIndex].Cells["id"].Value);

                    DialogResult dialogresult = MessageBox.Show("Are you sure you want to Delete this record?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogresult == DialogResult.Yes)
                    {
                        using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                        {
                            mySqlConnection1.Open();

                            using (MySqlCommand mySqlCommand1 = new MySqlCommand("DELETE FROM reg_subjenrolled WHERE id = '" + dgvSubjSchoolsID + "'", mySqlConnection1))
                            {
                                mySqlCommand1.ExecuteNonQuery();

                                //***************************************************
                                //Repaint dgvSubjSchools according to Search Criteria
                                //***************************************************
                                if (txtSearch.Text != " Search Course No or Description")
                                {
                                    TxtSearch_TextChanged(null, null);
                                }
                                else
                                {
                                    ClearDgvSubjSchools();
                                    GridFill();
                                    ActionButtons();
                                }

                                mskSY.Text = "";
                                cboSem.SelectedIndex = cboYrlevel.SelectedIndex = -1;

                                txtCourseNo.Text = "";
                                txtDesc.Text = "";
                                txtCredits.Text = "";
                                txtSchoolName.Text = "";

                                cbofgrade.Text = cbocgrade.Text = "";
                                cbofgrade.SelectedIndex = cbocgrade.SelectedIndex = -1;

                                dtpfgdate.Value = dtpcgrdate.Value = DateTime.Now;
                                dtpfgdate.CustomFormat = dtpcgrdate.CustomFormat = " ";

                                tssllastmodified.Text = "mm/dd/yyyy";
                                lblaccountability.Text = "accountability";

                                MessageBox.Show("Deleted Successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        
        private void TxtCredits_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            
            if ((ch == 45 && txtCredits.Text.IndexOf('-') != -1) || (ch == 46 && txtCredits.Text.IndexOf('.') != -1))
            {
                e.Handled = true;
                return;
            }

            //8 = Backspace, 45 = Negative, 46 = Dot
            if (!char.IsDigit(ch) && ch != 8 && ch != 45 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}