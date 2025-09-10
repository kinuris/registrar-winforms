namespace PrjRegistrar
{
    partial class FrmRecImportGrades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecImportGrades));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label56 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvSubjEnrolled = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoMultiple = new System.Windows.Forms.RadioButton();
            this.rdoIndividual = new System.Windows.Forms.RadioButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnSearchFlatFile = new System.Windows.Forms.Button();
            this.txtFlatFile = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnSearchStudent = new System.Windows.Forms.Button();
            this.lblschlyr = new System.Windows.Forms.Label();
            this.lblsemester = new System.Windows.Forms.Label();
            this.lblfullname = new System.Windows.Forms.Label();
            this.lblcourse = new System.Windows.Forms.Label();
            this.lblfcuidno = new System.Windows.Forms.Label();
            this.lblyrlevel = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblstudno = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cpbProfilePic = new PrjRegistrar.CircularPictureBox();
            this.cpbOutsideBorder = new PrjRegistrar.CircularPictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssllastmodified = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblaccountability = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjEnrolled)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpbProfilePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpbOutsideBorder)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.label56.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.Black;
            this.label56.Location = new System.Drawing.Point(25, 84);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(48, 18);
            this.label56.TabIndex = 28;
            this.label56.Text = "Course";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.BtnRefresh);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1173, 36);
            this.panel1.TabIndex = 49;
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
            this.btnSearch.Location = new System.Drawing.Point(1013, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 36);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearch, "Search");
            this.btnSearch.UseVisualStyleBackColor = false;
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
            this.label2.Size = new System.Drawing.Size(146, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Import Grades";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(25, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 18);
            this.label11.TabIndex = 53;
            this.label11.Text = "School Year";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(25, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 18);
            this.label5.TabIndex = 55;
            this.label5.Text = "Semester";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.BackColor = System.Drawing.Color.Transparent;
            this.label52.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Black;
            this.label52.Location = new System.Drawing.Point(25, 107);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(68, 18);
            this.label52.TabIndex = 57;
            this.label52.Text = "Year Level";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.Controls.Add(this.dgvSubjEnrolled);
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1175, 524);
            this.panel2.TabIndex = 62;
            // 
            // dgvSubjEnrolled
            // 
            this.dgvSubjEnrolled.AllowUserToAddRows = false;
            this.dgvSubjEnrolled.AllowUserToDeleteRows = false;
            this.dgvSubjEnrolled.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(245)))), ((int)(((byte)(237)))));
            this.dgvSubjEnrolled.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSubjEnrolled.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubjEnrolled.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSubjEnrolled.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSubjEnrolled.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSubjEnrolled.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubjEnrolled.Location = new System.Drawing.Point(22, 225);
            this.dgvSubjEnrolled.Name = "dgvSubjEnrolled";
            this.dgvSubjEnrolled.ReadOnly = true;
            this.dgvSubjEnrolled.Size = new System.Drawing.Size(1130, 287);
            this.dgvSubjEnrolled.TabIndex = 13;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(198, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(956, 200);
            this.tableLayoutPanel1.TabIndex = 74;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(233, 194);
            this.panel3.TabIndex = 72;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rdoMultiple);
            this.groupBox1.Controls.Add(this.rdoIndividual);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import Type";
            // 
            // rdoMultiple
            // 
            this.rdoMultiple.AutoSize = true;
            this.rdoMultiple.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoMultiple.Location = new System.Drawing.Point(32, 92);
            this.rdoMultiple.Name = "rdoMultiple";
            this.rdoMultiple.Size = new System.Drawing.Size(180, 24);
            this.rdoMultiple.TabIndex = 1;
            this.rdoMultiple.Text = "Multiple/Bulk Records";
            this.rdoMultiple.UseVisualStyleBackColor = true;
            this.rdoMultiple.CheckedChanged += new System.EventHandler(this.RdoMultiple_CheckedChanged);
            // 
            // rdoIndividual
            // 
            this.rdoIndividual.AutoSize = true;
            this.rdoIndividual.Checked = true;
            this.rdoIndividual.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoIndividual.Location = new System.Drawing.Point(32, 47);
            this.rdoIndividual.Name = "rdoIndividual";
            this.rdoIndividual.Size = new System.Drawing.Size(217, 24);
            this.rdoIndividual.TabIndex = 0;
            this.rdoIndividual.TabStop = true;
            this.rdoIndividual.Text = "Individual Student Records";
            this.rdoIndividual.UseVisualStyleBackColor = true;
            this.rdoIndividual.CheckedChanged += new System.EventHandler(this.RdoIndividual_CheckedChanged);
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.panel8.Controls.Add(this.btnSearchFlatFile);
            this.panel8.Controls.Add(this.txtFlatFile);
            this.panel8.Controls.Add(this.label12);
            this.panel8.Location = new System.Drawing.Point(600, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(353, 194);
            this.panel8.TabIndex = 71;
            // 
            // btnSearchFlatFile
            // 
            this.btnSearchFlatFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchFlatFile.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearchFlatFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchFlatFile.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchFlatFile.ForeColor = System.Drawing.Color.White;
            this.btnSearchFlatFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchFlatFile.Image")));
            this.btnSearchFlatFile.Location = new System.Drawing.Point(306, 48);
            this.btnSearchFlatFile.Name = "btnSearchFlatFile";
            this.btnSearchFlatFile.Size = new System.Drawing.Size(30, 25);
            this.btnSearchFlatFile.TabIndex = 3;
            this.btnSearchFlatFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchFlatFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchFlatFile, "Search for CSV Flat File");
            this.btnSearchFlatFile.UseVisualStyleBackColor = false;
            this.btnSearchFlatFile.Click += new System.EventHandler(this.BtnSearchFlatFile_Click);
            // 
            // txtFlatFile
            // 
            this.txtFlatFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFlatFile.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFlatFile.Location = new System.Drawing.Point(18, 48);
            this.txtFlatFile.Name = "txtFlatFile";
            this.txtFlatFile.Size = new System.Drawing.Size(282, 23);
            this.txtFlatFile.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(15, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 18);
            this.label12.TabIndex = 55;
            this.label12.Text = "Flat File";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.panel7.Controls.Add(this.btnSearchStudent);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.lblschlyr);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.lblsemester);
            this.panel7.Controls.Add(this.lblfullname);
            this.panel7.Controls.Add(this.lblcourse);
            this.panel7.Controls.Add(this.label52);
            this.panel7.Controls.Add(this.label56);
            this.panel7.Controls.Add(this.lblfcuidno);
            this.panel7.Controls.Add(this.lblyrlevel);
            this.panel7.Location = new System.Drawing.Point(242, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(352, 194);
            this.panel7.TabIndex = 71;
            // 
            // btnSearchStudent
            // 
            this.btnSearchStudent.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearchStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchStudent.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchStudent.ForeColor = System.Drawing.Color.White;
            this.btnSearchStudent.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchStudent.Image")));
            this.btnSearchStudent.Location = new System.Drawing.Point(26, 23);
            this.btnSearchStudent.Name = "btnSearchStudent";
            this.btnSearchStudent.Size = new System.Drawing.Size(30, 25);
            this.btnSearchStudent.TabIndex = 3;
            this.btnSearchStudent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchStudent, "Search for Student");
            this.btnSearchStudent.UseVisualStyleBackColor = false;
            this.btnSearchStudent.Click += new System.EventHandler(this.BtnSearchStudent_Click);
            // 
            // lblschlyr
            // 
            this.lblschlyr.AutoSize = true;
            this.lblschlyr.BackColor = System.Drawing.Color.Transparent;
            this.lblschlyr.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblschlyr.Location = new System.Drawing.Point(144, 153);
            this.lblschlyr.Name = "lblschlyr";
            this.lblschlyr.Size = new System.Drawing.Size(74, 18);
            this.lblschlyr.TabIndex = 53;
            this.lblschlyr.Text = "School Year";
            // 
            // lblsemester
            // 
            this.lblsemester.AutoSize = true;
            this.lblsemester.BackColor = System.Drawing.Color.Transparent;
            this.lblsemester.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsemester.ForeColor = System.Drawing.Color.Black;
            this.lblsemester.Location = new System.Drawing.Point(144, 130);
            this.lblsemester.Name = "lblsemester";
            this.lblsemester.Size = new System.Drawing.Size(63, 18);
            this.lblsemester.TabIndex = 55;
            this.lblsemester.Text = "Semester";
            // 
            // lblfullname
            // 
            this.lblfullname.AutoSize = true;
            this.lblfullname.BackColor = System.Drawing.Color.Transparent;
            this.lblfullname.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfullname.ForeColor = System.Drawing.Color.Black;
            this.lblfullname.Location = new System.Drawing.Point(59, 26);
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
            this.lblcourse.Location = new System.Drawing.Point(144, 84);
            this.lblcourse.Name = "lblcourse";
            this.lblcourse.Size = new System.Drawing.Size(48, 18);
            this.lblcourse.TabIndex = 28;
            this.lblcourse.Text = "Course";
            // 
            // lblfcuidno
            // 
            this.lblfcuidno.AutoSize = true;
            this.lblfcuidno.BackColor = System.Drawing.Color.Transparent;
            this.lblfcuidno.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfcuidno.ForeColor = System.Drawing.Color.Black;
            this.lblfcuidno.Location = new System.Drawing.Point(25, 51);
            this.lblfcuidno.Name = "lblfcuidno";
            this.lblfcuidno.Size = new System.Drawing.Size(96, 18);
            this.lblfcuidno.TabIndex = 67;
            this.lblfcuidno.Text = "FCU ID Number";
            // 
            // lblyrlevel
            // 
            this.lblyrlevel.AutoSize = true;
            this.lblyrlevel.BackColor = System.Drawing.Color.Transparent;
            this.lblyrlevel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblyrlevel.ForeColor = System.Drawing.Color.Black;
            this.lblyrlevel.Location = new System.Drawing.Point(144, 107);
            this.lblyrlevel.Name = "lblyrlevel";
            this.lblyrlevel.Size = new System.Drawing.Size(68, 18);
            this.lblyrlevel.TabIndex = 57;
            this.lblyrlevel.Text = "Year Level";
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
            // statusStrip1
            // 
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
            this.toolStripStatusLabel3.Visible = false;
            // 
            // tssllastmodified
            // 
            this.tssllastmodified.BackColor = System.Drawing.Color.Transparent;
            this.tssllastmodified.Name = "tssllastmodified";
            this.tssllastmodified.Size = new System.Drawing.Size(77, 17);
            this.tssllastmodified.Text = "mm/dd/yyyy";
            this.tssllastmodified.Visible = false;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(98, 17);
            this.toolStripStatusLabel2.Text = "Last Modified By:";
            this.toolStripStatusLabel2.Visible = false;
            // 
            // lblaccountability
            // 
            this.lblaccountability.BackColor = System.Drawing.Color.Transparent;
            this.lblaccountability.Name = "lblaccountability";
            this.lblaccountability.Size = new System.Drawing.Size(82, 17);
            this.lblaccountability.Text = "accountability";
            this.lblaccountability.Visible = false;
            // 
            // FrmRecImportGrades
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
            this.Name = "FrmRecImportGrades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmRecHigherEdGradSchool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjEnrolled)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cpbProfilePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpbOutsideBorder)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvSubjEnrolled;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblstudno;
        private System.Windows.Forms.Label lblfullname;
        private System.Windows.Forms.Label lblcourse;
        private System.Windows.Forms.Label lblyrlevel;
        private System.Windows.Forms.Label lblsemester;
        private System.Windows.Forms.Label lblschlyr;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblfcuidno;
        private System.Windows.Forms.Panel panel9;
        private CircularPictureBox cpbProfilePic;
        private CircularPictureBox cpbOutsideBorder;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSearchStudent;
        private System.Windows.Forms.TextBox txtFlatFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tssllastmodified;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblaccountability;
        private System.Windows.Forms.Button btnSearchFlatFile;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoMultiple;
        private System.Windows.Forms.RadioButton rdoIndividual;
    }
}