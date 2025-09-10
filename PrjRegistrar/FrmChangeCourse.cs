using System;
using System.Collections;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmChangeCourse : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string strCategory, cboCoursePrevText, StudNo, selectedDeptCode, selectedIdCode;
        public bool IsCourseUpdated;


        public FrmChangeCourse()
        {
            InitializeComponent();
        }

        private void FrmChangeCourse_Load(object sender, EventArgs e)
        {
            try
            {
                lblStudNo.Text = FrmPersonalData.Studno;
                lblstudfullname.Text = FrmPersonalData.StudFullname;

                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_category FROM mtbl_course WHERE cis_course = '" + FrmPersonalData.CurrentCourse + "'", mySqlConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                
                if (mySqlDataReader.HasRows)
                {
                    if (mySqlDataReader.Read())
                    {
                        strCategory = mySqlDataReader["cis_category"].ToString();

                        MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString);
                        mySqlConnection1.Open();
                        MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT cis_course FROM mtbl_course WHERE cis_category = '" + strCategory + "' ORDER BY cis_course", mySqlConnection1);
                        MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

                        if (mySqlDataReader1.HasRows)
                        {
                            while (mySqlDataReader1.Read())
                            {
                                cboCourse.Items.Add(mySqlDataReader1["cis_course"]);                                
                            }
                        }
                        mySqlCommand1.Dispose();
                        mySqlConnection1.Close();

                        //If cis_category = "GRADUATE SCHOOL" and Course is not "EDD" Display MAT/MBA only. Remove EDD
                        if (strCategory == "GRADUATE SCHOOL" && FrmPersonalData.CurrentCourse != "EDD")
                        {
                            cboCourse.Items.Remove("EDD");
                        }
                        //If cis_category = "GRADUATE SCHOOL" and Course is EDD Display EDD only. Remove MAT and MBA
                        else if (strCategory == "GRADUATE SCHOOL" && FrmPersonalData.CurrentCourse == "EDD")
                        {
                            cboCourse.Items.Remove("MAT");
                            cboCourse.Items.Remove("MBA");
                        }
                    }
                }
                mySqlCommand.Dispose();
                mySqlConnection.Close();
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

        private void CboCourse_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection mySqlCon = new MySqlConnection(connectionString);
                mySqlCon.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_coursedesc, cis_deptcode, cis_idcode FROM mtbl_course WHERE cis_course = '" + cboCourse.Text + "'", mySqlCon);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                if (mySqlDataReader.HasRows)
                {
                    if (mySqlDataReader.Read())
                    {
                        lblCourseDesc.Text = mySqlDataReader["cis_coursedesc"].ToString();
                        selectedDeptCode = mySqlDataReader["cis_deptcode"].ToString();
                        selectedIdCode = mySqlDataReader["cis_idcode"].ToString();

                        if (cboCoursePrevText != cboCourse.Text)
                        {
                            cboPrgSY.Items.Clear();
                            cboMajorDesc.Items.Clear();
                            lblMajorID.Text = "";
                            cboYearLevel.SelectedIndex = -1;
                            cboAcadStat.SelectedIndex = -1;
                        }
                    }
                }
                mySqlCommand.Dispose();
                mySqlCon.Close();
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
                    //POPULATE PROGRAM SCHOOL YEAR
                    MySqlConnection mySqlCon = new MySqlConnection(connectionString);
                    mySqlCon.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_prgsy FROM reg_curriculum WHERE cis_course = '" + cboCourse.Text + "' order by cis_prgsy desc", mySqlCon);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                    if (mySqlDataReader.HasRows)
                    {
                        while (mySqlDataReader.Read())
                        {
                            cboPrgSY.Items.Add(mySqlDataReader["cis_prgsy"]);
                        }
                    }
                    mySqlCommand.Dispose();
                    mySqlCon.Close();


                    //POPULATE SPECIALIZATION (MAJOR)
                    MySqlConnection mySqlCon1 = new MySqlConnection(connectionString);
                    mySqlCon1.Open();
                    MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT cis_majordesc FROM mtbl_coursemajor WHERE cis_course = '" + cboCourse.Text + "' order by cis_majordesc", mySqlCon1);
                    MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

                    if (mySqlDataReader1.HasRows)
                    {
                        while (mySqlDataReader1.Read())
                        {
                            cboMajorDesc.Items.Add(mySqlDataReader1["cis_majordesc"]);
                        }
                    }

                    mySqlCommand1.Dispose();
                    mySqlCon1.Close();
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
                MySqlConnection mySqlCon = new MySqlConnection(connectionString);
                mySqlCon.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_majorid FROM mtbl_coursemajor WHERE cis_majordesc = '" + cboMajorDesc.Text + "'", mySqlCon);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                if (mySqlDataReader.HasRows)
                {
                    if (mySqlDataReader.Read())
                    {
                        lblMajorID.Text = mySqlDataReader["cis_majorid"].ToString();
                    }
                }
                else
                {
                    lblMajorID.Text = "";
                }
                mySqlCommand.Dispose();
                mySqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboCourse.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Student's new Course.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboCourse.Focus();
                }
                else if (cboPrgSY.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Program School Year.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboPrgSY.Focus();
                }
                else if (cboYearLevel.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Student's Year Level.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboYearLevel.Focus();
                }
                else if (cboAcadStat.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Student's Academic Status.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboAcadStat.Focus();
                }
                else
                {
                    DialogResult dialogresult = MessageBox.Show("This will change the student's course/major. Are you sure you want to proceed?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogresult == DialogResult.Yes)
                    {
                        //withEnrollment = false will UPDATE mtbl_studprofile and mtbl_enrolcnt Table;
                        if (FrmPersonalData.withEnrollment == false)
                        {
                            //if student changed course (cis_idcode), Update student ID Number else retain old student ID Number
                            if (FrmPersonalData.CurrentIdCode != selectedIdCode)
                                StudNo = ChangeStudentID(FrmPersonalData.defaultSchlyr, selectedIdCode);
                            else
                                StudNo = lblStudNo.Text;

                            /**********************************
                            //1. update mtbl_studprofile Table
                            **********************************/
                            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                            {
                                mysqlcon.Open();

                                string UpdateMtblStudprofile = "UPDATE mtbl_studprofile SET " +
                                                                "cis_studno = @cis_studno, " +
                                                                "cis_username = @cis_studno, " +
                                                                "cis_oldstudno = @cis_oldstudno, " +
                                                                "cis_prgsy = @cis_prgsy, " +
                                                                "cis_course = @cis_course, " +
                                                                "cis_major = @cis_major, " +
                                                                "cis_yrlevel = @cis_yrlevel, " +
                                                                "cis_schlyr = @cis_schlyr, " +
                                                                "cis_semester = @cis_semester, " +
                                                                "cis_acadstat = @cis_acadstat, " +
                                                                "cis_enrolstatus = @cis_enrolstatus, " +
                                                                "cis_accountability = @cis_accountability, " +
                                                                "cis_lastmodified = @cis_lastmodified " +
                                                                "WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "'";
                                using (MySqlCommand mysqlcommand = new MySqlCommand(UpdateMtblStudprofile, mysqlcon))
                                {
                                    mysqlcommand.Parameters.AddWithValue("@cis_studno", StudNo);
                                    mysqlcommand.Parameters.AddWithValue("@cis_oldstudno", FrmPersonalData.CurrentIdCode == selectedIdCode ? DBNull.Value : (object)lblStudNo.Text);
                                    mysqlcommand.Parameters.AddWithValue("@cis_prgsy", cboPrgSY.Text);
                                    mysqlcommand.Parameters.AddWithValue("@cis_course", cboCourse.Text);
                                    mysqlcommand.Parameters.AddWithValue("@cis_major", String.IsNullOrEmpty(lblMajorID.Text) ? DBNull.Value : (object)lblMajorID.Text);
                                    mysqlcommand.Parameters.AddWithValue("@cis_yrlevel", cboYearLevel.Text);
                                    mysqlcommand.Parameters.AddWithValue("@cis_schlyr", FrmPersonalData.defaultSchlyr);
                                    mysqlcommand.Parameters.AddWithValue("@cis_semester", FrmPersonalData.defaultSemNo);
                                    mysqlcommand.Parameters.AddWithValue("@cis_acadstat", cboAcadStat.Text);
                                    mysqlcommand.Parameters.AddWithValue("@cis_enrolstatus", String.IsNullOrEmpty(FrmPersonalData.CurrentEnrolStatus) ? DBNull.Value : (object)FrmPersonalData.CurrentEnrolStatus);
                                    mysqlcommand.Parameters.AddWithValue("@cis_accountability", FrmLogin.userfullname);
                                    mysqlcommand.Parameters.AddWithValue("@cis_lastmodified", DateTime.Now);
                                    mysqlcommand.ExecuteNonQuery();
                                }
                            }

                            /**********************************
                            //2. update mtbl_enrolcnt Table
                            **********************************/
                            using (MySqlConnection mysqlcon1 = new MySqlConnection(connectionString))
                            {
                                mysqlcon1.Open();

                                string UpdateMtblEnrolCnt = "UPDATE mtbl_enrolcnt SET " +
                                                            "cis_studno = '" + StudNo + "', " +
                                                            "cis_course = '" + cboCourse.Text.Trim() + "', " +
                                                            "cis_schlyr = '" + FrmPersonalData.defaultSchlyr + "', " +
                                                            "cis_semester = '" + FrmPersonalData.defaultSemNo + "', " +
                                                            "cis_yrlevel = '" + cboYearLevel.Text.Trim() + "', " +
                                                            "cis_deptcode = '" + selectedDeptCode + "', " +
                                                            "cis_acadstat = '" + cboAcadStat.Text.Trim() + "' " +
                                                            "WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "'";
                                using (MySqlCommand mysqlcmd1 = new MySqlCommand(UpdateMtblEnrolCnt, mysqlcon1))
                                {
                                    mysqlcmd1.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("Student's Course changed succesfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            IsCourseUpdated = true;

                            this.Close();
                        }
                        //withEnrollment = true will DELETE or UPDATE affected Tables;
                        else if (FrmPersonalData.withEnrollment == true)
                        {
                            /**************************************************
                            //1. delete from acc_assessment Table
                            **************************************************/
                            using (MySqlConnection mysqlcon2 = new MySqlConnection(connectionString))
                            {
                                mysqlcon2.Open();

                                string DeleteAccAssessment = "DELETE FROM acc_assessment WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "' and cis_transid = '" + FrmPersonalData.cisTransId + "'";
                                using (MySqlCommand mysqlcmd2 = new MySqlCommand(DeleteAccAssessment, mysqlcon2))
                                {
                                    mysqlcmd2.ExecuteNonQuery();
                                }
                            }


                            /**************************************************
                            //2. delete from acc_studledger Table
                            **************************************************/
                            using (MySqlConnection mysqlcon3 = new MySqlConnection(connectionString))
                            {
                                mysqlcon3.Open();

                                string DeleteAccStudLedger = "DELETE FROM acc_studledger WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "' and cis_transid = '" + FrmPersonalData.cisTransId + "' and cis_transdetail = 'ENROLLMENT'";
                                using (MySqlCommand mysqlcmd3 = new MySqlCommand(DeleteAccStudLedger, mysqlcon3))
                                {
                                    mysqlcmd3.ExecuteNonQuery();
                                }
                            }


                            /**************************************************
                            //3. delete from reg_appraisal Table
                            **************************************************/
                            using (MySqlConnection mysqlcon4 = new MySqlConnection(connectionString))
                            {
                                mysqlcon4.Open();

                                string DeleteRegAppraisal = "DELETE FROM reg_appraisal WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "' and cis_schlyr = '" + FrmPersonalData.CurrentSchoolYear + "' and cis_semester = '" + FrmPersonalData.CurrentSemester + "'";
                                using (MySqlCommand mysqlcmd4 = new MySqlCommand(DeleteRegAppraisal, mysqlcon4))
                                {
                                    mysqlcmd4.ExecuteNonQuery();
                                }
                            }


                            /**************************************************
                            //4. select cis_classid from reg_subjenrolled
                            **************************************************/
                            ArrayList arrayClassIDList = new ArrayList();
                            using (MySqlConnection mysqlcon5 = new MySqlConnection(connectionString))
                            {
                                mysqlcon5.Open();

                                string SelectClassId = "SELECT cis_classid FROM reg_subjenrolled WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "' and cis_schlyr = '" + FrmPersonalData.CurrentSchoolYear + "' and cis_semester = '" + FrmPersonalData.CurrentSemester + "'";
                                using (MySqlCommand mysqlcmd5 = new MySqlCommand(SelectClassId, mysqlcon5))
                                {
                                    MySqlDataReader mySqlDataReader5 = mysqlcmd5.ExecuteReader();

                                    if (mySqlDataReader5.HasRows)
                                    {
                                        while (mySqlDataReader5.Read())
                                        {
                                            arrayClassIDList.Add(Convert.ToInt32(mySqlDataReader5["cis_classid"].ToString()));
                                        }
                                    }
                                }
                            }


                            /**************************************************
                            //5. update reg_subjschedule set cis_available + 1
                            ***************************************************/
                            using (MySqlConnection mysqlcon6 = new MySqlConnection(connectionString))
                            {
                                mysqlcon6.Open();

                                for (int i = 0; i < arrayClassIDList.Count; i++)
                                {
                                    int cisClassId = Convert.ToInt32(arrayClassIDList[i]);

                                    string UpdateRegSubjSchedule = "UPDATE reg_subjschedule SET cis_available = (cis_available + 1) WHERE cis_classid = '" + cisClassId + "'";
                                    using (MySqlCommand mysqlcmd6 = new MySqlCommand(UpdateRegSubjSchedule, mysqlcon6))
                                    {
                                        mysqlcmd6.ExecuteNonQuery();
                                    }
                                }
                            }


                            /**************************************************
                            //6. delete from reg_subjenrolled Table
                            **************************************************/
                            using (MySqlConnection mysqlcon7 = new MySqlConnection(connectionString))
                            {
                                mysqlcon7.Open();

                                string DeleteRegSubjEnrolled = "DELETE FROM reg_subjenrolled WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "' and cis_schlyr = '" + FrmPersonalData.CurrentSchoolYear + "' and cis_semester = '" + FrmPersonalData.CurrentSemester + "'";
                                using (MySqlCommand mysqlcmd7 = new MySqlCommand(DeleteRegSubjEnrolled, mysqlcon7))
                                {
                                    mysqlcmd7.ExecuteNonQuery();
                                }
                            }


                            /**************************************************
                            //7. update mtbl_enrollprogress Table
                            **************************************************/
                            using (MySqlConnection mysqlcon8 = new MySqlConnection(connectionString))
                            {
                                mysqlcon8.Open();

                                string UpdateMtblEnrollProgress = "UPDATE mtbl_enrollprogress SET " +
                                                                    "cis_admission = 0, " +
                                                                    "cis_appraisal = 0, " +
                                                                    "cis_classassess = 0, " +
                                                                    "cis_adjustment = 0, " +
                                                                    "cis_payment = 0, " +
                                                                    "cis_verification = 0, " +
                                                                    "cis_cancellation = 1, " +
                                                                    "cis_remarks = 'CHANGED COURSE' " +
                                                                    "WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "' and cis_schlyr = '" + FrmPersonalData.CurrentSchoolYear + "' and cis_semester = '" + FrmPersonalData.CurrentSemester + "'";
                                using (MySqlCommand mysqlcmd8 = new MySqlCommand(UpdateMtblEnrollProgress, mysqlcon8))
                                {
                                    mysqlcmd8.ExecuteNonQuery();
                                }
                            }


                            /**********************************
                            //8. update mtbl_studprofile Table
                            **********************************/
                            //if student changed course (cis_idcode), Update student ID Number else retain old student ID Number
                            if (FrmPersonalData.CurrentIdCode != selectedIdCode)
                                StudNo = ChangeStudentID(FrmPersonalData.defaultSchlyr, selectedIdCode);
                            else
                                StudNo = lblStudNo.Text;

                            using (MySqlConnection mysqlcon9 = new MySqlConnection(connectionString))
                            {
                                mysqlcon9.Open();

                                string UpdateMtblStudprofile = "UPDATE mtbl_studprofile SET " +
                                                                "cis_studno = @cis_studno, " +
                                                                "cis_username = @cis_studno, " +
                                                                "cis_oldstudno = @cis_oldstudno, " +
                                                                "cis_prgsy = @cis_prgsy, " +
                                                                "cis_course = @cis_course, " +
                                                                "cis_major = @cis_major, " +
                                                                "cis_yrlevel = @cis_yrlevel, " +
                                                                "cis_schlyr = @cis_schlyr, " +
                                                                "cis_semester = @cis_semester, " +
                                                                "cis_acadstat = @cis_acadstat, " +
                                                                "cis_enrolstatus = @cis_enrolstatus, " +
                                                                "cis_accountability = @cis_accountability, " +
                                                                "cis_lastmodified = @cis_lastmodified " +
                                                                "WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "'";
                                using (MySqlCommand mysqlcmd9 = new MySqlCommand(UpdateMtblStudprofile, mysqlcon9))
                                {
                                    mysqlcmd9.Parameters.AddWithValue("@cis_studno", StudNo);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_oldstudno", FrmPersonalData.CurrentIdCode == selectedIdCode ? DBNull.Value : (object)lblStudNo.Text);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_prgsy", cboPrgSY.Text);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_course", cboCourse.Text);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_major", String.IsNullOrEmpty(lblMajorID.Text) ? DBNull.Value : (object)lblMajorID.Text);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_yrlevel", cboYearLevel.Text);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_schlyr", FrmPersonalData.defaultSchlyr);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_semester", FrmPersonalData.defaultSemNo);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_acadstat", cboAcadStat.Text);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_enrolstatus", String.IsNullOrEmpty(FrmPersonalData.CurrentEnrolStatus) ? DBNull.Value : (object)FrmPersonalData.CurrentEnrolStatus);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_accountability", FrmLogin.userfullname);
                                    mysqlcmd9.Parameters.AddWithValue("@cis_lastmodified", DateTime.Now);
                                    mysqlcmd9.ExecuteNonQuery();
                                }
                            }


                            /**********************************
                            //9. update mtbl_enrolcnt Table
                            **********************************/
                            using (MySqlConnection mysqlcon10 = new MySqlConnection(connectionString))
                            {
                                mysqlcon10.Open();

                                string UpdateMtblEnrolCnt = "UPDATE mtbl_enrolcnt SET " +
                                                            "cis_studno = '" + StudNo + "', " +
                                                            "cis_course = '" + cboCourse.Text.Trim() + "', " +
                                                            "cis_schlyr = '" + FrmPersonalData.defaultSchlyr + "', " +
                                                            "cis_semester = '" + FrmPersonalData.defaultSemNo + "', " +
                                                            "cis_yrlevel = '" + cboYearLevel.Text.Trim() + "', " +
                                                            "cis_deptcode = '" + selectedDeptCode + "', " +
                                                            "cis_acadstat = '" + cboAcadStat.Text.Trim() + "' " +
                                                            "WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "'";
                                using (MySqlCommand mysqlcmd1 = new MySqlCommand(UpdateMtblEnrolCnt, mysqlcon10))
                                {
                                    mysqlcmd1.ExecuteNonQuery();
                                }
                            }


                            /**************************************************
                            //10. delete from mtbl_enrollment Table
                            **************************************************/
                            using (MySqlConnection mysqlcon11 = new MySqlConnection(connectionString))
                            {
                                mysqlcon11.Open();

                                string DeleteMtblEnrollment = "DELETE FROM mtbl_enrollment WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "' and cis_transid = '" + FrmPersonalData.cisTransId + "'";
                                using (MySqlCommand mysqlcmd11 = new MySqlCommand(DeleteMtblEnrollment, mysqlcon11))
                                {
                                    mysqlcmd11.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("Student's Course changed succesfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            IsCourseUpdated = true;

                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ChangeStudentID(string strYear, string IdCode)
        {   
            int i, indexofzero;
            string _cis_studno;
            string year = strYear.Substring(2, 2);
            string studno = year + '-' + IdCode + FrmPersonalData.defaultSemNo + '-' + '%';

            /****************************************************************
            //Autogenerated Updated cis_studno from mtbl_enrolcnt
            *****************************************************************/
            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
            {
                mysqlcon.Open();
                using (MySqlCommand Command = new MySqlCommand("SELECT cis_studno FROM mtbl_enrolcnt WHERE cis_studno LIKE '" + studno + "' ORDER BY cis_studno", mysqlcon))
                {
                    MySqlDataReader DataReader = Command.ExecuteReader();
                    if (DataReader.HasRows)
                    {
                        while (DataReader.Read())
                        {
                            _cis_studno = DataReader["cis_studno"].ToString();

                            //Look for the first 0 in cis_studno ex: 22-GS1-0001
                            indexofzero = _cis_studno.IndexOf("0");

                            i = int.Parse(_cis_studno.Substring(indexofzero, 4)) + 1;
                            studno = studno.Substring(0, indexofzero) + (i.ToString("D4"));
                        }
                    }
                    else
                    {
                        studno = year + '-' + IdCode + FrmPersonalData.defaultSemNo + "-0001";
                    }
                }
            }


            /****************************************************************
            //Counter Check Updated cis_studno in mtbl_studprofile if exist
            *****************************************************************/
            indexofzero = studno.IndexOf("0");
            studno = studno.Substring(0, indexofzero) + '%';            
                       
            using (MySqlConnection mysqlconnection = new MySqlConnection(connectionString))
            {
                mysqlconnection.Open();
                using (MySqlCommand mysqlcommand = new MySqlCommand("SELECT cis_studno FROM mtbl_studprofile WHERE cis_studno LIKE '" + studno + "' ORDER BY cis_studno", mysqlconnection))
                {
                    MySqlDataReader mysqldataReader = mysqlcommand.ExecuteReader();
                    if (mysqldataReader.HasRows)
                    {
                        while (mysqldataReader.Read())
                        {
                            _cis_studno = mysqldataReader["cis_studno"].ToString();

                            //Look for the first 0 in cis_studno ex: 22-GS1-0001
                            indexofzero = _cis_studno.IndexOf("0");
                            i = int.Parse(_cis_studno.Substring(indexofzero, 4)) + 1;
                            studno = studno.Substring(0, indexofzero) + (i.ToString("D4"));
                        }
                    }
                    else
                    {
                        studno = year + '-' + IdCode + FrmPersonalData.defaultSemNo + "-0001";
                    }
                }
            }

            return studno;
        }
    }
}
