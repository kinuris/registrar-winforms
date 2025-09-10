using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmRecHigherEdGradSchool : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        private static string subjenrolledid, previousCreditsValue, previousDescriptionValue, previousCourseNoValue, previousSchlName;
        string profilepic, finalgrade, finalentrydate, completiongrade, completiondate, credits, description, courseno, schlname;

        public FrmRecHigherEdGradSchool()
        {
            InitializeComponent();

            ClearDgvSubjEnrolled();
            SubjEnrolledGridFill();
        }

        private void ClearDgvSubjEnrolled()
        {
            dgvSubjEnrolled.DataSource = null;
            dgvSubjEnrolled.Rows.Clear();
            dgvSubjEnrolled.Columns.Clear();
            dgvSubjEnrolled.Refresh();
        }

        private void SubjEnrolledGridFill()
        {
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(connectionString);
                mysqlcon.Open();

                MySqlDataAdapter sqlDA = new MySqlDataAdapter("reg_subjenrolled_viewbyid", mysqlcon);
                sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDA.SelectCommand.Parameters.AddWithValue("_cis_fcuidno", lblfcuidno.Text);

                DataTable dtbl = new DataTable();
                sqlDA.Fill(dtbl);
                dgvSubjEnrolled.DataSource = dtbl;
                dgvSubjEnrolled.Refresh();

                dgvSubjEnrolled.DefaultCellStyle.Font = new Font("Tahoma", 9);
                dgvSubjEnrolled.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 9);

                dgvSubjEnrolled.Columns[0].HeaderText = "Rec ID";
                dgvSubjEnrolled.Columns[0].Width = 80;

                dgvSubjEnrolled.Columns[1].HeaderText = "School Yr";
                dgvSubjEnrolled.Columns[1].Width = 80;

                dgvSubjEnrolled.Columns[2].HeaderText = "Semester";
                dgvSubjEnrolled.Columns[2].Width = 50;

                dgvSubjEnrolled.Columns[3].HeaderText = "Course No.";
                dgvSubjEnrolled.Columns[3].Width = 100;

                dgvSubjEnrolled.Columns[4].HeaderText = "Description";
                dgvSubjEnrolled.Columns[4].Width = 375;

                dgvSubjEnrolled.Columns[5].HeaderText = "Credit";
                dgvSubjEnrolled.Columns[5].Width = 50;

                dgvSubjEnrolled.Columns[6].HeaderText = "Final Grade";
                dgvSubjEnrolled.Columns[6].Width = 80;

                dgvSubjEnrolled.Columns[7].HeaderText = "Entry Date";
                dgvSubjEnrolled.Columns[7].Width = 80;

                dgvSubjEnrolled.Columns[8].HeaderText = "Completion Grade";
                dgvSubjEnrolled.Columns[8].Width = 80;

                dgvSubjEnrolled.Columns[9].HeaderText = "Completion Date";
                dgvSubjEnrolled.Columns[9].Width = 80;

                dgvSubjEnrolled.Columns[10].HeaderText = "School Name";
                dgvSubjEnrolled.Columns[10].Width = 250;

                dgvSubjEnrolled.Columns[11].HeaderText = "Remarks";
                dgvSubjEnrolled.Columns[11].Width = 80;

                dgvSubjEnrolled.Columns[12].HeaderText = "Modified By";
                dgvSubjEnrolled.Columns[13].HeaderText = "Date Last Modified";

                DataGridViewButtonColumn CmdEdit = new DataGridViewButtonColumn();
                {
                    CmdEdit.UseColumnTextForButtonValue = true;
                    CmdEdit.HeaderText = "Update";
                    CmdEdit.Name = "btnUpdate";
                    CmdEdit.Text = "Update";
                    CmdEdit.FlatStyle = FlatStyle.Flat;
                    CmdEdit.CellTemplate.Style.BackColor = Color.SteelBlue;
                    CmdEdit.CellTemplate.Style.ForeColor = Color.White;
                }
                dgvSubjEnrolled.Columns.Add(CmdEdit);
                dgvSubjEnrolled.Columns[14].Width = 56;

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
                dgvSubjEnrolled.Columns.Add(CmdDelete);
                dgvSubjEnrolled.Columns[15].Width = 56;

                sqlDA.Dispose();
                mysqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFreezeControls()
        {   
            lblcourseno.Text = "Course No";
            lbldesc.Text = "Description";
            lblschlyr.Text = "";
            lblsemester.Text = "";
            lblrecordid.Text = "";
            lblcredits.Text = "";
            lblremarks.Text = "";

            cbofgrade.SelectedIndex = -1;
            cbofgrade.Text = "";
            cbocgrade.SelectedIndex = -1; 
            cbocgrade.Text = "";

            dtpfgdate.Value = DateTime.Now;
            dtpfgdate.CustomFormat = " ";

            dtpcgrdate.Value = DateTime.Now;
            dtpcgrdate.CustomFormat = " ";

            cbofgrade.Enabled = false;
            cbocgrade.Enabled = false;

            dtpfgdate.Enabled = false;
            dtpcgrdate.Enabled = false;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAddSubjectEnrolled_Click(object sender, EventArgs e)
        {
            FrmRecTransfereesNew frmRecTransfereesNew = new FrmRecTransfereesNew();
            frmRecTransfereesNew.ShowDialog();
            frmRecTransfereesNew.Dispose();

            ClearDgvSubjEnrolled();
            SubjEnrolledGridFill();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchHigherEdGradSchool frmSearchHigherEdGradSchool = new FrmSearchHigherEdGradSchool();
                frmSearchHigherEdGradSchool.ShowDialog();

                if (frmSearchHigherEdGradSchool.viewButtonClicked == true)
                {
                    lblfcuidno.Text = FrmSearchHigherEdGradSchool.selectedfcuidno;
                    lblstudno.Text = FrmSearchHigherEdGradSchool.selectedstudno;
                    lblfullname.Text = FrmSearchHigherEdGradSchool.selectedfullname;
                    lblcourse.Text = FrmSearchHigherEdGradSchool.selectedcourse;

                    ClearFreezeControls();
                    ClearDgvSubjEnrolled();
                    SubjEnrolledGridFill();

                    using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                    {
                        mysqlcon.Open();

                        using (MySqlCommand command = new MySqlCommand("SELECT cis_profilepic FROM mtbl_studprofile WHERE cis_fcuidno ='" + lblfcuidno.Text + "'", mysqlcon))
                        {
                            MySqlDataReader datareader = command.ExecuteReader();

                            if (datareader.HasRows)
                            {
                                if (datareader.Read())
                                {
                                    // Load Web image in Picture Box
                                    
                                    ////string webServUrl = Environment.GetEnvironmentVariable("envWebServPath");
                                    ////profilepic = datareader["cis_profilepic"] as string ?? null;
                                    ////if (profilepic != null)
                                    ////{
                                    ////    WebRequest request = WebRequest.Create(webServUrl + profilepic);
                                    ////    using (var response = request.GetResponse())
                                    ////    {
                                    ////        using (var str = response.GetResponseStream())
                                    ////        {
                                    ////            cpbProfilePic.Image = Bitmap.FromStream(str);
                                    ////        }
                                    ////    }
                                    ////}
                                    ////else
                                    ////{
                                    ////    cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                                    ////}
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
                    }
                }

                frmSearchHigherEdGradSchool.Dispose();
            }
            catch (Exception ex)
            {   
                cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            if (lblfcuidno.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to refresh the Higher Education and Graduate School Form? All data will be cleared.", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    cpbProfilePic.Image = Properties.Resources.Default_Profile_Picture;
                    lblfcuidno.Text = "FCU ID Number";
                    lblstudno.Text = "Student ID No.";
                    lblfullname.Text = "Student's Name";
                    lblcourse.Text = "Course";
                    lblyrlevel.Text = "Year Level";
                    lblschlyr.Text = "School Year";
                    lblsemester.Text = "Semester";

                    ClearFreezeControls();
                    ClearDgvSubjEnrolled();
                    SubjEnrolledGridFill();

                    tssllastmodified.Text = "mm/dd/yyyy";
                    lblaccountability.Text = "accountability";
                }
            }
        }

        private void Lblfcuidno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblfcuidno.Text != "FCU ID Number")
                {
                    using (MySqlConnection mysqlconnection = new MySqlConnection(connectionString))
                    {
                        mysqlconnection.Open();
                        
                        using (MySqlCommand command = new MySqlCommand("SELECT * FROM mtbl_studprofile WHERE cis_fcuidno = '" + lblfcuidno.Text + "'", mysqlconnection))
                        {
                            MySqlDataReader datareader = command.ExecuteReader();

                            if (datareader.HasRows)
                            {
                                if (datareader.Read())
                                {
                                    lblcourse.Text = datareader["cis_course"].ToString();
                                    lblyrlevel.Text = datareader["cis_yrlevel"].ToString();
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

        private void Dtpfgdate_ValueChanged(object sender, EventArgs e)
        {
            dtpfgdate.CustomFormat = "MMM dd, yyyy";
        }

        private void Dtpfgdate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtpfgdate.CustomFormat = " ";
            }
        }

        private void Dtpcgrdate_ValueChanged(object sender, EventArgs e)
        {
            dtpcgrdate.CustomFormat = "MMM dd, yyyy";
        }

        private void Dtpcgrdate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtpcgrdate.CustomFormat = " ";
            }
        }

        private void DgvSubjEnrolled_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Enable Credits Cell
            if (e.ColumnIndex == dgvSubjEnrolled.Columns[5].Index)
            {
                DataGridViewCell dataGridViewCell = dgvSubjEnrolled.Rows[e.RowIndex].Cells[5];
                dgvSubjEnrolled.CurrentCell = dataGridViewCell;

                dgvSubjEnrolled.ReadOnly = false;
                dgvSubjEnrolled.BeginEdit(true);
            }
            //Enable Description Cell
            else if (e.ColumnIndex == dgvSubjEnrolled.Columns[4].Index)
            {
                DataGridViewCell dataGridViewCell = dgvSubjEnrolled.Rows[e.RowIndex].Cells[4];
                dgvSubjEnrolled.CurrentCell = dataGridViewCell;

                dgvSubjEnrolled.ReadOnly = false;
                dgvSubjEnrolled.BeginEdit(true);
            }
            //Enable Course No. Cell
            else if (e.ColumnIndex == dgvSubjEnrolled.Columns[3].Index)
            {
                DataGridViewCell dataGridViewCell = dgvSubjEnrolled.Rows[e.RowIndex].Cells[3];
                dgvSubjEnrolled.CurrentCell = dataGridViewCell;

                dgvSubjEnrolled.ReadOnly = false;
                dgvSubjEnrolled.BeginEdit(true);
            }
            //Enable SchoolName Cell
            else if (e.ColumnIndex == dgvSubjEnrolled.Columns[10].Index)
            {
                DataGridViewCell dataGridViewCell = dgvSubjEnrolled.Rows[e.RowIndex].Cells[10];
                dgvSubjEnrolled.CurrentCell = dataGridViewCell;

                dgvSubjEnrolled.ReadOnly = false;
                dgvSubjEnrolled.BeginEdit(true);
            }
            else
            {
                dgvSubjEnrolled.ReadOnly = true;
                dgvSubjEnrolled.BeginEdit(false);
            }
        }

        private void DgvSubjEnrolled_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            previousCreditsValue = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[5].Value);
            previousDescriptionValue = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[4].Value);
            previousCourseNoValue = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[3].Value);
            previousSchlName = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[10].Value);
        }

        private void DgvSubjEnrolled_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvSubjEnrolled.ReadOnly = true;
            dgvSubjEnrolled.BeginEdit(false);
        }

        private void DgvSubjEnrolled_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    subjenrolledid = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[0].Value);
                    credits = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[5].Value);
                    description = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[4].Value);
                    courseno = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[3].Value);
                    schlname = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells[10].Value);

                    //if Credits Value Changed or Updated. Update automatically
                    if (e.ColumnIndex == dgvSubjEnrolled.Columns[5].Index)
                    {
                        if (dgvSubjEnrolled.CurrentRow != null && previousCreditsValue != credits)
                        {
                            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                            {
                                mysqlcon.Open();
                                using (MySqlCommand mysqlcmd = new MySqlCommand("UPDATE reg_subjenrolled SET cis_credits = '" + credits.Trim() + "' WHERE id = '" + subjenrolledid + "'", mysqlcon))
                                {
                                    mysqlcmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    //if Desription Value Changed or Updated. Update automatically
                    else if (e.ColumnIndex == dgvSubjEnrolled.Columns[4].Index)
                    {
                        if (dgvSubjEnrolled.CurrentRow != null && previousDescriptionValue != description)
                        {
                            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                            {
                                mysqlcon.Open();
                                using (MySqlCommand mysqlcmd = new MySqlCommand("UPDATE reg_subjenrolled SET cis_desc = '" + description.Replace("'", "''") + "' WHERE id = '" + subjenrolledid + "'", mysqlcon))
                                {
                                    mysqlcmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    //if Course No. Value Changed or Updated. Update automatically
                    else if (e.ColumnIndex == dgvSubjEnrolled.Columns[3].Index)
                    {
                        if (dgvSubjEnrolled.CurrentRow != null && previousCourseNoValue != courseno)
                        {
                            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                            {
                                mysqlcon.Open();
                                using (MySqlCommand mysqlcmd = new MySqlCommand("UPDATE reg_subjenrolled SET cis_courseno = '" + courseno.Trim() + "' WHERE id = '" + subjenrolledid + "'", mysqlcon))
                                {
                                    mysqlcmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    //if SchoolName Value Changed or Updated. Update automatically
                    else if (e.ColumnIndex == dgvSubjEnrolled.Columns[10].Index)
                    {
                        if (dgvSubjEnrolled.CurrentRow != null && previousSchlName != schlname)
                        {
                            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                            {
                                mysqlcon.Open();
                                using (MySqlCommand mysqlcmd = new MySqlCommand("UPDATE reg_subjenrolled SET cis_schlname = '" + schlname.Replace("'", "''") + "' WHERE id = '" + subjenrolledid + "'", mysqlcon))
                                {
                                    mysqlcmd.ExecuteNonQuery();
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

        private void DgvSubjEnrolled_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= AllowNumbersOnly;

            if (dgvSubjEnrolled.CurrentCell.ColumnIndex == 5)
            {
                e.Control.KeyPress += AllowNumbersOnly;
            }
        }

        private void AllowNumbersOnly(Object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void DgvSubjEnrolled_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //****************************************************************************
                //edit button clicked
                //****************************************************************************   
                if (e.ColumnIndex == dgvSubjEnrolled.Columns["btnUpdate"].Index)
                {                
                    FrmAuthorizationLogin frmAuthorizationLogin = new FrmAuthorizationLogin();
                    frmAuthorizationLogin.ShowDialog();

                    if (frmAuthorizationLogin.isAuthorized == true)
                    {
                        subjenrolledid = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells["id"].Value);

                        using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                        {
                            mysqlcon.Open();
                            using (MySqlCommand Command = new MySqlCommand("SELECT id, cis_schlyr, cis_semester, cis_courseno, cis_desc, cis_credits, cis_remarks, cis_fgrade, cis_fgdate, cis_cgrade, cis_cgrdate from reg_subjenrolled where id ='" + subjenrolledid + "'", mysqlcon))
                            {
                                MySqlDataReader DataReader = Command.ExecuteReader();

                                if (DataReader.HasRows)
                                {
                                    if (DataReader.Read())
                                    {
                                        lblschlyr.Text = DataReader["cis_schlyr"].ToString();
                                        lblsemester.Text = DataReader["cis_semester"].ToString();
                                        lblrecordid.Text = DataReader["id"].ToString();
                                        lblcourseno.Text = DataReader["cis_courseno"].ToString();
                                        lbldesc.Text = DataReader["cis_desc"].ToString();
                                        lblcredits.Text = DataReader["cis_credits"].ToString();
                                        lblremarks.Text = DataReader["cis_remarks"].ToString();

                                        //final grade
                                        finalgrade = DataReader["cis_fgrade"].ToString();
                                        if (finalgrade != "")
                                            cbofgrade.Text = finalgrade;
                                        else
                                            cbofgrade.SelectedIndex = -1;

                                        //final grade entry date
                                        finalentrydate = DataReader["cis_fgdate"].ToString();
                                        if (finalentrydate != "")
                                            dtpfgdate.Value = Convert.ToDateTime(finalentrydate);
                                        else
                                        {
                                            dtpfgdate.Value = DateTime.Now;
                                            dtpfgdate.CustomFormat = " ";
                                        }

                                        //completion grade
                                        completiongrade = DataReader["cis_cgrade"].ToString();
                                        if (completiongrade != "")
                                            cbocgrade.Text = completiongrade;
                                        else
                                        {
                                            cbocgrade.SelectedIndex = -1;
                                            cbocgrade.Text = "";
                                        }

                                        //completion grade entry date
                                        completiondate = DataReader["cis_cgrdate"].ToString();
                                        if (completiondate != "")
                                            dtpcgrdate.Value = Convert.ToDateTime(completiondate);
                                        else
                                        {
                                            dtpcgrdate.Value = DateTime.Now;
                                            dtpcgrdate.CustomFormat = " ";
                                        }
                                    }

                                    cbofgrade.Enabled = true;
                                    dtpfgdate.Enabled = true;

                                    if (cbofgrade.Text == "9" || cbofgrade.Text == "9.0" || cbofgrade.Text == "9.00" || cbofgrade.Text == "INC")
                                    {
                                        cbocgrade.Enabled = true;
                                        dtpcgrdate.Enabled = true;
                                    }
                                    else
                                    {
                                        cbocgrade.Enabled = false;
                                        dtpcgrdate.Enabled = false;
                                    }
                                }
                            }
                        }
                    }

                    frmAuthorizationLogin.Dispose();
                }

                //****************************************************************************
                //delete button clicked
                //****************************************************************************
                if (e.ColumnIndex == dgvSubjEnrolled.Columns["btnDelete"].Index)
                {
                    FrmAuthorizationLogin frmAuthorizationLogin = new FrmAuthorizationLogin();
                    frmAuthorizationLogin.ShowDialog();

                    if (frmAuthorizationLogin.isAuthorized == true)
                    {
                        subjenrolledid = Convert.ToString(dgvSubjEnrolled.Rows[e.RowIndex].Cells["id"].Value);

                        DialogResult dialogresult = MessageBox.Show("Are you sure you want to Delete this record?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (dialogresult == DialogResult.Yes)
                        {
                            using (MySqlConnection mySqlConnection1 = new MySqlConnection(connectionString))
                            {
                                mySqlConnection1.Open();

                                using (MySqlCommand mySqlCommand1 = new MySqlCommand("DELETE FROM reg_subjenrolled WHERE id = '" + subjenrolledid + "'", mySqlConnection1))
                                {
                                    int isDeleted = mySqlCommand1.ExecuteNonQuery();
                                    if (isDeleted > 0)
                                    {
                                        ClearFreezeControls();
                                        ClearDgvSubjEnrolled();
                                        SubjEnrolledGridFill();

                                        tssllastmodified.Text = "mm/dd/yyyy";
                                        lblaccountability.Text = "accountability";

                                        MessageBox.Show("Deleted Successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Unable to delete record.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }

                    frmAuthorizationLogin.Dispose();

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
                if (lblrecordid.Text == "")
                {
                    MessageBox.Show("Subject record must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    lblrecordid.Focus();
                }
                else if (cbofgrade.Text.Trim() == "")
                {   
                    MessageBox.Show("Final Grade must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbofgrade.Focus();
                }
                else if (dtpfgdate.CustomFormat == " ")
                {
                    MessageBox.Show("Final Grade Entry Date must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    dtpfgdate.Focus();
                }
                else if (cbocgrade.SelectedIndex >= 0 && dtpcgrdate.CustomFormat == " ")
                {
                    MessageBox.Show("Completion Date must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    dtpcgrdate.Focus();
                }
                else if (cbocgrade.SelectedIndex == -1 && dtpcgrdate.CustomFormat != " ")
                {
                    MessageBox.Show("Completion Grade must not be empty.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbocgrade.Focus();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to update the student's grade?", "FilCIS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //Select cis_credits from reg_curriculum                        
                        using (MySqlConnection mysqlconnection = new MySqlConnection(connectionString))
                        {
                            mysqlconnection.Open();

                            using (MySqlCommand command = new MySqlCommand("SELECT A.cis_credits FROM reg_curriculum A, reg_subjenrolled B WHERE A.cis_coursenoid = B.cis_coursenoid and B.id  = '" + subjenrolledid + "'", mysqlconnection))
                            {
                                MySqlDataReader datareader = command.ExecuteReader();

                                if (datareader.HasRows)
                                {
                                    if (datareader.Read())
                                        credits = datareader["cis_credits"].ToString();
                                }
                                else
                                {
                                    credits = lblcredits.Text;
                                }
                            }
                        }

                        //Removes the space and succeeding texts(final grade).
                        string finalGrade = cbofgrade.Text.Trim().ToUpper();
                        if (cbofgrade.SelectedIndex >= 10)
                        {
                            int indexofspace = finalGrade.IndexOf(" ");
                            finalGrade = finalGrade.Remove(indexofspace);
                        }

                        //Removes the space and succeeding texts(completion grade).
                        string completionGrade = cbocgrade.Text.Trim().ToUpper();
                        if (cbocgrade.SelectedIndex >= 9)
                        {
                            int indexofspace = completionGrade.IndexOf(" ");
                            completionGrade = completionGrade.Remove(indexofspace);
                        }

                        using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
                        {
                            mysqlcon.Open();
                            using (MySqlCommand mysqlcmd = new MySqlCommand("reg_subjenrolled_update", mysqlcon))
                            {
                                mysqlcmd.CommandType = CommandType.StoredProcedure;

                                mysqlcmd.Parameters.AddWithValue("_id", lblrecordid.Text.Trim().ToUpper());
                                mysqlcmd.Parameters.AddWithValue("_cis_credits", credits);
                                mysqlcmd.Parameters.AddWithValue("_cis_passed", "1");
                                mysqlcmd.Parameters.AddWithValue("_cis_fgrade", finalGrade);
                                mysqlcmd.Parameters.AddWithValue("_cis_fgdate", dtpfgdate.Value.Date.ToString("yyyy-MM-dd"));
                                mysqlcmd.Parameters.AddWithValue("_cis_lastmodified", DateTime.Now);
                                mysqlcmd.Parameters.AddWithValue("_cis_accountability", FrmLogin.username);

                                //Completion Grade value
                                if (cbocgrade.SelectedIndex == -1)
                                    mysqlcmd.Parameters.AddWithValue("_cis_cgrade", null);
                                else
                                    mysqlcmd.Parameters.AddWithValue("_cis_cgrade", completionGrade);

                                //Completion Grade Date value
                                if (dtpcgrdate.CustomFormat == " ")
                                    mysqlcmd.Parameters.AddWithValue("_cis_cgrdate", null);
                                else
                                    mysqlcmd.Parameters.AddWithValue("_cis_cgrdate", dtpcgrdate.Value.Date.ToString("yyyy-MM-dd"));

                                mysqlcmd.ExecuteNonQuery();
                                MessageBox.Show("Student's Grade updated successfully.", "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                ClearFreezeControls();
                                ClearDgvSubjEnrolled();
                                SubjEnrolledGridFill();

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