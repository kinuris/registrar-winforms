using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmCurriculumNew : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string cboCoursePrevText, prgsem;
        int indexofspace;

        public FrmCurriculumNew()
        {
            InitializeComponent();

            ClearFields();
            FillUpComboBox();
        }

        private void ClearFields()
        {
            mskPrgsy.Text = "";

            lblCourseDesc.Text = lblMajorID.Text = "";
            cboMajorDesc.Items.Clear();

            cboCourse.SelectedIndex = cboPrgsem.SelectedIndex = cboYrlevel.SelectedIndex = -1;
            
            ClearDgvCurriculum();
            //tssllastmodified.Text = "mm/dd/yyyy";
            //lblaccountability.Text = "accountability";
        }
        private void ClearDgvCurriculum()
        {
            dgvCurriculum.DataSource = null;
            dgvCurriculum.Rows.Clear();
            dgvCurriculum.Columns.Clear();
            dgvCurriculum.Refresh();
        }

        private void FillUpComboBox()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_course FROM mtbl_course ORDER BY cis_category, cis_course", mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                        while (mySqlDataReader.Read())
                        {
                            cboCourse.Items.Add(mySqlDataReader[0]);                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboPrgsem_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCourseDesc.Text = lblMajorID.Text = "";
            cboMajorDesc.Items.Clear();

            cboCourse.SelectedIndex = -1;
        }

        private void CboYrlevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCourseDesc.Text = lblMajorID.Text = "";
            cboMajorDesc.Items.Clear();

            cboCourse.SelectedIndex = -1;
        }

        private void CboCourse_Enter(object sender, EventArgs e)
        {
            cboCoursePrevText = cboCourse.Text;
        }

        private void CboCourse_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cboCoursePrevText != cboCourse.Text)
                {
                    PopulateSpecialization();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboCourse_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ClearDgvCurriculum();

                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_coursedesc FROM mtbl_course WHERE cis_course = '" + cboCourse.Text + "'", mySqlCon))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (mySqlDataReader.HasRows)
                        {
                            if (mySqlDataReader.Read())
                            {
                                lblCourseDesc.Text = mySqlDataReader["cis_coursedesc"].ToString();

                                if (cboCoursePrevText != cboCourse.Text)
                                {
                                    cboMajorDesc.Items.Clear();
                                    lblMajorID.Text = "";
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

        private void PopulateSpecialization()
        {
            //POPULATE SPECIALIZATION (MAJOR)
            try
            {
                cboMajorDesc.Items.Clear();
                lblMajorID.Text = "";

                using (MySqlConnection mySqlCon1 = new MySqlConnection(connectionString))
                {
                    mySqlCon1.Open();
                    using (MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT cis_majordesc FROM mtbl_coursemajor WHERE cis_course = '" + cboCourse.Text + "' order by cis_majordesc", mySqlCon1))
                    {
                        MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

                        if (mySqlDataReader1.HasRows)
                        {
                            while (mySqlDataReader1.Read())
                            {
                                cboMajorDesc.Items.Add(mySqlDataReader1["cis_majordesc"]);
                            }

                            if (mskPrgsy.Text != "" && cboPrgsem.Text != "" && cboYrlevel.Text != "" && cboCourse.Text != "")
                                GridFill();
                        }
                        else
                        {
                            //if there's no Major, and ProgramSY, Sem, YearLvl, and Course were selected. 
                            //and after leaving the cboCourse combobox, fill up dgvCurriculum
                            if (mskPrgsy.Text != "" && cboPrgsem.Text != "" && cboYrlevel.Text != "" && cboCourse.Text != "")
                                GridFill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboMajorDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_majorid FROM mtbl_coursemajor WHERE cis_course = '" + cboCourse.Text.Trim() + "' and cis_majordesc = '" + cboMajorDesc.Text.Trim() + "'", mySqlCon))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (mySqlDataReader.HasRows)
                        {
                            if (mySqlDataReader.Read())
                            {
                                lblMajorID.Text = mySqlDataReader["cis_majorid"].ToString();
                            }

                            // if Major and ProgramSY, Sem, YearLvl, and Course were selected, fill up dgvCurriculum
                            if (mskPrgsy.Text != "" && cboPrgsem.Text != "" && cboYrlevel.Text != "" && cboCourse.Text != "")
                                GridFill();
                        }
                        else
                        {
                            lblMajorID.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetProgSem()
        {
            //Removes the space and succeeding texts.
            prgsem = cboPrgsem.Text.ToUpper().Trim();
            if (prgsem != "")
            {
                indexofspace = prgsem.IndexOf(" ");
                prgsem = prgsem.Remove(indexofspace);
            }
        }

        private void GridFill()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    string SelectRegCurriculum;

                    // If without Major
                    if (cboMajorDesc.Text == "")    
                    {
                        GetProgSem();

                        SelectRegCurriculum = "SELECT id, cis_courseno, cis_desc, cis_credits, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn " +
                                                 "FROM reg_curriculum WHERE cis_prgsy = '" + mskPrgsy.Text + "' AND " +
                                                 "cis_prgsem = '" + prgsem + "' AND " +
                                                 "cis_yrlevel = '" + cboYrlevel.Text + "' AND " +
                                                 "cis_course = '" + cboCourse.Text + "'";
                    }
                    // else with Major
                    else
                    {
                        GetProgSem();

                        SelectRegCurriculum = "SELECT id, cis_courseno, cis_desc, cis_credits, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn " +
                                                 "FROM reg_curriculum WHERE cis_prgsy = '" + mskPrgsy.Text + "' AND " +
                                                 "cis_prgsem = '" + prgsem + "' AND " +
                                                 "cis_yrlevel = '" + cboYrlevel.Text + "' AND " +
                                                 "cis_course = '" + cboCourse.Text + "' AND " +
                                                 "cis_majordesc = '" + cboMajorDesc.Text + "' order by cis_prgsy";
                    }

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter(SelectRegCurriculum, mySqlConnection);                    

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);
                    dgvCurriculum.DataSource = table;

                    //Hide curriculum ID
                    dgvCurriculum.Columns[0].Visible = false;
                    dgvCurriculum.Columns[0].ReadOnly = true;

                    //Column Header Texts
                    dgvCurriculum.Columns[0].HeaderText = "Curriculum ID";
                    dgvCurriculum.Columns[1].HeaderText = "Course No. (Subject)";

                    dgvCurriculum.Columns[2].HeaderText = "Description";
                    dgvCurriculum.Columns[2].Width = 450;

                    dgvCurriculum.Columns[3].HeaderText = "Credits";
                    dgvCurriculum.Columns[4].HeaderText = "w/ Lec?";
                    dgvCurriculum.Columns[5].HeaderText = "w/ Lab?";
                    dgvCurriculum.Columns[6].HeaderText = "w/ Test Paper?";
                    dgvCurriculum.Columns[7].HeaderText = "BSN Subject?";

                    ((DataGridViewTextBoxColumn)dgvCurriculum.Columns[3]).MaxInputLength = 5;
                    ((DataGridViewTextBoxColumn)dgvCurriculum.Columns[4]).MaxInputLength = 1;
                    ((DataGridViewTextBoxColumn)dgvCurriculum.Columns[5]).MaxInputLength = 1;
                    ((DataGridViewTextBoxColumn)dgvCurriculum.Columns[6]).MaxInputLength = 1;
                    ((DataGridViewTextBoxColumn)dgvCurriculum.Columns[7]).MaxInputLength = 1;
                }
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCurriculum_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try 
            { 
                if (dgvCurriculum.CurrentRow != null)
                {
                    using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                    {
                        mySqlConnection1.Open();
                        DataGridViewRow dataGridViewRow = dgvCurriculum.CurrentRow;

                        using (MySqlCommand mySqlCommand1 = new MySqlCommand("reg_curriculum_addedit", mySqlConnection1))
                        {
                            mySqlCommand1.CommandType = CommandType.StoredProcedure;

                            //Insert
                            if (dataGridViewRow.Cells["id"].Value == DBNull.Value)
                            {
                                mySqlCommand1.Parameters.AddWithValue("_id", 0);
                                mySqlCommand1.Parameters.AddWithValue("_cis_coursenoid", 0);
                            }
                            //Update
                            else
                            {
                                mySqlCommand1.Parameters.AddWithValue("_id", Convert.ToInt32(dataGridViewRow.Cells["id"].Value));
                                mySqlCommand1.Parameters.AddWithValue("_cis_coursenoid", Convert.ToInt32(dataGridViewRow.Cells["id"].Value));
                            }

                            mySqlCommand1.Parameters.AddWithValue("_cis_course", cboCourse.Text.ToUpper().Trim());
                            mySqlCommand1.Parameters.AddWithValue("_cis_courseno", dataGridViewRow.Cells["cis_courseno"].Value == DBNull.Value ? null : dataGridViewRow.Cells["cis_courseno"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_desc", dataGridViewRow.Cells["cis_desc"].Value == DBNull.Value ? null : dataGridViewRow.Cells["cis_desc"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_credits", dataGridViewRow.Cells["cis_credits"].Value == DBNull.Value ? null : dataGridViewRow.Cells["cis_credits"].Value.ToString()); 
                            mySqlCommand1.Parameters.AddWithValue("_cis_prgsy", mskPrgsy.Text.Trim());
                            mySqlCommand1.Parameters.AddWithValue("_cis_prgsem", prgsem);
                            mySqlCommand1.Parameters.AddWithValue("_cis_yrlevel", cboYrlevel.Text.Trim());                        
                            mySqlCommand1.Parameters.AddWithValue("_cis_majorid", String.IsNullOrEmpty(lblMajorID.Text) ? DBNull.Value : (object)lblMajorID.Text);
                            mySqlCommand1.Parameters.AddWithValue("_cis_majordesc", String.IsNullOrEmpty(lblMajorID.Text) ? DBNull.Value : (object)cboMajorDesc.Text.ToUpper().Trim());
                            mySqlCommand1.Parameters.AddWithValue("_cis_lecture", dataGridViewRow.Cells["cis_lecture"].Value == DBNull.Value ? "0" : dataGridViewRow.Cells["cis_lecture"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_laboratory", dataGridViewRow.Cells["cis_laboratory"].Value == DBNull.Value ? "0" : dataGridViewRow.Cells["cis_laboratory"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_testpaper", dataGridViewRow.Cells["cis_testpaper"].Value == DBNull.Value ? "1" : dataGridViewRow.Cells["cis_testpaper"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_bsn", dataGridViewRow.Cells["cis_bsn"].Value == DBNull.Value ? "0" : dataGridViewRow.Cells["cis_bsn"].Value.ToString());
                            mySqlCommand1.Parameters.AddWithValue("_cis_optno", DBNull.Value);
                            mySqlCommand1.Parameters.AddWithValue("_cis_remarks", DBNull.Value);
                            mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                            mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);
                            mySqlCommand1.ExecuteNonQuery();

                            GridFill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCurriculum_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= AllowNumbersOnly;
            e.Control.KeyPress -= AllowZeroOneOnly;

            if (dgvCurriculum.CurrentCell.ColumnIndex == 3)
            {   
                e.Control.KeyPress += AllowNumbersOnly;
            }
            else if (dgvCurriculum.CurrentCell.ColumnIndex == 4 || dgvCurriculum.CurrentCell.ColumnIndex == 5 || dgvCurriculum.CurrentCell.ColumnIndex == 6 || dgvCurriculum.CurrentCell.ColumnIndex == 7)
            {
                e.Control.KeyPress += AllowZeroOneOnly;
            }
        }

        private void AllowNumbersOnly(object sender, KeyPressEventArgs e)
        {
            //only allow numeric, decimal point, negative
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            // only allow one negative
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        private void AllowZeroOneOnly(object sender, KeyPressEventArgs e)
        {
            //only allow numeric, decimal point, negative
            if (!char.IsControl(e.KeyChar) && e.KeyChar != '0' && e.KeyChar != '1')
            {
                e.Handled = true;
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


        //private void DgvCurriculum_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (dgvCurriculum.Columns[e.ColumnIndex].Index == 2)
        //    {
        //        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    }
        //}        
    }
}