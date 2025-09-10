using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Linq;

namespace PrjRegistrar
{
    public partial class FrmFTPAccount : Form
    {   
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string selectStatement, AddEditflag, FTPAccountID, dgvFTPAccountSelected, encryptPassword, accesslevel;
        int indexofspace, indexofaccesslevel;

        public FrmFTPAccount()
        {
            InitializeComponent();

            ClearFields();
            FillUpComboBox();
            GridFill();
            ActionButtons();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            ClearFields();

            txtFTPAccountID.Text = "NEW";
            AddEditflag = "NEW";

            FTPAccountID = "0";

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
            txtSearch.Text = " Search Username";
            txtFTPAccountID.Text = txtUserName.Text = txtOldPassword.Text = txtNewPassword.Text = txtConfirmNewPass.Text = "";

            txtOldPassword.Enabled = false;

            cboAccessLevel.SelectedIndex = -1;

            ClearDgvFTPAccount();
            tssllastmodified.Text = "mm/dd/yyyy";
            lblaccountability.Text = "accountability";
        }

        private void ClearDgvFTPAccount()
        {
            dgvFTPAccount.DataSource = null;
            dgvFTPAccount.Rows.Clear();
            dgvFTPAccount.Columns.Clear();
            dgvFTPAccount.Refresh();
        }

