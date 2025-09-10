using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Globalization;

namespace PrjRegistrar
{
    public partial class FrmLogin : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        public static string userfullname, username, empid, accesslevel, designation, registrarFullName, registrarPosLabel;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void CmdSignin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please provide Username and Password", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblencrypt.Text = CalculateMD5Hash(txtPassword.Text);

                    using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                    {
                        mysqlcon.Open();

                        using (MySqlCommand command = new MySqlCommand("SELECT concat(ucase(cis_firstname), ' ', ucase(left(cis_midname,1)), '. ', ucase(cis_lastname)) as fullname, cis_username, cis_password, cis_empid, cis_accesslevel " +
                                                                 "FROM mtbl_useraccount WHERE cis_username = '" + txtUsername.Text + "' and cis_password = '" + lblencrypt.Text + "'", mysqlcon))
                        {
                            MySqlDataReader DataReader = command.ExecuteReader();
                            if (DataReader.HasRows)
                            {
                                if (DataReader.Read())
                                {
                                    username = txtUsername.Text;
                                    userfullname = DataReader.GetValue(0).ToString();
                                    empid = DataReader["cis_empid"].ToString();
                                    accesslevel = DataReader["cis_accesslevel"].ToString();

                                    //gets the system user's designation
                                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                                    {
                                        mySqlConnection.Open();
                                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_poslabel FROM mtbl_signatories WHERE cis_empid = '" + empid + "'", mySqlConnection))
                                        {
                                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                                            if (mySqlDataReader.HasRows)
                                            {
                                                if (mySqlDataReader.Read())
                                                {
                                                    designation = mySqlDataReader["cis_poslabel"].ToString();
                                                }
                                            }
                                            else
                                            {
                                                designation = "";
                                            }
                                        }
                                    }

                                    //gets the registrar's fullname and poslabel for generating reports
                                    //FROM mtbl_employee and mtbl_signatories Table
                                    using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                                    {
                                        mySqlConnection1.Open();
                                        using (MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT concat(ucase(A.cis_firstname), ' ', ucase(left(A.cis_midname,1)), '. ', ucase(A.cis_lastname)) as regfullname, B.cis_poslabel " +
                                                                                            "FROM mtbl_employee A, mtbl_signatories B WHERE A.cis_empid = B.cis_empid and " +
                                                                                            "B.cis_poslabel like '%REGISTRAR' and B.cis_deptcode = 'REG'", mySqlConnection1))
                                        {
                                            MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();
                                            if (mySqlDataReader1.HasRows)
                                            {
                                                if (mySqlDataReader1.Read())
                                                {
                                                    registrarFullName = mySqlDataReader1["regfullname"].ToString();
                                                    registrarPosLabel = mySqlDataReader1["cis_poslabel"].ToString();

                                                    registrarPosLabel = ConvertTo_ProperCase(registrarPosLabel);
                                                }
                                            }
                                            else
                                            {
                                                registrarFullName = "";
                                                registrarPosLabel = "";
                                            }
                                        }
                                    }

                                    Hide();

                                    FrmMain frmMain = new FrmMain();
                                    frmMain.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username or Password. \nPlease Try again.", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static string ConvertTo_ProperCase(string text)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(text.ToLower());
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

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void TxtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CmdSignin_Click(null, null);
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                FrmLogin_FormClosing(null, null);
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CmdSignin_Click(null, null);
            }
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                FrmLogin_FormClosing(null, null);
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
