using System;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmAuthorizationLogin : Form
    {   
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        public bool isAuthorized;

        public FrmAuthorizationLogin()
        {
            isAuthorized = false;
            InitializeComponent();
        }

        private void BtnAuthorize_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please provide Registrar's Username and Password", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblencrypt.Text = CalculateMD5Hash(txtPassword.Text);

                    using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                    {
                        mysqlcon.Open();
                        using (MySqlCommand command = new MySqlCommand("SELECT cis_username, cis_password, cis_deptcode, cis_accesslevel FROM mtbl_useraccount WHERE cis_username = '" + txtUsername.Text.Trim() + "' and cis_password = '" + lblencrypt.Text + "'" +
                                                                       "and cis_deptcode = 'REG' and (cis_accesslevel = 1 or cis_accesslevel = 5)", mysqlcon))
                        {
                            MySqlDataReader DataReader = command.ExecuteReader();
                            if (DataReader.HasRows)
                            {
                                if (DataReader.Read())
                                {
                                    isAuthorized = true;

                                    command.Dispose();
                                    mysqlcon.Close();
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Unauthorized Registrar's Username or Password. \nPlease try again.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void TxtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnAuthorize_Click(null, null);
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {   
                Close();
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnAuthorize_Click(null, null);
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                Close();
            }
        }

        private void TxtUsername_Enter(object sender, EventArgs e)
        {
            txtUsername.BackColor = System.Drawing.Color.FromArgb(236, 245, 237);
        }

        private void TxtUsername_Leave(object sender, EventArgs e)
        {
            txtUsername.BackColor = System.Drawing.Color.White;
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = System.Drawing.Color.FromArgb(236, 245, 237);
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = System.Drawing.Color.White;
        }
    }
}
