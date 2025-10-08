using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmGeneralAverageParams : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        public static string selectedSchoolYear = "";
        public static string selectedCourse = "";
        public bool parametersSelected = false;

        public FrmGeneralAverageParams()
        {
            InitializeComponent();
        }

        private void FrmGeneralAverageParams_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSchoolYears();
                LoadCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSchoolYears()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_schlyr FROM reg_subjenrolled WHERE cis_schlyr IS NOT NULL AND cis_schlyr != '' ORDER BY cis_schlyr DESC", mySqlConnection))
                    {
                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            cboSchoolYear.Items.Clear();
                            while (mySqlDataReader.Read())
                            {
                                string schoolYear = mySqlDataReader["cis_schlyr"].ToString();
                                if (!string.IsNullOrEmpty(schoolYear))
                                {
                                    cboSchoolYear.Items.Add(schoolYear);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading school years: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCourses()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_course FROM mtbl_course ORDER BY cis_category, cis_course", mySqlConnection))
                    {
                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            cboCourse.Items.Clear();
                            while (mySqlDataReader.Read())
                            {
                                string course = mySqlDataReader["cis_course"].ToString();
                                if (!string.IsNullOrEmpty(course))
                                {
                                    cboCourse.Items.Add(course);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading courses: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboSchoolYear.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a School Year.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboSchoolYear.Focus();
                    return;
                }

                if (cboCourse.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Course.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboCourse.Focus();
                    return;
                }

                selectedSchoolYear = cboSchoolYear.Text;
                selectedCourse = cboCourse.Text;
                parametersSelected = true;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            parametersSelected = false;
            this.Close();
        }

        private void FrmGeneralAverageParams_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnCancel_Click(sender, e);
            }
        }
    }
}