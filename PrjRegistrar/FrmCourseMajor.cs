using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmCourseMajor : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string AddEditflag, MajorID, dgvMajorID;

        public FrmCourseMajor()
        {
            InitializeComponent();

            ClearFields();
            FillUpComboBox();
            GridFill();
            ActionButtons();
            AutoCompleteDescription();
        }

        private void BtnAddMajor_Click(object sender, EventArgs e)
        {
            ClearFields();

            txtMajorID.Text = "NEW";
            AddEditflag = "NEW";

            MajorID = "0";

            GridFill();
            ActionButtons();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ClearFields();
            GridFill();
            ActionButtons();
        }

        private void ClearFields()
        {
            txtSearch.Text = " Search Course or Description";
            txtMajorID.Text = txtMajorDesc.Text = lblCourseDesc.Text = "";
            cboCourse.SelectedIndex = -1;

            ClearDgvMajor();
            tssllastmodified.Text = "mm/dd/yyyy";
            lblaccountability.Text = "accountability";
        }

        private void ClearDgvMajor()
        {
            dgvMajor.DataSource = null;
            dgvMajor.Rows.Clear();
            dgvMajor.Columns.Clear();
            dgvMajor.Refresh();
        }

        private void FillUpComboBox()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                { 
                    mySqlConnection.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_course FROM mtbl_course ORDER BY cis_category, cis_course", mySqlConnection))
                    { 
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                        while (mySqlDataReader.Read())
                        {
                            cboCourse.Items.Add(mySqlDataReader[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridFill()
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();

                MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                string SelectMtblCourseMajor = "SELECT id, cis_course, cis_majordesc FROM mtbl_coursemajor order by cis_course";
                mySqlDA.SelectCommand = new MySqlCommand(SelectMtblCourseMajor, mySqlConnection);

                DataTable table = new DataTable();
                mySqlDA.Fill(table);

                BindingSource bindingSource = new BindingSource
                {
                    DataSource = table
                };

                dgvMajor.DataSource = bindingSource;

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
            dgvMajor.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvMajor.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);

            dgvMajor.Columns[0].HeaderText = "Major ID";
            dgvMajor.Columns[1].HeaderText = "Course";
            dgvMajor.Columns[2].HeaderText = "Major Description";
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
            dgvMajor.Columns.Add(CmdEdit);
            dgvMajor.Columns[3].Width = 58;


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
            dgvMajor.Columns.Add(CmdDelete);
            dgvMajor.Columns[4].Width = 58;
        }

        private void AutoCompleteDescription()
        {
            try
            {
                AutoCompleteStringCollection autoCollection = new AutoCompleteStringCollection();

                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_majordesc FROM mtbl_coursemajor", mySqlConnection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {
                        string description = mySqlDataReader["cis_majordesc"].ToString();
                        autoCollection.Add(description);
                    }
                }

                txtMajorDesc.AutoCompleteCustomSource = autoCollection;
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
            if (txtSearch.Text == "" || txtSearch.Text == " Search Course or Description") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = " Search Course or Description";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != " Search Course or Description")
                {
                    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    string SelectMtblCourse = "SELECT id, cis_course, cis_majordesc FROM mtbl_coursemajor WHERE " +
                                                 "cis_course LIKE concat('%', '" + txtSearch.Text + "' ,'%') or " +
                                                 "cis_majordesc LIKE concat('%', '" + txtSearch.Text + "' ,'%') order by cis_course";
                    mySqlDA.SelectCommand = new MySqlCommand(SelectMtblCourse, mySqlConnection);

                    dgvMajor.DataSource = null;
                    dgvMajor.Rows.Clear();
                    dgvMajor.Columns.Clear();

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvMajor.DataSource = bindingSource;

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
                    //Check course and major if already exist, to eliminate double entry
                    //***********************************************************************************
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_course, cis_majordesc FROM mtbl_coursemajor WHERE cis_course = '" + cboCourse.Text.ToUpper() + "' and cis_majordesc = '" + txtMajorDesc.Text.ToUpper().Trim() + "'", mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (txtMajorID.Text == "")
                        {
                            MessageBox.Show("Click the Add New button to enter a new Major.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMajorID.Focus();
                        }
                        else if (cboCourse.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select a Course.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cboCourse.Focus();
                        }
                        else if (txtMajorDesc.Text == "")
                        {
                            MessageBox.Show("Enter Major Description.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMajorDesc.Focus();
                        }
                        else if (mySqlDataReader.HasRows && AddEditflag == "NEW")     /*check for double entry*/
                        {
                            if (mySqlDataReader.Read())
                            {
                                MessageBox.Show("Can't save the current record. \nThe system detected that this major already exist.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                txtMajorDesc.Focus();
                            }
                        }
                        else
                        {
                            using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                            {
                                mySqlConnection1.Open();
                                using (MySqlCommand mySqlCommand1 = new MySqlCommand("mtbl_coursemajor_addedit", mySqlConnection1))
                                {
                                    mySqlCommand1.CommandType = CommandType.StoredProcedure;

                                    mySqlCommand1.Parameters.AddWithValue("_id", MajorID);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_course", cboCourse.Text.ToUpper().Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_majordesc", txtMajorDesc.Text.ToUpper().Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_majorid", MajorID);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);

                                    int isSaved = mySqlCommand1.ExecuteNonQuery();
                                    if (isSaved > 0)
                                        MessageBox.Show("Major (Specialization) record saved successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("Unable to save Major (Specialization) record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }


                            //****************************************************************************************
                            // Get the latest course id if NEW record, ready for updating the lastest inserted record.
                            //****************************************************************************************
                            if (AddEditflag == "NEW")
                            {
                                using (MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString))
                                {
                                    mySqlConnection2.Open();

                                    using (MySqlCommand mySqlCommand2 = new MySqlCommand("SELECT max(id) FROM mtbl_coursemajor WHERE cis_course = '" + cboCourse.Text.ToUpper() + "' and cis_majordesc = '" + txtMajorDesc.Text.ToUpper().Trim() + "'", mySqlConnection2))
                                    {
                                        MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

                                        if (mySqlDataReader2.HasRows)
                                        {
                                            while (mySqlDataReader2.Read())
                                            {
                                                txtMajorID.Text = mySqlDataReader2.GetValue(0).ToString();
                                                MajorID = txtMajorID.Text;
                                            }

                                            //*********************************************
                                            //update mtbl_coursemajor set cis_majorid = id
                                            //*********************************************
                                            using (MySqlConnection mySqlConnection3 = new MySqlConnection(connectionString))
                                            {
                                                mySqlConnection3.Open();
                                                string UpdateMtblCourseMajor = "UPDATE mtbl_coursemajor SET cis_majorid = '" + MajorID + "' " +
                                                                                "WHERE cis_course = '" + cboCourse.Text.ToUpper() + "' and cis_majordesc = '" + txtMajorDesc.Text.ToUpper().Trim() + "'";
                                                using (MySqlCommand mySqlCommand3 = new MySqlCommand(UpdateMtblCourseMajor, mySqlConnection3))
                                                {   
                                                    mySqlCommand3.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                        AddEditflag = "EDIT";
                                    }
                                }
                            }

                            ClearDgvMajor();
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

        private void CboCourse_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_coursedesc FROM mtbl_course WHERE cis_course = '" + cboCourse.Text + "'", mySqlCon))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (mySqlDataReader.HasRows)
                        {
                            if (mySqlDataReader.Read())
                            {
                                lblCourseDesc.Text = mySqlDataReader["cis_coursedesc"].ToString();
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

        private void DgvMajor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //****************************************************************************
                //edit button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvMajor.Columns["btnEdit"].Index)
                {
                    AddEditflag = "EDIT";
                    MajorID = dgvMajorID = Convert.ToString(dgvMajor.Rows[e.RowIndex].Cells["id"].Value);

                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM mtbl_coursemajor where id = '" + dgvMajorID + "'", mySqlConnection))
                        { 
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    txtMajorID.Text = mySqlDataReader["id"].ToString();
                                    cboCourse.Text = mySqlDataReader["cis_course"].ToString();
                                    txtMajorDesc.Text = mySqlDataReader["cis_majordesc"].ToString();

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
                                }
                            }
                        }
                    }
                }

                //****************************************************************************
                //delete button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvMajor.Columns["btnDelete"].Index)
                {
                    dgvMajorID = Convert.ToString(dgvMajor.Rows[e.RowIndex].Cells["id"].Value);

                    DialogResult dialogresult = MessageBox.Show("Are you sure you want to Delete this record?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogresult == DialogResult.Yes)
                    {
                        //**********************************************************************************************
                        //Check cis_majorid from reg_subjenrolled table. If it exists, Major record cant be deleted.
                        //**********************************************************************************************
                        using (MySqlConnection mySqlConnection0 = new MySqlConnection(connectionString))
                        {
                            mySqlConnection0.Open();
                            using (MySqlCommand mySqlCommand0 = new MySqlCommand("SELECT cis_majorid FROM reg_subjenrolled WHERE cis_majorid = '" + dgvMajorID + "'", mySqlConnection0))
                            {
                                MySqlDataReader mySqlDataReader0 = mySqlCommand0.ExecuteReader();

                                if (mySqlDataReader0.HasRows)
                                {
                                    MessageBox.Show("Unable to Delete Major (Specialization). \nMajor record is already active.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                                    {
                                        mySqlConnection1.Open();
                                        using (MySqlCommand mySqlCommand1 = new MySqlCommand("DELETE FROM mtbl_coursemajor WHERE id = '" + dgvMajorID + "'", mySqlConnection1))
                                        {
                                            int isDeleted = mySqlCommand1.ExecuteNonQuery();
                                            if (isDeleted > 0)
                                            {
                                                //***************************************************
                                                //Repaint dgvMajor according to Search Criteria
                                                //***************************************************
                                                if (txtSearch.Text != " Search Course or Description")
                                                {
                                                    TxtSearch_TextChanged(null, null);

                                                    txtMajorID.Text = txtMajorDesc.Text = lblCourseDesc.Text = "";
                                                    cboCourse.SelectedIndex = -1;
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
                                                MessageBox.Show("Unable to delete Major (Specialization) record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}