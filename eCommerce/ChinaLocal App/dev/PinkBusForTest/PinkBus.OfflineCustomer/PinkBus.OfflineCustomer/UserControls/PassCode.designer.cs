namespace PinkBus.OfflineCustomer.UserControls
{
    partial class PassCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassCode));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.passcode_Pogress_Panel = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressPercent_Label = new System.Windows.Forms.Label();
            this.process_Lable = new System.Windows.Forms.Label();
            this.passCode_Panel = new System.Windows.Forms.Panel();
            this.passcodeWarn_Lable = new System.Windows.Forms.Label();
            this.passcode_Confirm_Txt2 = new System.Windows.Forms.Label();
            this.passcode_Confirm_Txt1 = new System.Windows.Forms.Label();
            this.passcode_Cancel_Btn = new System.Windows.Forms.Button();
            this.passcode_Submit_Btn = new System.Windows.Forms.Button();
            this.passcode_TextBox = new System.Windows.Forms.TextBox();
            this.passcodeTop_Panel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.passcode_Title = new System.Windows.Forms.Label();
            this.passcode_Pogress_Panel.SuspendLayout();
            this.passCode_Panel.SuspendLayout();
            this.passcodeTop_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // passcode_Pogress_Panel
            // 
            this.passcode_Pogress_Panel.Controls.Add(this.progressBar);
            this.passcode_Pogress_Panel.Controls.Add(this.progressPercent_Label);
            this.passcode_Pogress_Panel.Controls.Add(this.process_Lable);
            this.passcode_Pogress_Panel.Location = new System.Drawing.Point(0, 47);
            this.passcode_Pogress_Panel.Name = "passcode_Pogress_Panel";
            this.passcode_Pogress_Panel.Size = new System.Drawing.Size(399, 231);
            this.passcode_Pogress_Panel.TabIndex = 5;
            this.passcode_Pogress_Panel.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(45, 78);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(314, 23);
            this.progressBar.TabIndex = 0;
            // 
            // progressPercent_Label
            // 
            this.progressPercent_Label.AutoSize = true;
            this.progressPercent_Label.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progressPercent_Label.ForeColor = System.Drawing.Color.Gray;
            this.progressPercent_Label.Location = new System.Drawing.Point(208, 119);
            this.progressPercent_Label.Name = "progressPercent_Label";
            this.progressPercent_Label.Size = new System.Drawing.Size(29, 21);
            this.progressPercent_Label.TabIndex = 1;
            this.progressPercent_Label.Text = " 0 ";
            // 
            // process_Lable
            // 
            this.process_Lable.AutoSize = true;
            this.process_Lable.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.process_Lable.ForeColor = System.Drawing.Color.Gray;
            this.process_Lable.Location = new System.Drawing.Point(131, 118);
            this.process_Lable.Name = "process_Lable";
            this.process_Lable.Size = new System.Drawing.Size(134, 21);
            this.process_Lable.TabIndex = 1;
            this.process_Lable.Text = "数据下载：      %";
            // 
            // passCode_Panel
            // 
            this.passCode_Panel.Controls.Add(this.passcodeWarn_Lable);
            this.passCode_Panel.Controls.Add(this.passcode_Confirm_Txt2);
            this.passCode_Panel.Controls.Add(this.passcode_Confirm_Txt1);
            this.passCode_Panel.Controls.Add(this.passcode_Cancel_Btn);
            this.passCode_Panel.Controls.Add(this.passcode_Submit_Btn);
            this.passCode_Panel.Controls.Add(this.passcode_TextBox);
            this.passCode_Panel.Location = new System.Drawing.Point(0, 47);
            this.passCode_Panel.Name = "passCode_Panel";
            this.passCode_Panel.Size = new System.Drawing.Size(399, 231);
            this.passCode_Panel.TabIndex = 4;
            // 
            // passcodeWarn_Lable
            // 
            this.passcodeWarn_Lable.AutoSize = true;
            this.passcodeWarn_Lable.ForeColor = System.Drawing.Color.Red;
            this.passcodeWarn_Lable.Location = new System.Drawing.Point(55, 101);
            this.passcodeWarn_Lable.Name = "passcodeWarn_Lable";
            this.passcodeWarn_Lable.Size = new System.Drawing.Size(0, 12);
            this.passcodeWarn_Lable.TabIndex = 6;
            this.passcodeWarn_Lable.Visible = false;
            // 
            // passcode_Confirm_Txt2
            // 
            this.passcode_Confirm_Txt2.AutoSize = true;
            this.passcode_Confirm_Txt2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passcode_Confirm_Txt2.ForeColor = System.Drawing.Color.DimGray;
            this.passcode_Confirm_Txt2.Location = new System.Drawing.Point(129, 76);
            this.passcode_Confirm_Txt2.Name = "passcode_Confirm_Txt2";
            this.passcode_Confirm_Txt2.Size = new System.Drawing.Size(138, 21);
            this.passcode_Confirm_Txt2.TabIndex = 5;
            this.passcode_Confirm_Txt2.Text = "您要继续下载吗？";
            this.passcode_Confirm_Txt2.Visible = false;
            // 
            // passcode_Confirm_Txt1
            // 
            this.passcode_Confirm_Txt1.AutoSize = true;
            this.passcode_Confirm_Txt1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passcode_Confirm_Txt1.ForeColor = System.Drawing.Color.DimGray;
            this.passcode_Confirm_Txt1.Location = new System.Drawing.Point(51, 50);
            this.passcode_Confirm_Txt1.Name = "passcode_Confirm_Txt1";
            this.passcode_Confirm_Txt1.Size = new System.Drawing.Size(298, 21);
            this.passcode_Confirm_Txt1.TabIndex = 4;
            this.passcode_Confirm_Txt1.Text = "当前活动下载尚未完成，完成后才能打开";
            this.passcode_Confirm_Txt1.Visible = false;
            // 
            // passcode_Cancel_Btn
            // 
            this.passcode_Cancel_Btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("passcode_Cancel_Btn.BackgroundImage")));
            this.passcode_Cancel_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.passcode_Cancel_Btn.FlatAppearance.BorderSize = 0;
            this.passcode_Cancel_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.passcode_Cancel_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passcode_Cancel_Btn.Location = new System.Drawing.Point(47, 152);
            this.passcode_Cancel_Btn.Name = "passcode_Cancel_Btn";
            this.passcode_Cancel_Btn.Size = new System.Drawing.Size(126, 39);
            this.passcode_Cancel_Btn.TabIndex = 2;
            this.passcode_Cancel_Btn.UseVisualStyleBackColor = true;
            this.passcode_Cancel_Btn.Click += new System.EventHandler(this.passcode_cancel_button_Click);
            // 
            // passcode_Submit_Btn
            // 
            this.passcode_Submit_Btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("passcode_Submit_Btn.BackgroundImage")));
            this.passcode_Submit_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.passcode_Submit_Btn.FlatAppearance.BorderSize = 0;
            this.passcode_Submit_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.passcode_Submit_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passcode_Submit_Btn.Location = new System.Drawing.Point(231, 152);
            this.passcode_Submit_Btn.Name = "passcode_Submit_Btn";
            this.passcode_Submit_Btn.Size = new System.Drawing.Size(128, 39);
            this.passcode_Submit_Btn.TabIndex = 3;
            this.passcode_Submit_Btn.UseVisualStyleBackColor = true;
            this.passcode_Submit_Btn.Click += new System.EventHandler(this.passcode_submit_button_Click);
            // 
            // passcode_TextBox
            // 
            this.passcode_TextBox.Font = new System.Drawing.Font("Microsoft YaHei", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passcode_TextBox.Location = new System.Drawing.Point(55, 59);
            this.passcode_TextBox.Name = "passcode_TextBox";
            this.passcode_TextBox.Size = new System.Drawing.Size(294, 32);
            this.passcode_TextBox.TabIndex = 1;
            this.passcode_TextBox.Click += new System.EventHandler(this.passcode_textBox_Click);
            // 
            // passcodeTop_Panel
            // 
            this.passcodeTop_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(108)))), ((int)(((byte)(117)))));
            this.passcodeTop_Panel.Controls.Add(this.button1);
            this.passcodeTop_Panel.Controls.Add(this.passcode_Title);
            this.passcodeTop_Panel.Location = new System.Drawing.Point(0, 0);
            this.passcodeTop_Panel.Name = "passcodeTop_Panel";
            this.passcodeTop_Panel.Size = new System.Drawing.Size(399, 45);
            this.passcodeTop_Panel.TabIndex = 1;
            this.passcodeTop_Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(458, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // passcode_Title
            // 
            this.passcode_Title.AutoSize = true;
            this.passcode_Title.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passcode_Title.ForeColor = System.Drawing.Color.White;
            this.passcode_Title.Location = new System.Drawing.Point(12, 12);
            this.passcode_Title.Name = "passcode_Title";
            this.passcode_Title.Size = new System.Drawing.Size(163, 21);
            this.passcode_Title.TabIndex = 0;
            this.passcode_Title.Text = "请输入6位活动下载码";
            this.passcode_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.passcode_title_MouseDown);
            // 
            // PassCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 278);
            this.Controls.Add(this.passcodeTop_Panel);
            this.Controls.Add(this.passcode_Pogress_Panel);
            this.Controls.Add(this.passCode_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PassCode";
            this.Text = "下载活动";
            this.Load += new System.EventHandler(this.PassCode_Load);
            this.passcode_Pogress_Panel.ResumeLayout(false);
            this.passcode_Pogress_Panel.PerformLayout();
            this.passCode_Panel.ResumeLayout(false);
            this.passCode_Panel.PerformLayout();
            this.passcodeTop_Panel.ResumeLayout(false);
            this.passcodeTop_Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label passcode_Title;
        private System.Windows.Forms.TextBox passcode_TextBox;
        private System.Windows.Forms.Button passcode_Cancel_Btn;
        private System.Windows.Forms.Button passcode_Submit_Btn;
        private System.Windows.Forms.Panel passCode_Panel;
        private System.Windows.Forms.Panel passcode_Pogress_Panel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel passcodeTop_Panel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label progressPercent_Label;
        private System.Windows.Forms.Label process_Lable;
        private System.Windows.Forms.Label passcode_Confirm_Txt2;
        private System.Windows.Forms.Label passcode_Confirm_Txt1;
        private System.Windows.Forms.Label passcodeWarn_Lable;
    }
}