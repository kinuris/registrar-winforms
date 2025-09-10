using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic.FileIO;
using System.Configuration;


namespace PrjRegistrar
{
    public partial class FrmRecImportGrades : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        string profilepic, selectStatement, strFcuIDNo; 
        readonly FrmWaitFormFunc frmwaitformfunc = new FrmWaitFormFunc();

        public FrmRecImportGrades()
        {
            InitializeComponent();
            ClearFields();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ClearFields();

            rdoIndividual.Checked = true;
            rdoMultiple.Checked = false;
        }

        private void BtnSearchStudent_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmSearchPersonalData frmSearchPersonalData = new FrmSearchPersonalData())
                {
                    frmSearchPersonalData.ShowDialog();

                    if (frmSearchPersonalData.viewButtonClicked == true)
                    {
                        lblfullname.Text = FrmSearchPersonalData.selectedfullname;
                        lblstudno.Text = FrmSearchPersonalData.selectedstudno;
                        lblfcuidno.Text = FrmSearchPersonalData.selectedfcuidno;
                        lblcourse.Text = FrmSearchPersonalData.selectedcourse;

                        txtFlatFile.Text = "";
                        ClearDgvSubjEnrolled();

                        frmwaitformfunc.Show(this);
                        StudentProfileLoad(lblfcuidno.Text);
                        frmwaitformfunc.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                frmwaitformfunc.Close();
                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StudentProfileLoad(string strfcuidno)
        {
            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
            { 
                mysqlcon.Open();

                selectStatement = "SELECT A.cis_profilepic, A.cis_studno, A.cis_yrlevel, A.cis_semester, A.cis_schlyr, concat(A.cis_lastname, ', ', ucase(left(A.cis_firstname, 1)), lcase(substring(A.cis_firstname, 2)), ' ', ucase(left(A.cis_midname, 1)), lcase(substring(A.cis_midname, 2))) as fullname " +                              
                                  "FROM mtbl_studprofile A WHERE A.cis_fcuidno = '" + strfcuidno + "'";
                using (MySqlCommand command = new MySqlCommand(selectStatement, mysqlcon))
                {
                    MySqlDataReader datareader = command.ExecuteReader();

                    if (datareader.HasRows)
                    {
                        if (datareader.Read())
                        {
                            // Load fullname, etc.
                            lblfullname.Text = datareader["fullname"].ToString();
                            lblstudno.Text = datareader["cis_studno"].ToString();
                            lblyrlevel.Text = datareader["cis_yrlevel"].ToString();
                            lblsemester.Text = datareader["cis_semester"].ToString();
                            lblschlyr.Text = datareader["cis_schlyr"].ToString();

                            // Load Web image in Picture Box
                            string webServUrl = Environment.GetEnvironmentVariable("envWebServPath");
                            profilepic = datareader["cis_profilepic"] as string ?? null;
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
                            else
                            {
                                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                            }
                        }
                    }
                }
            }
        }

        private void ClearFields()
        {
            cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;

            lblfullname.Text = "Student's Name";
            lblfcuidno.Text = "FCU ID Number";
            lblcourse.Text = "Course";
            lblyrlevel.Text = "Year Level";
            lblsemester.Text = "Semester";
            lblschlyr.Text = "School Year";
            lblstudno.Text = "Student ID No.";

            txtFlatFile.Text = "";

            ClearDgvSubjEnrolled();
            tssllastmodified.Text = "mm/dd/yyyy";
            lblaccountability.Text = "accountability";
        }

        private void ClearDgvSubjEnrolled()
        {
            dgvSubjEnrolled.DataSource = null;
            dgvSubjEnrolled.Rows.Clear();
            dgvSubjEnrolled.Columns.Clear();
            dgvSubjEnrolled.Refresh();
        }

        private void BtnSearchFlatFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoIndividual.Checked == true && lblfullname.Text == "Student's Name")
                {
                    MessageBox.Show("Click the Search for Student button to select student's name.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "CSV|*.csv", Multiselect = false })
                    {
                        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("FCU ID No");
                            dataTable.Columns.Add("Student No");
                            dataTable.Columns.Add("Course");
                            dataTable.Columns.Add("School Year");
                            dataTable.Columns.Add("Semester");
                            dataTable.Columns.Add("Year Level");
                            dataTable.Columns.Add("Course No");
                            dataTable.Columns.Add("Description");
                            dataTable.Columns.Add("Credits");
                            dataTable.Columns.Add("Final Grade");
                            dataTable.Columns.Add("FG Date");
                            dataTable.Columns.Add("Completion Grade");
                            dataTable.Columns.Add("CG Date");
                            dataTable.Columns.Add("School Name");
                            dataTable.Columns.Add("Accountability");
                            dataTable.Columns.Add("Class ID");

                            txtFlatFile.Text = openFileDialog.FileName;
                            using (TextFieldParser csvReader = new TextFieldParser(openFileDialog.FileName))
                            {
                                csvReader.SetDelimiters(new string[] { "," });
                                csvReader.HasFieldsEnclosedInQuotes = true;

                                frmwaitformfunc.Show(this);

                                while (!csvReader.EndOfData)
                                {
                                    string[] fieldData = csvReader.ReadFields();

                                    for (int i = 0; i < fieldData.Length; i++)
                                    {
                                        //Replace dates value [--] or [//] with [null]
                                        if (fieldData[i].Replace(" ", "") == "--" || fieldData[i].Replace(" ", "") == "//")
                                        {
                                            fieldData[i] = null;
                                        }

                                        //Replace completion grade value [0] with [null]
                                        if (i == 13 && fieldData[i] == "0")
                                        {
                                            fieldData[i] = null;
                                        }
                                    }

                                    //Check csv file for validity of content
                                    if (csvReader.LineNumber == 2 && fieldData[0] != "stud_no")
                                    {
                                        ClearDgvSubjEnrolled();
                                        MessageBox.Show("Invalid Items. Please check the content of this File.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    //Look for cis_fcuidno FROM mtbl_studprofile based on its stud_no
                                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                                    {
                                        mySqlConnection.Open();

                                        selectStatement = "SELECT cis_fcuidno FROM mtbl_studprofile WHERE cis_studno = '" + fieldData[0] + "'";

                                        using (MySqlCommand mySqlCommand = new MySqlCommand(selectStatement, mySqlConnection))
                                        {
                                            MySqlDataReader datareader = mySqlCommand.ExecuteReader();

                                            if (datareader.HasRows)
                                            {
                                                if (datareader.Read())
                                                {
                                                    strFcuIDNo = datareader["cis_fcuidno"].ToString();
                                                }
                                            }
                                            else
                                            {
                                                strFcuIDNo = "";
                                            }
                                        }
                                    }

                                    //Add rows to dataTable from CSV File. Starts with csvReader.LineNumber = 3
                                    if (csvReader.LineNumber >= 3 || csvReader.LineNumber == -1)
                                    {
                                        dataTable.Rows.Add(new object[] { strFcuIDNo, fieldData[0], fieldData[1], fieldData[9], fieldData[11], fieldData[10], fieldData[2],
                                                                    fieldData[3] + " " + fieldData[4] + " " + fieldData[5] + " " + fieldData[23] + " " + fieldData[24],
                                                                    fieldData[6], fieldData[7], fieldData[12], fieldData[13], fieldData[14], fieldData[18], fieldData[8], fieldData[28] });
                                    }
                                }

                                frmwaitformfunc.Close();

                            }

                            dgvSubjEnrolled.DataSource = dataTable;

                            if (dgvSubjEnrolled.Rows.Count == 0)
                            {
                                MessageBox.Show("There is no data in this file", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                frmwaitformfunc.Close();
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RdoIndividual_CheckedChanged(object sender, EventArgs e)
        {
            btnSearchStudent.Enabled = true;

            lblfullname.ForeColor = Color.Black;
            lblfcuidno.ForeColor = Color.Black;
            lblcourse.ForeColor = Color.Black;
            lblyrlevel.ForeColor = Color.Black;
            lblsemester.ForeColor = Color.Black;
            lblschlyr.ForeColor = Color.Black;

            ClearFields();
        }

        private void RdoMultiple_CheckedChanged(object sender, EventArgs e)
        {
            btnSearchStudent.Enabled = false;

            lblfullname.ForeColor = Color.Gray;
            lblfcuidno.ForeColor = Color.Gray;
            lblcourse.ForeColor = Color.Gray;
            lblyrlevel.ForeColor = Color.Gray;
            lblsemester.ForeColor = Color.Gray;
            lblschlyr.ForeColor = Color.Gray;

            ClearFields();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoIndividual.Checked == true && lblfullname.Text == "Student's Name")
                {
                    MessageBox.Show("Click the Search for Student button to select student's name.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearchStudent.Focus();
                }
                else if (txtFlatFile.Text.Trim().Replace(" ", "") == "")
                {
                    MessageBox.Show("Click the Search for CSV Flat File button.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearchFlatFile.Focus();
                }
                else if (dgvSubjEnrolled.DataSource == null)
                {
                    MessageBox.Show("Unable to import file. Click the Search for CSV Flat File button.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearchFlatFile.Focus();
                }
                else
                {
                    DataTable dataTable = (DataTable)dgvSubjEnrolled.DataSource;
                    string InsertQuery = "";
                    int countImported = 0, countInvalid = 0;

                    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            int dataRowIndex = dataTable.Rows.IndexOf(dataRow);

                            selectStatement = "SELECT cis_fcuidno, cis_studno, cis_course, cis_schlyr, cis_semester, cis_yrlevel, cis_courseno, cis_desc, cis_credits, cis_fgrade, cis_cgrade, cis_schlname, cis_accountability, cis_classid FROM reg_subjenrolled WHERE " +
                                                "cis_fcuidno = '" + dataRow["FCU ID No"] + "' AND " +
                                                "cis_studno = '" + dataRow["Student No"] + "' AND " +
                                                "cis_course = '" + dataRow["Course"] + "' AND " +
                                                "cis_schlyr = '" + dataRow["School Year"] + "' AND " +
                                                "cis_semester = '" + dataRow["Semester"] + "' AND " +
                                                "cis_yrlevel = '" + dataRow["Year Level"] + "' AND " +
                                                "cis_courseno = '" + dataRow["Course No"].ToString().Trim() + "' AND " +
                                                "cis_desc = '" + dataRow["Description"].ToString().Trim().Replace("'", "''") + "' AND " +
                                                "cis_credits = '" + dataRow["Credits"].ToString().Trim() + "' AND " +
                                                "cis_fgrade = '" + dataRow["Final Grade"].ToString().Trim() + "' AND " +
                                                "cis_schlname = '" + dataRow["School Name"].ToString().Trim() + "'";
                                                
                            using (MySqlCommand mySqlCommand = new MySqlCommand(selectStatement, mySqlConnection))
                            {
                                using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                                {
                                    //***********************************************************************************
                                    //Check if already exist, to eliminate double entry.     BackColor = Color.OrangeRed;
                                    //***********************************************************************************
                                    if (mySqlDataReader.HasRows)
                                    {
                                        dgvSubjEnrolled.Rows[dataRowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
                                        countInvalid++;
                                    }
                                    else
                                    {
                                        using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                                        {
                                            mySqlConnection1.Open();
                                            
                                            InsertQuery = "INSERT INTO reg_subjenrolled (cis_fcuidno, cis_studno, cis_course, cis_schlyr, cis_semester, cis_yrlevel, cis_courseno, cis_desc, " +
                                                                                    "cis_credits, cis_fgrade, cis_fgdate, cis_cgrade, cis_cgrdate, cis_schlname, cis_accountability, cis_lastmodified, cis_classid) " +
                                                                            "VALUES (@cis_fcuidno, @cis_studno, @cis_course, @cis_schlyr, @cis_semester, @cis_yrlevel, @cis_courseno, @cis_desc, " +
                                                                                    "@cis_credits, @cis_fgrade, @cis_fgdate, @cis_cgrade, @cis_cgrdate, @cis_schlname, @cis_accountability, @cis_lastmodified, @cis_classid);";

                                            using (MySqlCommand mySqlCommand1 = new MySqlCommand(InsertQuery, mySqlConnection1))
                                            {
                                                mySqlCommand1.Parameters.AddWithValue("@cis_fcuidno", dataRow["FCU ID No"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_studno", dataRow["Student No"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_course", dataRow["Course"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_schlyr", dataRow["School Year"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_semester", dataRow["Semester"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_yrlevel", dataRow["Year Level"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_courseno", dataRow["Course No"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_desc", dataRow["Description"].ToString().Trim().Replace("'", "''"));
                                                mySqlCommand1.Parameters.AddWithValue("@cis_credits", dataRow["Credits"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_fgrade", dataRow["Final Grade"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_cgrade", dataRow["Completion Grade"]);

                                                if (dataRow["FG Date"] == DBNull.Value)
                                                    mySqlCommand1.Parameters.AddWithValue("@cis_fgdate", DBNull.Value);
                                                else
                                                {
                                                    DateTime dtfgdate = Convert.ToDateTime(dataRow["FG Date"]);
                                                    string strfgdate = dtfgdate.ToString("yyyy-MM-dd");

                                                    mySqlCommand1.Parameters.AddWithValue("@cis_fgdate", strfgdate);
                                                }

                                                if (dataRow["CG Date"] == DBNull.Value)
                                                    mySqlCommand1.Parameters.AddWithValue("@cis_cgrdate", DBNull.Value);
                                                else
                                                {
                                                    DateTime dtcgrdate = Convert.ToDateTime(dataRow["CG Date"]);
                                                    string strcgrdate = dtcgrdate.ToString("yyyy-MM-dd");

                                                    mySqlCommand1.Parameters.AddWithValue("@cis_cgrdate", strcgrdate);
                                                }

                                                mySqlCommand1.Parameters.AddWithValue("@cis_schlname", dataRow["School Name"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_accountability", dataRow["Accountability"]);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_lastmodified", DateTime.Now);
                                                mySqlCommand1.Parameters.AddWithValue("@cis_classid", dataRow["Class ID"]);

                                                int isSaved = mySqlCommand1.ExecuteNonQuery();
                                                if (isSaved > 0) countImported++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (countImported >= 1)
                    {
                        if (countInvalid == 0)
                            MessageBox.Show("Imported Grades successfully. \nTotal imported records : " + countImported + " \nTotal invalid records : " + countInvalid + "", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Imported Grades successfully but with invalid records. \nTotal imported records : " + countImported + " \nTotal invalid records : " + countInvalid + "", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Unable to Import Records in the CSV Flat File. \nTotal imported records : " + countImported + " \nTotal invalid records : " + countInvalid + "", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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