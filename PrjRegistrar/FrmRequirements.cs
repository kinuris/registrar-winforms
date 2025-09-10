using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmRequirements : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        private static string requirementsid, fcuidno;
        int picture, goodmoral, form138, form137, birthcert, marriagecert, ncae;
        int transf_credential, transf_grades, transf_birthcert, transf_marriagecert;

        public FrmRequirements()
        {
            InitializeComponent();
        }

        private void FrmRequirements_Load(object sender, EventArgs e)
        {
            try
            {
                lblStudNo.Text = FrmPersonalData.Studno;
                lblstudfullname.Text = FrmPersonalData.StudFullname;
                fcuidno = FrmPersonalData.Fcuidno;

                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM reg_requirements WHERE cis_fcuidno = '" + fcuidno + "'", mySqlConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                if (mySqlDataReader.HasRows)
                {
                    if (mySqlDataReader.Read())
                    {
                        requirementsid = mySqlDataReader.GetValue(0).ToString();

                        picture = Convert.ToInt32(mySqlDataReader.GetValue(2).ToString());
                        goodmoral = Convert.ToInt32(mySqlDataReader.GetValue(3).ToString());
                        form138 = Convert.ToInt32(mySqlDataReader.GetValue(4).ToString());
                        form137 = Convert.ToInt32(mySqlDataReader.GetValue(5).ToString());
                        birthcert = Convert.ToInt32(mySqlDataReader.GetValue(6).ToString());
                        marriagecert = Convert.ToInt32(mySqlDataReader.GetValue(7).ToString());
                        ncae = Convert.ToInt32(mySqlDataReader.GetValue(8).ToString());
                        transf_credential = Convert.ToInt32(mySqlDataReader.GetValue(9).ToString());
                        transf_grades = Convert.ToInt32(mySqlDataReader.GetValue(10).ToString());
                        transf_birthcert = Convert.ToInt32(mySqlDataReader.GetValue(11).ToString());
                        transf_marriagecert = Convert.ToInt32(mySqlDataReader.GetValue(12).ToString());

                        if (picture == 1) chkpicture.Checked = true; else chkpicture.Checked = false;
                        if (goodmoral == 1) chkgoodmoral.Checked = true; else chkgoodmoral.Checked = false;
                        if (form138 == 1) chkform138.Checked = true; else chkform138.Checked = false;
                        if (form137 == 1) chkform137.Checked = true; else chkform137.Checked = false;
                        if (birthcert == 1) chkbirthcert.Checked = true; else chkbirthcert.Checked = false;
                        if (marriagecert == 1) chkmarriagecert.Checked = true; else chkmarriagecert.Checked = false;
                        if (ncae == 1) chkncae.Checked = true; else chkncae.Checked = false;
                        if (transf_credential == 1) chktransf_credential.Checked = true; else chktransf_credential.Checked = false;
                        if (transf_grades == 1) chktransf_grades.Checked = true; else chktransf_grades.Checked = false;
                        if (transf_birthcert == 1) chktransf_birthcert.Checked = true; else chktransf_birthcert.Checked = false;
                        if (transf_marriagecert == 1) chktransf_marriagecert.Checked = true; else chktransf_marriagecert.Checked = false;
                    }
                }
                else
                {
                    requirementsid = "0";
                }
                mySqlCommand.Dispose();
                mySqlConnection.Close();
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
                MySqlConnection mysqlcon = new MySqlConnection(connectionString);
                mysqlcon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("reg_requirements_addedit", mysqlcon)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (chkpicture.Checked) picture = 1; else picture = 0;
                if (chkgoodmoral.Checked) goodmoral = 1; else goodmoral = 0;
                if (chkform138.Checked) form138 = 1; else form138 = 0;
                if (chkform137.Checked) form137 = 1; else form137 = 0;
                if (chkbirthcert.Checked) birthcert = 1; else birthcert = 0;
                if (chkmarriagecert.Checked) marriagecert = 1; else marriagecert = 0;
                if (chkncae.Checked) ncae = 1; else ncae = 0;
                if (chktransf_credential.Checked) transf_credential = 1; else transf_credential = 0;
                if (chktransf_grades.Checked) transf_grades = 1; else transf_grades = 0;
                if (chktransf_birthcert.Checked) transf_birthcert = 1; else transf_birthcert = 0;
                if (chktransf_marriagecert.Checked) transf_marriagecert = 1; else transf_marriagecert = 0;

                mySqlCmd.Parameters.AddWithValue("_id", requirementsid);
                mySqlCmd.Parameters.AddWithValue("_cis_fcuidno", fcuidno);
                mySqlCmd.Parameters.AddWithValue("_cis_picture", picture);
                mySqlCmd.Parameters.AddWithValue("_cis_goodmoral", goodmoral);
                mySqlCmd.Parameters.AddWithValue("_cis_form138", form138);
                mySqlCmd.Parameters.AddWithValue("_cis_form137", form137);
                mySqlCmd.Parameters.AddWithValue("_cis_birthcert", birthcert);
                mySqlCmd.Parameters.AddWithValue("_cis_marriagecert", marriagecert);
                mySqlCmd.Parameters.AddWithValue("_cis_ncae", ncae);
                mySqlCmd.Parameters.AddWithValue("_cis_transf_credential", transf_credential);
                mySqlCmd.Parameters.AddWithValue("_cis_transf_grades", transf_grades);
                mySqlCmd.Parameters.AddWithValue("_cis_transf_birthcert", transf_birthcert);
                mySqlCmd.Parameters.AddWithValue("_cis_transf_marriagecert", transf_marriagecert);

                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Saved Successfully.", "Requirements", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                mySqlCmd.Dispose();
                mysqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}