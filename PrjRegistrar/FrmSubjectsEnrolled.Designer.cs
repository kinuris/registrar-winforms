namespace PrjRegistrar
{
    partial class FrmSubjectsEnrolled
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSubjectsEnrolled));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalCredits = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboSchlyr = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboSemester = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvSubjEnrolled = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStudNo = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.lblstudfullname = new System.Windows.Forms.Label();
            this.lblYearLevel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjEnrolled)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 36);
            this.panel1.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Subjects Enrolled";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblYearLevel);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cboSchlyr);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cboSemester);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.dgvSubjEnrolled);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblStudNo);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.BtnClose);
            this.panel2.Controls.Add(this.lblstudfullname);
            this.panel2.Location = new System.Drawing.Point(1, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(540, 652);
            this.panel2.TabIndex = 48;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(31, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 2);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.panel3.Controls.Add(this.lblTotalCredits);
            this.panel3.Location = new System.Drawing.Point(439, 421);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(89, 36);
            this.panel3.TabIndex = 74;
            // 
            // lblTotalCredits
            // 
            this.lblTotalCredits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblTotalCredits.AutoSize = true;
            this.lblTotalCredits.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCredits.ForeColor = System.Drawing.Color.White;
            this.lblTotalCredits.Location = new System.Drawing.Point(30, 5);
            this.lblTotalCredits.Name = "lblTotalCredits";
            this.lblTotalCredits.Size = new System.Drawing.Size(20, 27);
            this.lblTotalCredits.TabIndex = 0;
            this.lblTotalCredits.Text = "-";
            this.lblTotalCredits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(28, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 72;
            this.label3.Text = "School Year";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(166, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 18);
            this.label6.TabIndex = 72;
            this.label6.Text = "Semester";
            // 
            // cboSchlyr
            // 
            this.cboSchlyr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSchlyr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSchlyr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSchlyr.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSchlyr.FormattingEnabled = true;
            this.cboSchlyr.Location = new System.Drawing.Point(28, 100);
            this.cboSchlyr.Name = "cboSchlyr";
            this.cboSchlyr.Size = new System.Drawing.Size(120, 24);
            this.cboSchlyr.TabIndex = 70;
            this.cboSchlyr.SelectedValueChanged += new System.EventHandler(this.CboSchlyr_SelectedValueChanged);
            this.cboSchlyr.Enter += new System.EventHandler(this.CboSchlyr_Enter);
            this.cboSchlyr.Leave += new System.EventHandler(this.CboSchlyr_Leave);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(310, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 18);
            this.label7.TabIndex = 73;
            this.label7.Text = "Year Level";
            // 
            // cboSemester
            // 
            this.cboSemester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSemester.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSemester.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.Location = new System.Drawing.Point(166, 100);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(115, 24);
            this.cboSemester.TabIndex = 70;
            this.cboSemester.SelectedValueChanged += new System.EventHandler(this.CboSemester_SelectedValueChanged);
            this.cboSemester.Enter += new System.EventHandler(this.CboSemester_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(348, 433);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 18);
            this.label1.TabIndex = 69;
            this.label1.Text = "Total Credits";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(116)))));
            this.panel4.Location = new System.Drawing.Point(11, 136);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(517, 10);
            this.panel4.TabIndex = 68;
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
            this.dgvSubjEnrolled.Location = new System.Drawing.Point(11, 148);
            this.dgvSubjEnrolled.Name = "dgvSubjEnrolled";
            this.dgvSubjEnrolled.ReadOnly = true;
            this.dgvSubjEnrolled.Size = new System.Drawing.Size(517, 272);
            this.dgvSubjEnrolled.TabIndex = 67;
            this.dgvSubjEnrolled.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvSubjEnrolled_CellFormatting);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(28, 39);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 18);
            this.label15.TabIndex = 48;
            this.label15.Text = "Student\'s Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(28, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 18);
            this.label4.TabIndex = 47;
            this.label4.Text = "ID Number";
            // 
            // lblStudNo
            // 
            this.lblStudNo.AutoSize = true;
            this.lblStudNo.BackColor = System.Drawing.Color.Transparent;
            this.lblStudNo.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudNo.ForeColor = System.Drawing.Color.Black;
            this.lblStudNo.Location = new System.Drawing.Point(163, 9);
            this.lblStudNo.Name = "lblStudNo";
            this.lblStudNo.Size = new System.Drawing.Size(88, 22);
            this.lblStudNo.TabIndex = 1;
            this.lblStudNo.Text = "ID Number";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.SteelBlue;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(342, 480);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 29);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(438, 480);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(90, 29);
            this.BtnClose.TabIndex = 13;
            this.BtnClose.Text = " &Close";
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblstudfullname
            // 
            this.lblstudfullname.AutoSize = true;
            this.lblstudfullname.BackColor = System.Drawing.Color.Transparent;
            this.lblstudfullname.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstudfullname.ForeColor = System.Drawing.Color.Black;
            this.lblstudfullname.Location = new System.Drawing.Point(163, 35);
            this.lblstudfullname.Name = "lblstudfullname";
            this.lblstudfullname.Size = new System.Drawing.Size(177, 22);
            this.lblstudfullname.TabIndex = 2;
            this.lblstudfullname.Text = "STUDENT\'S FULLNAME";
            // 
            // lblYearLevel
            // 
            this.lblYearLevel.AutoSize = true;
            this.lblYearLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblYearLevel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearLevel.ForeColor = System.Drawing.Color.Black;
            this.lblYearLevel.Location = new System.Drawing.Point(338, 100);
            this.lblYearLevel.Name = "lblYearLevel";
            this.lblYearLevel.Size = new System.Drawing.Size(16, 22);
            this.lblYearLevel.TabIndex = 49;
            this.lblYearLevel.Text = "-";
            // 
            // FrmSubjectsEnrolled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 554);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSubjectsEnrolled";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subjects Enrolled";
            this.Load += new System.EventHandler(this.FrmSubjectsEnrolled_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjEnrolled)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStudNo;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label lblstudfullname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvSubjEnrolled;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboSchlyr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboSemester;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTotalCredits;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblYearLevel;
    }
}