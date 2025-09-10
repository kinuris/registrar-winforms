using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrjRegistrar
{
    public partial class FrmSearchOptionalAcct : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["filcis_db"].ConnectionString;
        public static string selectedOptNo;
        public bool selectButtonClicked;

        public FrmSearchOptionalAcct()
        {
            InitializeComponent();
        }

        private void FrmSearchOptionalAcct_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();

                MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                string SelectRegCurriculum = "SELECT cis_optno, cis_acctcode, cis_acctitle, FORMAT(cis_amount,2), cis_course " +
                                                "FROM acc_optionalacct order by cis_acctitle";
                mySqlDA.SelectCommand = new MySqlCommand(SelectRegCurriculum, mySqlConnection);

                DataTable table = new DataTable();
                mySqlDA.Fill(table);

                BindingSource bindingSource = new BindingSource
                {
                    DataSource = table
                };

                dgvOptionalAcct.DataSource = bindingSource;

                HeaderTexts();

                mySqlConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvOptionalAcct_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (e.ColumnIndex == 5)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void HeaderTexts()
        {
            dgvOptionalAcct.DefaultCellStyle.Font = new Font("Tahoma", 8);
            dgvOptionalAcct.AlternatingRowsDefaultCellStyle.Font = new Font("Tahoma", 8);

            dgvOptionalAcct.Columns[0].HeaderText = "Opt Acct";
            dgvOptionalAcct.Columns[1].HeaderText = "Account Code";
            dgvOptionalAcct.Columns[2].HeaderText = "Account Title";
            dgvOptionalAcct.Columns[3].HeaderText = "Amount";
            dgvOptionalAcct.Columns[4].HeaderText = "Courses";

            dgvOptionalAcct.Columns[0].Width = 50;
            dgvOptionalAcct.Columns[1].Width = 100;
            dgvOptionalAcct.Columns[2].Width = 200;
            dgvOptionalAcct.Columns[3].Width = 100;
            dgvOptionalAcct.Columns[4].Width = 200;

            DataGridViewButtonColumn BtnSelect = new DataGridViewButtonColumn();
            {
                BtnSelect.UseColumnTextForButtonValue = true;
                BtnSelect.HeaderText = "Action";
                BtnSelect.Name = "btnSelect";
                BtnSelect.Text = "Select";
                BtnSelect.FlatStyle = FlatStyle.Flat;
                BtnSelect.CellTemplate.Style.BackColor = Color.SteelBlue;
                BtnSelect.CellTemplate.Style.ForeColor = Color.White;
            }
            dgvOptionalAcct.Columns.Add(BtnSelect);
            dgvOptionalAcct.Columns[5].Width = 60;
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

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "" || txtSearch.Text == " Search Account Code, Account Title, or Amount") txtSearch.Text = "";
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = " Search Account Code, Account Title, or Amount";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != " Search Account Code, Account Title, or Amount")
                {
                    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                    mySqlConnection.Open();

                    MySqlDataAdapter mySqlDA = new MySqlDataAdapter();

                    string SelectRegCurriculum = "SELECT cis_optno, cis_acctcode, cis_acctitle, FORMAT(cis_amount,2), cis_course " +
                                                 "FROM acc_optionalacct WHERE " +
                                                 "cis_acctcode LIKE concat('%', '" + txtSearch.Text + "' ,'%') or " +
                                                 "cis_acctitle LIKE concat('%', '" + txtSearch.Text + "' ,'%') or " +
                                                 "cis_amount LIKE concat('%', '" + txtSearch.Text + "' ,'%') order by cis_acctitle";
                    mySqlDA.SelectCommand = new MySqlCommand(SelectRegCurriculum, mySqlConnection);

                    dgvOptionalAcct.DataSource = null;
                    dgvOptionalAcct.Rows.Clear();
                    dgvOptionalAcct.Columns.Clear();

                    DataTable table = new DataTable();
                    mySqlDA.Fill(table);

                    BindingSource bindingSource = new BindingSource
                    {
                        DataSource = table
                    };

                    dgvOptionalAcct.DataSource = bindingSource;

                    HeaderTexts();

                    mySqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FilCIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvOptionalAcct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Select Button clicked
            if (e.ColumnIndex == dgvOptionalAcct.Columns["btnSelect"].Index)
            {
                selectButtonClicked = true;
                selectedOptNo = Convert.ToString(dgvOptionalAcct.Rows[e.RowIndex].Cells["cis_optno"].Value + " - " + dgvOptionalAcct.Rows[e.RowIndex].Cells["cis_acctitle"].Value);
                Close();
            }
        }
    }
}
