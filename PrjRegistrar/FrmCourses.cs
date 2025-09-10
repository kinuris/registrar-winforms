using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmCourses : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string AddEditflag, CourseID, dgvCourseID, cisCourse;

        public FrmCourses()
        {
            InitializeComponent();

            ClearFields();
            FillUpComboBoxes();
            GridFill();
            ActionButtons();
        }

        private void BtnAddCourse_Click(object sender, EventArgs e)
        {
            ClearFields();

            txtCourseID.Text = "NEW";
            AddEditflag = "NEW";

            CourseID = "0";

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
            txtCourseID.Text = txtCourse.Text = txtCourseDesc.Text = txtDeptAbbrev.Text = "";
            cboCategory.SelectedIndex = -1;
            cboSubCategory.SelectedIndex = -1;
            cboDeptCode.SelectedIndex = -1;

            ClearDgvCourses();
            tssllastmodified.Text = "mm/dd/yyyy";
            lblaccountability.Text = "accountability";
        }

        private void ClearDgvCourses()
        {
            dgvCourses.DataSource = null;
            dgvCourses.Rows.Clear();
            dgvCourses.Columns.Clear();
            dgvCourses.Refresh();
        }

        private void FillUpComboBoxes()
        {
            try
            {
                /************************************
                //fillup category combobox
                ************************************/
                using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                {
                    mySqlConnection1.Open();
                    using (MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT cis_category FROM mtbl_defaultperiod WHERE cis_status = '1'", mySqlConnection1))
                    {
                        MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();
                        while (mySqlDataReader1.Read())
                        {
                            cboCategory.Items.Add(mySqlDataReader1[0]);
                        }
                    }
                }

                /************************************
                //fillup Department Code combobox
                ************************************/
                using (MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString))
                {
                    mySqlConnection2.Open();
                    using (MySqlCommand mySqlCommand2 = new MySqlCommand("SELECT cis_deptcode, cis_deptname FROM mtbl_department WHERE cis_deptcode like 'DEPT%' OR cis_deptcode like 'BED%' order by cis_deptcode", mySqlConnection2))
                    {
                        MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();
                        while (mySqlDataReader2.Read())
                        {
                            cboDeptCode.Items.Add(mySqlDataReader2["cis_deptcode"] + " - " + mySqlDataReader2["cis_deptname"]);
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

                string SelectMtblCourse = "SELECT id, cis_course, cis_coursedesc, cis_category, cis_subcategory, cis_deptcode, cis_idcode FROM mtbl_course order by cis_category, cis_course";
                mySqlDA.SelectCommand = new MySqlCommand(SelectMtblCourse, mySqlConnection);

                DataTable table = new DataTable();
                mySqlDA.Fill(table);

                BindingSource bindingSource = new BindingSource
                {
                    DataSource = table
                };

                dgvCourses.DataSource = bindingSource;

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
            dgvCourses.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvCourses.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);

            dgvCourses.Columns[0].HeaderText = "Course ID";
            dgvCourses.Columns[1].HeaderText = "Course";
            dgvCourses.Columns[2].HeaderText = "Course Description";
            dgvCourses.Columns[3].HeaderText = "Category";
            dgvCourses.Columns[4].HeaderText = "Sub Category";
            dgvCourses.Columns[5].HeaderText = "Department Code";
            dgvCourses.Columns[6].HeaderText = "Dept. Abbreviation";
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
            dgvCourses.Columns.Add(CmdEdit);
            dgvCourses.Columns[7].Width = 58;


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
            dgvCourses.Columns.Add(CmdDelete);
            dgvCourses.Columns[8].Width = 58;
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

                    string SelectMtblCourse = "SELECT id, cis_course, cis_coursedesc, cis_category, cis_subcategory, cis_deptcode, cis_idcode FROM mtbl_course WHERE " +
                                                 "cis_course LIKE concat('%', '" + txtSearch.Text + "' ,'%') or " +                                                 
                                                 "cis_coursedesc LIKE concat('%', '" + txtSearch.Text + "' ,'%') order by cis_category, cis_course";
                    mySqlDA.SelectCommand = new MySqlCommand(SelectMtblCourse, mySqlConnection);

                    dgvCourses.DataSource = null;
                    dgvCourses.Rows.Clear();
                    dgvCourses.Columns.Clear();

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvCourses.DataSource = bindingSource;

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
                    //Check course if already exist, to eliminate double entry
                    //***********************************************************************************
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_course FROM mtbl_course WHERE cis_course = '" + txtCourse.Text + "'", mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (txtCourseID.Text == "")
                        {
                            MessageBox.Show("Click the Add New button to enter a new Course.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCourseID.Focus();
                        }
                        else if (txtCourse.Text == "")
                        {
                            MessageBox.Show("Enter valid Course.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCourse.Focus();
                        }
                        else if (txtCourseDesc.Text == "")
                        {
                            MessageBox.Show("Enter Course Description.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCourseDesc.Focus();
                        }
                        else if (cboCategory.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select Course Category.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cboCategory.Focus();
                        }
                        else if (cboDeptCode.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select Course Department Code.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cboDeptCode.Focus();
                        }
                        else if (mySqlDataReader.HasRows && AddEditflag == "NEW")     //check for double entry
                        {
                            if (mySqlDataReader.Read())
                            {
                                MessageBox.Show("Can't save the current record. The system detected that this course already exist.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                txtCourse.Focus();
                            }
                        }
                        else
                        {
                            using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                            {
                                mySqlConnection1.Open();
                                using (MySqlCommand mySqlCommand1 = new MySqlCommand("mtbl_course_addedit", mySqlConnection1))
                                {
                                    mySqlCommand1.CommandType = CommandType.StoredProcedure;

                                    //****************************************************************************
                                    // Gets the DeptCode without the DeptName
                                    //****************************************************************************
                                    string deptcode = cboDeptCode.Text.ToUpper().Trim();
                                    int indexofspace = deptcode.IndexOf(" ");
                                    deptcode = deptcode.Remove(indexofspace);

                                    mySqlCommand1.Parameters.AddWithValue("_id", CourseID);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_course", txtCourse.Text.ToUpper().Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_coursedesc", txtCourseDesc.Text.ToUpper().Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_category", cboCategory.Text.ToUpper().Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_subcategory", cboSubCategory.Text.ToUpper().Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_deptcode", deptcode);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_idcode", txtDeptAbbrev.Text.ToUpper().Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);
                                    
                                    int isSaved = mySqlCommand1.ExecuteNonQuery();
                                    if (isSaved > 0)
                                        MessageBox.Show("Course record saved successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("Unable to save Course record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                    using (MySqlCommand mySqlCommand2 = new MySqlCommand("SELECT max(id) FROM mtbl_course WHERE cis_course = '" + txtCourse.Text.ToUpper() + "'", mySqlConnection2))
                                    {
                                        MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

                                        if (mySqlDataReader2.HasRows)
                                        {
                                            while (mySqlDataReader2.Read())
                                            {
                                                txtCourseID.Text = mySqlDataReader2.GetValue(0).ToString();
                                                CourseID = txtCourseID.Text;
                                            }
                                        }
                                        AddEditflag = "EDIT";
                                    }
                                }
                            }

                            ClearDgvCourses();
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

        private void DgvCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //****************************************************************************
                //edit button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvCourses.Columns["btnEdit"].Index)
                {
                    AddEditflag = "EDIT";
                    CourseID = dgvCourseID = Convert.ToString(dgvCourses.Rows[e.RowIndex].Cells["id"].Value);

                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM mtbl_course where id = '" + dgvCourseID + "'", mySqlConnection))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    txtCourseID.Text = mySqlDataReader["id"].ToString();
                                    txtCourse.Text = mySqlDataReader["cis_course"].ToString();
                                    txtCourseDesc.Text = mySqlDataReader["cis_coursedesc"].ToString();
                                    cboCategory.Text = mySqlDataReader["cis_category"].ToString();

                                    string subCategory = mySqlDataReader["cis_subcategory"].ToString();
                                    if (subCategory == "")
                                        cboSubCategory.SelectedIndex = -1;
                                    else
                                        cboSubCategory.Text = mySqlDataReader["cis_subcategory"].ToString();

                                    txtDeptAbbrev.Text = mySqlDataReader["cis_idcode"].ToString();

                                    int index;
                                    string deptcode = mySqlDataReader["cis_deptcode"].ToString();
                                    if (deptcode == "")
                                        cboDeptCode.SelectedIndex = -1;
                                    else
                                    {
                                        index = cboDeptCode.FindString(deptcode);
                                        cboDeptCode.SelectedIndex = index;
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
                                }
                            }
                        }
                    }
                }

                //****************************************************************************
                //delete button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvCourses.Columns["btnDelete"].Index)
                {
                    dgvCourseID = Convert.ToString(dgvCourses.Rows[e.RowIndex].Cells["id"].Value);
                    cisCourse = Convert.ToString(dgvCourses.Rows[e.RowIndex].Cells["cis_course"].Value);

                    DialogResult dialogresult = MessageBox.Show("Are you sure you want to Delete this record?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogresult == DialogResult.Yes)
                    {
                        //**********************************************************************************************
                        //Check cis_course from reg_subjenrolled table. If it exists, course record cant be deleted.
                        //**********************************************************************************************
                        using (MySqlConnection mySqlConnection0 = new MySqlConnection(connectionString))
                        {
                            mySqlConnection0.Open();
                            using (MySqlCommand mySqlCommand0 = new MySqlCommand("SELECT cis_course FROM reg_subjenrolled WHERE cis_course = '" + cisCourse + "'", mySqlConnection0))
                            {
                                MySqlDataReader mySqlDataReader0 = mySqlCommand0.ExecuteReader();

                                if (mySqlDataReader0.HasRows)
                                {
                                    MessageBox.Show("Unable to Delete Course. Course record is already active.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                                    {
                                        mySqlConnection1.Open();
                                        using (MySqlCommand mySqlCommand1 = new MySqlCommand("DELETE FROM mtbl_course WHERE id = '" + dgvCourseID + "'", mySqlConnection1))
                                        {
                                            int isDeleted = mySqlCommand1.ExecuteNonQuery();
                                            if (isDeleted > 0)
                                            {
                                                //***************************************************
                                                //Repaint dgvCourses according to Search Criteria
                                                //***************************************************
                                                if (txtSearch.Text != " Search Course or Description")
                                                {
                                                    TxtSearch_TextChanged(null, null);

                                                    txtCourseID.Text = txtCourse.Text = txtCourseDesc.Text = txtDeptAbbrev.Text = "";
                                                    cboCategory.SelectedIndex = -1;
                                                    cboSubCategory.SelectedIndex = -1;
                                                    cboDeptCode.SelectedIndex = -1;
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
                                                MessageBox.Show("Unable to delete Course record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
