namespace PrjRegistrar
{
    partial class FrmRecTransferees
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecTransferees));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpfgdate = new System.Windows.Forms.DateTimePicker();
            this.dtpcgrdate = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cboYrlevel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSem = new System.Windows.Forms.ComboBox();
            this.mskSY = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearchStudent = new System.Windows.Forms.Button();
            this.lblfullname = new System.Windows.Forms.Label();
            this.lblcourse = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbofgrade = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbocgrade = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtCredits = new System.Windows.Forms.TextBox();
            this.txtSchoolName = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtCourseNo = new System.Windows.Forms.TextBox();
            this.lblcourseno = new System.Windows.Forms.Label();
            this.lbldesc = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblstudno = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cpbProfilePic = new PrjRegistrar.CircularPictureBox();
            this.cpbOutsideBorder = new PrjRegistrar.CircularPictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvSubjSchools = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssllastmodified = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblaccountability = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnAddRecord = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpbProfilePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpbOutsideBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjSchools)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.BtnAddRecord);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.BtnRefresh);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1173, 36);
            this.panel1.TabIndex = 49;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(591, 7);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(359, 23);
            this.txtSearch.TabIndex = 14;
            this.txtSearch.Text = "Search";
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.TxtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.TxtSearch_Leave);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.BtnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnRefresh.FlatAppearance.BorderSize = 0;
            this.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefresh.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefresh.ForeColor = System.Drawing.Color.White;
            this.BtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("BtnRefresh.Image")));
            this.BtnRefresh.Location = new System.Drawing.Point(1053, 0);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(40, 36);
            this.BtnRefresh.TabIndex = 16;
            this.BtnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.BtnRefresh, "Refresh");
            this.BtnRefresh.UseVisualStyleBackColor = false;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(1093, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 36);
            this.btnSave.TabIndex = 17;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSave, "Save");
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1133, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 36);
            this.btnClose.TabIndex = 18;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnClose, "Close");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(393, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "GRADES - Transferees from other Schools";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 18);
            this.label6.TabIndex = 55;
            this.label6.Text = "Credits";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 18);
            this.label7.TabIndex = 55;
            this.label7.Text = "Final Grade";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(101, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 18);
            this.label9.TabIndex = 55;
            this.label9.Text = "Final Grade Entry Date";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(9, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 40);
            this.label10.TabIndex = 55;
            this.label10.Text = "Completion Grade";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(104, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 18);
            this.label12.TabIndex = 55;
            this.label12.Text = "Completion Date";
            // 
            // dtpfgdate
            // 
            this.dtpfgdate.CustomFormat = " ";
            this.dtpfgdate.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfgdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfgdate.Location = new System.Drawing.Point(104, 24);
            this.dtpfgdate.Name = "dtpfgdate";
            this.dtpfgdate.Size = new System.Drawing.Size(135, 23);
            this.dtpfgdate.TabIndex = 10;
            this.dtpfgdate.ValueChanged += new System.EventHandler(this.Dtpfgdate_ValueChanged);
            this.dtpfgdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dtpfgdate_KeyDown);
            // 
            // dtpcgrdate
            // 
            this.dtpcgrdate.CustomFormat = " ";
            this.dtpcgrdate.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpcgrdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpcgrdate.Location = new System.Drawing.Point(103, 42);
            this.dtpcgrdate.Name = "dtpcgrdate";
            this.dtpcgrdate.Size = new System.Drawing.Size(137, 23);
            this.dtpcgrdate.TabIndex = 12;
            this.dtpcgrdate.ValueChanged += new System.EventHandler(this.Dtpcgrdate_ValueChanged);
            this.dtpcgrdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dtpcgrdate_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.dgvSubjSchools);
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1175, 524);
            this.panel2.TabIndex = 62;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.08853F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.91147F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel10, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(198, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(956, 200);
            this.tableLayoutPanel1.TabIndex = 74;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.panel7.Controls.Add(this.cboYrlevel);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.cboSem);
            this.panel7.Controls.Add(this.mskSY);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.btnSearchStudent);
            this.panel7.Controls.Add(this.lblfullname);
            this.panel7.Controls.Add(this.lblcourse);
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(316, 193);
            this.panel7.TabIndex = 71;
            // 
            // cboYrlevel
            // 
            this.cboYrlevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYrlevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboYrlevel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYrlevel.FormattingEnabled = true;
            this.cboYrlevel.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cboYrlevel.Location = new System.Drawing.Point(172, 146);
            this.cboYrlevel.Name = "cboYrlevel";
            this.cboYrlevel.Size = new System.Drawing.Size(127, 24);
            this.cboYrlevel.TabIndex = 72;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(55, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 73;
            this.label5.Text = "Year Level";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(55, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 18);
            this.label3.TabIndex = 71;
            this.label3.Text = "Semester";
            // 
            // cboSem
            // 
            this.cboSem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSem.Font = new System.Drawing.Font("Trebuchet MS", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSem.FormattingEnabled = true;
            this.cboSem.Items.AddRange(new object[] {
            "1 - 1ST SEMESTER",
            "2 - 2ND SEMESTER",
            "3 - SUMMER",
            "4 - 1ST TRIMESTER",
            "5 - 2ND TRIMESTER",
            "6 - 3RD TRIMESTER",
            "7 - TERM 1",
            "8 - TERM 2",
            "9 - TERM 3",
            "10 - TERM 4",
            "11 - TERM 5"});
            this.cboSem.Location = new System.Drawing.Point(172, 114);
            this.cboSem.Name = "cboSem";
            this.cboSem.Size = new System.Drawing.Size(127, 23);
            this.cboSem.TabIndex = 70;
            // 
            // mskSY
            // 
            this.mskSY.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskSY.Location = new System.Drawing.Point(172, 82);
            this.mskSY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mskSY.Mask = "0000-0000";
            this.mskSY.Name = "mskSY";
            this.mskSY.Size = new System.Drawing.Size(127, 23);
            this.mskSY.TabIndex = 68;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(55, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 18);
            this.label4.TabIndex = 69;
            this.label4.Text = "School Year";
            // 
            // btnSearchStudent
            // 
            this.btnSearchStudent.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearchStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchStudent.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchStudent.ForeColor = System.Drawing.Color.White;
            this.btnSearchStudent.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchStudent.Image")));
            this.btnSearchStudent.Location = new System.Drawing.Point(16, 29);
            this.btnSearchStudent.Name = "btnSearchStudent";
            this.btnSearchStudent.Size = new System.Drawing.Size(30, 25);
            this.btnSearchStudent.TabIndex = 3;
            this.btnSearchStudent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchStudent, "Search for Student");
            this.btnSearchStudent.UseVisualStyleBackColor = false;
            this.btnSearchStudent.Click += new System.EventHandler(this.BtnSearchStudent_Click);
            // 
            // lblfullname
            // 
            this.lblfullname.AutoSize = true;
            this.lblfullname.BackColor = System.Drawing.Color.Transparent;
            this.lblfullname.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfullname.ForeColor = System.Drawing.Color.Black;
            this.lblfullname.Location = new System.Drawing.Point(54, 33);
            this.lblfullname.Name = "lblfullname";
            this.lblfullname.Size = new System.Drawing.Size(119, 20);
            this.lblfullname.TabIndex = 42;
            this.lblfullname.Text = "Student\'s Name";
            // 
            // lblcourse
            // 
            this.lblcourse.AutoSize = true;
            this.lblcourse.BackColor = System.Drawing.Color.Transparent;
            this.lblcourse.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcourse.ForeColor = System.Drawing.Color.Black;
            this.lblcourse.Location = new System.Drawing.Point(54, 57);
            this.lblcourse.Name = "lblcourse";
            this.lblcourse.Size = new System.Drawing.Size(48, 18);
            this.lblcourse.TabIndex = 28;
            this.lblcourse.Text = "Course";
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.panel10.Controls.Add(this.panel3);
            this.panel10.Controls.Add(this.panel5);
            this.panel10.Location = new System.Drawing.Point(673, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(280, 193);
            this.panel10.TabIndex = 73;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.SlateGray;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cbofgrade);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.dtpfgdate);
            this.panel3.Location = new System.Drawing.Point(14, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(253, 60);
            this.panel3.TabIndex = 64;
            // 
            // cbofgrade
            // 
            this.cbofgrade.BackColor = System.Drawing.Color.White;
            this.cbofgrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbofgrade.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbofgrade.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbofgrade.FormattingEnabled = true;
            this.cbofgrade.Items.AddRange(new object[] {
            "1.0",
            "1.25",
            "1.5",
            "1.75",
            "2.0",
            "2.25",
            "2.5",
            "2.75",
            "3.0",
            "5.0",
            "P",
            "INC",
            "DRP",
            "NP",
            "NC",
            "UD",
            "L",
            "FA",
            "NE",
            "VG",
            "NG",
            "A",
            "A-",
            "B+",
            "B",
            "B-",
            "C+",
            "C",
            "C-",
            "D",
            "F",
            "1",
            "1-",
            "2+",
            "2",
            "2-",
            "3+",
            "3",
            "4",
            "5",
            "WP",
            "Exempted",
            "HNA",
            "No Grade",
            "DR",
            "Left",
            "Credited",
            "nfe",
            "DA",
            "6.0",
            "No Record",
            "Dropped",
            "drpd",
            "W",
            "OD",
            "passed",
            "Completed",
            "GNA",
            "NF",
            "NA",
            "NFT",
            "XXX",
            "In Progress",
            "Failed",
            "None",
            "Inc",
            "-",
            "Pass",
            "FA",
            "Incomplete",
            "4.0/3.0",
            "66",
            "WF",
            "NCA",
            "RP",
            "CR",
            "Withdrew",
            "S",
            "***",
            "Wd",
            "Withdrawn",
            "Passed",
            "Drp",
            "inc",
            "NFE",
            "No OJT",
            "CE",
            "FDA",
            "NT",
            "FWP",
            "Inc/5.0",
            "x",
            "INP",
            "*",
            "LFT",
            "CON",
            "con",
            "on going",
            "AF",
            "W",
            "D+",
            "Validated",
            "Not Attended",
            "N/A",
            "7",
            "9",
            "EXT",
            "DRPD",
            "NL"});
            this.cbofgrade.Location = new System.Drawing.Point(9, 25);
            this.cbofgrade.Name = "cbofgrade";
            this.cbofgrade.Size = new System.Drawing.Size(86, 26);
            this.cbofgrade.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SlateGray;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.dtpcgrdate);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.cbocgrade);
            this.panel5.Location = new System.Drawing.Point(14, 89);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(253, 78);
            this.panel5.TabIndex = 69;
            // 
            // cbocgrade
            // 
            this.cbocgrade.BackColor = System.Drawing.Color.White;
            this.cbocgrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbocgrade.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbocgrade.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbocgrade.FormattingEnabled = true;
            this.cbocgrade.Items.AddRange(new object[] {
            "1.0",
            "1.25",
            "1.5",
            "1.75",
            "2.0",
            "2.25",
            "2.5",
            "2.75",
            "3.0",
            "NC"});
            this.cbocgrade.Location = new System.Drawing.Point(9, 42);
            this.cbocgrade.Name = "cbocgrade";
            this.cbocgrade.Size = new System.Drawing.Size(85, 26);
            this.cbocgrade.TabIndex = 11;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.panel8.Controls.Add(this.txtCredits);
            this.panel8.Controls.Add(this.txtSchoolName);
            this.panel8.Controls.Add(this.txtDesc);
            this.panel8.Controls.Add(this.txtCourseNo);
            this.panel8.Controls.Add(this.lblcourseno);
            this.panel8.Controls.Add(this.lbldesc);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.label1);
            this.panel8.Location = new System.Drawing.Point(325, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(341, 193);
            this.panel8.TabIndex = 71;
            // 
            // txtCredits
            // 
            this.txtCredits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCredits.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredits.Location = new System.Drawing.Point(84, 113);
            this.txtCredits.MaxLength = 5;
            this.txtCredits.Name = "txtCredits";
            this.txtCredits.Size = new System.Drawing.Size(91, 23);
            this.txtCredits.TabIndex = 6;
            this.txtCredits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCredits_KeyPress);
            // 
            // txtSchoolName
            // 
            this.txtSchoolName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSchoolName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSchoolName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSchoolName.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSchoolName.Location = new System.Drawing.Point(84, 147);
            this.txtSchoolName.Name = "txtSchoolName";
            this.txtSchoolName.Size = new System.Drawing.Size(250, 23);
            this.txtSchoolName.TabIndex = 8;
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.Location = new System.Drawing.Point(84, 55);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(250, 45);
            this.txtDesc.TabIndex = 5;
            // 
            // txtCourseNo
            // 
            this.txtCourseNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCourseNo.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCourseNo.Location = new System.Drawing.Point(84, 24);
            this.txtCourseNo.Name = "txtCourseNo";
            this.txtCourseNo.Size = new System.Drawing.Size(250, 23);
            this.txtCourseNo.TabIndex = 4;
            // 
            // lblcourseno
            // 
            this.lblcourseno.AutoSize = true;
            this.lblcourseno.BackColor = System.Drawing.Color.Transparent;
            this.lblcourseno.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcourseno.ForeColor = System.Drawing.Color.Black;
            this.lblcourseno.Location = new System.Drawing.Point(6, 28);
            this.lblcourseno.Name = "lblcourseno";
            this.lblcourseno.Size = new System.Drawing.Size(67, 18);
            this.lblcourseno.TabIndex = 55;
            this.lblcourseno.Text = "Course No";
            // 
            // lbldesc
            // 
            this.lbldesc.BackColor = System.Drawing.Color.Transparent;
            this.lbldesc.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldesc.ForeColor = System.Drawing.Color.Black;
            this.lbldesc.Location = new System.Drawing.Point(6, 55);
            this.lbldesc.Name = "lbldesc";
            this.lbldesc.Size = new System.Drawing.Size(75, 22);
            this.lbldesc.TabIndex = 55;
            this.lbldesc.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 18);
            this.label1.TabIndex = 55;
            this.label1.Text = "School";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.SlateGray;
            this.panel9.Controls.Add(this.lblstudno);
            this.panel9.Location = new System.Drawing.Point(22, 6);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(170, 30);
            this.panel9.TabIndex = 72;
            // 
            // lblstudno
            // 
            this.lblstudno.BackColor = System.Drawing.Color.Transparent;
            this.lblstudno.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstudno.ForeColor = System.Drawing.Color.White;
            this.lblstudno.Location = new System.Drawing.Point(3, 3);
            this.lblstudno.Name = "lblstudno";
            this.lblstudno.Size = new System.Drawing.Size(164, 23);
            this.lblstudno.TabIndex = 36;
            this.lblstudno.Text = "Student ID No.";
            this.lblstudno.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel6.Controls.Add(this.cpbProfilePic);
            this.panel6.Controls.Add(this.cpbOutsideBorder);
            this.panel6.Location = new System.Drawing.Point(22, 36);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(170, 170);
            this.panel6.TabIndex = 71;
            // 
            // cpbProfilePic
            // 
            this.cpbProfilePic.Image = global::PrjRegistrar.Properties.Resources.Default_Profile_Picture;
            this.cpbProfilePic.Location = new System.Drawing.Point(10, 10);
            this.cpbProfilePic.Name = "cpbProfilePic";
            this.cpbProfilePic.Size = new System.Drawing.Size(150, 150);
            this.cpbProfilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cpbProfilePic.TabIndex = 42;
            this.cpbProfilePic.TabStop = false;
            // 
            // cpbOutsideBorder
            // 
            this.cpbOutsideBorder.Location = new System.Drawing.Point(7, 7);
            this.cpbOutsideBorder.Name = "cpbOutsideBorder";
            this.cpbOutsideBorder.Size = new System.Drawing.Size(156, 156);
            this.cpbOutsideBorder.TabIndex = 41;
            this.cpbOutsideBorder.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.panel4.Location = new System.Drawing.Point(22, 214);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1130, 10);
            this.panel4.TabIndex = 66;
            // 
            // dgvSubjSchools
            // 
            this.dgvSubjSchools.AllowUserToAddRows = false;
            this.dgvSubjSchools.AllowUserToDeleteRows = false;
            this.dgvSubjSchools.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(237)))));
            this.dgvSubjSchools.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSubjSchools.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubjSchools.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSubjSchools.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSubjSchools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubjSchools.Location = new System.Drawing.Point(22, 225);
            this.dgvSubjSchools.Name = "dgvSubjSchools";
            this.dgvSubjSchools.ReadOnly = true;
            this.dgvSubjSchools.RowHeadersWidth = 51;
            this.dgvSubjSchools.Size = new System.Drawing.Size(1130, 287);
            this.dgvSubjSchools.TabIndex = 13;
            this.dgvSubjSchools.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSubjEnrolled_CellContentClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.tssllastmodified,
            this.toolStripStatusLabel2,
            this.lblaccountability});
            this.statusStrip1.Location = new System.Drawing.Point(0, 562);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1175, 22);
            this.statusStrip1.TabIndex = 64;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(82, 17);
            this.toolStripStatusLabel3.Text = "Last Modified:";
            // 
            // tssllastmodified
            // 
            this.tssllastmodified.BackColor = System.Drawing.Color.Transparent;
            this.tssllastmodified.Name = "tssllastmodified";
            this.tssllastmodified.Size = new System.Drawing.Size(77, 17);
            this.tssllastmodified.Text = "mm/dd/yyyy";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(98, 17);
            this.toolStripStatusLabel2.Text = "Last Modified By:";
            // 
            // lblaccountability
            // 
            this.lblaccountability.BackColor = System.Drawing.Color.Transparent;
            this.lblaccountability.Name = "lblaccountability";
            this.lblaccountability.Size = new System.Drawing.Size(82, 17);
            this.lblaccountability.Text = "accountability";
            // 
            // BtnAddRecord
            // 
            this.BtnAddRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.BtnAddRecord.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnAddRecord.FlatAppearance.BorderSize = 0;
            this.BtnAddRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddRecord.Font = new System.Drawing.Font("Trebuchet MS", 9.75F);
            this.BtnAddRecord.ForeColor = System.Drawing.Color.White;
            this.BtnAddRecord.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddRecord.Image")));
            this.BtnAddRecord.Location = new System.Drawing.Point(992, 0);
            this.BtnAddRecord.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAddRecord.Name = "BtnAddRecord";
            this.BtnAddRecord.Size = new System.Drawing.Size(61, 36);
            this.BtnAddRecord.TabIndex = 24;
            this.BtnAddRecord.Text = "New";
            this.BtnAddRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.BtnAddRecord, "Add new grade for subjects from other schools");
            this.BtnAddRecord.UseVisualStyleBackColor = false;
            this.BtnAddRecord.Click += new System.EventHandler(this.BtnAddRecord_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Trebuchet MS", 9.75F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(952, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 36);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearch, "Search");
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // FrmRecTransferees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1175, 584);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(185, 108);
            this.Name = "FrmRecTransferees";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmRecHigherEdGradSchool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cpbProfilePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpbOutsideBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjSchools)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpfgdate;
        private System.Windows.Forms.DateTimePicker dtpcgrdate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbocgrade;
        private System.Windows.Forms.DataGridView dgvSubjSchools;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cbofgrade;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblstudno;
        private System.Windows.Forms.Label lblfullname;
        private System.Windows.Forms.Label lblcourse;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblcourseno;
        private System.Windows.Forms.Label lbldesc;
        private System.Windows.Forms.Panel panel9;
        private CircularPictureBox cpbProfilePic;
        private CircularPictureBox cpbOutsideBorder;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSearchStudent;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtCourseNo;
        private System.Windows.Forms.TextBox txtSchoolName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tssllastmodified;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblaccountability;
        private System.Windows.Forms.TextBox txtCredits;
        private System.Windows.Forms.MaskedTextBox mskSY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSem;
        private System.Windows.Forms.ComboBox cboYrlevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button BtnAddRecord;
    }
}