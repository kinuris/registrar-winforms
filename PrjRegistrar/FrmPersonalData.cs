using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmPersonalData : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        bool isCollapsedMenu = true;
        bool isCollapsedPhotoMenu = true;
        int uniting, irregular, transferee, crossenrolee;
        string major, admitdate, graddate, sodate, profilepic,gui_profilepic;
        string gui_birthday, gui_status, gui_gender, gui_contactno, gui_email, gui_religion, gui_province, gui_city, gui_barangay, gui_street, gui_zipcode, gui_homeaddress;
        string gui_mothfname, gui_mothmname, gui_mothlname, gui_fathfname, gui_fathmname, gui_fathlname, gui_guardian, gui_grdianmobileno, gui_capitaincome;
        public static string Fcuidno, Studno, studlastname, studfirstname, studmidname, StudFullname, CurrentCourse, CurrentIdCode, CurrentEnrolStatus, CurrentSchoolYear, CurrentSemester, defaultSchlyr, defaultSemNo, cisTransId, cisEnrolStatus, cisCategory;
        public static bool withEnrollment, clickedFrmPersonalData;

        public FrmPersonalData()
        {
            InitializeComponent();

            FillUpCboProvince();
            
            FrmPersonalData_SizeChanged(null, null);
        }


        private void FillUpCboProvince()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_province FROM mtbl_province", mySqlConnection))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                        while (mySqlDataReader.Read())
                        {
                            cboprovince.Items.Add(mySqlDataReader[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmPersonalData_SizeChanged(object sender, EventArgs e)
        {
            int newflowLayoutPanelWidth1 = flowLayoutPanel1.Width - 25;
            panelFirstRow.Width = newflowLayoutPanelWidth1 - 5;
            panelSecondRow.Width = newflowLayoutPanelWidth1 - 5;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPhotoMenu_Click(object sender, EventArgs e)
        {
            timer1.Start();

            //******************************
            //Collapse menu panel
            panelMenu.Size = new Size(168, 0);
            isCollapsedMenu = true;
            //******************************
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {   
            timer2.Start();

            //******************************
            //Collapse photo panel
            panelPhotoMenu.Size = new Size(150, 0);
            isCollapsedPhotoMenu = true;
            //******************************
        }

        private void LblStudentIDNo_DoubleClick(object sender, EventArgs e)
        {
            if (txtstudno.Enabled == false)
                txtstudno.Enabled = true;
            else
                txtstudno.Enabled = false;
        }

        private void Lblfirstname_DoubleClick(object sender, EventArgs e)
        {
            if (txtfirstname.Enabled == false)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update Student's Firstname/Middlename/Lastname? Authorization is required.", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    FrmAuthorizationLogin frmAuthorizationLogin = new FrmAuthorizationLogin();
                    frmAuthorizationLogin.ShowDialog();

                    if (frmAuthorizationLogin.isAuthorized == true)
                    {
                        txtfirstname.Enabled = true;
                        txtmidname.Enabled = true;
                        txtlastname.Enabled = true;
                        txtsuffix.Enabled = true;
                    }
                }
            }
            else
            {
                txtfirstname.Enabled = false;
                txtmidname.Enabled = false;
                txtlastname.Enabled = false;
                txtsuffix.Enabled = false;
            }   
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //******************************
                //Collapse menu and photo panels
                panelMenu.Size = new Size(168, 0);
                isCollapsedMenu = true;

                panelPhotoMenu.Size = new Size(150, 0);
                isCollapsedPhotoMenu = true;
                //******************************

                FrmSearchPersonalData frmSearchPersonalData = new FrmSearchPersonalData();
                frmSearchPersonalData.ShowDialog();

                if (frmSearchPersonalData.viewButtonClicked == true)
                {
                    txtfcuidno.Text = FrmSearchPersonalData.selectedfcuidno;
                }

                frmSearchPersonalData.Dispose();
            }
            catch (Exception ex)
            {
                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtstudno.Text.Trim() == "")
                {
                    MessageBox.Show("Please select Student Personal Data", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    clickedFrmPersonalData = true;

                    studlastname = txtlastname.Text.Trim();
                    studfirstname = txtfirstname.Text.Trim();
                    studmidname = txtmidname.Text.Trim();

                    FrmMain.rptName = "RptTranscript";
                    FrmReportViewer frmReportViewer = new FrmReportViewer(FrmMain.paramrptUserFullname, FrmMain.paramrptUserDesignation);
                    frmReportViewer.ShowDialog();
                    frmReportViewer.Dispose();

                    clickedFrmPersonalData = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmdRefresh_Click(object sender, EventArgs e)
        {
            //******************************
            //Collapse menu and photo panels
            panelMenu.Size = new Size(168, 0);
            isCollapsedMenu = true;

            panelPhotoMenu.Size = new Size(150, 0);
            isCollapsedPhotoMenu = true;
            //******************************

            if (txtstudno.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to refresh the Student Personal Data Page? All data will be cleared.", "Student Personal Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                    txtfcuidno.Text = "";
                    txtstudno.Text = "";
                    lblstudno.Text = "-";
                    txtfirstname.Text = "";
                    txtmidname.Text = "";
                    txtlastname.Text = "";
                    txtsuffix.Text = "";

                    txtstudno.Enabled = false;
                    txtfirstname.Enabled = false;
                    txtmidname.Enabled = false;
                    txtlastname.Enabled = false;
                    txtsuffix.Enabled = false;

                    dtpbirthdate.Value = DateTime.Now;
                    dtpbirthdate.CustomFormat = " ";

                    txtage.Text = "";
                    txtbirthplace.Text = "";
                    cbocstatus.SelectedIndex = -1;
                    cbogender.SelectedIndex = -1;
                    cbocitizen.SelectedIndex = -1;
                    txtparentguardian.Text = "";
                    txtelemschl.Text = "";
                    txtelemsy.Text = "";
                    txtsecondschl.Text = "";
                    txtsecondsy.Text = "";
                    txthomeadd.Text = "";
                    txtofficeadd.Text = "";

                    txtprgsy.Text = "";
                    txtcourse.Text = "";
                    txtmajor.Text = "";                    
                    txtyrlevel.Text = "";
                    txtschlyr.Text = "";
                    txtsemester.Text = "";
                    txtenrolstatus.Text = "";
                    txtacadstat.Text = "";

                    cboPrgSY.Items.Clear();
                    cboMajorDesc.Items.Clear();

                    lblMajorID.Text = "";
                    cboPrgSY.SelectedIndex = -1;                    
                    cboMajorDesc.SelectedIndex = -1;
                    cboYearLevel.SelectedIndex = -1;
                    cboPrgsem.SelectedIndex = -1;
                    cboAcadStat.SelectedIndex = -1;

                    dtpadmitdate.Value = DateTime.Now;
                    dtpadmitdate.CustomFormat = " ";

                    dtpgraddate.Value = DateTime.Now;
                    dtpgraddate.CustomFormat = " ";

                    txtadmissioncred.Text = "";
                    txtspecialorder.Text = "";

                    dtpsodate.Value = DateTime.Now;
                    dtpsodate.CustomFormat = " ";

                    chkuniting.Checked = false;
                    chkirregular.Checked = false;
                    chktransferee.Checked = false;
                    chkcrossenrolee.Checked = false;

                    tsslcreated.Text = "mm/dd/yyyy";
                    tssllastmodified.Text = "mm/dd/yyyy";
                    tsslaccountability.Text = "accountability";
                    tsslaccstatus.Text = "accstatus";
                                        
                    txtcontactno.Text = "";
                    txtemail.Text = "";
                    cboreligion.SelectedIndex = -1;
                    cboprovince.SelectedIndex = -1;
                    cbocity.SelectedIndex = -1;
                    cbobarangay.SelectedIndex = -1;
                    txtstreet.Text = "";
                    txtzipcode.Text = "";

                    txtmothfname.Text = "";
                    txtmothmname.Text = "";
                    txtmothlname.Text = "";
                    txtfathfname.Text = "";
                    txtfathmname.Text = "";
                    txtfathlname.Text = "";
                    txtcontactnoparent.Text = "";
                    cbocapitaincome.SelectedIndex = -1;

                    cbocity.Enabled = false;
                    cbobarangay.Enabled = false;
                    txtzipcode.Enabled = false;
                }
            }
        }

        private void Txtfcuidno_TextChanged(object sender, EventArgs e)
        {
            //******************************************************************
            //Loads the student record from search box to the PersonalData Form.
            //******************************************************************
            
            Fcuidno = txtfcuidno.Text;

            MySqlConnection mysqlcon = new MySqlConnection(connectionString);
            mysqlcon.Open();
                                
            MySqlCommand command = new MySqlCommand("SELECT mtbl_studprofile.*, " +
                                                    "CASE " +
                                                        "WHEN mtbl_studprofile.cis_major IS NOT NULL THEN mtbl_coursemajor.cis_majordesc " +
                                                        "ELSE NULL " +
                                                    "END AS cis_majordesc " +
                                                    "FROM mtbl_studprofile " +
                                                    "LEFT JOIN mtbl_coursemajor ON mtbl_studprofile.cis_major = mtbl_coursemajor.cis_majorid " +
                                                    "WHERE cis_fcuidno ='" + Fcuidno + "'", mysqlcon);
            MySqlDataReader datareader = command.ExecuteReader();

            if (datareader.HasRows)
            {
                if (datareader.Read())
                {
                    //******************************************************************
                    //Query fields from guidance table     02.06.2020
                    //This query is used when populating Birthday, Civil Status, Gender, parent/guardian, home address, profilepic from gui_studprofile table
                    //******************************************************************
                    MySqlConnection gui_mysqlcon = new MySqlConnection(connectionString);
                    gui_mysqlcon.Open();
                        
                    MySqlCommand gui_command = new MySqlCommand("SELECT cis_birthdate, cis_civilstat, cis_gender, cis_contactno, cis_email, cis_religion, " +
                                                                "cis_province, cis_city, cis_barangay, cis_street, cis_zipcode, " +
                                                                "cis_mothfname, cis_mothmname, cis_mothlname, cis_fathfname, cis_fathmname, cis_fathlname, cis_capitaincome, cis_grdianname, cis_grdianmobileno, " +
                                                                "concat(cis_barangay, ', ',  cis_city, ', ', cis_province) as cis_homeaddress, cis_profilepic " +
                                                                "FROM gui_studprofile WHERE id ='" + datareader["cis_profileid"].ToString() + "'", gui_mysqlcon);
                    MySqlDataReader gui_datareader = gui_command.ExecuteReader();

                    if (gui_datareader.HasRows)
                    {
                        if (gui_datareader.Read())
                        {
                            gui_birthday = gui_datareader["cis_birthdate"].ToString();
                            gui_status = gui_datareader["cis_civilstat"].ToString();
                            gui_gender = gui_datareader["cis_gender"].ToString();
                            gui_contactno = gui_datareader["cis_contactno"].ToString();
                            gui_email = gui_datareader["cis_email"].ToString();
                            gui_religion = gui_datareader["cis_religion"].ToString();
                            gui_province = gui_datareader["cis_province"].ToString();
                            gui_city = gui_datareader["cis_city"].ToString();
                            gui_barangay = gui_datareader["cis_barangay"].ToString();
                            gui_street = gui_datareader["cis_street"].ToString();
                            gui_zipcode = gui_datareader["cis_zipcode"].ToString();
                            gui_homeaddress = gui_datareader["cis_homeaddress"].ToString();
                            gui_mothfname = gui_datareader["cis_mothfname"].ToString();
                            gui_mothmname = gui_datareader["cis_mothmname"].ToString();
                            gui_mothlname = gui_datareader["cis_mothlname"].ToString();
                            gui_fathfname = gui_datareader["cis_fathfname"].ToString();
                            gui_fathmname = gui_datareader["cis_fathmname"].ToString();
                            gui_fathlname = gui_datareader["cis_fathlname"].ToString();
                            gui_guardian = gui_datareader["cis_grdianname"].ToString();
                            gui_grdianmobileno = gui_datareader["cis_grdianmobileno"].ToString();
                            gui_capitaincome = gui_datareader["cis_capitaincome"].ToString();
                            gui_profilepic = gui_datareader["cis_profilepic"] as string ?? null;
                        }
                    }

                    lblstudno.Text = txtstudno.Text = datareader["cis_studno"].ToString();
                     
                    txtfirstname.Text = datareader["cis_firstname"].ToString(); 
                    txtmidname.Text = datareader["cis_midname"].ToString();
                    txtlastname.Text = datareader["cis_lastname"].ToString();
                    txtsuffix.Text = datareader["cis_suffix"].ToString();

                    Studno = txtstudno.Text;
                    StudFullname = txtlastname.Text.Trim().ToUpper() + ", " + txtfirstname.Text.Trim().ToUpper() + " " + txtmidname.Text.Trim().ToUpper();

                    //******************************************************************
                    //Retrieve data from mtbl_studprofile first. (datareader)  
                    //If does not exist.... Retrieve data from gui_studprofile (gui_datareader)
                    //******************************************************************

                    //******************************************************************
                    // Birth Date 
                    //******************************************************************
                    string birthday = datareader["cis_birthdate"].ToString();
                    if (birthday != "")
                    {
                        dtpbirthdate.Value = Convert.ToDateTime(birthday);
                    }
                    else if (birthday == "")
                    {
                        if (gui_datareader.HasRows)
                        {
                            dtpbirthdate.Value = Convert.ToDateTime(gui_birthday);
                        }
                        else
                        {
                            dtpbirthdate.Value = DateTime.Now;
                            dtpbirthdate.CustomFormat = " ";
                            txtage.Text = "";
                        }
                    }
                    else
                    {
                        dtpbirthdate.Value = DateTime.Now;
                        dtpbirthdate.CustomFormat = " ";
                        txtage.Text = "";
                    }

                    txtbirthplace.Text = datareader["cis_birthplace"].ToString();

                    //******************************************************************
                    // Civil Status
                    //******************************************************************
                    string status = datareader["cis_cstatus"].ToString();
                    if (status != "")
                    {
                        cbocstatus.Text = status;
                    }
                    else if (status == "")
                    {
                        if (gui_datareader.HasRows)
                            cbocstatus.Text = gui_status;
                        else
                            cbocstatus.SelectedIndex = -1;
                    }
                    else
                    {
                        cbocstatus.SelectedIndex = -1;
                    }

                    //******************************************************************
                    // Gender
                    //******************************************************************
                    string gender = datareader["cis_gender"].ToString();
                    if (gender == "M")
                    {
                        cbogender.SelectedIndex = 0;
                    }
                    else if (gender == "F")
                    {
                        cbogender.SelectedIndex = 1;
                    }
                    else if (gender == "")
                    {
                        if (gui_datareader.HasRows)
                        {
                            if (gui_gender == "M")
                                cbogender.SelectedIndex = 0;
                            else if (gui_gender == "F")
                                cbogender.SelectedIndex = 1;
                            else
                                cbogender.SelectedIndex = -1;
                        }
                        else
                        {
                            cbogender.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        cbogender.SelectedIndex = -1;
                    }

                    cbocitizen.Text = datareader["cis_citizen"].ToString();

                    //******************************************************************
                    // Contactno
                    //******************************************************************
                    string contactno = datareader["cis_contactno"].ToString();
                    if (contactno != "")
                    {
                        txtcontactno.Text = contactno;
                    }
                    else if (contactno == "")
                    {
                        if (gui_datareader.HasRows)
                            txtcontactno.Text = gui_contactno;
                        else
                            txtcontactno.Text = "";
                    }
                    else
                    {
                        txtcontactno.Text = "";
                    }

                    //******************************************************************
                    // Email
                    //******************************************************************
                    string email = datareader["cis_email"].ToString();
                    if (email != "")
                    {
                        txtemail.Text = email.ToLower();
                    }
                    else if (email == "")
                    {
                        if (gui_datareader.HasRows)
                            txtemail.Text = gui_email.ToLower();
                        else
                            txtemail.Text = "";
                    }
                    else
                    {
                        txtemail.Text = "";
                    }

                    //******************************************************************
                    // Religion
                    //******************************************************************
                    string religion = datareader["cis_religion"].ToString();
                    if (religion != "")
                    {
                        cboreligion.Text = religion;
                    }
                    else if (religion == "")
                    {
                        if (gui_datareader.HasRows)
                            cboreligion.Text = gui_religion;
                        else
                            cboreligion.SelectedIndex = -1;
                    }
                    else
                    {
                        cboreligion.SelectedIndex = -1;
                    }

                    //******************************************************************
                    // Province
                    //******************************************************************
                    string province = datareader["cis_province"].ToString();
                    if (province != "")
                    {
                        cboprovince.Text = province;
                    }
                    else if (province == "")
                    {
                        if (gui_datareader.HasRows)
                            cboprovince.Text = gui_province;
                        else
                            cboprovince.SelectedIndex = -1;
                    }
                    else
                    {
                        cboprovince.SelectedIndex = -1;
                    }

                    //******************************************************************
                    // City
                    //******************************************************************
                    string city = datareader["cis_city"].ToString();
                    if (city != "")
                    {
                        cbocity.Text = city;
                    }
                    else if (city == "")
                    {
                        if (gui_datareader.HasRows)
                            cbocity.Text = gui_city;
                        else
                            cbocity.SelectedIndex = -1;
                    }
                    else
                    {
                        cbocity.SelectedIndex = -1;
                    }

                    //******************************************************************
                    // Barangay
                    //******************************************************************
                    string barangay = datareader["cis_barangay"].ToString();
                    if (barangay != "")
                    {
                        cbobarangay.Text = barangay;
                    }
                    else if (barangay == "")
                    {
                        if (gui_datareader.HasRows)
                            cbobarangay.Text = gui_barangay;
                        else
                            cbobarangay.SelectedIndex = -1;
                    }
                    else
                    {
                        cbobarangay.SelectedIndex = -1;
                    }

                    //******************************************************************
                    // Street
                    //******************************************************************
                    string street = datareader["cis_street"].ToString();
                    if (street != "")
                    {
                        txtstreet.Text = street;
                    }
                    else if (street == "")
                    {
                        if (gui_datareader.HasRows)
                            txtstreet.Text = gui_street;
                        else
                            txtstreet.Text = "";
                    }
                    else
                    {
                        txtstreet.Text = "";
                    }

                    //******************************************************************
                    // zipcode
                    //******************************************************************
                    string zipcode = datareader["cis_zipcode"].ToString();
                    if (zipcode != "")
                    {
                        txtzipcode.Text = zipcode;
                    }
                    else if (zipcode == "")
                    {
                        if (gui_datareader.HasRows)
                            txtzipcode.Text = gui_zipcode;
                        else
                            txtzipcode.Text = "";
                    }
                    else
                    {
                        txtzipcode.Text = "";
                    }

                    //******************************************************************
                    // Home Address
                    //******************************************************************
                    string homeaddress = datareader["cis_homeadd"].ToString();
                    if (homeaddress != "")
                    {
                        txthomeadd.Text = homeaddress;
                    }
                    else if (homeaddress == "")
                    {
                        if (gui_datareader.HasRows)
                            txthomeadd.Text = "BRGY. " + gui_homeaddress;
                        else
                            txthomeadd.Text = "BRGY. ";
                    }
                    else
                    {
                        txthomeadd.Text = "BRGY. ";
                    }

                    txtofficeadd.Text = datareader["cis_officeadd"].ToString();

                    //******************************************************************
                    // Mother's Firstname
                    //******************************************************************
                    string mothfname = datareader["cis_mothfname"].ToString();
                    if (mothfname != "")
                    {
                        txtmothfname.Text = mothfname;
                    }
                    else if (mothfname == "")
                    {
                        if (gui_datareader.HasRows)
                            txtmothfname.Text = gui_mothfname;
                        else
                            txtmothfname.Text = "";
                    }
                    else
                    {
                        txtmothfname.Text = "";
                    }

                    //******************************************************************
                    // Mother's Middlename
                    //******************************************************************
                    string mothmname = datareader["cis_mothmname"].ToString();
                    if (mothmname != "")
                    {
                        txtmothmname.Text = mothmname;
                    }
                    else if (mothmname == "")
                    {
                        if (gui_datareader.HasRows)
                            txtmothmname.Text = gui_mothmname;
                        else
                            txtmothmname.Text = "";
                    }
                    else
                    {
                        txtmothmname.Text = "";
                    }

                    //******************************************************************
                    // Mother's Lastname
                    //******************************************************************
                    string mothlname = datareader["cis_mothlname"].ToString();
                    if (mothlname != "")
                    {
                        txtmothlname.Text = mothlname;
                    }
                    else if (mothlname == "")
                    {
                        if (gui_datareader.HasRows)
                            txtmothlname.Text = gui_mothlname;
                        else
                            txtmothlname.Text = "";
                    }
                    else
                    {
                        txtmothlname.Text = "";
                    }

                    //******************************************************************
                    // Father's Firstname
                    //******************************************************************
                    string fathfname = datareader["cis_fathfname"].ToString();
                    if (fathfname != "")
                    {
                        txtfathfname.Text = fathfname;
                    }
                    else if (fathfname == "")
                    {
                        if (gui_datareader.HasRows)
                            txtfathfname.Text = gui_fathfname;
                        else
                            txtfathfname.Text = "";
                    }
                    else
                    {
                        txtfathfname.Text = "";
                    }

                    //******************************************************************
                    // Father's Middlename
                    //******************************************************************
                    string fathmname = datareader["cis_fathmname"].ToString();
                    if (fathmname != "")
                    {
                        txtfathmname.Text = fathmname;
                    }
                    else if (fathmname == "")
                    {
                        if (gui_datareader.HasRows)
                            txtfathmname.Text = gui_fathmname;
                        else
                            txtfathmname.Text = "";
                    }
                    else
                    {
                        txtfathmname.Text = "";
                    }

                    //******************************************************************
                    // Father's Lastname
                    //******************************************************************
                    string fathlname = datareader["cis_fathlname"].ToString();
                    if (fathlname != "")
                    {
                        txtfathlname.Text = fathlname;
                    }
                    else if (fathlname == "")
                    {
                        if (gui_datareader.HasRows)
                            txtfathlname.Text = gui_fathlname;
                        else
                            txtfathlname.Text = "";
                    }
                    else
                    {
                        txtfathlname.Text = "";
                    }

                    //******************************************************************
                    // ParentGuardian
                    //******************************************************************
                    string guardian = datareader["cis_parentguardian"].ToString();
                    if (guardian != "")
                    {
                        txtparentguardian.Text = guardian;
                    }
                    else if (guardian == "")
                    {
                        if (gui_datareader.HasRows)
                            txtparentguardian.Text = gui_guardian;
                        else
                            txtparentguardian.Text = "";
                    }
                    else
                    {
                        txtparentguardian.Text = "";
                    }

                    //******************************************************************
                    // Parent/Guardian Contact No
                    //******************************************************************
                    string contactnoparent = datareader["cis_contactno_parent"].ToString();
                    if (contactnoparent != "")
                    {
                        txtcontactnoparent.Text = contactnoparent;
                    }
                    else if (contactnoparent == "")
                    {
                        if (gui_datareader.HasRows)
                            txtcontactnoparent.Text = gui_grdianmobileno;
                        else
                            txtcontactnoparent.Text = "";
                    }
                    else
                    {
                        txtcontactnoparent.Text = "";
                    }

                    //******************************************************************
                    // Capitaincome
                    //******************************************************************
                    string capitaincome = datareader["cis_capitaincome"].ToString();
                    if (capitaincome != "")
                    {
                        cbocapitaincome.Text = capitaincome;
                    }
                    else if (capitaincome == "")
                    {
                        if (gui_datareader.HasRows)
                            cbocapitaincome.Text = gui_capitaincome;
                        else
                            cbocapitaincome.SelectedIndex = -1;
                    }
                    else
                    {
                        cbocapitaincome.SelectedIndex = -1;
                    }

                    cboPrgSY.Items.Clear();
                    cboMajorDesc.Items.Clear();

                    txtcourse.Text = datareader["cis_course"].ToString();

                    txtprgsy.Text = datareader["cis_prgsy"].ToString();
                    cboPrgSY.Text = datareader["cis_prgsy"].ToString();

                    major = lblMajorID.Text = datareader["cis_major"].ToString();
                    txtmajor.Text = datareader["cis_majordesc"].ToString();                    
                    cboMajorDesc.Text = datareader["cis_majordesc"].ToString();
                    if (cboMajorDesc.Text == "")
                        major = lblMajorID.Text = "";

                    txtyrlevel.Text = datareader["cis_yrlevel"].ToString();
                    cboYearLevel.Text = datareader["cis_yrlevel"].ToString();

                    txtschlyr.Text = datareader["cis_schlyr"].ToString();

                    txtsemester.Text = datareader["cis_semester"].ToString();
                    cboPrgsem.Text = datareader["cis_semester"].ToString();

                    txtenrolstatus.Text = datareader["cis_enrolstatus"].ToString();

                    txtacadstat.Text = datareader["cis_acadstat"].ToString();
                    cboAcadStat.Text = datareader["cis_acadstat"].ToString();

                    txtelemschl.Text = datareader["cis_elemschl"].ToString();
                    txtelemsy.Text = datareader["cis_elemsy"].ToString();
                    txtsecondschl.Text = datareader["cis_secondschl"].ToString();
                    txtsecondsy.Text = datareader["cis_secondsy"].ToString();

                    // Admission Date
                    admitdate = datareader["cis_admitdate"].ToString();
                    if (admitdate != "")
                    {
                        //if admitdate value is 1/1/2000 or lower, change it to Today's Date but do not display it.
                        if (Convert.ToDateTime(admitdate) < dtpadmitdate.MinDate)
                        {
                            dtpadmitdate.Value = DateTime.Now;
                            dtpadmitdate.CustomFormat = " ";
                        }
                        else
                        {
                            dtpadmitdate.Value = Convert.ToDateTime(admitdate);
                        }
                    }
                    else
                    {
                        dtpadmitdate.Value = DateTime.Now;
                        dtpadmitdate.CustomFormat = " ";
                    }

                    // Graduation Date 
                    graddate = datareader["cis_graddate"].ToString();
                    if (graddate != "")
                    {
                        //if graddate value is 1/1/2000 or lower, change it to Today's Date but do not display it.
                        if (Convert.ToDateTime(graddate) < dtpgraddate.MinDate)
                        {
                            dtpgraddate.Value = DateTime.Now;
                            dtpgraddate.CustomFormat = " ";
                        }
                        else
                        {
                            dtpgraddate.Value = Convert.ToDateTime(graddate);
                        }
                    }
                    else
                    {
                        dtpgraddate.Value = DateTime.Now;
                        dtpgraddate.CustomFormat = " ";
                    }

                    txtadmissioncred.Text = datareader["cis_admissioncred"].ToString();
                    txtspecialorder.Text = datareader["cis_specialorder"].ToString();

                    // Special Order Date
                    sodate = datareader["cis_sodate"].ToString();
                    if (sodate != "")
                    {
                        //if sodate value is 1/1/2000 or lower, change it to Today's Date but do not display it.
                        if (Convert.ToDateTime(sodate) < dtpsodate.MinDate)
                        {
                            dtpsodate.Value = DateTime.Now;
                            dtpsodate.CustomFormat = " ";
                        }
                        else
                        {
                            dtpsodate.Value = Convert.ToDateTime(sodate);
                        }
                    }
                    else
                    {
                        dtpsodate.Value = DateTime.Now;
                        dtpsodate.CustomFormat = " ";
                    }

                    // check uniting if not null
                    string strUniting = datareader["cis_uniting"].ToString();
                    if (strUniting != "")
                    {
                        uniting = Convert.ToInt32(strUniting);
                        if (uniting == 1) chkuniting.Checked = true; else chkuniting.Checked = false;
                    }
                    else
                    {
                        chkuniting.Checked = false;
                    }

                    // check irregular if not null
                    string strIrregular = datareader["cis_irregular"].ToString();
                    if (strIrregular != "")
                    {
                        irregular = Convert.ToInt32(strIrregular);
                        if (irregular == 1) chkirregular.Checked = true; else chkirregular.Checked = false;
                    }
                    else
                    {
                        chkirregular.Checked = false;
                    }

                    // check transferee if not null
                    string strTransferee = datareader["cis_transferee"].ToString();
                    if (strTransferee != "")
                    {
                        transferee = Convert.ToInt32(strTransferee);
                        if (transferee == 1) chktransferee.Checked = true; else chktransferee.Checked = false;
                    }
                    else
                    {
                        chktransferee.Checked = false;
                    }

                    // check crossenrolee if not null
                    string strCrossenrolee = datareader["cis_crossenrolee"].ToString();
                    if (strCrossenrolee != "")
                    {
                        crossenrolee = Convert.ToInt32(strCrossenrolee);
                        if (crossenrolee == 1) chkcrossenrolee.Checked = true; else chkcrossenrolee.Checked = false;
                    }
                    else
                    {
                        chkcrossenrolee.Checked = false;
                    }

                    // Account Status:
                    string accstatus = datareader["cis_accstatus"].ToString();
                    if (accstatus != "")
                        tsslaccstatus.Text = accstatus;
                    else
                        tsslaccstatus.Text = "accstatus";

                    // Last Modified By: (Accountability)
                    string accountability = datareader["cis_accountability"].ToString();
                    if (accountability != "")
                        tsslaccountability.Text = accountability;
                    else
                        tsslaccountability.Text = "accountability";

                    //Created at:
                    tsslcreated.Text = datareader["created_at"].ToString();

                    //Check if lastmodified has value. If it doesn't have value, it will have the same value as tsslcreated.
                    string lastmodified = datareader["cis_lastmodified"].ToString();
                    if (lastmodified != "")
                        tssllastmodified.Text = lastmodified;
                    else
                        tssllastmodified.Text = tsslcreated.Text;


                    // Load Web image in Picture Box
                    
                    //string webServUrl = Environment.GetEnvironmentVariable("envWebServPath");
                    profilepic = datareader["cis_profilepic"] as string ?? null;
                    /********************************
                    if (profilepic != null)
                    {
                        WebRequest request = WebRequest.Create(webServUrl + profilepic);
                        using (var response = request.GetResponse())
                        {
                            using (var str = response.GetResponseStream())
                            {
                                cpbProfilePic.Image = Bitmap.FromStream(str);
                            }
                        }
                    }
                    else if (profilepic == null)
                    {
                        // if cis_profilepic is null from mtbl_studprofile table, get cis_profilepic from gui_studprofile table
                        if (gui_datareader.HasRows)
                        {
                            if (gui_profilepic != null)
                            {
                                WebRequest request = WebRequest.Create(webServUrl + gui_profilepic);
                                using (var response = request.GetResponse())
                                {
                                    using (var str = response.GetResponseStream())
                                    {
                                        cpbProfilePic.Image = Bitmap.FromStream(str);
                                        profilepic = gui_profilepic;
                                    }
                                }
                            }
                            else
                            {
                                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                            }
                        }
                        else
                        {
                            cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                        }
                    }
                    else
                    {
                        cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                    }
                    *********************************/

                    gui_command.Dispose();
                    gui_mysqlcon.Close();
                }
            }

            command.Dispose();
            mysqlcon.Close();
        }

        private void Txtcourse_TextChanged(object sender, EventArgs e)
        {
            //POPULATE PROGRAM SCHOOL YEAR
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT DISTINCT cis_prgsy FROM reg_curriculum WHERE cis_course = '" + txtcourse.Text + "' order by cis_prgsy desc", mySqlCon))
                {
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                    if (mySqlDataReader.HasRows)
                    {
                        while (mySqlDataReader.Read())
                        {
                            cboPrgSY.Items.Add(mySqlDataReader["cis_prgsy"]);
                        }
                    }
                }
            }

            //POPULATE SPECIALIZATION (MAJOR)
            using (MySqlConnection mySqlCon1 = new MySqlConnection(connectionString))
            {
                mySqlCon1.Open();
                using (MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT cis_majordesc FROM mtbl_coursemajor WHERE cis_course = '" + txtcourse.Text + "' order by cis_majordesc", mySqlCon1))
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

        private void Dtpbirthdate_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(dtpbirthdate.Value) == "")
            {
                txtage.Text = "";
            }
            else
            {
                dtpbirthdate.CustomFormat = "dddd, MMMM dd, yyyy";

                TimeSpan span = DateTime.Now - dtpbirthdate.Value;
                var totalDays = span.TotalDays;
                var totalYears = Math.Truncate(totalDays / 365.25);

                //txtage.Text Derived from birthday
                txtage.Text = Convert.ToString(totalYears);                 
            }
        }

        private void Dtpbirthdate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtpbirthdate.CustomFormat = " ";
                txtage.Text = "";
            }
        }

        private void Dtpadmitdate_ValueChanged(object sender, EventArgs e)
        {
            dtpadmitdate.CustomFormat = "dddd, MMMM dd, yyyy";
        }

        private void Dtpadmitdate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtpadmitdate.CustomFormat = " ";
            }
        }

        private void Txtcontactno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Txtzipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Txtcontactnoparent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void CboMajorDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT cis_majorid FROM mtbl_coursemajor WHERE cis_course = '" + txtcourse.Text.Trim() + "' and cis_majordesc = '" + cboMajorDesc.Text.Trim() + "'", mySqlCon))
                    {
                        MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                        if (mySqlDataReader.HasRows)
                        {
                            if (mySqlDataReader.Read())
                                major = lblMajorID.Text = mySqlDataReader["cis_majorid"].ToString();
                        }
                        else
                        {
                            major = lblMajorID.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cboprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                /************************************
                //fillup City/Municipality combobox
                ************************************/
                using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                {
                    mysqlcon.Open();
                    using (MySqlCommand Command = new MySqlCommand("Select cis_citymunicipality from mtbl_citymunicipality where cis_province = '" + cboprovince.Text + "'", mysqlcon))
                    {
                        MySqlDataReader DataReader = Command.ExecuteReader();

                        if (cboprovince.Text != "OTHERS")
                        {
                            while (DataReader.Read())
                            {
                                cbocity.Items.Add(DataReader[0]);
                            }
                            cbocity.SelectedIndex = -1;
                            cbobarangay.SelectedIndex = -1;
                            txtzipcode.Text = "";

                            cbocity.Enabled = true;
                            cbobarangay.Enabled = false;
                            txtzipcode.Enabled = true;
                        }
                        else
                        {
                            cbocity.SelectedIndex = -1;
                            cbobarangay.SelectedIndex = -1;
                            txtzipcode.Text = "";

                            cbocity.Enabled = false;
                            cbobarangay.Enabled = false;
                            txtzipcode.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cbocity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                /************************************
                //fillup Barangay combobox
                ************************************/
                using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                {
                    mysqlcon.Open();
                    using (MySqlCommand Command = new MySqlCommand("Select a.cis_barangay, b.cis_postalcode from mtbl_barangay a, mtbl_citymunicipality b where a.cis_citymunicipality = '" + cbocity.Text + "' and a.cis_citymunicipality = b.cis_citymunicipality", mysqlcon))
                    {
                        MySqlDataReader DataReader = Command.ExecuteReader();

                        if (cbocity.SelectedIndex != -1)
                        {
                            cbobarangay.Items.Clear();

                            while (DataReader.Read())
                            {
                                cbobarangay.Items.Add(DataReader[0]);
                            }

                            txtzipcode.Text = DataReader.GetValue(1).ToString();

                            cbobarangay.Enabled = true;
                            txtzipcode.Enabled = true;
                        }
                        else
                        {
                            txtzipcode.Text = "";

                            cbobarangay.Enabled = false;
                            txtzipcode.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dtpgraddate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpgraddate.Value < dtpadmitdate.Value)
            {
                dtpgraddate.CustomFormat = " ";
            }
            else
            {
                dtpgraddate.CustomFormat = "dddd, MMMM dd, yyyy";
            }
        }

        private void Dtpgraddate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back)|| (e.KeyCode == Keys.Delete))
            {
                dtpgraddate.CustomFormat = " ";
            }
        }

        private void Dtpsodate_ValueChanged(object sender, EventArgs e)
        {
            dtpsodate.CustomFormat = "dddd, MMMM dd, yyyy";
        }

        private void Dtpsodate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtpsodate.CustomFormat = " ";
            }
        }

        private void BtnCapturePhoto_Click(object sender, EventArgs e)
        {
            //if (txtfirstname.Text == "" || txtlastname.Text == "")
            //{
            //    MessageBox.Show("First Name and Last Name is required before capturing photo", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    txtfirstname.Focus();
            //}
            //else
            //{
            //    Process.Start("microsoft.windows.camera:");
            //}

            //timer1.Start();
        }

        private void BtnBrowsePhoto_Click(object sender, EventArgs e)
        {
            //if (txtfirstname.Text == "" || txtlastname.Text == "")
            //{
            //    MessageBox.Show("First Name and Last Name is required before selecting photo", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    txtfirstname.Focus();
            //}
            //else
            //{
            //    OpenFileDialog openFileDialog = new OpenFileDialog
            //    {
            //        Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*",
            //        Title = "Select Image"
            //    };

            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        picPath = openFileDialog.FileName.ToString();
            //        cpbProfilePic.ImageLocation = picPath;
            //    }

            //    openFileDialog.Dispose();
            //}

            //timer1.Start();
        }

        private void BtnRemovePhoto_Click(object sender, EventArgs e)
        {
            //cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
            //picPath = null;
            //timer1.Start();
        }

        private void CpbProfilePic_Click(object sender, EventArgs e)
        {
            FrmProfilePicture frmProfilePicture = new FrmProfilePicture();
            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = cpbProfilePic.Image,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            frmProfilePicture.Controls.Add(pictureBox);
            frmProfilePicture.ShowDialog();
            frmProfilePicture.Dispose();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsedPhotoMenu)
            {
                panelPhotoMenu.Height += 180;
                if (panelPhotoMenu.Size == panelPhotoMenu.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsedPhotoMenu = false;
                }
            }
            else
            {
                panelPhotoMenu.Height -= 180;
                if (panelPhotoMenu.Size == panelPhotoMenu.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsedPhotoMenu = true;
                }
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (isCollapsedMenu)
            {
                panelMenu.Height += 180;
                if (panelMenu.Size == panelMenu.MaximumSize)
                {
                    timer2.Stop();
                    isCollapsedMenu = false;
                }
            }
            else
            {
                panelMenu.Height -= 180;
                if (panelMenu.Size == panelMenu.MinimumSize)
                {
                    timer2.Stop();
                    isCollapsedMenu = true;
                }
            }
        }

        private void Panel2_MouseHover(object sender, EventArgs e)
        {
            //******************************
            //Collapse menu and photo panels
            panelMenu.Size = new Size(168, 0);
            isCollapsedMenu = true;

            panelPhotoMenu.Size = new Size(150, 0);
            isCollapsedPhotoMenu = true;
            //******************************
        }

        private void Panel3_MouseHover(object sender, EventArgs e)
        {
            //******************************
            //Collapse menu and photo panels
            panelMenu.Size = new Size(168, 0);
            isCollapsedMenu = true;

            panelPhotoMenu.Size = new Size(150, 0);
            isCollapsedPhotoMenu = true;
            //******************************
        }

        private void BtnRequirements_Click(object sender, EventArgs e)
        {
            panelMenu.Size = new Size(168, 0);
            isCollapsedMenu = true;

            if (txtfcuidno.Text == "")
            {
                MessageBox.Show("FCU ID or Student ID number must not be empty.", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                FrmRequirements frmRequirements = new FrmRequirements();
                frmRequirements.ShowDialog();

                frmRequirements.Dispose();
            }
        }

        private void BtnCertificates_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtstudno.Text.Trim() == "")
                {
                    MessageBox.Show("Please select Student Personal Data", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    clickedFrmPersonalData = true;

                    studlastname = txtlastname.Text.Trim();
                    studfirstname = txtfirstname.Text.Trim();
                    studmidname = txtmidname.Text.Trim();

                    FrmMain.rptName = "RptHonorableDismissal";
                    FrmReportViewer frmReportViewer = new FrmReportViewer(FrmMain.paramrptUserFullname, "");
                    frmReportViewer.ShowDialog();
                    frmReportViewer.Dispose();

                    clickedFrmPersonalData = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            panelMenu.Size = new Size(168, 0);
            isCollapsedMenu = true;
        }

        private void BtnChangeCourse_Click(object sender, EventArgs e)
        {
            try
            {
                panelMenu.Size = new Size(168, 0);
                isCollapsedMenu = true;

                if (txtfcuidno.Text == "")
                {
                    MessageBox.Show("FCU ID or Student ID number must not be empty.", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    CurrentCourse = txtcourse.Text.Trim();
                    CurrentEnrolStatus = txtenrolstatus.Text.Trim();
                    CurrentSchoolYear = txtschlyr.Text.Trim();
                    CurrentSemester = cboPrgsem.Text.Trim();

                    //1.Get cis_category
                    //SELECT cis_category FROM mtbl_course
                    //WHERE cis_course = '" + FrmPersonalData.CurrentCourse + "'"
                    using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                    {
                        mySqlConnection1.Open();
                        using (MySqlCommand mySqlCommand1 = new MySqlCommand("SELECT cis_category, cis_idcode FROM mtbl_course WHERE cis_course = '" + CurrentCourse + "'", mySqlConnection1))
                        {
                            MySqlDataReader mySqlDataReader1 = mySqlCommand1.ExecuteReader();

                            if (mySqlDataReader1.HasRows)
                            {
                                if (mySqlDataReader1.Read())
                                {
                                    cisCategory = mySqlDataReader1["cis_category"].ToString();
                                    CurrentIdCode = mySqlDataReader1["cis_idcode"].ToString();

                                    if (cisCategory.ToUpper().Trim() == "HIGHER EDUCATION" || cisCategory.ToUpper().Trim() == "GRADUATE SCHOOL")
                                    {
                                        //2.Get cis_schlyr, cis_semno
                                        //SELECT cis_schlyr, cis_semno FROM mtbl_defaultperiod
                                        //WHERE cis_category = mtbl_course.cis_category AND cis_status = 1
                                        using (MySqlConnection mySqlConnection2 = new MySqlConnection(connectionString))
                                        { 
                                            mySqlConnection2.Open();
                                            using (MySqlCommand mySqlCommand2 = new MySqlCommand("SELECT cis_schlyr, cis_semno FROM mtbl_defaultperiod WHERE cis_category = '" + cisCategory + "' and cis_status = '1'", mySqlConnection2))
                                            {
                                                MySqlDataReader mySqlDataReader2 = mySqlCommand2.ExecuteReader();

                                                /*Check the number of default periods open*/
                                                int countDefaultPeriod = 0;
                                                while (mySqlDataReader2.Read())
                                                {
                                                    defaultSchlyr = mySqlDataReader2["cis_schlyr"].ToString();
                                                    defaultSemNo = mySqlDataReader2["cis_semno"].ToString();
                                                    countDefaultPeriod++;
                                                }

                                                /*****************************************************
                                                /*IF MORE THAN TWO DEFAULT PERIOD LET THE USER CHOOSE
                                                /*ELSE CHOOSE THE ONLY DEFAULT PERIOD*/
                                                /*****************************************************/
                                                if (countDefaultPeriod > 1)
                                                {
                                                    FrmDefaultPeriod frmDefaultPeriod = new FrmDefaultPeriod();
                                                    frmDefaultPeriod.ShowDialog();

                                                    if (frmDefaultPeriod.selectButtonClicked == true)
                                                    {
                                                        defaultSchlyr = frmDefaultPeriod.cisSchlyr;
                                                        defaultSemNo = frmDefaultPeriod.cisSemNo;
                                                        
                                                        ChangeCourse(defaultSchlyr, defaultSemNo);

                                                    }
                                                    frmDefaultPeriod.Dispose();
                                                }
                                                else 
                                                {
                                                    ChangeCourse(defaultSchlyr, defaultSemNo);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Can't change course. Changing of Course is applicable in HIGHER EDUCATION or GRADUATE SCHOOL only.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void ChangeCourse(string Schlyr, string SemNo)
        {
            try
            { 
                //3.Get cis_fcuidno, cis_course, cis_schlyr, cis_semester, cis_transid
                //SELECT cis_fcuidno, cis_course, cis_schlyr, cis_semester, cis_transid FROM mtbl_enrollment
                //WHERE cis_fcuidno = txtfcuidno.Text AND
                //   cis_course = txtcourse.Text AND
                //   cis_schlyr = mtbl_defaultperiod.cis_schlyr AND
                //   cis_semester = mtbl_defaultperiod.cis_semno
                //IF ENROLLED dont Process / If WITHDREW Process Change Course
                using (MySqlConnection mySqlConnection3 = new MySqlConnection(connectionString))
                {
                    mySqlConnection3.Open();
                    using (MySqlCommand mySqlCommand3 = new MySqlCommand("SELECT cis_fcuidno, cis_course, cis_schlyr, cis_semester, cis_enrolstatus, cis_transid FROM mtbl_enrollment WHERE cis_fcuidno = '" + txtfcuidno.Text + "' and cis_course = '" + CurrentCourse + "' and cis_schlyr = '" + Schlyr + "' and cis_semester = '" + SemNo + "'", mySqlConnection3))
                    {
                        MySqlDataReader mySqlDataReader3 = mySqlCommand3.ExecuteReader();

                        if (mySqlDataReader3.HasRows)
                        {
                            if (mySqlDataReader3.Read())
                            {
                                //withEnrollment = true will DELETE or UPDATE affected Tables; Processing in FrmChangeCourse.cs
                                withEnrollment = true;
                                cisTransId = mySqlDataReader3["cis_transid"].ToString();
                                cisEnrolStatus = mySqlDataReader3["cis_enrolstatus"].ToString();

                                if (cisEnrolStatus == "WITHDREW")
                                {
                                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to change student's course?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        FrmChangeCourse frmChangeCourse = new FrmChangeCourse();
                                        frmChangeCourse.ShowDialog();
                                        frmChangeCourse.Dispose();

                                        //If course was updated, retrieve latest student personal data
                                        if (frmChangeCourse.IsCourseUpdated == true)
                                        {
                                            Txtfcuidno_TextChanged(null, null);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Student is currently Enrolled. Change course in not allowed.\nPlease advise student to WITHDREW Enrolment prior to changing of course.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            //withEnrollment = false will UPDATE mtbl_studprofile Table; Processing in FrmChangeCourse.cs
                            withEnrollment = false;

                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to change student's course?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                FrmChangeCourse frmChangeCourse = new FrmChangeCourse();
                                frmChangeCourse.ShowDialog();
                                frmChangeCourse.Dispose();

                                //If course was updated, retrieve latest student personal data
                                if (frmChangeCourse.IsCourseUpdated == true)
                                {
                                    Txtfcuidno_TextChanged(null, null);
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                panelMenu.Size = new Size(168, 0);
                isCollapsedMenu = true;

                if (txtfcuidno.Text.Trim() == "" || txtstudno.Text.Trim() == "" || txtfirstname.Text.Trim() == "" || txtlastname.Text.Trim() == "")
                {
                    MessageBox.Show("Student's FCU ID Number, Student Number, First Name, and Last Name must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtlastname.Focus();
                }
                else if (dtpbirthdate.CustomFormat == " ")
                {
                    MessageBox.Show("Birth Date must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    dtpbirthdate.Focus();
                }
                else if (cbogender.SelectedIndex == -1)
                {
                    MessageBox.Show("Gender must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbogender.Focus();
                }
                //else if (cbocstatus.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Civil Status must not be empty.", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    cbocstatus.Focus();
                //}
                //else if (cbocitizen.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Citizenship must not be empty.", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    cbocitizen.Focus();
                //}
                else if (txtcontactno.Text.Trim() == "")
                {
                    MessageBox.Show("Contact Number must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtcontactno.Focus();
                }
                //else if (txtelemschl.Text.Trim() == "" || txtelemsy.Text.Trim() == "")
                //{
                //    MessageBox.Show("Elementary School and School Year must not be empty.", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    txtelemschl.Focus();
                //}
                //else if (txtsecondschl.Text.Trim() == "" || txtsecondsy.Text.Trim() == "")
                //{
                //    MessageBox.Show("Secondary School and School Year must not be empty.", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    txtsecondschl.Focus();
                //}
                //else if (cboprovince.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Select Province", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    cboprovince.Focus();
                //}
                //else if (cboprovince.Text != "OTHERS" && cbocity.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Select City / Municipality", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    cbocity.Focus();
                //}
                //else if (cboprovince.Text != "OTHERS" && cbobarangay.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Select Barangay", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    cbobarangay.Focus();
                //}
                //else if (txthomeadd.Text.Trim() == "")
                //{
                //    MessageBox.Show("Home Address must not be empty.", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    txthomeadd.Focus();
                //}
                else
                {
                    using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                    {
                        mysqlcon.Open();
                        using (MySqlCommand mysqlcmd = new MySqlCommand("mtbl_studprofile_update", mysqlcon))
                        {
                            mysqlcmd.CommandType = CommandType.StoredProcedure;

                            mysqlcmd.Parameters.AddWithValue("_cis_fcuidno", txtfcuidno.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_studno", txtstudno.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_firstname", txtfirstname.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_midname", txtmidname.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_lastname", txtlastname.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_suffix", txtsuffix.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_birthdate", dtpbirthdate.Value.Date.ToString("yyyy-MM-dd"));
                            mysqlcmd.Parameters.AddWithValue("_cis_age", txtage.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_birthplace", txtbirthplace.Text.Trim());
                            mysqlcmd.Parameters.AddWithValue("_cis_gender", cbogender.Text.Trim().ToUpper().Substring(0, 1));
                            mysqlcmd.Parameters.AddWithValue("_cis_cstatus", cbocstatus.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_citizen", cbocitizen.Text.Trim().ToUpper());

                            mysqlcmd.Parameters.AddWithValue("_cis_parentguardian", txtparentguardian.Text.ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_elemschl", txtelemschl.Text);
                            mysqlcmd.Parameters.AddWithValue("_cis_elemsy", txtelemsy.Text.Trim());
                            mysqlcmd.Parameters.AddWithValue("_cis_secondschl", txtsecondschl.Text);
                            mysqlcmd.Parameters.AddWithValue("_cis_secondsy", txtsecondsy.Text.Trim());
                            mysqlcmd.Parameters.AddWithValue("_cis_homeadd", txthomeadd.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_officeadd", txtofficeadd.Text.Trim().ToUpper());

                            mysqlcmd.Parameters.AddWithValue("_cis_prgsy", cboPrgSY.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_course", txtcourse.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_major", String.IsNullOrEmpty(major) ? 0 : (object)major);
                            mysqlcmd.Parameters.AddWithValue("_cis_yrlevel", cboYearLevel.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_schlyr", txtschlyr.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_semester", cboPrgsem.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_enrolstatus", txtenrolstatus.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_acadstat", cboAcadStat.Text.Trim().ToUpper());

                            if (dtpadmitdate.CustomFormat == " ") admitdate = null; else admitdate = dtpadmitdate.Value.Date.ToString("yyyy-MM-dd");
                            if (dtpgraddate.CustomFormat == " ") graddate = null; else graddate = dtpgraddate.Value.Date.ToString("yyyy-MM-dd");
                            mysqlcmd.Parameters.AddWithValue("_cis_admitdate", admitdate);
                            mysqlcmd.Parameters.AddWithValue("_cis_graddate", graddate);

                            mysqlcmd.Parameters.AddWithValue("_cis_admissioncred", txtadmissioncred.Text);
                            mysqlcmd.Parameters.AddWithValue("_cis_specialorder", txtspecialorder.Text.Trim().ToUpper());

                            if (dtpsodate.CustomFormat == " ") sodate = null; else sodate = dtpsodate.Value.Date.ToString("yyyy-MM-dd");
                            mysqlcmd.Parameters.AddWithValue("_cis_sodate", sodate);

                            if (chkuniting.Checked == true) uniting = 1; else uniting = 0;
                            if (chkirregular.Checked == true) irregular = 1; else irregular = 0;
                            if (chktransferee.Checked == true) transferee = 1; else transferee = 0;
                            if (chkcrossenrolee.Checked == true) crossenrolee = 1; else crossenrolee = 0;
                            mysqlcmd.Parameters.AddWithValue("_cis_uniting", uniting);
                            mysqlcmd.Parameters.AddWithValue("_cis_irregular", irregular);
                            mysqlcmd.Parameters.AddWithValue("_cis_transferee", transferee);
                            mysqlcmd.Parameters.AddWithValue("_cis_crossenrolee", crossenrolee);

                            mysqlcmd.Parameters.AddWithValue("_cis_profilepic", profilepic);
                            mysqlcmd.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);
                            mysqlcmd.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);

                            mysqlcmd.Parameters.AddWithValue("_cis_contactno", txtcontactno.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_contactno_parent", txtcontactnoparent.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_email", txtemail.Text.Trim().ToLower());
                            mysqlcmd.Parameters.AddWithValue("_cis_religion", cboreligion.Text.Trim().ToUpper());

                            mysqlcmd.Parameters.AddWithValue("_cis_province", cboprovince.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_city", String.IsNullOrEmpty(cbocity.Text) ? "NA" : (object)cbocity.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_barangay", String.IsNullOrEmpty(cbocity.Text) ? "NA" : (object)cbobarangay.Text.Trim().ToUpper());

                            mysqlcmd.Parameters.AddWithValue("_cis_street", txtstreet.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_zipcode", txtzipcode.Text.Trim().ToUpper());

                            mysqlcmd.Parameters.AddWithValue("_cis_mothfname", txtmothfname.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_mothmname", txtmothmname.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_mothlname", txtmothlname.Text.Trim().ToUpper());

                            mysqlcmd.Parameters.AddWithValue("_cis_fathfname", txtfathfname.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_fathmname", txtfathmname.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_fathlname", txtfathlname.Text.Trim().ToUpper());
                            mysqlcmd.Parameters.AddWithValue("_cis_capitaincome", cbocapitaincome.Text.Trim().ToUpper());

                            int isSaved = mysqlcmd.ExecuteNonQuery();
                            if (isSaved > 0)
                            {
                                lblstudno.Text = txtstudno.Text.Trim();

                                //********************************************************************
                                //Refetch and display the latest cis_accountability, cis_lastmodified
                                //********************************************************************
                                using (MySqlConnection mysqlcon1 = new MySqlConnection(connectionString))
                                {
                                    mysqlcon1.Open();
                                    using (MySqlCommand mysqlcmd1 = new MySqlCommand("SELECT cis_accountability, cis_lastmodified FROM mtbl_studprofile WHERE cis_fcuidno ='" + txtfcuidno.Text.Trim() + "'", mysqlcon1))
                                    {
                                        MySqlDataReader datareader1 = mysqlcmd1.ExecuteReader();

                                        if (datareader1.HasRows)
                                        {
                                            if (datareader1.Read())
                                            {
                                                tsslaccountability.Text = datareader1["cis_accountability"].ToString();
                                                tssllastmodified.Text = datareader1["cis_lastmodified"].ToString();
                                            }
                                        }
                                    }
                                }

                                //********************************************************************
                                //Update Year Level in [mtbl_enrollment, reg_subjenrolled, acc_assessment, acc_studledger]
                                //********************************************************************
                                using (MySqlConnection mySqlconn = new MySqlConnection(connectionString))
                                {
                                    mySqlconn.Open();
                                    using (MySqlCommand mySqlCmd = new MySqlCommand("mtbl_studprofile_yrlevel_update", mySqlconn))
                                    {
                                        mySqlCmd.CommandType = CommandType.StoredProcedure;

                                        mySqlCmd.Parameters.AddWithValue("_cis_fcuidno", txtfcuidno.Text.Trim().ToUpper());
                                        mySqlCmd.Parameters.AddWithValue("_cis_yrlevel", cboYearLevel.Text.Trim().ToUpper());
                                        mySqlCmd.Parameters.AddWithValue("_cis_schlyr", txtschlyr.Text.Trim().ToUpper());
                                        mySqlCmd.Parameters.AddWithValue("_cis_semester", cboPrgsem.Text.Trim().ToUpper());
                                        mySqlCmd.ExecuteNonQuery();
                                    }
                                }

                                MessageBox.Show("Student Personal Data updated successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else
                            {
                                MessageBox.Show("Unable to save Student Personal Data record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnSubjectEnrolled_Click(object sender, EventArgs e)
        {
            try
            {
                panelMenu.Size = new Size(168, 0);
                isCollapsedMenu = true;

                if (txtfcuidno.Text == "")
                {
                    MessageBox.Show("FCU ID or Student ID number must not be empty.", "Student Personal Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    FrmSubjectsEnrolled frmSubjectsEnrolled = new FrmSubjectsEnrolled();
                    frmSubjectsEnrolled.ShowDialog();
                    frmSubjectsEnrolled.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}