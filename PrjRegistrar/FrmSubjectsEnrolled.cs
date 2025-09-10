using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmSubjectsEnrolled : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string cboSchlyrPrevText, cboSemesterPrevText;

        public FrmSubjectsEnrolled()
        {
            InitializeComponent();
        }

        private void FrmSubjectsEnrolled_Load(object sender, EventArgs e)
        {
            try 
            { 
                lblStudNo.Text = FrmPersonalData.Studno;
                lblstudfullname.Text = FrmPersonalData.StudFullname;

                //POPULATE SCHOOL YEAR
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                { 
                    mySqlCon.Open();

                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_schlyr FROM reg_subjenrolled WHERE cis_studno = '" + lblStudNo.Text + "' order by cis_schlyr", mySqlCon))
                    { 
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (mySqlDataReader.HasRows)
                        {
                            while (mySqlDataReader.Read())
                            {
                                cboSchlyr.Items.Add(mySqlDataReader["cis_schlyr"]);
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

        private void CboSchlyr_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboSchlyrPrevText != cboSchlyr.Text)
            {
                cboSemester.Items.Clear();
                lblYearLevel.Text = "";
                lblTotalCredits.Text = "-";

                ClearDgvSubjEnrolled();
            }
        }

        private void CboSchlyr_Enter(object sender, EventArgs e)
        {
            cboSchlyrPrevText = cboSchlyr.Text;
        }

        private void CboSchlyr_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cboSchlyrPrevText != cboSchlyr.Text)
                {
                    //POPULATE SEMESTER
                    using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                    {
                        mySqlCon.Open();
                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_semester FROM reg_subjenrolled WHERE cis_studno = '" + lblStudNo.Text + "' and cis_schlyr = '" + cboSchlyr.Text + "'", mySqlCon))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                while (mySqlDataReader.Read())
                                {
                                    cboSemester.Items.Add(mySqlDataReader["cis_semester"]);
                                }
                            }
                        }
                    }

                    //POPULATE Year Level
                    using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                    {
                        mySqlCon.Open();
                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_yrlevel FROM reg_subjenrolled WHERE cis_studno = '" + lblStudNo.Text + "' and cis_schlyr = '" + cboSchlyr.Text + "'", mySqlCon))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    lblYearLevel.Text = mySqlDataReader["cis_yrlevel"].ToString();
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

        private void CboSemester_Enter(object sender, EventArgs e)
        {
            cboSemesterPrevText = cboSemester.Text;
        }

        private void CboSemester_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboSemesterPrevText != cboSemester.Text)
            {
                ClearDgvSubjEnrolled();
                GridFill();
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            cboSchlyr.SelectedIndex = -1;
            cboSemester.Items.Clear();
            lblYearLevel.Text = "-";
            lblTotalCredits.Text = "-";

            ClearDgvSubjEnrolled();
        }

        private void ClearDgvSubjEnrolled()
        {
            dgvSubjEnrolled.DataSource = null;
            dgvSubjEnrolled.Rows.Clear();
            dgvSubjEnrolled.Columns.Clear();
            dgvSubjEnrolled.Refresh();
        }

        private void DgvSubjEnrolled_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSubjEnrolled.Columns[e.ColumnIndex].Index == 2)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void GridFill()
        {
            try
            {
                //Gets the Subjects Enrolled and its Credit
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    string selectRegSubjenrolled = "SELECT cis_courseno, cis_desc, cis_credits " +
                                                   "FROM reg_subjenrolled WHERE cis_studno = '" + lblStudNo.Text + "' and cis_schlyr = '" + cboSchlyr.Text + "' and cis_semester = '" + cboSemester.Text + "'";

                    mySqlDA.SelectCommand = new MySqlCommand(selectRegSubjenrolled, mySqlConnection);

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvSubjEnrolled.DataSource = bindingSource;

                    //HeaderTexts
                    dgvSubjEnrolled.DefaultCellStyle.Font = new Font("Tahoma", 9);
                    dgvSubjEnrolled.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);

                    dgvSubjEnrolled.Columns[0].HeaderText = "Course No";
                    dgvSubjEnrolled.Columns[1].HeaderText = "Course Description";
                    dgvSubjEnrolled.Columns[2].HeaderText = "Credit";

                    dgvSubjEnrolled.Columns[0].Width = 110;
                    dgvSubjEnrolled.Columns[1].Width = 315;
                    dgvSubjEnrolled.Columns[2].Width = 90;
                }

                //Gets the Total Sum of Credits
                using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                {
                    mySqlConnection1.Open();
                    using (MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT SUM(cis_credits) AS sum_credits FROM reg_subjenrolled WHERE cis_studno = '" + lblStudNo.Text + "' and cis_schlyr = '" + cboSchlyr.Text + "' and cis_semester = '" + cboSemester.Text + "'", mySqlConnection1))
                    { 
                        MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

                        if (mySqlDataReader1.HasRows)
                        {
                            if (mySqlDataReader1.Read())
                            {
                                lblTotalCredits.Text = mySqlDataReader1["sum_credits"].ToString();
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