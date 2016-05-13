namespace PinkBus.CheckInClient.PopControls
{
    partial class UploadPassCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadPassCode));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PassCode_panel = new System.Windows.Forms.Panel();
            this.passcede_warn = new System.Windows.Forms.Label();
            this.passcode_textBox = new System.Windows.Forms.TextBox();
            this.passcode_cancel_button = new System.Windows.Forms.Button();
            this.passcode_submit_button = new System.Windows.Forms.Button();
            this.passcode_progress_panel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.progress_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.passcode_title = new System.Windows.Forms.Label();
            this.PassCode_panel.SuspendLayout();
            this.passcode_progress_panel.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // PassCode_panel
            // 
            this.PassCode_panel.Controls.Add(this.passcede_warn);
            this.PassCode_panel.Controls.Add(this.passcode_textBox);
            this.PassCode_panel.Controls.Add(this.passcode_cancel_button);
            this.PassCode_panel.Controls.Add(this.passcode_submit_button);
            this.PassCode_panel.Location = new System.Drawing.Point(16, 70);
            this.PassCode_panel.Name = "PassCode_panel";
            this.PassCode_panel.Size = new System.Drawing.Size(355, 220);
            this.PassCode_panel.TabIndex = 1;
            // 
            // passcede_warn
            // 
            this.passcede_warn.AutoSize = true;
            this.passcede_warn.ForeColor = System.Drawing.Color.Red;
            this.passcede_warn.Location = new System.Drawing.Point(27, 87);
            this.passcede_warn.Name = "passcede_warn";
            this.passcede_warn.Size = new System.Drawing.Size(155, 12);
            this.passcede_warn.TabIndex = 7;
            this.passcede_warn.Text = "该活动已下载,无法再次导入";
            this.passcede_warn.Visible = false;
            // 
            // passcode_textBox
            // 
            this.passcode_textBox.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passcode_textBox.Location = new System.Drawing.Point(29, 44);
            this.passcode_textBox.Name = "passcode_textBox";
            this.passcode_textBox.Size = new System.Drawing.Size(294, 32);
            this.passcode_textBox.TabIndex = 4;
            this.passcode_textBox.Click += new System.EventHandler(this.passcode_textBox_Click);
            this.passcode_textBox.TextChanged += new System.EventHandler(this.passcode_textBox_Click);
            // 
            // passcode_cancel_button
            // 
            this.passcode_cancel_button.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but3;
            this.passcode_cancel_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.passcode_cancel_button.FlatAppearance.BorderSize = 0;
            this.passcode_cancel_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.passcode_cancel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passcode_cancel_button.Location = new System.Drawing.Point(29, 169);
            this.passcode_cancel_button.Name = "passcode_cancel_button";
            this.passcode_cancel_button.Size = new System.Drawing.Size(126, 39);
            this.passcode_cancel_button.TabIndex = 5;
            this.passcode_cancel_button.UseVisualStyleBackColor = true;
            this.passcode_cancel_button.Click += new System.EventHandler(this.passcode_cancel_button_Click);
            // 
            // passcode_submit_button
            // 
            this.passcode_submit_button.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but4;
            this.passcode_submit_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.passcode_submit_button.FlatAppearance.BorderSize = 0;
            this.passcode_submit_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.passcode_submit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passcode_submit_button.Location = new System.Drawing.Point(205, 169);
            this.passcode_submit_button.Name = "passcode_submit_button";
            this.passcode_submit_button.Size = new System.Drawing.Size(128, 39);
            this.passcode_submit_button.TabIndex = 6;
            this.passcode_submit_button.UseVisualStyleBackColor = true;
            this.passcode_submit_button.Click += new System.EventHandler(this.passcode_submit_button_Click);
            // 
            // passcode_progress_panel
            // 
            this.passcode_progress_panel.Controls.Add(this.progressBar1);
            this.passcode_progress_panel.Controls.Add(this.label1);
            this.passcode_progress_panel.Controls.Add(this.progress_label);
            this.passcode_progress_panel.Location = new System.Drawing.Point(10, 61);
            this.passcode_progress_panel.Name = "passcode_progress_panel";
            this.passcode_progress_panel.Size = new System.Drawing.Size(366, 232);
            this.passcode_progress_panel.TabIndex = 2;
            this.passcode_progress_panel.Tag = "共有1000条变更记录需要上传";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(26, 45);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(314, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(78, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "共有 0 条变更记录需要上传";
            // 
            // progress_label
            // 
            this.progress_label.AutoSize = true;
            this.progress_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progress_label.ForeColor = System.Drawing.Color.Gray;
            this.progress_label.Location = new System.Drawing.Point(84, 110);
            this.progress_label.Name = "progress_label";
            this.progress_label.Size = new System.Drawing.Size(157, 21);
            this.progress_label.TabIndex = 5;
            this.progress_label.Text = "正在上传第 0 条记录";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(108)))), ((int)(((byte)(117)))));
            this.panel1.Controls.Add(this.passcode_title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 45);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // passcode_title
            // 
            this.passcode_title.AutoSize = true;
            this.passcode_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passcode_title.ForeColor = System.Drawing.Color.White;
            this.passcode_title.Location = new System.Drawing.Point(12, 12);
            this.passcode_title.Name = "passcode_title";
            this.passcode_title.Size = new System.Drawing.Size(163, 21);
            this.passcode_title.TabIndex = 1;
            this.passcode_title.Text = "请输入6位活动上传码";
            this.passcode_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // UploadPassCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(399, 308);
            this.Controls.Add(this.PassCode_panel);
            this.Controls.Add(this.passcode_progress_panel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UploadPassCode";
            this.Text = "UploadPassCode";
            this.Load += new System.EventHandler(this.UploadPassCode_Load);
            this.PassCode_panel.ResumeLayout(false);
            this.PassCode_panel.PerformLayout();
            this.passcode_progress_panel.ResumeLayout(false);
            this.passcode_progress_panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label passcode_title;
        private System.Windows.Forms.Panel PassCode_panel;
        private System.Windows.Forms.TextBox passcode_textBox;
        private System.Windows.Forms.Button passcode_cancel_button;
        private System.Windows.Forms.Button passcode_submit_button;
        private System.Windows.Forms.Panel passcode_progress_panel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label progress_label;
        private System.Windows.Forms.Label passcede_warn;
    }
}