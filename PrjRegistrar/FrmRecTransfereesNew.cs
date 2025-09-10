using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Net;

namespace PrjRegistrar
{
    public partial class FrmRecTransfereesNew : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string fcuidno, sem, selectStatement, profilepic;
        int indexofspace;

        public FrmRecTransfereesNew()
        {
            InitializeComponent();

            ClearFields();
            AutoCompleteSchool();
        }

        private void ClearFields()
        {
            lblstudno.Text = "Student ID No.";
            lblfullname.Text = "Student's Name";
            lblcourse.Text = "Course";

            txtSchoolName.Text = "";
            mskSY.Text = "";
            cboSem.SelectedIndex = cboYrlevel.SelectedIndex = -1;

            ClearDgvSubjSchools();
        }
        private void ClearDgvSubjSchools()
        {
            dgvSubjSchools.DataSource = null;
            dgvSubjSchools.Rows.Clear();
            dgvSubjSchools.Columns.Clear();
            dgvSubjSchools.Refresh();
        }

        private void AutoCompleteSchool()
        {
            try
            {
                AutoCompleteStringCollection autoCollection = new AutoCompleteStringCollection();

                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_schlname FROM reg_subjenrolled ORDER BY cis_schlname ASC", mySqlConnection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {
                        string school = mySqlDataReader["cis_schlname"].ToString();
                        autoCollection.Add(school);
                    }
                }

                txtSchoolName.AutoCompleteCustomSource = autoCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearchStudent_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPersonalData frmSearchPersonalData = new FrmSearchPersonalData();
                frmSearchPersonalData.ShowDialog();

                if (frmSearchPersonalData.viewButtonClicked == true)
                {
                    lblfullname.Text = FrmSearchPersonalData.selectedfullname;
                    lblstudno.Text = FrmSearchPersonalData.selectedstudno;
                    lblcourse.Text = FrmSearchPersonalData.selectedcourse;
                    fcuidno = FrmSearchPersonalData.selectedfcuidno;

                    //Select the latest inputed school for the student selected.
                    using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                    {
                        mysqlcon.Open();
                        string selectStatement = "SELECT cis_schlname FROM reg_subjenrolled WHERE cis_fcuidno = '" + fcuidno + "' ORDER BY id DESC LIMIT 1";
                        using (MySqlCommand command = new MySqlCommand(selectStatement, mysqlcon))
                        {
                            MySqlDataReader datareader = command.ExecuteReader();

                            if (datareader.HasRows)
                            {
                                if (datareader.Read())
                                {
                                    txtSchoolName.Text = datareader["cis_schlname"].ToString();

                                    mskSY.Text = "";
                                }
                            }
                            else
                                txtSchoolName.Text = "";
                        }
                    }

                    StudentProfileLoad(fcuidno);
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
            MySqlConnection mysqlcon = new MySqlConnection(connectionString);
            mysqlcon.Open();

            selectStatement = "SELECT cis_profilepic FROM mtbl_studprofile WHERE cis_fcuidno = '" + strfcuidno + "'";
            MySqlCommand command = new MySqlCommand(selectStatement, mysqlcon);
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

        private void MskSY_TextChanged(object sender, EventArgs e)
        {
            cboSem.SelectedIndex = -1;
            cboYrlevel.SelectedIndex = -1;
            ClearDgvSubjSchools();
        }

        private void CboSem_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboYrlevel.SelectedIndex = -1;
            ClearDgvSubjSchools();
        }

        private void CboYrlevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if SY, Sem, YearLvl, and Sschool are complete, Populate Grid
            if (mskSY.Text != "" && cboSem.Text != "" && cboYrlevel.Text != "" && txtSchoolName.Text != "")
            {
                //if (txtSchoolName.Text.ToUpper().Trim() == "FILAMER CHRISTIAN UNIVERSITY")
                //    MessageBox.Show("School Name should be different from FCU.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //else
                GridFill();
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

        private void GridFill()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();


                    GetSem();
                    string selectRegSubjEnrolled;
                    selectRegSubjEnrolled = "SELECT id, cis_courseno, cis_desc, cis_credits, cis_fgrade, cis_fgdate, cis_cgrade, cis_cgrdate " +
                                                 "FROM reg_subjenrolled WHERE cis_schlyr = '" + mskSY.Text + "' AND " +
                                                 "cis_semester = '" + sem + "' AND " +
                                                 "cis_yrlevel = '" + cboYrlevel.Text + "' AND " +
                                                 "cis_studno = '" + lblstudno.Text + "' AND " +
                                                 "cis_course = '" + lblcourse.Text + "' AND " +
                                                 "cis_schlname = '" + txtSchoolName.Text.Replace("'", "''") + "'";
                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter(selectRegSubjEnrolled, mySqlConnection);                    

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);
                    dgvSubjSchools.DataSource = table;

                    //Hide record ID
                    dgvSubjSchools.Columns[0].Visible = false;
                    dgvSubjSchools.Columns[0].ReadOnly = true;

                    //Column Header Texts
                    dgvSubjSchools.Columns[0].HeaderText = "ID";

