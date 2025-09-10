using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmSignatories : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string selectMtblSignatories, AddEditflag, SignatoriesID, dgvSignatoriesID, deptcode;

        public FrmSignatories()
        {
            InitializeComponent();

            ClearFields();
            FillUpComboBox();
            GridFill();
            ActionButtons();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearFields();

            txtSignatoriesID.Text = "NEW";
            AddEditflag = "NEW";

            SignatoriesID = "0";

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
            txtSearch.Text = " Search Emp ID, Lastname, Dept Code, or Dept Name";
            txtSignatoriesID.Text = "";
            lblEmpID.Text = "Employee ID";
            lblEmpFullName.Text = "Employee FullName";
            cboDeptCode.SelectedIndex = -1;
            txtPositionLabel.Text = "";

            ClearDgvSignatories();
            tssllastmodified.Text = "mm/dd/yyyy";
            lblaccountability.Text = "accountability";
        }

        private void ClearDgvSignatories()
        {
            dgvSignatories.DataSource = null;
            dgvSignatories.Rows.Clear();
            dgvSignatories.Columns.Clear();
            dgvSignatories.Refresh();
        }

        private void FillUpComboBox()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_deptcode, cis_deptname FROM mtbl_department order by cis_deptcode", mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                        while (mySqlDataReader.Read())
                        {
                            cboDeptCode.Items.Add(mySqlDataReader["cis_deptcode"] + " - " + mySqlDataReader["cis_deptname"]);
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

                selectMtblSignatories = "SELECT A.id, A.cis_empid, concat(B.cis_lastname, ', ', ucase(left(B.cis_firstname, 1)), lcase(substring(B.cis_firstname, 2)), ' ', ucase(left(B.cis_midname, 1)), lcase(substring(B.cis_midname, 2))) as fullname, A.cis_deptcode, C.cis_deptname, A.cis_poslabel " +
                                                "FROM mtbl_signatories A, mtbl_employee B, mtbl_department C WHERE A.cis_empid = B.cis_empid AND A.cis_deptcode = C.cis_deptcode ORDER BY fullname, A.id";

                mySqlDA.SelectCommand = new MySqlCommand(selectMtblSignatories, mySqlConnection);

                DataTable table = new DataTable();
                mySqlDA.Fill(table);

                BindingSource bindingSource = new BindingSource
                {
                    DataSource = table
                };

                dgvSignatories.DataSource = bindingSource;

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
            dgvSignatories.DefaultCellStyle.Font = new Font("Tahoma", 9);
            dgvSignatories.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);

            dgvSignatories.Columns[0].HeaderText = "Signatories ID";
            dgvSignatories.Columns[1].HeaderText = "Employee ID";
            dgvSignatories.Columns[2].HeaderText = "Employee Name";
            dgvSignatories.Columns[3].HeaderText = "Department Code";
            dgvSignatories.Columns[4].HeaderText = "Department Name";
            dgvSignatories.Columns[5].HeaderText = "Customized Position Label";
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
            dgvSignatories.Columns.Add(CmdEdit);
            dgvSignatories.Columns[6].Width = 58;


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
            dgvSignatories.Columns.Add(CmdDelete);
            dgvSignatories.Columns[7].Width = 58;
        }

        private void BtnSearchEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmSearchEmployee frmSearchEmployee = new FrmSearchEmployee())
                {
                    frmSearchEmployee.ShowDialog();

                    if (frmSearchEmployee.selectButtonClicked == true)
                    {
                        lblEmpID.Text = FrmSearchEmployee.selectedEmpID;
                        lblEmpFullName.Text = FrmSearchEmployee.selectedFullname;
                    }
                }
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
            if (txtSearch.Text == "" || txtSearch.Text == " Search Emp ID, Lastname, Dept Code, or Dept Name") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = " Search Emp ID, Lastname, Dept Code, or Dept Name";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != " Search Emp ID, Lastname, Dept Code, or Dept Name")
                {
                    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    selectMtblSignatories = "SELECT A.id, A.cis_empid, concat(B.cis_lastname, ', ', ucase(left(B.cis_firstname, 1)), lcase(substring(B.cis_firstname, 2)), ' ', ucase(left(B.cis_midname, 1)), lcase(substring(B.cis_midname, 2))) as fullname, A.cis_deptcode, C.cis_deptname, A.cis_poslabel " +
                                            "FROM mtbl_signatories A, mtbl_employee B, mtbl_department C WHERE A.cis_empid = B.cis_empid AND A.cis_deptcode = C.cis_deptcode AND " +
                                            "(A.cis_empid LIKE concat('%', '" + txtSearch.Text + "' ,'%') OR A.cis_deptcode LIKE concat('%', '" + txtSearch.Text + "' ,'%') OR " +
                                            "B.cis_lastname LIKE concat('%', '" + txtSearch.Text + "' ,'%') OR C.cis_deptname LIKE concat('%', '" + txtSearch.Text + "' ,'%')) ORDER BY fullname";
                    mySqlDA.SelectCommand = new MySqlCommand(selectMtblSignatories, mySqlConnection);

                    dgvSignatories.DataSource = null;
                    dgvSignatories.Rows.Clear();
                    dgvSignatories.Columns.Clear();

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvSignatories.DataSource = bindingSource;

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
                //****************************************************************************
                // Gets the DeptCode without the DeptName
                //****************************************************************************                
                if (cboDeptCode.SelectedIndex >= 0)
                {
                    deptcode = cboDeptCode.Text.ToUpper().Trim();
                    int indexofspace = deptcode.IndexOf(" ");
                    deptcode = deptcode.Remove(indexofspace);
                }
                
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    //***********************************************************************************
                    //Check empid if already exist, to eliminate double entry
                    //***********************************************************************************
                    selectMtblSignatories = "SELECT cis_empid, cis_deptcode FROM mtbl_signatories WHERE cis_empid = '" + lblEmpID.Text + "' and cis_deptcode = '" + deptcode + "'";
                    using (MySqlCommand mySqlCommand = new MySqlCommand(selectMtblSignatories, mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (txtSignatoriesID.Text == "")
                        {
                            MessageBox.Show("Click the Add New button to enter a new Signatory.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSignatoriesID.Focus();
                        }
                        else if (lblEmpID.Text == "Employee ID" || lblEmpFullName.Text == "Employee FullName")
                        {
                            MessageBox.Show("Click the Search for Employee button to select a Signatory.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSearchEmployee.Focus();
                        }
                        else if (cboDeptCode.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select a Department.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cboDeptCode.Focus();
                        }
                        else if (txtPositionLabel.Text.Trim().Replace(" ","") == "")
                        {
                            MessageBox.Show("Enter a Position label for this Signatory.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPositionLabel.Focus();
                        }
                        else if (txtSignatoriesID.Text == "NEW" && mySqlDataReader.HasRows)     /*check for double entry*/
                        {   
                            if (mySqlDataReader.Read())
                            {
                                MessageBox.Show("Can't save the current record. \nThe system detected that this signatory already exist.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                txtSignatoriesID.Focus();
                            }
                        }
                        else
                        {
                            using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                            {
                                mySqlConnection1.Open();
                                using (MySqlCommand mySqlCommand1 = new MySqlCommand("mtbl_signatories_addedit", mySqlConnection1))
                                {
                                    mySqlCommand1.CommandType = CommandType.StoredProcedure;

                                    mySqlCommand1.Parameters.AddWithValue("_id", SignatoriesID);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_empid", lblEmpID.Text);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_deptcode", deptcode);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_poslabel", txtPositionLabel.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);                                    

                                    int isSaved = mySqlCommand1.ExecuteNonQuery();
                                    if (isSaved > 0)
                                        MessageBox.Show("Signatory record saved successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("Unable to save Signatory record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            //****************************************************************************
                            // Get the latest signatories id if NEW record, ready for updating the lastest inserted record.
                            //*****************************************************************************
                            if (AddEditflag == "NEW")
                            {
                                using (MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString))
                                {
                                    mySqlConnection2.Open();
                                    selectMtblSignatories = "SELECT max(id), cis_lastmodified, cis_accountability FROM mtbl_signatories WHERE cis_empid = '" + lblEmpID.Text + "'";
                                    using (MySqlCommand mySqlCommand2 = new MySqlCommand(selectMtblSignatories, mySqlConnection2))
                                    {
                                        MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

                                        if (mySqlDataReader2.HasRows)
                                        {
                                            while (mySqlDataReader2.Read())
                                            {
                                                SignatoriesID = txtSignatoriesID.Text = mySqlDataReader2.GetValue(0).ToString();
                                                tssllastmodified.Text = mySqlDataReader2["cis_lastmodified"].ToString();
                                                lblaccountability.Text = mySqlDataReader2["cis_accountability"].ToString();
                                            }
                                        }
                                        AddEditflag = "EDIT";
                                    }
                                }
                            }

                            ClearDgvSignatories();
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

        private void DgvSignatories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //****************************************************************************
                //edit button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvSignatories.Columns["btnEdit"].Index)
                {
                    AddEditflag = "EDIT";
                    SignatoriesID = dgvSignatoriesID = Convert.ToString(dgvSignatories.Rows[e.RowIndex].Cells["id"].Value);

                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM mtbl_signatories where id = '" + dgvSignatoriesID + "'", mySqlConnection))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    txtSignatoriesID.Text = mySqlDataReader["id"].ToString();

                                    lblEmpID.Text = mySqlDataReader["cis_empid"].ToString();
                                    lblEmpFullName.Text = Convert.ToString(dgvSignatories.Rows[e.RowIndex].Cells["fullname"].Value);

                                    deptcode = mySqlDataReader["cis_deptcode"].ToString();
                                    if (deptcode == "")
                                        cboDeptCode.SelectedIndex = -1;
                                    else
                                    {
                                        int index = cboDeptCode.FindString(deptcode);
                                        cboDeptCode.SelectedIndex = index;
                                    }

                                    txtPositionLabel.Text = mySqlDataReader["cis_poslabel"].ToString();

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
                if (e.ColumnIndex == dgvSignatories.Columns["btnDelete"].Index)
                {
                    dgvSignatoriesID = Convert.ToString(dgvSignatories.Rows[e.RowIndex].Cells["id"].Value);

                    DialogResult dialogresult = MessageBox.Show("Are you sure you want to Delete this record?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogresult == DialogResult.Yes)
                    {
                        using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                        {
                            mySqlConnection.Open();
                            using (MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM mtbl_signatories WHERE id = '" + dgvSignatoriesID + "'", mySqlConnection))
                            {
                                int isDeleted = mySqlCommand.ExecuteNonQuery();
                                if (isDeleted > 0)
                                {
                                    //***************************************************
                                    //Repaint dgvSignatories according to Search Criteria
                                    //***************************************************
                                    if (txtSearch.Text != " Search Emp ID, Lastname, Dept Code, or Dept Name")
                                    {
                                        TxtSearch_TextChanged(null, null);

                                        txtSignatoriesID.Text = "";
                                        lblEmpID.Text = "Employee ID";
                                        lblEmpFullName.Text = "Employee FullName";
                                        cboDeptCode.SelectedIndex = -1;
                                        txtPositionLabel.Text = "";

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
                                    MessageBox.Show("Unable to delete Signatory record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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