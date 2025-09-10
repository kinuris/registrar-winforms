using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmSearchHigherEdGradSchool : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        public static string selectedfcuidno, selectedstudno, selectedfullname, selectedcourse;
        public bool viewButtonClicked;
        public MySqlDataAdapter sqlDA;
        public DataSet pagingDS;
        int scrollVal;

        public FrmSearchHigherEdGradSchool()
        {
            scrollVal = 0;
            viewButtonClicked = false;
            InitializeComponent();
            txtsearch.Focus();
        }

        private void FrmSearchHigherEdGradSchool_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(connectionString);
                mysqlcon.Open();

                sqlDA = new MySqlDataAdapter("reg_subjenrolled_search", mysqlcon);
                sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dtbl = new DataTable();
                pagingDS = new DataSet();

                sqlDA.Fill(dtbl);
                sqlDA.Fill(pagingDS, scrollVal, dtbl.Rows.Count, "reg_subjenrolled");

                dgvStudent.DataSource = pagingDS;
                dgvStudent.DataMember = "reg_subjenrolled";

                lblRecordCount.Text = dtbl.Rows.Count.ToString();
                lblRowsCount.Text = "Record  " + (scrollVal + 1) + "  of Top  " + lblRecordCount.Text;

                HeaderTexts();

                mysqlcon.Close();

                txtsearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void DgvStudent_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void HeaderTexts()
        {
            dgvStudent.Columns[0].HeaderText = "Student ID";
            dgvStudent.Columns[1].HeaderText = "Student's Name";
            dgvStudent.Columns[2].HeaderText = "Course";
            dgvStudent.Columns[3].HeaderText = "FCU ID";

            dgvStudent.Columns[0].Width = 120;
            dgvStudent.Columns[1].Width = 300;
            dgvStudent.Columns[2].Width = 70;
            dgvStudent.Columns[3].Width = 155;

            DataGridViewButtonColumn BtnView = new DataGridViewButtonColumn();
            {
                BtnView.UseColumnTextForButtonValue = true;
                BtnView.HeaderText = "Action";
                BtnView.Name = "btnView";
                BtnView.Text = "View";
                BtnView.FlatStyle = FlatStyle.Flat;
                BtnView.CellTemplate.Style.BackColor = Color.SteelBlue;
                BtnView.CellTemplate.Style.ForeColor = Color.White;
            }
            dgvStudent.Columns.Add(BtnView);
            dgvStudent.Columns[4].Width = 60;
        }

        private void CmdSearch_Click(object sender, EventArgs e)
        {
            Txtsearch_TextChanged(null, null);
        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                scrollVal = 0;

                MySqlConnection mysqlcon = new MySqlConnection(connectionString);
                mysqlcon.Open();

                MySqlDataAdapter sqlDA = new MySqlDataAdapter("reg_subjenrolled_searchbyparam", mysqlcon);
                sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDA.SelectCommand.Parameters.AddWithValue("_cis_studno", txtsearch.Text);
                sqlDA.SelectCommand.Parameters.AddWithValue("_cis_lastname", txtsearch.Text);
                sqlDA.SelectCommand.Parameters.AddWithValue("_cis_firstname", txtsearch.Text);

                dgvStudent.DataSource = null;
                dgvStudent.Rows.Clear();
                dgvStudent.Columns.Clear();

                DataTable dtbl = new DataTable();
                pagingDS = new DataSet();

                sqlDA.Fill(dtbl);
                sqlDA.Fill(pagingDS, scrollVal, dtbl.Rows.Count, "reg_subjenrolled");

                dgvStudent.DataSource = pagingDS;
                dgvStudent.DataMember = "reg_subjenrolled";

                lblRecordCount.Text = dtbl.Rows.Count.ToString();
                lblRowsCount.Text = "Record  " + (scrollVal + 1) + "  of  " + lblRecordCount.Text;

                HeaderTexts();

                mysqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView Row clicked
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dgvStudent.Rows[index];
                lblRowsCount.Text = "Record  " + ((selectedRow.Index + 1) + scrollVal) + "  of  " + lblRecordCount.Text;
            }

            //View Button clicked
            if (e.ColumnIndex == dgvStudent.Columns["btnView"].Index)
            {
                viewButtonClicked = true;
                selectedfcuidno = Convert.ToString(dgvStudent.Rows[e.RowIndex].Cells["cis_fcuidno"].Value);
                selectedstudno = Convert.ToString(dgvStudent.Rows[e.RowIndex].Cells["cis_studno"].Value);
                selectedfullname = Convert.ToString(dgvStudent.Rows[e.RowIndex].Cells["fullname"].Value);
                selectedcourse = Convert.ToString(dgvStudent.Rows[e.RowIndex].Cells["cis_course"].Value);

                sqlDA.Dispose();
                pagingDS.Dispose();

                Close();
            }
        }

        private void DgvStudent_KeyUp(object sender, KeyEventArgs e)
        {
            //DataGridView Row Arrow Key Pressed
            int index = dgvStudent.CurrentCell.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dgvStudent.Rows[index];
                lblRowsCount.Text = "Record  " + ((selectedRow.Index + 1) + scrollVal) + "  of  " + lblRecordCount.Text;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Esc KeyPressed
            if (keyData == Keys.Escape)
            {
                viewButtonClicked = false;
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
