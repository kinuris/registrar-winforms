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
        public static string selectedYearLevel = "";
        public static int selectedTopStudents = 0; // 0 means all students
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
                LoadYearLevels();
                
                // Set default value for top students (0 = all students)
                numTopStudents.Value = 0;
                numTopStudents.Minimum = 0;
                numTopStudents.Maximum = 1000;
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

        private void LoadYearLevels()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_yrlevel FROM mtbl_studprofile WHERE cis_yrlevel IS NOT NULL AND cis_yrlevel != '' ORDER BY cis_yrlevel", mySqlConnection))
                    {
                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
                            cboYearLevel.Items.Clear();
                            cboYearLevel.Items.Add("All Year Levels"); // Add option for all year levels
                            
                            while (mySqlDataReader.Read())
                            {
                                string yearLevel = mySqlDataReader["cis_yrlevel"].ToString();
                                if (!string.IsNullOrEmpty(yearLevel))
                                {
                                    cboYearLevel.Items.Add(yearLevel);
                                }
                            }
                            
                            // Set default selection to "All Year Levels"
                            if (cboYearLevel.Items.Count > 0)
                            {
                                cboYearLevel.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading year levels: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (cboYearLevel.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a Year Level.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboYearLevel.Focus();
                    return;
                }

                selectedSchoolYear = cboSchoolYear.Text;
                selectedCourse = cboCourse.Text;
                selectedYearLevel = cboYearLevel.Text;
                selectedTopStudents = (int)numTopStudents.Value;
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