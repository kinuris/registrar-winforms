namespace PrjRegistrar
{
    partial class FrmChangeCourse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChangeCourse));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboMajorDesc = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.cboAcadStat = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.lblCourseDesc = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboYearLevel = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPrgSY = new System.Windows.Forms.ComboBox();
            this.cboCourse = new System.Windows.Forms.ComboBox();
            this.lblMajorID = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStudNo = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnChange = new System.Windows.Forms.Button();
            this.lblstudfullname = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
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
            this.label2.Size = new System.Drawing.Size(150, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Change Course";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblStudNo);
            this.panel2.Controls.Add(this.BtnClose);
            this.panel2.Controls.Add(this.BtnChange);
            this.panel2.Controls.Add(this.lblstudfullname);
            this.panel2.Location = new System.Drawing.Point(1, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(540, 652);
            this.panel2.TabIndex = 48;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.cboMajorDesc);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label53);
            this.panel4.Controls.Add(this.cboAcadStat);
            this.panel4.Controls.Add(this.label52);
            this.panel4.Controls.Add(this.lblCourseDesc);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.cboYearLevel);
            this.panel4.Controls.Add(this.label29);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.cboPrgSY);
            this.panel4.Controls.Add(this.cboCourse);
            this.panel4.Controls.Add(this.lblMajorID);
            this.panel4.Location = new System.Drawing.Point(28, 96);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(484, 349);
            this.panel4.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 18);
            this.label1.TabIndex = 48;
            this.label1.Text = "Course";
            // 
            // cboMajorDesc
            // 
            this.cboMajorDesc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMajorDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMajorDesc.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMajorDesc.FormattingEnabled = true;
            this.cboMajorDesc.Location = new System.Drawing.Point(128, 187);
            this.cboMajorDesc.Name = "cboMajorDesc";
            this.cboMajorDesc.Size = new System.Drawing.Size(347, 26);
            this.cboMajorDesc.TabIndex = 9;
            this.cboMajorDesc.SelectedIndexChanged += new System.EventHandler(this.CboMajorDesc_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(8, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 18);
            this.label7.TabIndex = 49;
            this.label7.Text = "Specialization";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.BackColor = System.Drawing.Color.Transparent;
            this.label53.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.Black;
            this.label53.Location = new System.Drawing.Point(8, 165);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(58, 18);
            this.label53.TabIndex = 49;
            this.label53.Text = "Major ID";
            // 
            // cboAcadStat
            // 
            this.cboAcadStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAcadStat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAcadStat.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAcadStat.FormattingEnabled = true;
            this.cboAcadStat.Items.AddRange(new object[] {
            "NEW",
            "CONTINUING"});
            this.cboAcadStat.Location = new System.Drawing.Point(128, 296);
            this.cboAcadStat.Name = "cboAcadStat";
            this.cboAcadStat.Size = new System.Drawing.Size(142, 26);
            this.cboAcadStat.TabIndex = 11;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.BackColor = System.Drawing.Color.Transparent;
            this.label52.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Black;
            this.label52.Location = new System.Drawing.Point(8, 250);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(72, 18);
            this.label52.TabIndex = 50;
            this.label52.Text = "Year Level";
            // 
            // lblCourseDesc
            // 
            this.lblCourseDesc.AutoSize = true;
            this.lblCourseDesc.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourseDesc.ForeColor = System.Drawing.Color.Black;
            this.lblCourseDesc.Location = new System.Drawing.Point(128, 62);
            this.lblCourseDesc.Name = "lblCourseDesc";
            this.lblCourseDesc.Size = new System.Drawing.Size(13, 18);
            this.lblCourseDesc.TabIndex = 6;
            this.lblCourseDesc.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(8, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 48;
            this.label6.Text = "School Year";
            // 
            // cboYearLevel
            // 
            this.cboYearLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYearLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboYearLevel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYearLevel.FormattingEnabled = true;
            this.cboYearLevel.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cboYearLevel.Location = new System.Drawing.Point(128, 242);
            this.cboYearLevel.Name = "cboYearLevel";
            this.cboYearLevel.Size = new System.Drawing.Size(142, 26);
            this.cboYearLevel.TabIndex = 10;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(8, 304);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(109, 18);
            this.label29.TabIndex = 51;
            this.label29.Text = "Academic Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(8, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 18);
            this.label5.TabIndex = 48;
            this.label5.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 18);
            this.label3.TabIndex = 48;
            this.label3.Text = "Program";
            // 
            // cboPrgSY
            // 
            this.cboPrgSY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrgSY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPrgSY.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPrgSY.FormattingEnabled = true;
            this.cboPrgSY.Location = new System.Drawing.Point(128, 112);
            this.cboPrgSY.Name = "cboPrgSY";
            this.cboPrgSY.Size = new System.Drawing.Size(142, 26);
            this.cboPrgSY.TabIndex = 7;
            // 
            // cboCourse
            // 
            this.cboCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCourse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCourse.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCourse.FormattingEnabled = true;
            this.cboCourse.Location = new System.Drawing.Point(128, 26);
            this.cboCourse.Name = "cboCourse";
            this.cboCourse.Size = new System.Drawing.Size(142, 26);
            this.cboCourse.TabIndex = 5;
            this.cboCourse.SelectedValueChanged += new System.EventHandler(this.CboCourse_SelectedValueChanged);
            this.cboCourse.Enter += new System.EventHandler(this.CboCourse_Enter);
            this.cboCourse.Leave += new System.EventHandler(this.CboCourse_Leave);
            // 
            // lblMajorID
            // 
            this.lblMajorID.AutoSize = true;
            this.lblMajorID.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMajorID.ForeColor = System.Drawing.Color.Black;
            this.lblMajorID.Location = new System.Drawing.Point(128, 165);
            this.lblMajorID.Name = "lblMajorID";
            this.lblMajorID.Size = new System.Drawing.Size(13, 18);
            this.lblMajorID.TabIndex = 8;
            this.lblMajorID.Text = "-";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(28, 53);
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
            this.label4.Location = new System.Drawing.Point(28, 27);
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
            this.lblStudNo.Location = new System.Drawing.Point(163, 23);
            this.lblStudNo.Name = "lblStudNo";
            this.lblStudNo.Size = new System.Drawing.Size(88, 22);
            this.lblStudNo.TabIndex = 1;
            this.lblStudNo.Text = "ID Number";
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Location = new System.Drawing.Point(420, 467);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(90, 29);
            this.BtnClose.TabIndex = 13;
            this.BtnClose.Text = " &Close";
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnChange
            // 
            this.BtnChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.BtnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnChange.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnChange.ForeColor = System.Drawing.Color.White;
            this.BtnChange.Location = new System.Drawing.Point(324, 467);
            this.BtnChange.Name = "BtnChange";
            this.BtnChange.Size = new System.Drawing.Size(90, 29);
            this.BtnChange.TabIndex = 12;
            this.BtnChange.Text = " &Change";
            this.BtnChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnChange.UseVisualStyleBackColor = false;
            this.BtnChange.Click += new System.EventHandler(this.BtnChange_Click);
            // 
            // lblstudfullname
            // 
            this.lblstudfullname.AutoSize = true;
            this.lblstudfullname.BackColor = System.Drawing.Color.Transparent;
            this.lblstudfullname.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstudfullname.ForeColor = System.Drawing.Color.Black;
            this.lblstudfullname.Location = new System.Drawing.Point(163, 49);
            this.lblstudfullname.Name = "lblstudfullname";
            this.lblstudfullname.Size = new System.Drawing.Size(177, 22);
            this.lblstudfullname.TabIndex = 2;
            this.lblstudfullname.Text = "STUDENT\'S FULLNAME";
            // 
            // FrmChangeCourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 553);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangeCourse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Course";
            this.Load += new System.EventHandler(this.FrmChangeCourse_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
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
        private System.Windows.Forms.Button BtnChange;
        private System.Windows.Forms.Label lblstudfullname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cboCourse;
        private System.Windows.Forms.Label lblCourseDesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboPrgSY;
        private System.Windows.Forms.ComboBox cboMajorDesc;
        private System.Windows.Forms.Label lblMajorID;
        private System.Windows.Forms.ComboBox cboAcadStat;
        private System.Windows.Forms.ComboBox cboYearLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
    }
}