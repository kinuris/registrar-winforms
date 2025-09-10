using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Drawing;

namespace PrjRegistrar
{
    public partial class FrmDefaultPeriod : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        public bool selectButtonClicked;
        public string cisSchlyr, cisSemNo;

        public FrmDefaultPeriod()
        {
            InitializeComponent();
        }
        private void FrmDefaultPeriod_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    string SelectRegCurriculum = "SELECT cis_category AS CATEGORY, cis_schlyr AS 'SCHOOL YEAR', cis_semno AS SEMESTER FROM mtbl_defaultperiod WHERE cis_category = '" + FrmPersonalData.cisCategory + "' and cis_status = '1'";
                    mySqlDA.SelectCommand = new MySqlCommand(SelectRegCurriculum, mySqlConnection);

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvDefaultPeriod.DataSource = bindingSource;

                    DataGridViewButtonColumn CmdSelect = new DataGridViewButtonColumn();
                    {
                        CmdSelect.UseColumnTextForButtonValue = true;
                        CmdSelect.HeaderText = "Action";
                        CmdSelect.Name = "btnSelect";
                        CmdSelect.Text = "Select";
                        CmdSelect.FlatStyle = FlatStyle.Flat;
                        CmdSelect.CellTemplate.Style.BackColor = Color.SteelBlue;
                        CmdSelect.CellTemplate.Style.ForeColor = Color.White;
                    }
                    dgvDefaultPeriod.Columns.Add(CmdSelect);
                    dgvDefaultPeriod.Columns[3].Width = 90;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDefaultPeriod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //************************
                //Select button clicked
                //************************
                if (e.ColumnIndex == dgvDefaultPeriod.Columns["btnSelect"].Index)
                {
                    selectButtonClicked = true;
                    cisSchlyr = Convert.ToString(dgvDefaultPeriod.Rows[e.RowIndex].Cells["SCHOOL YEAR"].Value);
                    cisSemNo = Convert.ToString(dgvDefaultPeriod.Rows[e.RowIndex].Cells["SEMESTER"].Value);
                    Close();
                }
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
    }
}