                    dgvSubjSchools.Columns[1].HeaderText = "Course No. (Subject)";
                    dgvSubjSchools.Columns[1].Width = 250;

                    dgvSubjSchools.Columns[2].HeaderText = "Description";
                    dgvSubjSchools.Columns[2].Width = 500;

                    dgvSubjSchools.Columns[3].HeaderText = "Credits";
                    dgvSubjSchools.Columns[4].HeaderText = "Final Grade";

                    dgvSubjSchools.Columns[5].HeaderText = "Entry Date \nmm/dd/yyyy";
                    dgvSubjSchools.Columns[5].Width = 150;

                    dgvSubjSchools.Columns[6].HeaderText = "Completion Grade";
                                        
                    dgvSubjSchools.Columns[7].HeaderText = "Completion Date \nmm/dd/yyyy";
                    dgvSubjSchools.Columns[7].Width = 150;

                    ((DataGridViewTextBoxColumn)dgvSubjSchools.Columns[3]).MaxInputLength = 5;
                    ((DataGridViewTextBoxColumn)dgvSubjSchools.Columns[4]).MaxInputLength = 15;
                    ((DataGridViewTextBoxColumn)dgvSubjSchools.Columns[5]).MaxInputLength = 10;
                    ((DataGridViewTextBoxColumn)dgvSubjSchools.Columns[6]).MaxInputLength = 15;
                    ((DataGridViewTextBoxColumn)dgvSubjSchools.Columns[7]).MaxInputLength = 10;
                }
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSubjSchools_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                if (dgvSubjSchools.CurrentRow != null)
                {
                    using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                    {
                        mySqlConnection1.Open();
                        DataGridViewRow dataGridViewRow = dgvSubjSchools.CurrentRow;

                        using (MySqlCommand mySqlCommand1 = new MySqlCommand("reg_subjschools_addedit", mySqlConnection1))
                        {
                            mySqlCommand1.CommandType = CommandType.StoredProcedure;

                            
                            if (dataGridViewRow.Cells["id"].Value == DBNull.Value)
                                mySqlCommand1.Parameters.AddWithValue("_id", 0);                                                    //Insert
                            else
                                mySqlCommand1.Parameters.AddWithValue("_id", Convert.ToInt32(dataGridViewRow.Cells["id"].Value));   //Update


                            mySqlCommand1.Parameters.AddWithValue("_cis_fcuidno", fcuidno);
                            mySqlCommand1.Parameters.AddWithValue("_cis_studno", lblstudno.Text);
                            mySqlCommand1.Parameters.AddWithValue("_cis_course", lblcourse.Text);
                            mySqlCommand1.Parameters.AddWithValue("_cis_schlyr", mskSY.Text);
                            mySqlCommand1.Parameters.AddWithValue("_cis_semester", sem);
                            mySqlCommand1.Parameters.AddWithValue("_cis_yrlevel", cboYrlevel.Text);                            

                            mySqlCommand1.Parameters.AddWithValue("_cis_courseno", dataGridViewRow.Cells["cis_courseno"].Value == DBNull.Value ? null : dataGridViewRow.Cells["cis_courseno"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_desc", dataGridViewRow.Cells["cis_desc"].Value == DBNull.Value ? null : dataGridViewRow.Cells["cis_desc"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_credits", dataGridViewRow.Cells["cis_credits"].Value == DBNull.Value ? null : dataGridViewRow.Cells["cis_credits"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_remarks", DBNull.Value);
                            mySqlCommand1.Parameters.AddWithValue("_cis_schlname", txtSchoolName.Text.ToUpper());

                            mySqlCommand1.Parameters.AddWithValue("_cis_fgrade", dataGridViewRow.Cells["cis_fgrade"].Value == DBNull.Value ? null : dataGridViewRow.Cells["cis_fgrade"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_fgdate", dataGridViewRow.Cells["cis_fgdate"].Value == DBNull.Value ? (object)DateTime.Now : DateTime.Parse(dataGridViewRow.Cells["cis_fgdate"].Value.ToString()).ToString("yyyy-MM-dd"));

                            mySqlCommand1.Parameters.AddWithValue("_cis_cgrade", dataGridViewRow.Cells["cis_cgrade"].Value == DBNull.Value ? null : dataGridViewRow.Cells["cis_cgrade"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_cgrdate", dataGridViewRow.Cells["cis_cgrdate"].Value == DBNull.Value ? null : DateTime.Parse(dataGridViewRow.Cells["cis_cgrdate"].Value.ToString()).ToString("yyyy-MM-dd"));

                            mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                            mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);
                            mySqlCommand1.ExecuteNonQuery();

                            GridFill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvSubjSchools_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= AllowNumbersOnly;

            if (dgvSubjSchools.CurrentCell.ColumnIndex == 3)
            {   
                e.Control.KeyPress += AllowNumbersOnly;
            }
        }

        private void AllowNumbersOnly(object sender, KeyPressEventArgs e)
        {
            //only allow numeric, decimal point, negative
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            // only allow one negative
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}