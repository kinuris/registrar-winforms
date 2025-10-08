using System;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PrjRegistrar
{
    public partial class FrmMain : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        private Form activeForm = null;
        private bool isCollapsedRecord = true;
        private bool isCollapsedReports = true;
        private bool isCollapsedTables = true;
        private bool isCollapsedUtility = true;
        private bool isCollapsedSignOut = true;
        private bool CompletionAuthorized = false;
        public static string rptName, paramrptUserDesignation, paramrptUserFullname, studFrmMainlastname, studFrmMainfirstname, studFrmMainmidname, studFrmMainFCUIDno;

        public FrmMain()
        {
            InitializeComponent();

            btnRptGenAverage.Click += (sender, e) =>
            {
                try
                {
                    // Show parameter selection form
                    FrmGeneralAverageParams frmParams = new FrmGeneralAverageParams();
                    frmParams.ShowDialog();

                    if (frmParams.parametersSelected)
                    {
                        // Get selected parameters
                        string schoolYear = FrmGeneralAverageParams.selectedSchoolYear;
                        string course = FrmGeneralAverageParams.selectedCourse;
                        string yearLevel = FrmGeneralAverageParams.selectedYearLevel;
                        int topStudents = FrmGeneralAverageParams.selectedTopStudents;

                        // Show the general average report
                        FrmGeneralAverageReport frmReport = new FrmGeneralAverageReport(schoolYear, course, yearLevel, topStudents);
                        frmReport.ShowDialog();
                        frmReport.Dispose();
                    }

                    frmParams.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating General Average report: {ex.Message}", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            };
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmLogin.userfullname != null)
                {
                    btnUserFullname.Text = FrmLogin.userfullname;
                    paramrptUserDesignation = FrmLogin.designation;
                    paramrptUserFullname = btnUserFullname.Text;
                }

                OpenChildForm(new FrmHome());
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null && activeForm.Text != "Home")
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmHome());

            //Collapse SignOut Menu
            panelSignOut.Size = new Size(168, 0);
            isCollapsedSignOut = true;
        }

        private void BtnRecord_Click(object sender, EventArgs e)
        {
            timer1.Start();

            //Collapse SignOut Menu
            panelSignOut.Size = new Size(168, 0);
            isCollapsedSignOut = true;
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            timer2.Start();

            //Collapse SignOut Menu
            panelSignOut.Size = new Size(168, 0);
            isCollapsedSignOut = true;
        }

        private void BtnTables_Click(object sender, EventArgs e)
        {
            timer3.Start();

            //Collapse SignOut Menu
            panelSignOut.Size = new Size(168, 0);
            isCollapsedSignOut = true;
        }

        private void BtnUtility_Click(object sender, EventArgs e)
        {
            timer4.Start();

            //Collapse SignOut Menu
            panelSignOut.Size = new Size(168, 0);
            isCollapsedSignOut = true;
        }

        private void BtnUserFullname_Click(object sender, EventArgs e)
        {
            timer5.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsedRecord)
            {
                panelRecord.Height += 100;
                if (panelRecord.Size == panelRecord.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsedRecord = false;
                }
            }
            else
            {
                panelRecord.Height -= 100;
                if (panelRecord.Size == panelRecord.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsedRecord = true;
                }
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (isCollapsedReports)
            {
                panelReports.Height += 100;
                if (panelReports.Size == panelReports.MaximumSize)
                {
                    timer2.Stop();
                    isCollapsedReports = false;
                }
            }
            else
            {
                panelReports.Height -= 100;
                if (panelReports.Size == panelReports.MinimumSize)
                {
                    timer2.Stop();
                    isCollapsedReports = true;
                }
            }
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (isCollapsedTables)
            {
                panelTables.Height += 100;
                if (panelTables.Size == panelTables.MaximumSize)
                {
                    timer3.Stop();
                    isCollapsedTables = false;
                }
            }
            else
            {
                panelTables.Height -= 100;
                if (panelTables.Size == panelTables.MinimumSize)
                {
                    timer3.Stop();
                    isCollapsedTables = true;
                }
            }
        }

        private void Timer4_Tick(object sender, EventArgs e)
        {
            if (isCollapsedUtility)
            {
                panelUtility.Height += 100;
                if (panelUtility.Size == panelUtility.MaximumSize)
                {
                    timer4.Stop();
                    isCollapsedUtility = false;
                }
            }
            else
            {
                panelUtility.Height -= 100;
                if (panelUtility.Size == panelUtility.MinimumSize)
                {
                    timer4.Stop();
                    isCollapsedUtility = true;
                }
            }
        }
        
        private void Timer5_Tick(object sender, EventArgs e)
        {
            if (isCollapsedSignOut)
            {
                panelSignOut.Height += 100;
                if (panelSignOut.Size == panelSignOut.MaximumSize)
                {
                    timer5.Stop();
                    isCollapsedSignOut = false;
                }
            }
            else
            {
                panelSignOut.Height -= 100;
                if (panelSignOut.Size == panelSignOut.MinimumSize)
                {
                    timer5.Stop();
                    isCollapsedSignOut = true;
                }
            }
        }

        private void PanelTopBanner_MouseHover(object sender, EventArgs e)
        {
            CollapseSideBarAndSignOutMenu();
        }

        private void PanelTopBanner_Click(object sender, EventArgs e)
        {
            CollapseSideBarAndSignOutMenu();
        }

        private void FlowLayoutPanel1_Click(object sender, EventArgs e)
        {
            CollapseSideBarAndSignOutMenu();
        }

        private void CollapseSideBarAndSignOutMenu()
        {
            //****************************************
            //Collapsing all SideBar and SignOut Menu
            //****************************************

            panelRecord.Size = new Size(168, 45);
            isCollapsedRecord = true;

            panelReports.Size = new Size(168, 45);
            isCollapsedReports = true;

            panelTables.Size = new Size(168, 45);
            isCollapsedTables = true;

            panelUtility.Size = new Size(168, 45);
            isCollapsedUtility = true;

            panelSignOut.Size = new Size(168, 0);
            isCollapsedSignOut = true;
        }

        private void BtnUserFullname_MouseEnter(object sender, EventArgs e)
        {
            btnUserFullname.ForeColor = Color.FromArgb(247, 251, 3);
        }

        private void BtnUserFullname_MouseLeave(object sender, EventArgs e)
        {
            btnUserFullname.ForeColor = Color.White;
        }

        private void BtnChangePass_Click(object sender, EventArgs e)
        {
            CollapseSideBarAndSignOutMenu();

            new FrmChangePass().ShowDialog();
        }

        private void BtnSignOut_Click(object sender, EventArgs e)
        {
            panelSignOut.Size = new Size(168, 0);
            isCollapsedSignOut = true;

            DialogResult result = MessageBox.Show("Do you really want to Sign out?", "Sign Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to Sign out?", "Sign Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else if (result == DialogResult.Yes)
                {
                    e.Cancel = false;
                    Application.Exit();
                }
            }
        }

        private void BtnUtilCloseAll_Click(object sender, EventArgs e)
        {
            if (activeForm != null) activeForm.Close();
        }

        private void BtnRecPersonalData_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmPersonalData());
        }

        private void BtnRecHigherEdGradSchool_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmRecHigherEdGradSchool());
        }

        private void BtnRecGradesheetVerification_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmRecGradesheetVerification());
        }

        private void BtnRecCompletion_Click(object sender, EventArgs e)
        {
            //Require Registrar's Authorization only once.
            if (CompletionAuthorized == false)
            {
                FrmAuthorizationLogin frmAuthorizationLogin = new FrmAuthorizationLogin();
                frmAuthorizationLogin.ShowDialog();

                if (frmAuthorizationLogin.isAuthorized == true)
                {
                    OpenChildForm(new FrmRecCompletion());
                    CompletionAuthorized = true;
                }
            }
            else
            {
                OpenChildForm(new FrmRecCompletion());
            }
        }

        private void BtnRecTransferees_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmRecTransferees());
        }

        private void BtnRecImportGrades_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmRecImportGrades());
        }

        private void BtnRecNurseryKinder_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmRecNurseryKinder());
            //bool IsOpen = false;

            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.Text == "FrmNurseryKinder")
            //    {
            //        IsOpen = true;
            //        form.Focus();
            //        break;
            //    }
            //}

            //if (IsOpen == false)
            //{
            //    FrmRecNurseryKinder frmRecNurseryKinder = new FrmRecNurseryKinder
            //    {
            //        MdiParent = this
            //    };
            //    frmRecNurseryKinder.Show();
            //}
        }

        private void BtnRptTranscript_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPersonalData frmSearchPersonalData = new FrmSearchPersonalData();
                frmSearchPersonalData.ShowDialog();

                if (frmSearchPersonalData.viewButtonClicked == true)
                {

                    string selectedFcuidno = FrmSearchPersonalData.selectedfcuidno;
                    string selectedStudno = FrmSearchPersonalData.selectedstudno;

                    using (MySqlConnection mysqlcon1 = new MySqlConnection(connectionString))
                    {
                        mysqlcon1.Open();
                        using (MySqlCommand mysqlcmd1 = new MySqlCommand("SELECT cis_fcuidno, cis_firstname, cis_midname, cis_lastname FROM mtbl_studprofile " +
                                                                            "WHERE cis_fcuidno = '" + selectedFcuidno + "' AND cis_studno = '" + selectedStudno + "'", mysqlcon1))
                        {
                            MySqlDataReader datareader1 = mysqlcmd1.ExecuteReader();

                            if (datareader1.HasRows)
                            {
                                if (datareader1.Read())
                                {
                                    studFrmMainfirstname = datareader1["cis_firstname"].ToString();
                                    studFrmMainmidname = datareader1["cis_midname"].ToString();
                                    studFrmMainlastname = datareader1["cis_lastname"].ToString();
                                    studFrmMainFCUIDno = datareader1["cis_fcuidno"].ToString();
                                }
                            }
                        }
                    }

                    rptName = "RptTranscript";
                    FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
                    frmReportViewer.ShowDialog();
                    frmReportViewer.Dispose();
                }

                frmSearchPersonalData.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRptCard_Click(object sender, EventArgs e)
        {
            rptName = "RptReportCard";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptHonorableDismissal_Click(object sender, EventArgs e)
        {
            rptName = "RptHonorableDismissal";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, "");
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptEnrolmentList_Click(object sender, EventArgs e)
        {
            rptName = "RptEnrolmentList";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptEnrolmentListByName_Click(object sender, EventArgs e)
        {
            rptName = "RptEnrolmentListByName";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptEnrolmentListByDepartment_Click(object sender, EventArgs e)
        {
            rptName = "RptEnrolmentListByDepartment";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptGender_Click(object sender, EventArgs e)
        {
            rptName = "RptGenderList";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptCHEDEnrolmentList_Click(object sender, EventArgs e)
        {
            rptName = "RptCHEDEnrolmentList";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptCHEDGradesCreditsEarned_Click(object sender, EventArgs e)
        {
            rptName = "RptCHEDGradesCreditsEarned";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptCHEDGenderBreakdown_Click(object sender, EventArgs e)
        {
            rptName = "RptCHEDGenderBreakdown";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptReligion_Click(object sender, EventArgs e)
        {
            rptName = "RptReligionList";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptTransferees_Click(object sender, EventArgs e)
        {
            rptName = "RptTransfereesStudentsList";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptUniting_Click(object sender, EventArgs e)
        {
            rptName = "RptUnitingStudentsList";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptNewStud_Click(object sender, EventArgs e)
        {
            rptName = "RptNewStudentsList";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptContinuing_Click(object sender, EventArgs e)
        {
            rptName = "RptContinuingStudentsList";
            FrmReportViewer frmReportViewer = new FrmReportViewer(btnUserFullname.Text, paramrptUserDesignation);
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnRptInsurance_Click(object sender, EventArgs e)
        {

        }

        private void BtnRptPersonalData_Click(object sender, EventArgs e)
        {
            rptName = "RptPersonalDataList";
            FrmReportViewer frmReportViewer = new FrmReportViewer();
            frmReportViewer.ShowDialog();
            frmReportViewer.Dispose();
        }

        private void BtnTblCurriculum_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCurriculum());
        }

        private void BtnTblMajorSub_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCourseMajor());
        }

        private void BtnTblSignatories_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmSignatories());
        }

        private void BtnTblGenTRRemarks_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTRRemarksGeneral());
        }

        private void BtnTblBegTRRemarks_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTRRemarksBeginning());
        }



        private void BtnTblCourses_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCourses());
        }

        private void BtnUtilFTPAccount_Click(object sender, EventArgs e)
        {
            //Check user accesslevel if Administrator
            if (FrmLogin.accesslevel == "1")
                OpenChildForm(new FrmFTPAccount());
            else
                MessageBox.Show("You have insufficient privileges for the current operation. Please contact your System Administrator.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
    