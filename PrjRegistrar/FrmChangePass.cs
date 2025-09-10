using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmChangePass : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;

        public FrmChangePass()
        {
            InitializeComponent();
        }

        private void TxtOldPass_Enter(object sender, EventArgs e)
        {
            txtOldPass.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);            
        }

        private void TxtOldPass_Leave(object sender, EventArgs e)
        {
            txtOldPass.BackColor = System.Drawing.Color.White;

            ValidateOldPassword();
        }

        private void TxtNewPass_Enter(object sender, EventArgs e)
        {
            txtNewPass.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
        }

        private void TxtNewPass_Leave(object sender, EventArgs e)
        {
            txtNewPass.BackColor = System.Drawing.Color.White;
        }

        private void TxtConfirmNewPass_Enter(object sender, EventArgs e)
        {
            txtConfirmNewPass.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
        }

        private void TxtConfirmNewPass_Leave(object sender, EventArgs e)
        {
            txtConfirmNewPass.BackColor = System.Drawing.Color.White;
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2").ToLower());
            }
            return sb.ToString();
        }

        private void ValidateOldPassword()
        {
            try
            {
                if (txtOldPass.Text.Length > 0)
                {
                    lblencrypt.Text = CalculateMD5Hash(txtOldPass.Text);

                    MySqlConnection mysqlcon = new MySqlConnection(connectionString);
                    mysqlcon.Open();
                    MySqlCommand mysqlcommand = new MySqlCommand("SELECT cis_password FROM mtbl_useraccount where cis_username = '" + FrmLogin.username + "' and cis_password = '" + lblencrypt.Text + "'", mysqlcon);
                    MySqlDataReader mysqldatareader = mysqlcommand.ExecuteReader();

                    if (mysqldatareader.HasRows)
                    {
                        if (mysqldatareader.Read())
                        {
                            //MessageBox.Show(FrmLogin.username);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid old password. Try again.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtOldPass.Focus();
                    }

                    mysqlcommand.Dispose();
                    mysqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void BtnChangePass_Click(object sender, EventArgs e)
        {
            try 
            {
                if (txtOldPass.Text.Trim() != "")
                {
                    lblencrypt.Text = CalculateMD5Hash(txtOldPass.Text);

                    MySqlConnection mysqlcon = new MySqlConnection(connectionString);
                    mysqlcon.Open();
                    MySqlCommand mysqlcommand = new MySqlCommand("SELECT cis_password FROM mtbl_useraccount where cis_username = '" + FrmLogin.username + "' and cis_password = '" + lblencrypt.Text + "'", mysqlcon);
                    MySqlDataReader mysqldatareader = mysqlcommand.ExecuteReader();

                    if (mysqldatareader.HasRows)
                    {
                        if (mysqldatareader.Read())
                        {
                            if (txtNewPass.Text.Trim() != "" && txtConfirmNewPass.Text.Trim() != "")
                            {
                                if (txtNewPass.Text.Trim() == txtConfirmNewPass.Text.Trim())
                                {
                                    string newPassword = CalculateMD5Hash(txtConfirmNewPass.Text);                                    

                                    MySqlConnection mysqlcon1 = new MySqlConnection(connectionString);
                                    mysqlcon1.Open();

                                    MySqlCommand mysqlcommand1 = new MySqlCommand("UPDATE mtbl_useraccount SET cis_password = '" + newPassword + "', cis_lastmodified = @cis_lastmodified WHERE cis_username = '" + FrmLogin.username + "' and cis_password = '" + lblencrypt.Text + "'", mysqlcon1);
                                    mysqlcommand1.Parameters.AddWithValue("@cis_lastmodified", DateTime.Now);
                                    mysqlcommand1.ExecuteNonQuery();

                                    MessageBox.Show("Password changed succesfully.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    mysqlcommand1.Dispose();
                                    mysqlcon1.Close();

                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("New password and Confirm Password doesn't match. Please try again.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtConfirmNewPass.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("New password or Confirm Password can't have blank entry. Please try again.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                
                                if (txtNewPass.Text.Trim() == "")
                                    txtNewPass.Focus();
                                else
                                    txtConfirmNewPass.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid old password. Try again.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtOldPass.Focus();
                    }

                    mysqlcommand.Dispose();
                    mysqlcon.Close();
                }
                else
                {
                    MessageBox.Show("Old password can't have blank entry. Please try again.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOldPass.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TxtOldPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnChangePass_Click(null, null);
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                this.Close();
            }
        }

        private void TxtNewPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnChangePass_Click(null, null);
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                this.Close();
            }
        }

        private void TxtConfirmNewPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnChangePass_Click(null, null);
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                this.Close();
            }
        }

        private void BtnPeekOld_MouseDown(object sender, MouseEventArgs e)
        {
            txtOldPass.UseSystemPasswordChar = false;
        }

        private void BtnPeekOld_MouseUp(object sender, MouseEventArgs e)
        {
            txtOldPass.UseSystemPasswordChar = true;
        }

        private void BtnPeekNew_MouseDown(object sender, MouseEventArgs e)
        {
            txtNewPass.UseSystemPasswordChar = false;
        }

        private void BtnPeekNew_MouseUp(object sender, MouseEventArgs e)
        {
            txtNewPass.UseSystemPasswordChar = true;
        }

        private void BtnPeekConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            txtConfirmNewPass.UseSystemPasswordChar = false;
        }

        private void BtnPeekConfirm_MouseUp(object sender, MouseEventArgs e)
        {
            txtConfirmNewPass.UseSystemPasswordChar = true;
        }

        private void BtnPeekOld_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                txtOldPass.UseSystemPasswordChar = false;
            }
        }

        private void BtnPeekOld_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                txtOldPass.UseSystemPasswordChar = true;
            }
        }

        private void BtnPeekNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                txtNewPass.UseSystemPasswordChar = false;
            }
        }

        private void BtnPeekNew_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                txtNewPass.UseSystemPasswordChar = true;
            }
        }

        private void BtnPeekConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                txtConfirmNewPass.UseSystemPasswordChar = false;
            }
        }

        private void BtnPeekConfirm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                txtConfirmNewPass.UseSystemPasswordChar = true;
            }
        }
    }
}
