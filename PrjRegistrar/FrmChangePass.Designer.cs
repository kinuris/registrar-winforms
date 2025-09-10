namespace PrjRegistrar
{
    partial class FrmChangePass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChangePass));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblencrypt = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOldPass = new System.Windows.Forms.TextBox();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.txtConfirmNewPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPeekOld = new System.Windows.Forms.Button();
            this.btnPeekNew = new System.Windows.Forms.Button();
            this.btnPeekConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lblencrypt
            // 
            this.lblencrypt.AutoSize = true;
            this.lblencrypt.Location = new System.Drawing.Point(30, 389);
            this.lblencrypt.Name = "lblencrypt";
            this.lblencrypt.Size = new System.Drawing.Size(52, 13);
            this.lblencrypt.TabIndex = 10;
            this.lblencrypt.Text = "lblencrypt";
            this.lblencrypt.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(29, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Old Password";
            // 
            // txtOldPass
            // 
            this.txtOldPass.BackColor = System.Drawing.SystemColors.Window;
            this.txtOldPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOldPass.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldPass.Location = new System.Drawing.Point(30, 172);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.Size = new System.Drawing.Size(259, 28);
            this.txtOldPass.TabIndex = 1;
            this.txtOldPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOldPass.UseSystemPasswordChar = true;
            this.txtOldPass.Enter += new System.EventHandler(this.TxtOldPass_Enter);
            this.txtOldPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOldPass_KeyPress);
            this.txtOldPass.Leave += new System.EventHandler(this.TxtOldPass_Leave);
            // 
            // txtNewPass
            // 
            this.txtNewPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPass.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPass.Location = new System.Drawing.Point(30, 248);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(259, 28);
            this.txtNewPass.TabIndex = 3;
            this.txtNewPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNewPass.UseSystemPasswordChar = true;
            this.txtNewPass.Enter += new System.EventHandler(this.TxtNewPass_Enter);
            this.txtNewPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNewPass_KeyPress);
            this.txtNewPass.Leave += new System.EventHandler(this.TxtNewPass_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(29, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "New Password";
            // 
            // btnChangePass
            // 
            this.btnChangePass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.btnChangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePass.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePass.ForeColor = System.Drawing.Color.White;
            this.btnChangePass.Location = new System.Drawing.Point(30, 405);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(292, 48);
            this.btnChangePass.TabIndex = 7;
            this.btnChangePass.Text = "Change Password";
            this.btnChangePass.UseVisualStyleBackColor = false;
            this.btnChangePass.Click += new System.EventHandler(this.BtnChangePass_Click);
            // 
            // txtConfirmNewPass
            // 
            this.txtConfirmNewPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfirmNewPass.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmNewPass.Location = new System.Drawing.Point(30, 325);
            this.txtConfirmNewPass.Name = "txtConfirmNewPass";
            this.txtConfirmNewPass.Size = new System.Drawing.Size(259, 28);
            this.txtConfirmNewPass.TabIndex = 5;
            this.txtConfirmNewPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConfirmNewPass.UseSystemPasswordChar = true;
            this.txtConfirmNewPass.Enter += new System.EventHandler(this.TxtConfirmNewPass_Enter);
            this.txtConfirmNewPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtConfirmNewPass_KeyPress);
            this.txtConfirmNewPass.Leave += new System.EventHandler(this.TxtConfirmNewPass_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(29, 298);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "Confirm Password";
            // 
            // btnPeekOld
            // 
            this.btnPeekOld.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPeekOld.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeekOld.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeekOld.ForeColor = System.Drawing.Color.White;
            this.btnPeekOld.Image = ((System.Drawing.Image)(resources.GetObject("btnPeekOld.Image")));
            this.btnPeekOld.Location = new System.Drawing.Point(290, 172);
            this.btnPeekOld.Name = "btnPeekOld";
            this.btnPeekOld.Size = new System.Drawing.Size(32, 28);
            this.btnPeekOld.TabIndex = 2;
            this.btnPeekOld.UseVisualStyleBackColor = false;
            this.btnPeekOld.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnPeekOld_KeyDown);
            this.btnPeekOld.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BtnPeekOld_KeyUp);
            this.btnPeekOld.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPeekOld_MouseDown);
            this.btnPeekOld.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnPeekOld_MouseUp);
            // 
            // btnPeekNew
            // 
            this.btnPeekNew.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPeekNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeekNew.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeekNew.ForeColor = System.Drawing.Color.White;
            this.btnPeekNew.Image = ((System.Drawing.Image)(resources.GetObject("btnPeekNew.Image")));
            this.btnPeekNew.Location = new System.Drawing.Point(290, 248);
            this.btnPeekNew.Name = "btnPeekNew";
            this.btnPeekNew.Size = new System.Drawing.Size(32, 28);
            this.btnPeekNew.TabIndex = 4;
            this.btnPeekNew.UseVisualStyleBackColor = false;
            this.btnPeekNew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnPeekNew_KeyDown);
            this.btnPeekNew.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BtnPeekNew_KeyUp);
            this.btnPeekNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPeekNew_MouseDown);
            this.btnPeekNew.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnPeekNew_MouseUp);
            // 
            // btnPeekConfirm
            // 
            this.btnPeekConfirm.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPeekConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeekConfirm.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeekConfirm.ForeColor = System.Drawing.Color.White;
            this.btnPeekConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btnPeekConfirm.Image")));
            this.btnPeekConfirm.Location = new System.Drawing.Point(290, 325);
            this.btnPeekConfirm.Name = "btnPeekConfirm";
            this.btnPeekConfirm.Size = new System.Drawing.Size(32, 28);
            this.btnPeekConfirm.TabIndex = 6;
            this.btnPeekConfirm.UseVisualStyleBackColor = false;
            this.btnPeekConfirm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnPeekConfirm_KeyDown);
            this.btnPeekConfirm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BtnPeekConfirm_KeyUp);
            this.btnPeekConfirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPeekConfirm_MouseDown);
            this.btnPeekConfirm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnPeekConfirm_MouseUp);
            // 
            // FrmChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(350, 489);
            this.Controls.Add(this.btnPeekConfirm);
            this.Controls.Add(this.btnPeekNew);
            this.Controls.Add(this.btnPeekOld);
            this.Controls.Add(this.btnChangePass);
            this.Controls.Add(this.lblencrypt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtConfirmNewPass);
            this.Controls.Add(this.txtNewPass);
            this.Controls.Add(this.txtOldPass);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FilCIS v1.0";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblencrypt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOldPass;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangePass;
        private System.Windows.Forms.TextBox txtConfirmNewPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPeekOld;
        private System.Windows.Forms.Button btnPeekNew;
        private System.Windows.Forms.Button btnPeekConfirm;
    }
}