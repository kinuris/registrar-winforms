using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace PrjRegistrar
{
    public partial class FrmReportViewer : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        private readonly string paramRptUser, paramRptUserDesignation, strRegistrarFullName, strRegistrarPosLabel;

        public FrmReportViewer()
        {
            strRegistrarFullName = FrmLogin.registrarFullName;
            strRegistrarPosLabel = FrmLogin.registrarPosLabel;

            InitializeComponent();
        }

        public FrmReportViewer(string parameterReportUser, string parameterReportUserDesignation)
        {
            paramRptUser = parameterReportUser;
            paramRptUserDesignation = parameterReportUserDesignation;

            strRegistrarFullName = FrmLogin.registrarFullName;
            strRegistrarPosLabel = FrmLogin.registrarPosLabel;

            InitializeComponent();
        }



        private void FrmReportViewer_Load(object sender, EventArgs e)
        {
            if (FrmMain.rptName == "RptTranscript")
            {
                RptTranscript rptTranscript = new RptTranscript();
                
                //RptTranscript is called from FrmPersonalData where LN, FN, MN is passed in crystalReportViewer1 as parameter
                if (FrmPersonalData.clickedFrmPersonalData == true)
                {
                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();

                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT(cis_course) FROM reg_subjenrolled WHERE cis_fcuidno = '" + FrmPersonalData.Fcuidno + "'", mySqlConnection))
                        {
                            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                            {
                                while (mySqlDataReader.Read())
                                {
                                    // Retrieve the column value
                                    string columnValue = mySqlDataReader.GetString(0);
                                    // Add the column value to the parameter list of values
                                    rptTranscript.ParameterFields["Courses"].CurrentValues.AddValue(columnValue);
                                }
                            }
                        }
                    }

                    rptTranscript.SetParameterValue("Lastname", FrmPersonalData.studlastname);
                    rptTranscript.SetParameterValue("Firstname", FrmPersonalData.studfirstname);
                    rptTranscript.SetParameterValue("Midname", FrmPersonalData.studmidname);
                    rptTranscript.SetParameterValue("Date Issued", DateTime.Now);
                }
                //RptTranscript is called from FrmMain where LN, FN, MN is passed in crystalReportViewer1 as parameter
                else
                {
                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();

                        using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT(cis_course) FROM reg_subjenrolled WHERE cis_fcuidno = '" + FrmMain.studFrmMainFCUIDno + "'", mySqlConnection))
                        {
                            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                            { 
                                while (mySqlDataReader.Read())
                                {
                                    // Retrieve the column value
                                    string columnValue = mySqlDataReader.GetString(0);
                                    // Add the column value to the parameter list of values
                                    rptTranscript.ParameterFields["Courses"].CurrentValues.AddValue(columnValue);
                                }
                            }
                        }
                    }

                    rptTranscript.SetParameterValue("Lastname", FrmMain.studFrmMainlastname);
                    rptTranscript.SetParameterValue("Firstname", FrmMain.studFrmMainfirstname);
                    rptTranscript.SetParameterValue("Midname", FrmMain.studFrmMainmidname);
                    rptTranscript.SetParameterValue("Date Issued", DateTime.Now);
                }

                rptTranscript.SetParameterValue("Issued By", paramRptUser);

                TextObject textRegistrarFullName = (TextObject)rptTranscript.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptTranscript.ReportDefinition.ReportObjects["txtPosLabel"];
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptTranscript;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptReportCard")
            {
                RptReportCard rptReportCard = new RptReportCard();

                TextObject textRegistrarFullName = (TextObject)rptReportCard.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptReportCard.ReportDefinition.ReportObjects["txtPosLabel"];
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptReportCard;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptHonorableDismissal")
            {
                RptHonorableDismissal rptHonorableDismissal = new RptHonorableDismissal();
                RptHonorableDismissalCascadingParameter rptHonorableDismissalCascadingParameter = new RptHonorableDismissalCascadingParameter();

                //RptHonorableDismissal is called from FrmPersonalData where LN, FN, MN is passed in crystalReportViewer1 as parameter
                if (FrmPersonalData.clickedFrmPersonalData == true)
                {
                    rptHonorableDismissal.SetParameterValue("Lastname", FrmPersonalData.studlastname);
                    rptHonorableDismissal.SetParameterValue("Firstname", FrmPersonalData.studfirstname);
                    rptHonorableDismissal.SetParameterValue("Midname", FrmPersonalData.studmidname);
                    rptHonorableDismissal.SetParameterValue("Prepared By", paramRptUser);
                    crystalReportViewer1.ReportSource = rptHonorableDismissal;
                    crystalReportViewer1.RefreshReport();
                }
                else
                {
                    rptHonorableDismissalCascadingParameter.SetParameterValue("Prepared By", paramRptUser);
                    crystalReportViewer1.ReportSource = rptHonorableDismissalCascadingParameter;
                    crystalReportViewer1.RefreshReport();
                }
            }
            else if (FrmMain.rptName == "RptEnrolmentList")
            {
                RptEnrolmentList rptEnrolmentList = new RptEnrolmentList();

                //*** Main Report  ***//
                TextObject textRegistrarFullName = (TextObject)rptEnrolmentList.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptEnrolmentList.ReportDefinition.ReportObjects["txtPosLabel"];
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptEnrolmentList;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptEnrolmentListByName")
            {
                RptEnrolmentListByName rptEnrolmentListByName = new RptEnrolmentListByName();

                //*** Main Report  ***//
                TextObject textPreparedBy = (TextObject)rptEnrolmentListByName.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptEnrolmentListByName.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptEnrolmentListByName.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptEnrolmentListByName.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                //*** Subreport  ***//
                TextObject txtPreparedBy = (TextObject)rptEnrolmentListByName.Subreports["RptEnrolmentListByName_SubReport.rpt"].ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject txtDesignation = (TextObject)rptEnrolmentListByName.Subreports["RptEnrolmentListByName_SubReport.rpt"].ReportDefinition.ReportObjects["txtDesignation"];
                TextObject txtRegistrarFullName = (TextObject)rptEnrolmentListByName.Subreports["RptEnrolmentListByName_SubReport.rpt"].ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject txtRegistrarPosLabel = (TextObject)rptEnrolmentListByName.Subreports["RptEnrolmentListByName_SubReport.rpt"].ReportDefinition.ReportObjects["txtPosLabel"];
                txtPreparedBy.Text = paramRptUser;
                txtDesignation.Text = paramRptUserDesignation;
                txtRegistrarFullName.Text = strRegistrarFullName;
                txtRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptEnrolmentListByName;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptEnrolmentListByDepartment")
            {
                RptEnrolmentListByDepartment rptEnrolmentListByDepartment = new RptEnrolmentListByDepartment();

                //*** Main Report  ***//
                TextObject textPreparedBy = (TextObject)rptEnrolmentListByDepartment.ReportDefinition.ReportObjects["txtPreparedBy"];    
                TextObject textDesignation = (TextObject)rptEnrolmentListByDepartment.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptEnrolmentListByDepartment.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptEnrolmentListByDepartment.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                //*** Subreport  ***//
                TextObject txtPreparedBy = (TextObject)rptEnrolmentListByDepartment.Subreports["RptEnrolmentListByDepartment_SubReport.rpt"].ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject txtDesignation = (TextObject)rptEnrolmentListByDepartment.Subreports["RptEnrolmentListByDepartment_SubReport.rpt"].ReportDefinition.ReportObjects["txtDesignation"];
                TextObject txtRegistrarFullName = (TextObject)rptEnrolmentListByDepartment.Subreports["RptEnrolmentListByDepartment_SubReport.rpt"].ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject txtRegistrarPosLabel = (TextObject)rptEnrolmentListByDepartment.Subreports["RptEnrolmentListByDepartment_SubReport.rpt"].ReportDefinition.ReportObjects["txtPosLabel"];
                txtPreparedBy.Text = paramRptUser;
                txtDesignation.Text = paramRptUserDesignation;
                txtRegistrarFullName.Text = strRegistrarFullName;
                txtRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptEnrolmentListByDepartment;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptGenderList")
            {
                RptGenderList rptGenderList = new RptGenderList();

                TextObject textPreparedBy = (TextObject)rptGenderList.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptGenderList.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptGenderList.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptGenderList.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptGenderList;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptCHEDEnrolmentList")
            {
                RptCHEDEnrolmentList rptCHEDEnrolmentList = new RptCHEDEnrolmentList();

                //*** Main Report  ***//
                TextObject textRegistrarFullName = (TextObject)rptCHEDEnrolmentList.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptCHEDEnrolmentList.ReportDefinition.ReportObjects["txtPosLabel"];
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                //*** Subreport  ***//
                TextObject txtPreparedBy = (TextObject)rptCHEDEnrolmentList.Subreports["RptCHEDEnrolmentList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject txtDesignation = (TextObject)rptCHEDEnrolmentList.Subreports["RptCHEDEnrolmentList_SubReport.rpt"].ReportDefinition.ReportObjects["txtDesignation"];
                TextObject txtRegistrarFullName = (TextObject)rptCHEDEnrolmentList.Subreports["RptCHEDEnrolmentList_SubReport.rpt"].ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject txtRegistrarPosLabel = (TextObject)rptCHEDEnrolmentList.Subreports["RptCHEDEnrolmentList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPosLabel"];
                txtPreparedBy.Text = paramRptUser;
                txtDesignation.Text = paramRptUserDesignation;
                txtRegistrarFullName.Text = strRegistrarFullName;
                txtRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptCHEDEnrolmentList;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptCHEDGradesCreditsEarned")
            {
                RptCHEDGradesCreditsEarned rptCHEDGradesCreditsEarned = new RptCHEDGradesCreditsEarned();

                TextObject textPreparedBy = (TextObject)rptCHEDGradesCreditsEarned.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptCHEDGradesCreditsEarned.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptCHEDGradesCreditsEarned.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptCHEDGradesCreditsEarned.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptCHEDGradesCreditsEarned;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptCHEDGenderBreakdown")
            {
                RptCHEDGenderBreakdown rptCHEDGenderBreakdown = new RptCHEDGenderBreakdown();

                //*** Main Report  ***//
                TextObject textPreparedBy = (TextObject)rptCHEDGenderBreakdown.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptCHEDGenderBreakdown.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptCHEDGenderBreakdown.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptCHEDGenderBreakdown.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                //*** Subreport  ***//
                TextObject txtPreparedBy = (TextObject)rptCHEDGenderBreakdown.Subreports["RptCHEDGenderBreakdown_SubReport.rpt"].ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject txtDesignation = (TextObject)rptCHEDGenderBreakdown.Subreports["RptCHEDGenderBreakdown_SubReport.rpt"].ReportDefinition.ReportObjects["txtDesignation"];
                TextObject txtRegistrarFullName = (TextObject)rptCHEDGenderBreakdown.Subreports["RptCHEDGenderBreakdown_SubReport.rpt"].ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject txtRegistrarPosLabel = (TextObject)rptCHEDGenderBreakdown.Subreports["RptCHEDGenderBreakdown_SubReport.rpt"].ReportDefinition.ReportObjects["txtPosLabel"];
                txtPreparedBy.Text = paramRptUser;
                txtDesignation.Text = paramRptUserDesignation;
                txtRegistrarFullName.Text = strRegistrarFullName;
                txtRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptCHEDGenderBreakdown;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptReligionList")
            {
                RptReligionList rptReligionList = new RptReligionList();

                TextObject textPreparedBy = (TextObject)rptReligionList.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptReligionList.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptReligionList.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptReligionList.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptReligionList;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptNewStudentsList")
            {
                RptNewStudentsList rptNewStudentsList = new RptNewStudentsList();

                //*** Main Report  ***//
                TextObject textPreparedBy = (TextObject)rptNewStudentsList.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptNewStudentsList.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptNewStudentsList.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptNewStudentsList.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                //*** Subreport  ***//
                TextObject txtPreparedBy = (TextObject)rptNewStudentsList.Subreports["RptNewStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject txtDesignation = (TextObject)rptNewStudentsList.Subreports["RptNewStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtDesignation"];
                TextObject txtRegistrarFullName = (TextObject)rptNewStudentsList.Subreports["RptNewStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject txtRegistrarPosLabel = (TextObject)rptNewStudentsList.Subreports["RptNewStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPosLabel"];
                txtPreparedBy.Text = paramRptUser;
                txtDesignation.Text = paramRptUserDesignation;
                txtRegistrarFullName.Text = strRegistrarFullName;
                txtRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptNewStudentsList;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptContinuingStudentsList")
            {
                RptContinuingStudentsList rptContinuingStudentsList = new RptContinuingStudentsList();

                //*** Main Report  ***//
                TextObject textPreparedBy = (TextObject)rptContinuingStudentsList.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptContinuingStudentsList.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptContinuingStudentsList.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptContinuingStudentsList.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                //*** Subreport  ***//
                TextObject txtPreparedBy = (TextObject)rptContinuingStudentsList.Subreports["RptContinuingStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject txtDesignation = (TextObject)rptContinuingStudentsList.Subreports["RptContinuingStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtDesignation"];
                TextObject txtRegistrarFullName = (TextObject)rptContinuingStudentsList.Subreports["RptContinuingStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject txtRegistrarPosLabel = (TextObject)rptContinuingStudentsList.Subreports["RptContinuingStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPosLabel"];
                txtPreparedBy.Text = paramRptUser;
                txtDesignation.Text = paramRptUserDesignation;
                txtRegistrarFullName.Text = strRegistrarFullName;
                txtRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptContinuingStudentsList;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptUnitingStudentsList")
            {
                RptUnitingStudentsList rptUnitingStudentsList = new RptUnitingStudentsList();

                //*** Main Report  ***//
                TextObject textPreparedBy = (TextObject)rptUnitingStudentsList.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptUnitingStudentsList.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptUnitingStudentsList.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptUnitingStudentsList.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                //*** Subreport  ***//
                TextObject txtPreparedBy = (TextObject)rptUnitingStudentsList.Subreports["RptUnitingStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject txtDesignation = (TextObject)rptUnitingStudentsList.Subreports["RptUnitingStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtDesignation"];
                TextObject txtRegistrarFullName = (TextObject)rptUnitingStudentsList.Subreports["RptUnitingStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject txtRegistrarPosLabel = (TextObject)rptUnitingStudentsList.Subreports["RptUnitingStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPosLabel"];
                txtPreparedBy.Text = paramRptUser;
                txtDesignation.Text = paramRptUserDesignation;
                txtRegistrarFullName.Text = strRegistrarFullName;
                txtRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptUnitingStudentsList;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptTransfereesStudentsList")
            {
                RptTransfereesStudentsList rptTransfereesStudentsList = new RptTransfereesStudentsList();

                //*** Main Report  ***//
                TextObject textPreparedBy = (TextObject)rptTransfereesStudentsList.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptTransfereesStudentsList.ReportDefinition.ReportObjects["txtDesignation"];
                TextObject textRegistrarFullName = (TextObject)rptTransfereesStudentsList.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptTransfereesStudentsList.ReportDefinition.ReportObjects["txtPosLabel"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                //*** Subreport  ***//
                TextObject txtPreparedBy = (TextObject)rptTransfereesStudentsList.Subreports["RptTransfereesStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject txtDesignation = (TextObject)rptTransfereesStudentsList.Subreports["RptTransfereesStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtDesignation"];
                TextObject txtRegistrarFullName = (TextObject)rptTransfereesStudentsList.Subreports["RptTransfereesStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject txtRegistrarPosLabel = (TextObject)rptTransfereesStudentsList.Subreports["RptTransfereesStudentsList_SubReport.rpt"].ReportDefinition.ReportObjects["txtPosLabel"];
                txtPreparedBy.Text = paramRptUser;
                txtDesignation.Text = paramRptUserDesignation;
                txtRegistrarFullName.Text = strRegistrarFullName;
                txtRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptTransfereesStudentsList;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptCurriculum")
            {
                RptCurriculum rptCurriculum = new RptCurriculum();
                
                TextObject textRegistrarFullName = (TextObject)rptCurriculum.ReportDefinition.ReportObjects["txtRegistrar"];
                TextObject textRegistrarPosLabel = (TextObject)rptCurriculum.ReportDefinition.ReportObjects["txtPosLabel"];
                
                textRegistrarFullName.Text = strRegistrarFullName;
                textRegistrarPosLabel.Text = strRegistrarPosLabel;

                crystalReportViewer1.ReportSource = rptCurriculum;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptGradingSheet")
            {
                RptGradingSheet rptGradingSheet = new RptGradingSheet();

                rptGradingSheet.SetParameterValue("Class ID", FrmRecGradesheetVerification.classID);

                TextObject textPreparedBy = (TextObject)rptGradingSheet.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject textDesignation = (TextObject)rptGradingSheet.ReportDefinition.ReportObjects["txtDesignation"];
                textPreparedBy.Text = paramRptUser;
                textDesignation.Text = paramRptUserDesignation;

                crystalReportViewer1.ReportSource = rptGradingSheet;
                crystalReportViewer1.RefreshReport();
            }
            else if (FrmMain.rptName == "RptPersonalDataList")
            {
                RptPersonalDataList rptPersonalDataList = new RptPersonalDataList();

                crystalReportViewer1.ReportSource = rptPersonalDataList;
                crystalReportViewer1.RefreshReport();
            }

        }
    }
}