using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmCurriculum : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        bool isCollapsedAdvancedSearch = true;
        bool isClicked_BtnAdvancedSearch;
        string cboCoursePrevText, cboSearchCoursePrevText, AddEditflag, CurriculumID, dgvCurriculumID;
        string prgsem, lecture, laboratory, testpaper, bsn, optno;
        int indexofspace, indexPrgsem;
        readonly FrmWaitFormFunc frmwaitformfunc = new FrmWaitFormFunc();

        public FrmCurriculum()
        {
            InitializeComponent();

            ClearFields();
            FillUpComboBox();
            GridFill();
            ActionButtons();
            AutoCompleteDescription();
        }

        private void BtnAddCurriculum_Click(object sender, EventArgs e)
        {
            AddEditflag = "NEW";
            CurriculumID = "0";
            
            FrmCurriculumNew frmCurriculumNew = new FrmCurriculumNew();
            frmCurriculumNew.ShowDialog();
            frmCurriculumNew.Dispose();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            FrmMain.rptName = "RptCurriculum";
            FrmReportViewer frmReportViewer = new FrmReportViewer();
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ClearFields();
            GridFill();
            ActionButtons();
        }

        private void ClearFields()
        {
            txtSearch.Text = " Search Course, Subject, or Description";
            txtCurriculumID.Text = txtCourseNo.Text = txtDesc.Text = txtCredits.Text = mskPrgsy.Text = txtRemarks.Text = txtOptNo.Text = "";

            lblCourseDesc.Text = lblMajorID.Text = "";
            cboMajorDesc.Items.Clear();

            cboCourse.SelectedIndex = cboPrgsem.SelectedIndex = cboYrlevel.SelectedIndex = -1;

            cboLecture.SelectedIndex = cboLaboratory.SelectedIndex = cboBsn.SelectedIndex = 0;
            cboTestPaper.SelectedIndex = 1;

            ClearDgvCurriculum();
            tssllastmodified.Text = "mm/dd/yyyy";
            lblaccountability.Text = "accountability";

            //panelAdvancedSearch
            mskSearchPrgsy.Text = "";
            cboSearchCourse.SelectedIndex = cboSearchPrgsem.SelectedIndex = cboSearchYrlevel.SelectedIndex = -1;
            cboSearchMajorDesc.Items.Clear();
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
                            cboSearchCourse.Items.Add(mySqlDataReader[0]);
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
                frmwaitformfunc.Show(this);

                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();

                MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                string SelectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, " +
                                                "cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno FROM reg_curriculum order by cis_course";
                mySqlDA.SelectCommand = new MySqlCommand(SelectRegCurriculum, mySqlConnection);

                DataTable table = new DataTable();
                mySqlDA.Fill(table);

                BindingSource bindingSource = new BindingSource
                {
                    DataSource = table
                };

                dgvCurriculum.DataSource = bindingSource;

                HeaderTexts();

                mySqlConnection.Close();

                frmwaitformfunc.Close();
            }
            catch (Exception ex)
            {
                frmwaitformfunc.Close();
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCurriculum_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void HeaderTexts()
        {
            dgvCurriculum.DefaultCellStyle.Font = new Font("Tahoma", 8);
            dgvCurriculum.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 8);

            dgvCurriculum.Columns[0].HeaderText = "Curriculum ID";
            dgvCurriculum.Columns[1].HeaderText = "Course";
            dgvCurriculum.Columns[2].HeaderText = "Subject";
            dgvCurriculum.Columns[3].HeaderText = "Description";
            dgvCurriculum.Columns[4].HeaderText = "Credits";
            dgvCurriculum.Columns[5].HeaderText = "Program SY";
            dgvCurriculum.Columns[6].HeaderText = "Semester";
            dgvCurriculum.Columns[7].HeaderText = "Yr Lvl";
            dgvCurriculum.Columns[8].HeaderText = "Major";
            dgvCurriculum.Columns[9].HeaderText = "with Lec";
            dgvCurriculum.Columns[10].HeaderText = "with Lab";
            dgvCurriculum.Columns[11].HeaderText = "Test Paper";
            dgvCurriculum.Columns[12].HeaderText = "BSN Subj";
            dgvCurriculum.Columns[13].HeaderText = "Opt Acct";
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
            dgvCurriculum.Columns.Add(CmdEdit);
            dgvCurriculum.Columns[14].Width = 58;


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
            dgvCurriculum.Columns.Add(CmdDelete);
            dgvCurriculum.Columns[15].Width = 58;
        }

        private void AutoCompleteDescription()
        {
            try
            {
                AutoCompleteStringCollection autoCollection = new AutoCompleteStringCollection();

                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_desc FROM reg_curriculum", mySqlConnection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                    while (mySqlDataReader.Read())
                    {
                        string description = mySqlDataReader["cis_desc"].ToString();
                        autoCollection.Add(description);
                    }
                }

                txtDesc.AutoCompleteCustomSource = autoCollection;
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

        private void BtnAdvancedSearch_Click(object sender, EventArgs e)
        {
            isClicked_BtnAdvancedSearch = true;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsedAdvancedSearch)
            {
                panelAdvancedSearch.Height += 180;
                if (panelAdvancedSearch.Size == panelAdvancedSearch.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsedAdvancedSearch = false;
                }
            }
            else
            {
                panelAdvancedSearch.Height -= 180;
                if (panelAdvancedSearch.Size == panelAdvancedSearch.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsedAdvancedSearch = true;
                }
            }
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" || txtSearch.Text == " Search Course, Subject, or Description") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = " Search Course, Subject, or Description"; 
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != " Search Course, Subject, or Description")
                {
                    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    string SelectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                                 "FROM reg_curriculum WHERE " +
                                                 "cis_course LIKE concat('%', '" + txtSearch.Text + "' ,'%') or " +
                                                 "cis_courseno LIKE concat('%', '" + txtSearch.Text + "' ,'%') or " +
                                                 "cis_desc LIKE concat('%', '" + txtSearch.Text + "' ,'%') order by cis_course";
                    mySqlDA.SelectCommand = new MySqlCommand(SelectRegCurriculum, mySqlConnection);

                    dgvCurriculum.DataSource = null;
                    dgvCurriculum.Rows.Clear();
                    dgvCurriculum.Columns.Clear();

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvCurriculum.DataSource = bindingSource;

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

        private void BtnAdvSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    string selectRegCurriculum;

                    //Semester = ALL AND YearLevel = ALL AND No Major Selected
                    if (cboSearchPrgsem.Text == "ALL" && cboSearchYrlevel.Text == "ALL" && cboSearchMajorDesc.Text == "")
                    {
                        selectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                              "FROM reg_curriculum WHERE " +
                                              "cis_prgsy = '" + mskSearchPrgsy.Text.Trim() + "' AND " +
                                              "cis_course = '" + cboSearchCourse.Text + "' order by cis_prgsy";
                    }
                    //Semester = ALL AND YearLevel = ALL AND Major Selected
                    else if (cboSearchPrgsem.Text == "ALL" && cboSearchYrlevel.Text == "ALL" && cboSearchMajorDesc.Text != "")
                    {
                        selectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                              "FROM reg_curriculum WHERE " +
                                              "cis_prgsy = '" + mskSearchPrgsy.Text.Trim() + "' AND " +
                                              "cis_course = '" + cboSearchCourse.Text + "' AND " +
                                              "cis_majordesc = '" + cboSearchMajorDesc.Text + "'  order by cis_prgsy";
                    }
                    //Semester = ALL AND YearLevel Selected  AND No Major Selected
                    else if (cboSearchPrgsem.Text == "ALL" && cboSearchYrlevel.Text != "ALL" && cboSearchMajorDesc.Text == "")
                    {
                        selectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                              "FROM reg_curriculum WHERE " +
                                              "cis_prgsy = '" + mskSearchPrgsy.Text.Trim() + "' AND " +
                                              "cis_course = '" + cboSearchCourse.Text + "' AND " +
                                              "cis_yrlevel = '" + cboSearchYrlevel.Text + "' order by cis_prgsy";
                    }
                    //Semester = ALL AND YearLevel Selected AND Major Selected
                    else if (cboSearchPrgsem.Text == "ALL" && cboSearchYrlevel.Text != "ALL" && cboSearchMajorDesc.Text != "")
                    {
                        selectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                              "FROM reg_curriculum WHERE " +
                                              "cis_prgsy = '" + mskSearchPrgsy.Text.Trim() + "' AND " +
                                              "cis_course = '" + cboSearchCourse.Text + "' AND " +
                                              "cis_yrlevel = '" + cboSearchYrlevel.Text + "' AND " +
                                              "cis_majordesc = '" + cboSearchMajorDesc.Text + "'  order by cis_prgsy";
                    }
                    //Semester selected AND YearLevel = ALL AND No Major Selected
                    else if (cboSearchPrgsem.Text != "ALL" && cboSearchYrlevel.Text == "ALL" && cboSearchMajorDesc.Text == "")
                    {
                        GetProgSem();

                        selectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                              "FROM reg_curriculum WHERE " +
                                              "cis_prgsy = '" + mskSearchPrgsy.Text.Trim() + "' AND " +
                                              "cis_course = '" + cboSearchCourse.Text + "' AND " +
                                              "cis_prgsem = '" + prgsem + "' order by cis_prgsy";
                    }
                    //Semester selected AND YearLevel = ALL AND Major Selected
                    else if (cboSearchPrgsem.Text != "ALL" && cboSearchYrlevel.Text == "ALL" && cboSearchMajorDesc.Text != "")
                    {
                        GetProgSem();

                        selectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                              "FROM reg_curriculum WHERE " +
                                              "cis_prgsy = '" + mskSearchPrgsy.Text.Trim() + "' AND " +
                                              "cis_course = '" + cboSearchCourse.Text + "' AND " +
                                              "cis_prgsem = '" + prgsem + "' AND " +
                                              "cis_majordesc = '" + cboSearchMajorDesc.Text + "'  order by cis_prgsy";
                    }
                    //Semester selected AND YearLevel selected AND No Major Selected
                    else if (cboSearchPrgsem.Text != "ALL" && cboSearchYrlevel.Text != "ALL" && cboSearchMajorDesc.Text == "")
                    {
                        GetProgSem();

                        selectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                              "FROM reg_curriculum WHERE " +
                                              "cis_prgsy = '" + mskSearchPrgsy.Text.Trim() + "' AND " +
                                              "cis_course = '" + cboSearchCourse.Text + "' AND " +
                                              "cis_prgsem = '" + prgsem + "' AND " +
                                              "cis_yrlevel = '" + cboSearchYrlevel.Text + "' order by cis_prgsy";
                    }
                    //Semester selected AND YearLevel selected AND Major Selected
                    else if (cboSearchPrgsem.Text != "ALL" && cboSearchYrlevel.Text != "ALL" && cboSearchMajorDesc.Text != "")
                    {
                        GetProgSem();

                        selectRegCurriculum = "SELECT id, cis_course, cis_courseno, cis_desc, cis_credits, cis_prgsy, cis_prgsem, cis_yrlevel, cis_majordesc, cis_lecture, cis_laboratory, cis_testpaper, cis_bsn, cis_optno " +
                                              "FROM reg_curriculum WHERE " +
                                              "cis_prgsy = '" + mskSearchPrgsy.Text.Trim() + "' AND " +
                                              "cis_course = '" + cboSearchCourse.Text + "' AND " +
                                              "cis_prgsem = '" + prgsem + "' AND " +
                                              "cis_yrlevel = '" + cboSearchYrlevel.Text + "' AND " +
                                              "cis_majordesc = '" + cboSearchMajorDesc.Text + "'  order by cis_prgsy";
                    }
                    else
                    {
                        selectRegCurriculum = "";
                    }


                    mySqlDA.SelectCommand = new MySqlCommand(selectRegCurriculum, mySqlConnection);

                    dgvCurriculum.DataSource = null;
                    dgvCurriculum.Rows.Clear();
                    dgvCurriculum.Columns.Clear();

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvCurriculum.DataSource = bindingSource;

                    HeaderTexts();
                    ActionButtons();
                }

                if (isClicked_BtnAdvancedSearch == true)
                {
                    timer1.Start();
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
            prgsem = cboSearchPrgsem.Text.ToUpper().Trim();
            if (prgsem != "")
            {
                indexofspace = prgsem.IndexOf(" ");
                prgsem = prgsem.Remove(indexofspace);
            }
        }

        private void Panel2_Click(object sender, EventArgs e)
        {
            if (isCollapsedAdvancedSearch == false)
                timer1.Start();
        }

        private void Panel3_Click(object sender, EventArgs e)
        {
            if (isCollapsedAdvancedSearch == false)
                timer1.Start();
        }

        private void Panel4_Click(object sender, EventArgs e)
        {
            if (isCollapsedAdvancedSearch == false)
                timer1.Start();
        }

        private void CboSearchCourse_Enter(object sender, EventArgs e)
        {
            cboSearchCoursePrevText = cboSearchCourse.Text;
        }

        private void CboSearchCourse_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cboSearchCoursePrevText != cboSearchCourse.Text)
                {
                    SearchPopulateSpecialization();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboSearchCourse_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboSearchCoursePrevText != cboSearchCourse.Text)
            {
                cboSearchMajorDesc.Items.Clear();
            }
        }

        private void SearchPopulateSpecialization()
        {
            //****************************************************************
            //POPULATE SPECIALIZATION (MAJOR) in search box
            //****************************************************************
            try
            {   
                cboSearchMajorDesc.Items.Clear();

                using (MySqlConnection mySqlCon1 = new MySqlConnection(connectionString))
                {
                    mySqlCon1.Open();
                    using (MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT cis_majordesc FROM mtbl_coursemajor WHERE cis_course = '" + cboSearchCourse.Text + "' order by cis_majordesc", mySqlCon1))
                    {
                        MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

                        if (mySqlDataReader1.HasRows)
                        {
                            while (mySqlDataReader1.Read())
                            {
                                cboSearchMajorDesc.Items.Add(mySqlDataReader1["cis_majordesc"]);
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
            //****************************************************************
            //POPULATE SPECIALIZATION (MAJOR)
            //****************************************************************
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboMajorDesc_TextChanged(object sender, EventArgs e)
        {
            if (cboMajorDesc.Text.Trim() == "")
            {
                lblMajorID.Text = "";
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
                                lblMajorID.Text = mySqlDataReader["cis_majorid"].ToString();
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

        private void TxtCredits_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////only allow numeric, decimal point, negative
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

        private void TxtPrgsy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one hyphen
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        private void BtnOptNo_Click(object sender, EventArgs e)
        {
            FrmSearchOptionalAcct frmSearchOptionalAcct = new FrmSearchOptionalAcct();
            frmSearchOptionalAcct.ShowDialog();

            if (frmSearchOptionalAcct.selectButtonClicked == true)
            {
                txtOptNo.Text = FrmSearchOptionalAcct.selectedOptNo;
            }

            frmSearchOptionalAcct.Dispose();
        }

        private void BtnClearOpt_Click(object sender, EventArgs e)
        {
            txtOptNo.Text = "";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    //****************************************************************************************
                    //Check cis_course, cis_majorid, and cis_prgsy if already exist, to eliminate double entry
                    //****************************************************************************************

                    //Removes the space and succeeding texts.
                    prgsem = cboPrgsem.Text.ToUpper().Trim();
                    if (prgsem != "")
                    {
                        indexofspace = prgsem.IndexOf(" ");
                        prgsem = prgsem.Remove(indexofspace);
                    }

                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_course, cis_majorid, cis_courseno, cis_desc, cis_prgsy FROM reg_curriculum WHERE " +
                                                                 "cis_course = '" + cboCourse.Text + "' and " +
                                                                 "cis_majorid = '" + lblMajorID.Text + "' and " +
                                                                 "cis_courseno = '" + txtCourseNo.Text.Trim() + "' and " +
                                                                 "cis_desc = '" + txtDesc.Text.Trim().Replace("'", "''") + "' and " +
                                                                 "cis_prgsy = '" + mskPrgsy.Text.Trim() + "'", mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (txtCurriculumID.Text.Trim() == "")
                        {
                            MessageBox.Show("Click the Add New button to enter new Curriculum.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCurriculumID.Focus();
                        }
                        else if (cboCourse.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select a Course.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cboCourse.Focus();
                        }
                        //--- Commenting : Enable to Save even without Major ---
                        //else if (MajorDescCount > 0 && cboMajorDesc.SelectedIndex == -1)
                        //{
                        //    MessageBox.Show("Select a Major", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    cboMajorDesc.Focus();
                        //}
                        else if (txtCourseNo.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter a Course No (Subject).", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCourseNo.Focus();
                        }
                        else if (txtDesc.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter Description.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDesc.Focus();
                        }
                        else if (txtCredits.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter Credits.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCredits.Focus();
                        }
                        else if (mskPrgsy.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter Program School Year.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskPrgsy.Focus();
                        }
                        else if (cboPrgsem.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select a Semester.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cboPrgsem.Focus();
                        }
                        else if (cboYrlevel.SelectedIndex == -1)
                        {
                            MessageBox.Show("Select a Year Level.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cboYrlevel.Focus();
                        }
                        /*check for double entry*/
                        else if (mySqlDataReader.HasRows && AddEditflag == "NEW")
                        {
                            if (mySqlDataReader.Read())
                            {
                                MessageBox.Show("Can't save the current record. The system detected that Course, Major, Course No., Description and Program School Year already exist.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                cboCourse.Focus();
                            }
                        }
                        else
                        {
                            if (txtOptNo.Text.Trim() == "")
                            {
                                optno = "";
                            }
                            else
                            {
                                optno = txtOptNo.Text.ToUpper().Trim();
                                indexofspace = optno.IndexOf(" ");
                                optno = optno.Remove(indexofspace);
                            }

                            if (cboLecture.Text == "NO") lecture = "0"; else lecture = "1";
                            if (cboLaboratory.Text == "NO") laboratory = "0"; else laboratory = "1";
                            if (cboTestPaper.Text == "NO") testpaper = "0"; else testpaper = "1";
                            if (cboBsn.Text == "NO") bsn = "0"; else bsn = "1";

                            using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                            {
                                mySqlConnection1.Open();
                                using (MySqlCommand mySqlCommand1 = new MySqlCommand("reg_curriculum_addedit", mySqlConnection1))
                                {
                                    mySqlCommand1.CommandType = CommandType.StoredProcedure;

                                    mySqlCommand1.Parameters.AddWithValue("_id", CurriculumID);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_course", cboCourse.Text.ToUpper().Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_courseno", txtCourseNo.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_desc", txtDesc.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_credits", txtCredits.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_prgsy", mskPrgsy.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_prgsem", prgsem);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_yrlevel", cboYrlevel.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_coursenoid", CurriculumID);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_majorid", String.IsNullOrEmpty(lblMajorID.Text) ? DBNull.Value : (object)lblMajorID.Text);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_majordesc", String.IsNullOrEmpty(lblMajorID.Text) ? DBNull.Value : (object)cboMajorDesc.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_lecture", lecture);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_laboratory", laboratory);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_testpaper", testpaper);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_bsn", bsn);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_optno", String.IsNullOrEmpty(optno) ? DBNull.Value : (object)optno);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_remarks", txtRemarks.Text.Trim());
                                    mySqlCommand1.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                    mySqlCommand1.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);

                                    int isSaved = mySqlCommand1.ExecuteNonQuery();
                                    if (isSaved > 0)
                                        MessageBox.Show("Curriculum record saved successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("Unable to save Curriculum record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            //********************************************************************************************
                            // Get the latest curriculum id if NEW record, ready for updating the lastest inserted record.
                            //********************************************************************************************
                            if (AddEditflag == "NEW")
                            {
                                using (MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString))
                                {
                                    mySqlConnection2.Open();

                                    string selectRegCurriculum;
                                    if (lblMajorID.Text == "")
                                    {
                                        selectRegCurriculum = "SELECT id, cis_course, cis_majorid, cis_courseno, cis_desc, cis_prgsy, cis_lastmodified, cis_accountability FROM reg_curriculum WHERE " +
                                                                "cis_course = '" + cboCourse.Text + "' and " +
                                                                "cis_courseno = '" + txtCourseNo.Text.Trim() + "' and " +
                                                                "cis_desc = '" + txtDesc.Text.Trim().Replace("'", "''") + "' and " +
                                                                "cis_prgsy = '" + mskPrgsy.Text.Trim() + "' and " +
                                                                "cis_prgsem = '" + prgsem + "' and " +
                                                                "cis_yrlevel = '" + cboYrlevel.Text.Trim() + "'";
                                    }
                                    else
                                    {
                                        selectRegCurriculum = "SELECT id, cis_course, cis_majorid, cis_courseno, cis_desc, cis_prgsy, cis_lastmodified, cis_accountability FROM reg_curriculum WHERE " +
                                                                "cis_course = '" + cboCourse.Text + "' and " +
                                                                "cis_courseno = '" + txtCourseNo.Text.Trim() + "' and " +
                                                                "cis_desc = '" + txtDesc.Text.Trim().Replace("'", "''") + "' and " +
                                                                "cis_prgsy = '" + mskPrgsy.Text.Trim() + "' and " +
                                                                "cis_prgsem = '" + prgsem + "' and " +
                                                                "cis_yrlevel = '" + cboYrlevel.Text.Trim() + "' and " +
                                                                "cis_majorid = '" + lblMajorID.Text + "'";
                                    }

                                    using (MySqlCommand mySqlCommand2 = new MySqlCommand(selectRegCurriculum, mySqlConnection2))
                                    {
                                        MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

                                        if (mySqlDataReader2.HasRows)
                                        {
                                            while (mySqlDataReader2.Read())
                                            {
                                                txtCurriculumID.Text = mySqlDataReader2.GetValue(0).ToString();
                                                CurriculumID = txtCurriculumID.Text;

                                                tssllastmodified.Text = mySqlDataReader2["cis_lastmodified"].ToString();
                                                lblaccountability.Text = mySqlDataReader2["cis_accountability"].ToString();
                                            }

                                            //*********************************************
                                            //update reg_curriculum set cis_coursenoid = id
                                            //*********************************************
                                            using (MySqlConnection mySqlConnection3 = new MySqlConnection(connectionString))
                                            {
                                                mySqlConnection3.Open();

                                                string UpdateRegCurriculum;

                                                if (lblMajorID.Text == "")
                                                {
                                                    UpdateRegCurriculum = "UPDATE reg_curriculum SET cis_coursenoid = @cis_coursenoid WHERE " +
                                                                            "cis_course = '" + cboCourse.Text + "' and " +
                                                                            "cis_courseno = '" + txtCourseNo.Text.ToUpper().Trim() + "' and " +
                                                                            "cis_desc = '" + txtDesc.Text.Trim().Replace("'", "''") + "' and " +
                                                                            "cis_prgsy = '" + mskPrgsy.Text.Trim() + "'";
                                                }
                                                else 
                                                {
                                                    UpdateRegCurriculum = "UPDATE reg_curriculum SET cis_coursenoid = @cis_coursenoid WHERE " +
                                                                            "cis_course = '" + cboCourse.Text + "' and " +
                                                                            "cis_courseno = '" + txtCourseNo.Text.ToUpper().Trim() + "' and " +
                                                                            "cis_desc = '" + txtDesc.Text.Trim().Replace("'", "''") + "' and " +
                                                                            "cis_prgsy = '" + mskPrgsy.Text.Trim() + "' and " +
                                                                            "cis_majorid = '" + lblMajorID.Text + "'";
                                                }

                                                using (MySqlCommand mySqlCommand3 = new MySqlCommand(UpdateRegCurriculum, mySqlConnection3))
                                                {
                                                    mySqlCommand3.Parameters.AddWithValue("@cis_coursenoid", CurriculumID);
                                                    mySqlCommand3.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                        AddEditflag = "EDIT";
                                    }
                                }
                            }

                            //***************************************************
                            //Repaint dgvCurriculum according to Search Criteria
                            //***************************************************
                            if (txtSearch.Text != " Search Course, Subject, or Description")
                            {
                                TxtSearch_TextChanged(null, null);
                            }
                            else
                            {
                                ClearDgvCurriculum();

                                isClicked_BtnAdvancedSearch = false;
                                BtnAdvSearch_Click(null, null);
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

        private void DgvCurriculum_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //****************************************************************************
                //edit button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvCurriculum.Columns["btnEdit"].Index)
                {
                    AddEditflag = "EDIT";
                    CurriculumID = dgvCurriculumID = Convert.ToString(dgvCurriculum.Rows[e.RowIndex].Cells["id"].Value);

                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM reg_curriculum where id = '" + dgvCurriculumID + "'", mySqlConnection))
                        {
                            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                            if (mySqlDataReader.HasRows)
                            {
                                if (mySqlDataReader.Read())
                                {
                                    txtCurriculumID.Text = mySqlDataReader["id"].ToString();
                                    cboCourse.Text = mySqlDataReader["cis_course"].ToString();

                                    PopulateSpecialization();
                                    lblMajorID.Text = mySqlDataReader["cis_majorid"].ToString();
                                    cboMajorDesc.Text = mySqlDataReader["cis_majordesc"].ToString();

                                    txtCourseNo.Text = mySqlDataReader["cis_courseno"].ToString();
                                    txtDesc.Text = mySqlDataReader["cis_desc"].ToString();
                                    txtCredits.Text = string.Format("{0:0.00}", mySqlDataReader["cis_credits"]);
                                    mskPrgsy.Text = mySqlDataReader["cis_prgsy"].ToString();
                                    cboYrlevel.Text = mySqlDataReader["cis_yrlevel"].ToString();
                                    txtRemarks.Text = mySqlDataReader["cis_remarks"].ToString();

                                    prgsem = mySqlDataReader["cis_prgsem"].ToString();
                                    if (prgsem == "" || prgsem == "0")
                                        cboPrgsem.SelectedIndex = -1;
                                    else
                                    {
                                        indexPrgsem = cboPrgsem.FindString(prgsem);
                                        cboPrgsem.SelectedIndex = indexPrgsem;
                                    }

                                    lecture = mySqlDataReader["cis_lecture"].ToString();
                                    laboratory = mySqlDataReader["cis_laboratory"].ToString();
                                    testpaper = mySqlDataReader["cis_testpaper"].ToString();
                                    bsn = mySqlDataReader["cis_bsn"].ToString();

                                    if (lecture == "0") cboLecture.Text = "NO"; else if (lecture == "1") cboLecture.Text = "YES";
                                    if (laboratory == "0") cboLaboratory.Text = "NO"; else if (laboratory == "1") cboLaboratory.Text = "YES";
                                    if (testpaper == "0") cboTestPaper.Text = "NO"; else if (testpaper == "1") cboTestPaper.Text = "YES";
                                    if (bsn == "0") cboBsn.Text = "NO"; else if (bsn == "1") cboBsn.Text = "YES";

                                    optno = mySqlDataReader["cis_optno"].ToString();

                                    if (optno == "")
                                        txtOptNo.Text = "";
                                    else
                                    {
                                        using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                                        {
                                            mySqlConnection1.Open();
                                            using (MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT cis_acctitle, cis_optno FROM acc_optionalacct where cis_optno = '" + optno + "'", mySqlConnection1))
                                            {
                                                MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

                                                if (mySqlDataReader1.HasRows)
                                                {
                                                    if (mySqlDataReader1.Read())
                                                    {
                                                        txtOptNo.Text = mySqlDataReader1["cis_optno"].ToString() + " - " + mySqlDataReader1["cis_acctitle"].ToString();
                                                    }
                                                }
                                            }
                                        }
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
                        }
                    }
                }

                //****************************************************************************
                //delete button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvCurriculum.Columns["btnDelete"].Index)
                {
                    dgvCurriculumID = Convert.ToString(dgvCurriculum.Rows[e.RowIndex].Cells["id"].Value);

                    DialogResult dialogresult = MessageBox.Show("Are you sure you want to Delete this record?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogresult == DialogResult.Yes)
                    {
                        //**********************************************************************************************
                        //Check cis_coursenoid from reg_subjenrolled table. If it exists, curriculum record cant be deleted.
                        //**********************************************************************************************
                        using (MySqlConnection mySqlConnection0 = new MySqlConnection(connectionString))
                        {
                            mySqlConnection0.Open();
                            using (MySqlCommand mySqlCommand0 = new MySqlCommand("SELECT cis_coursenoid FROM reg_subjenrolled WHERE cis_coursenoid = '" + dgvCurriculumID + "'", mySqlConnection0))
                            {
                                MySqlDataReader mySqlDataReader0 = mySqlCommand0.ExecuteReader();

                                if (mySqlDataReader0.HasRows)
                                {
                                    MessageBox.Show("Unable to Delete Curriculum. Curriculum record is already active.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                                    {
                                        mySqlConnection1.Open();
                                        using (MySqlCommand mySqlCommand1 = new MySqlCommand("DELETE FROM reg_curriculum WHERE id = '" + dgvCurriculumID + "'", mySqlConnection1))
                                        {
                                            int isDeleted = mySqlCommand1.ExecuteNonQuery();
                                            if (isDeleted > 0)
                                            {
                                                //***************************************************
                                                //Repaint dgvCurriculum according to Search Criteria
                                                //***************************************************
                                                if (txtSearch.Text != " Search Course, Subject, or Description")
                                                {
                                                    TxtSearch_TextChanged(null, null);

                                                    txtCurriculumID.Text = txtCourseNo.Text = txtDesc.Text = txtCredits.Text = mskPrgsy.Text = txtRemarks.Text = txtOptNo.Text = "";
                                                    lblCourseDesc.Text = lblMajorID.Text = "";
                                                    cboMajorDesc.Items.Clear();
                                                    cboCourse.SelectedIndex = cboPrgsem.SelectedIndex = cboYrlevel.SelectedIndex = -1;
                                                    cboLecture.SelectedIndex = cboLaboratory.SelectedIndex = cboTestPaper.SelectedIndex = cboBsn.SelectedIndex = 0;
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
                                                MessageBox.Show("Unable to Delete Curriculum record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}