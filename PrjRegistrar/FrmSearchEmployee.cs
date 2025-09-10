using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmSearchEmployee : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        public static string selectedEmpID, selectedFullname, selectedDeptName;
        public bool selectButtonClicked;
        public MySqlDataAdapter sqlDA;
        public DataSet pagingDS;
        public DataTable dtbl;
        int scrollVal;

        public FrmSearchEmployee()
        {
            scrollVal = 0;
            selectButtonClicked = false;
            InitializeComponent();
        }

        private void FrmSearchEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(connectionString);
                mysqlcon.Open();

                sqlDA = new MySqlDataAdapter("mtbl_employee_search", mysqlcon);
                sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                                
                dtbl = new DataTable();
                pagingDS = new DataSet();

                sqlDA.Fill(dtbl);
                sqlDA.Fill(pagingDS, scrollVal, dtbl.Rows.Count, "mtbl_employee");

                dgvEmployees.DataSource = pagingDS;
                dgvEmployees.DataMember = "mtbl_employee";

                lblRecordCount.Text = dtbl.Rows.Count.ToString();
                lblRowsCount.Text = "Record  " + (scrollVal + 1) + "  of  " + lblRecordCount.Text;

                HeaderTexts();

                mysqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void DgvEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void HeaderTexts()
        {
            dgvEmployees.Columns[0].HeaderText = "Emp ID";
            dgvEmployees.Columns[1].HeaderText = "Employee Name";            
            dgvEmployees.Columns[2].HeaderText = "Department";

            dgvEmployees.Columns[0].Width = 80;
            dgvEmployees.Columns[1].Width = 250;
            dgvEmployees.Columns[2].Width = 315;

            DataGridViewButtonColumn btnSelect = new DataGridViewButtonColumn();
            {
                btnSelect.UseColumnTextForButtonValue = true;
                btnSelect.HeaderText = "Action";
                btnSelect.Name = "btnSelect";
                btnSelect.Text = "Select";
                btnSelect.FlatStyle = FlatStyle.Flat;
                btnSelect.CellTemplate.Style.BackColor = Color.SteelBlue;
                btnSelect.CellTemplate.Style.ForeColor = Color.White;
            }
            dgvEmployees.Columns.Add(btnSelect);
            dgvEmployees.Columns[3].Width = 60;
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

                MySqlDataAdapter sqlDA = new MySqlDataAdapter("mtbl_employee_searchbyparam", mysqlcon);
                sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDA.SelectCommand.Parameters.AddWithValue("_cis_empid", txtsearch.Text);
                sqlDA.SelectCommand.Parameters.AddWithValue("_cis_lastname", txtsearch.Text);
                sqlDA.SelectCommand.Parameters.AddWithValue("_cis_firstname", txtsearch.Text);

                dgvEmployees.DataSource = null;
                dgvEmployees.Rows.Clear();
                dgvEmployees.Columns.Clear();

                dtbl = new DataTable();
                pagingDS = new DataSet();

                sqlDA.Fill(dtbl);
                sqlDA.Fill(pagingDS, scrollVal, dtbl.Rows.Count, "mtbl_employee");

                dgvEmployees.DataSource = pagingDS;
                dgvEmployees.DataMember = "mtbl_employee";

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

        private void DgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView Row clicked
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dgvEmployees.Rows[index];
                lblRowsCount.Text = "Record  " + ((selectedRow.Index + 1) + scrollVal) + "  of  " + lblRecordCount.Text;
            }

            //Select Button clicked
            if (e.ColumnIndex == dgvEmployees.Columns["btnSelect"].Index)
            {
                selectButtonClicked = true;
                selectedEmpID = Convert.ToString(dgvEmployees.Rows[e.RowIndex].Cells["cis_empid"].Value);
                selectedFullname = Convert.ToString(dgvEmployees.Rows[e.RowIndex].Cells["fullname"].Value);
                selectedDeptName = Convert.ToString(dgvEmployees.Rows[e.RowIndex].Cells["cis_deptname"].Value);
                Close();
            }
        }

        private void DgvEmployees_KeyUp(object sender, KeyEventArgs e)
        {
            //DataGridView Row Arrow Key Pressed
            int index = dgvEmployees.CurrentCell.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dgvEmployees.Rows[index];
                lblRowsCount.Text = "Record  " + ((selectedRow.Index + 1) + scrollVal) + "  of  " + lblRecordCount.Text;
            }
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Esc KeyPressed
            if (keyData == Keys.Escape)
            {
                selectButtonClicked = false;
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}