        private void FillUpComboBox()
        {
            try
            {
                using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                {
                    mysqlcon.Open();
                    using (MySqlCommand Command = new MySqlCommand("SELECT cis_accesslevel, cis_description FROM mtbl_accesslevel", mysqlcon))
                    {
                        MySqlDataReader DataReader = Command.ExecuteReader();
                        while (DataReader.Read())
                        {
                            cboAccessLevel.Items.Add(DataReader["cis_accesslevel"].ToString() + " - " + DataReader["cis_description"].ToString());
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
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    selectStatement = "SELECT id, cis_username, cis_accesslevel FROM mtbl_ftpaccount order by id";
                    mySqlDA.SelectCommand = new MySqlCommand(selectStatement, mySqlConnection);

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvFTPAccount.DataSource = bindingSource;

                    HeaderTexts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HeaderTexts()
        {
            dgvFTPAccount.DefaultCellStyle.Font = new Font("Tahoma", 10);
            dgvFTPAccount.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 10);

            dgvFTPAccount.Columns[0].HeaderText = "FTP Account ID";
            dgvFTPAccount.Columns[1].HeaderText = "User Name";
            dgvFTPAccount.Columns[2].HeaderText = "Access Level";
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
            dgvFTPAccount.Columns.Add(CmdEdit);
            dgvFTPAccount.Columns[3].Width = 58;


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
            dgvFTPAccount.Columns.Add(CmdDelete);
            dgvFTPAccount.Columns[4].Width = 58;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CboAccessLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cboAccessLevel.SelectedIndex > -1 )
                {
                    accesslevel = cboAccessLevel.Text.ToUpper().Trim();
                    indexofspace = accesslevel.IndexOf(" ");
                    accesslevel = accesslevel.Remove(indexofspace);

                    //if Administrator only
                    if (accesslevel == "1") 
                    {
                        using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                        {
                            mysqlcon.Open();

                            selectStatement = "SELECT cis_username, cis_accesslevel FROM mtbl_ftpaccount WHERE cis_username = '" + txtUserName.Text  + "' AND cis_accesslevel = '" + accesslevel + "'";
                            using (MySqlCommand mysqlcommand = new MySqlCommand(selectStatement, mysqlcon))
                            {
                                MySqlDataReader mysqldatareader = mysqlcommand.ExecuteReader();
                                if (mysqldatareader.HasRows)
                                {
                                    
                                }
                                else
                                {
                                    using (MySqlConnection mysqlcon1 = new MySqlConnection(connectionString))
                                    {
                                        mysqlcon1.Open();

                                        selectStatement = "SELECT cis_accesslevel FROM mtbl_ftpaccount WHERE cis_accesslevel = '" + accesslevel + "'";
                                        using (MySqlCommand mysqlcommand1 = new MySqlCommand(selectStatement, mysqlcon1))
                                        {
                                            MySqlDataReader mysqldatareader1 = mysqlcommand1.ExecuteReader();
                                            if (mysqldatareader1.HasRows)
                                            {
                                                MessageBox.Show("An Administrator already been set. Only one administrator account is allowed.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                cboAccessLevel.Focus();
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

        private void TxtOldPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtOldPassword.Text.Length > 0)
                {   
                    encryptPassword = Encrypt(txtOldPassword.Text);

                    using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                    {
                        mysqlcon.Open();

                        selectStatement = "SELECT cis_password FROM mtbl_ftpaccount where id = '" + txtFTPAccountID.Text + "' and cis_password = '" + encryptPassword + "'";
                        using (MySqlCommand mysqlcommand = new MySqlCommand(selectStatement, mysqlcon))
                        {
                            MySqlDataReader mysqldatareader = mysqlcommand.ExecuteReader();
                            if (mysqldatareader.HasRows)
                            {
                                
                            }
                            else
                            {
                                MessageBox.Show("Invalid old password. Try again.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtOldPassword.Focus();
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

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" || txtSearch.Text == " Search Username") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = " Search Username";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != " Search Username")
                {
                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();

                        MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                        selectStatement = "SELECT id, cis_username, cis_accesslevel FROM mtbl_ftpaccount WHERE cis_username LIKE concat('%', '" + txtSearch.Text + "' ,'%') order by id";
                        mySqlDA.SelectCommand = new MySqlCommand(selectStatement, mySqlConnection);

                        dgvFTPAccount.DataSource = null;
                        dgvFTPAccount.Rows.Clear();
                        dgvFTPAccount.Columns.Clear();

                        DataTable table = new DataTable();
                        mySqlDA.Fill(table);

                        BindingSource bindingSource = new BindingSource
                        {
                            DataSource = table
                        };

                        dgvFTPAccount.DataSource = bindingSource;

                        HeaderTexts();
                        ActionButtons();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string Encrypt(string decrypted)
        {
            string hash = "Password@2021$";
            byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDES = new TripleDESCryptoServiceProvider();

            tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //***********************************************************************************
                //Check ftp username if already exist, to eliminate double entry
                //***********************************************************************************
                encryptPassword = Encrypt(txtNewPassword.Text);

                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    selectStatement = "SELECT cis_username FROM mtbl_ftpaccount WHERE cis_username = '" + txtUserName.Text.Trim() + "'";
                    using (MySqlCommand mySqlCommand = new MySqlCommand(selectStatement, mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (txtFTPAccountID.Text == "")
                        {
                            MessageBox.Show("Click the Add New button to enter a new FTP Account.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtFTPAccountID.Focus();
                        }
                        else if (txtUserName.Text.Trim() == "")
                        {
                            MessageBox.Show("Please provide Username", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtUserName.Focus();
                        }
                        else if (cboAccessLevel.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select Acces Level", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cboAccessLevel.Focus();
                        }
                        else if (txtOldPassword.Enabled == true && txtOldPassword.Text.Trim() == "")
                        {
                            MessageBox.Show("Please provide Old Password.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOldPassword.Focus();
                        }
                        else if (txtNewPassword.Text.Trim() == "")
                        {
                            MessageBox.Show("Please provide New Password.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNewPassword.Focus();
                        }
                        else if (txtConfirmNewPass.Text.Trim() == "")
                        {
                            MessageBox.Show("Please confirm Password", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtConfirmNewPass.Focus();
                        }
                        else if (txtNewPassword.Text.Trim() != txtConfirmNewPass.Text.Trim())
                        {
                            MessageBox.Show("New password and Confirm Password doesn't match. Please try again.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtConfirmNewPass.Focus();
                        }
                        else if (mySqlDataReader.HasRows && AddEditflag == "NEW")     //check for double entry
                        {
                            if (mySqlDataReader.Read())
                            {
                                MessageBox.Show("User name already exist. Register new user name.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                txtUserName.Focus();
                            }
                        }
                        else
                        {
                            using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                            {
                                mySqlConnection1.Open();

                                using (MySqlCommand mySqlCommand1 = new MySqlCommand("mtbl_ftpaccount_addedit", mySqlConnection1))
                                {
                                    mySqlCommand1.CommandType = CommandType.StoredProcedure;
                                    mySqlCommand1.Parameters.AddWithValue("_id", FTPAccountID);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_username", txtUserName.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_password", encryptPassword);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_accesslevel", accesslevel);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);

                                    int isSaved = mySqlCommand1.ExecuteNonQuery();
                                    if (isSaved > 0)
                                        MessageBox.Show("FTP Account record saved successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("Unable to save FTP Account.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            //****************************************************************************************
                            // Get the latest account id if NEW record, ready for updating the latest inserted record.
                            //****************************************************************************************
                            if (AddEditflag == "NEW")
                            {
                                using (MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString))
                                {
                                    mySqlConnection2.Open();

                                    selectStatement = "SELECT id, cis_lastmodified, cis_accountability FROM mtbl_ftpaccount WHERE cis_username = '" + txtUserName.Text.Trim() + "' and cis_password = '" + encryptPassword + "'";
                                    using (MySqlCommand mySqlCommand2 = new MySqlCommand(selectStatement, mySqlConnection2))
                                    {
                                        MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

                                        if (mySqlDataReader2.HasRows)
                                        {
                                            while (mySqlDataReader2.Read())
                                            {
                                                FTPAccountID = txtFTPAccountID.Text = mySqlDataReader2.GetValue(0).ToString();

                                                tssllastmodified.Text = mySqlDataReader2["cis_lastmodified"].ToString();
                                                lblaccountability.Text = mySqlDataReader2["cis_accountability"].ToString();
                                            }
                                        }
                                        AddEditflag = "EDIT";
                                    }
                                }
                            }

                            ClearDgvFTPAccount();
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

        private void DgvFTPAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //****************************************************************************
                //edit button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvFTPAccount.Columns["btnEdit"].Index)
                {
                    dgvFTPAccountSelected = Convert.ToString(dgvFTPAccount.Rows[e.RowIndex].Cells["id"].Value);

                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM mtbl_ftpaccount where id = '" + dgvFTPAccountSelected + "'", mySqlConnection))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    FTPAccountID = txtFTPAccountID.Text = mySqlDataReader["id"].ToString();
                                    txtUserName.Text = mySqlDataReader["cis_username"].ToString();

                                    accesslevel = mySqlDataReader["cis_accesslevel"].ToString();
                                    if (accesslevel == "" || accesslevel == null)
                                        cboAccessLevel.SelectedIndex = -1;
                                    else
                                    {
                                        indexofaccesslevel = cboAccessLevel.FindString(accesslevel);
                                        cboAccessLevel.SelectedIndex = indexofaccesslevel;
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

                            txtOldPassword.Enabled = true;
                            txtOldPassword.Text = txtNewPassword.Text = txtConfirmNewPass.Text = "";

                            AddEditflag = "EDIT";
                        }
                    }
                }

                //****************************************************************************
                //delete button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvFTPAccount.Columns["btnDelete"].Index)
                {
                    dgvFTPAccountSelected = Convert.ToString(dgvFTPAccount.Rows[e.RowIndex].Cells["id"].Value);

                    DialogResult dialogresult = MessageBox.Show("Are you sure you want to Delete this FTP User Account?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogresult == DialogResult.Yes)
                    {
                        using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                        {
                            mySqlConnection1.Open();
                            using (MySqlCommand mySqlCommand1 = new MySqlCommand("DELETE FROM mtbl_ftpaccount WHERE id = '" + dgvFTPAccountSelected + "'", mySqlConnection1))
                            {
                                int isDeleted = mySqlCommand1.ExecuteNonQuery();
                                if (isDeleted > 0)
                                {
                                    //***************************************************
                                    //Repaint dgvFTPAccount according to Search Criteria
                                    //***************************************************
                                    if (txtSearch.Text != " Search Username")
                                    {
                                        TxtSearch_TextChanged(null, null);

                                        txtFTPAccountID.Text = txtUserName.Text = txtOldPassword.Text = txtNewPassword.Text = txtConfirmNewPass.Text = "";

                                        txtOldPassword.Enabled = false;

                                        cboAccessLevel.SelectedIndex = -1;

                                        tssllastmodified.Text = "mm/dd/yyyy";
                                        lblaccountability.Text = "accountability";
                                    }
                                    else
                                    {
                                        ClearFields();
                                        GridFill();
                                        ActionButtons();
                                    }

                                    MessageBox.Show("FTP Account Deleted Successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                }
                                else
                                {
                                    MessageBox.Show("Unable to Delete FTP Account.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
