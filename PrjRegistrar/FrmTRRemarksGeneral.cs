using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmTRRemarksGeneral : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string profilepic, selectStatement, AddEditflag, RemarksID, dgvRemarksID;

        public FrmTRRemarksGeneral()
        {
            InitializeComponent();

            ClearFields();
            GridFill();
            ActionButtons();

            AutoCompleteSchoolName();
        }

        private void BtnAddRecord_Click(object sender, EventArgs e)
        {
            ClearFields();

            txtID.Text = "NEW";
            AddEditflag = "NEW";

            RemarksID = "0";

            GridFill();
            ActionButtons();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ClearFields();
            GridFill();
            ActionButtons();
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
                    lblfcuidno.Text = FrmSearchPersonalData.selectedfcuidno;
                    txtcourse.Text = FrmSearchPersonalData.selectedcourse;

                    StudentProfileLoad(lblfcuidno.Text);
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

            selectStatement = "SELECT cis_profilepic, cis_studno, concat(cis_lastname, ', ', ucase(left(cis_firstname, 1)), lcase(substring(cis_firstname, 2)), ' ', ucase(left(cis_midname, 1)), lcase(substring(cis_midname, 2))) as fullname " +
                              "FROM mtbl_studprofile WHERE cis_fcuidno ='" + strfcuidno + "'";
            MySqlCommand command = new MySqlCommand(selectStatement, mysqlcon);
            MySqlDataReader datareader = command.ExecuteReader();

            if (datareader.HasRows)
            {
                if (datareader.Read())
                {
                    // Load fullname and cis_studno
                    lblfullname.Text = datareader["fullname"].ToString();
                    lblstudno.Text = datareader["cis_studno"].ToString();

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

        private void ClearFields()
        {
            cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
            txtSearch.Text = " Search Lastname, Course, School Name or Remarks";
            txtID.Text = txtSchoolName.Text = txtRemarks.Text =  "";
            lblfullname.Text = "Student's Name";
            lblfcuidno.Text = "FCU ID Number";            
            lblstudno.Text = "Student ID No.";
            txtcourse.Text = "";
            txtcourse.Enabled = false;

            ClearDgvRemarks();
            tssllastmodified.Text = "mm/dd/yyyy";
            lblaccountability.Text = "accountability";
        }

        private void ClearDgvRemarks()
        {
            dgvRemarks.DataSource = null;
            dgvRemarks.Rows.Clear();
            dgvRemarks.Columns.Clear();
            dgvRemarks.Refresh();
        }

        private void GridFill()
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();

                MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                selectStatement = "SELECT A.id, concat(B.cis_lastname, ', ', ucase(left(B.cis_firstname, 1)), lcase(substring(B.cis_firstname, 2)), ' ', ucase(left(B.cis_midname, 1)), lcase(substring(B.cis_midname, 2))) as fullname, A.cis_course, A.cis_schoolname, A.cis_remarks " +
                                    "FROM reg_trremarksgeneral A, mtbl_studprofile B WHERE A.cis_fcuidno = B.cis_fcuidno ORDER BY A.id DESC";
                mySqlDA.SelectCommand = new MySqlCommand(selectStatement, mySqlConnection);

                DataTable table = new DataTable();
                mySqlDA.Fill(table);

                BindingSource bindingSource = new BindingSource
                {
                    DataSource = table
                };

                dgvRemarks.DataSource = bindingSource;

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
            dgvRemarks.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvRemarks.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);

            dgvRemarks.Columns[0].HeaderText = "Remarks ID";
            dgvRemarks.Columns[1].HeaderText = "Fullname";
            dgvRemarks.Columns[2].HeaderText = "Course";
            dgvRemarks.Columns[3].HeaderText = "School Name";
            dgvRemarks.Columns[4].HeaderText = "Remarks";
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
            dgvRemarks.Columns.Add(CmdEdit);
            dgvRemarks.Columns[5].Width = 58;


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
            dgvRemarks.Columns.Add(CmdDelete);
            dgvRemarks.Columns[6].Width = 58;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AutoCompleteSchoolName()
        {
            try
            {
                AutoCompleteStringCollection autoCollection = new AutoCompleteStringCollection();

                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("SELECT distinct(cis_schlname) FROM reg_subjenrolled", mySqlConnection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {
                        string schoolName = mySqlDataReader["cis_schlname"].ToString();
                        autoCollection.Add(schoolName);
                    }
                }

                txtSchoolName.AutoCompleteCustomSource = autoCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lblcourse_DoubleClick(object sender, EventArgs e)
        {
            if (txtcourse.Enabled == false)
                txtcourse.Enabled = true;
            else
                txtcourse.Enabled = false;
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" || txtSearch.Text == " Search Lastname, Course, School Name or Remarks") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = " Search Lastname, Course, School Name or Remarks";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != " Search Lastname, Course, School Name or Remarks")
                {
                    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    selectStatement = "SELECT A.id, concat(B.cis_lastname, ', ', ucase(left(B.cis_firstname, 1)), lcase(substring(B.cis_firstname, 2)), ' ', ucase(left(B.cis_midname, 1)), lcase(substring(B.cis_midname, 2))) as fullname, A.cis_course, A.cis_schoolname, A.cis_remarks " +
                                      "FROM reg_trremarksgeneral A, mtbl_studprofile B WHERE A.cis_fcuidno = B.cis_fcuidno AND " +
                                           "(A.cis_course LIKE concat('%', '" + txtSearch.Text + "' ,'%') OR A.cis_schoolname LIKE concat('%', '" + txtSearch.Text + "' ,'%') OR " +
                                           "A.cis_remarks LIKE concat('%', '" + txtSearch.Text + "' ,'%') OR B.cis_lastname LIKE concat('%', '" + txtSearch.Text + "' ,'%')) ORDER BY A.id DESC";
                    mySqlDA.SelectCommand = new MySqlCommand(selectStatement, mySqlConnection);

                    dgvRemarks.DataSource = null;
                    dgvRemarks.Rows.Clear();
                    dgvRemarks.Columns.Clear();

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvRemarks.DataSource = bindingSource;

                    HeaderTexts();
                    ActionButtons();

                    mySqlConnection.Close();
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
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    //***********************************************************************************
                    //Check if already exist, to eliminate double entry
                    //***********************************************************************************
                    selectStatement = "SELECT cis_fcuidno, cis_course, cis_schoolname FROM reg_trremarksgeneral WHERE " +
                                        "cis_fcuidno = '" + lblfcuidno.Text + "' AND " +
                                        "cis_course = '" + txtcourse.Text + "' AND " +
                                        "REPLACE(cis_schoolname, ' ', '') = '" + txtSchoolName.Text.Replace(" ", "") + "' AND " +
                                        "REPLACE(cis_remarks, ' ', '') = '" + txtRemarks.Text.Trim().Replace(" ", "").Replace("'", "''") + "'";
                    using (MySqlCommand mySqlCommand = new MySqlCommand(selectStatement, mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (txtID.Text == "")
                        {
                            MessageBox.Show("Click the Add New button to enter a new General Transcript of Records Remarks.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtID.Focus();
                        }
                        else if (lblfullname.Text == "Student's Name")
                        {
                            MessageBox.Show("Click the Search for Student button to select student's name.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSearchStudent.Focus();
                        }
                        else if (txtSchoolName.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter a School Name for this record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSchoolName.Focus();
                        }
                        else if (txtRemarks.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter Remarks for this record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtRemarks.Focus();
                        }
                        else if (mySqlDataReader.HasRows && AddEditflag == "NEW")     //check for double entry
                        {
                            if (mySqlDataReader.Read())
                            {
                                MessageBox.Show("Can't save the current record. The system detected that this record already exist.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                txtSchoolName.Focus();
                            }
                        }
                        else
                        {
                            using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                            {
                                mySqlConnection1.Open();
                                using (MySqlCommand mySqlCommand1 = new MySqlCommand("reg_trremarksgeneral_addedit", mySqlConnection1))
                                {
                                    mySqlCommand1.CommandType = CommandType.StoredProcedure;

                                    mySqlCommand1.Parameters.AddWithValue("_id", RemarksID);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_fcuidno", lblfcuidno.Text);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_course", txtcourse.Text.Trim().ToUpper());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_schoolname", txtSchoolName.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_remarks", txtRemarks.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);

                                    int isSaved = mySqlCommand1.ExecuteNonQuery();
                                    if (isSaved > 0)
                                        MessageBox.Show("General Transcript of Records Remarks saved successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("Unable to save Genereal TOR Remarks record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            //****************************************************************************
                            // Get the latest course id if NEW record, ready for updating the lastest inserted record.
                            //*****************************************************************************
                            if (AddEditflag == "NEW")
                            {
                                using (MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString))
                                {
                                    mySqlConnection2.Open();
                                    using (MySqlCommand mySqlCommand2 = new MySqlCommand("SELECT max(id) FROM reg_trremarksgeneral WHERE cis_fcuidno = '" + lblfcuidno.Text + "'", mySqlConnection2))
                                    {
                                        MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

                                        if (mySqlDataReader2.HasRows)
                                        {
                                            while (mySqlDataReader2.Read())
                                            {
                                                RemarksID = txtID.Text = mySqlDataReader2.GetValue(0).ToString();
                                            }
                                        }
                                        AddEditflag = "EDIT";
                                    }
                                }
                            }

                            ClearDgvRemarks();
                            GridFill();
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

        private void DgvRemarks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //****************************************************************************
                //edit button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvRemarks.Columns["btnEdit"].Index)
                {
                    AddEditflag = "EDIT";
                    RemarksID = dgvRemarksID = Convert.ToString(dgvRemarks.Rows[e.RowIndex].Cells["id"].Value);

                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM reg_trremarksgeneral where id = '" + dgvRemarksID + "'", mySqlConnection))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    txtID.Text = mySqlDataReader["id"].ToString();
                                    lblfcuidno.Text = mySqlDataReader["cis_fcuidno"].ToString();
                                    txtcourse.Text = mySqlDataReader["cis_course"].ToString();
                                    txtSchoolName.Text = mySqlDataReader["cis_schoolname"].ToString();
                                    txtRemarks.Text = mySqlDataReader["cis_remarks"].ToString();

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
                                                                        
                                    StudentProfileLoad(lblfcuidno.Text);
                                }
                            }
                        }
                    }
                }

                //****************************************************************************
                //delete button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvRemarks.Columns["btnDelete"].Index)
                {
                    dgvRemarksID = Convert.ToString(dgvRemarks.Rows[e.RowIndex].Cells["id"].Value);

                    DialogResult dialogresult = MessageBox.Show("Are you sure you want to Delete this record?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogresult == DialogResult.Yes)
                    {
                        using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                        {
                            mySqlConnection1.Open();
                            using (MySqlCommand mySqlCommand1 = new MySqlCommand("DELETE FROM reg_trremarksgeneral WHERE id = '" + dgvRemarksID + "'", mySqlConnection1))
                            {
                                int isDeleted = mySqlCommand1.ExecuteNonQuery();
                                if (isDeleted > 0)
                                {
                                    //***************************************************
                                    //Repaint dgvRemarks according to Search Criteria
                                    //***************************************************
                                    if (txtSearch.Text != " Search Lastname, Course, School Name or Remarks")
                                    {
                                        TxtSearch_TextChanged(null, null);

                                        cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;

                                        txtID.Text = txtSchoolName.Text = txtRemarks.Text = "";
                                        lblfullname.Text = "Student's Name";
                                        lblfcuidno.Text = "FCU ID Number";
                                        lblstudno.Text = "Student ID No.";
                                        txtcourse.Text = "";
                                        txtcourse.Enabled = false;

                                        tssllastmodified.Text = "mm/dd/yyyy";
                                        lblaccountability.Text = "accountability";
                                    }
                                    else
                                    {
                                        ClearFields();
                                        GridFill();
                                        ActionButtons();
                                    }

                                    MessageBox.Show("Deleted Successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                }
                                else
                                {
                                   MessageBox.Show("Unable to delete General TOR Remarks record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
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
    }
}