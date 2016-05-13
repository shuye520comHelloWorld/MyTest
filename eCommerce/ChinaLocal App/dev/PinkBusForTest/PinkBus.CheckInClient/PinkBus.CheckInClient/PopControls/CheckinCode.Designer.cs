namespace PinkBus.CheckInClient.PopControls
{
    partial class CheckinCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckinCode));
            this.label3 = new System.Windows.Forms.Label();
            this.checkincode_submit_btn = new System.Windows.Forms.Button();
            this.checkincode_smstoken = new System.Windows.Forms.TextBox();
            this.checkincode_label2 = new System.Windows.Forms.Label();
            this.checkincode_phone = new System.Windows.Forms.TextBox();
            this.checkincode_label1 = new System.Windows.Forms.Label();
            this.checkincode_title_panel = new System.Windows.Forms.Panel();
            this.checkincode_close_btn = new System.Windows.Forms.Button();
            this.checkincode_title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkincode_title_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(74, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "二维码请通过扫描设备进行验证";
            // 
            // checkincode_submit_btn
            // 
            this.checkincode_submit_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but10;
            this.checkincode_submit_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkincode_submit_btn.FlatAppearance.BorderSize = 0;
            this.checkincode_submit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.checkincode_submit_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkincode_submit_btn.Location = new System.Drawing.Point(80, 227);
            this.checkincode_submit_btn.Name = "checkincode_submit_btn";
            this.checkincode_submit_btn.Size = new System.Drawing.Size(222, 44);
            this.checkincode_submit_btn.TabIndex = 5;
            this.checkincode_submit_btn.UseVisualStyleBackColor = true;
            this.checkincode_submit_btn.Click += new System.EventHandler(this.checkincode_submit_btn_Click);
            // 
            // checkincode_smstoken
            // 
            this.checkincode_smstoken.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkincode_smstoken.Location = new System.Drawing.Point(154, 153);
            this.checkincode_smstoken.MaxLength = 6;
            this.checkincode_smstoken.Name = "checkincode_smstoken";
            this.checkincode_smstoken.Size = new System.Drawing.Size(142, 32);
            this.checkincode_smstoken.TabIndex = 4;
            this.checkincode_smstoken.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // checkincode_label2
            // 
            this.checkincode_label2.AutoSize = true;
            this.checkincode_label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkincode_label2.Location = new System.Drawing.Point(53, 156);
            this.checkincode_label2.Name = "checkincode_label2";
            this.checkincode_label2.Size = new System.Drawing.Size(74, 21);
            this.checkincode_label2.TabIndex = 3;
            this.checkincode_label2.Text = "短信口令";
            // 
            // checkincode_phone
            // 
            this.checkincode_phone.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkincode_phone.Location = new System.Drawing.Point(154, 89);
            this.checkincode_phone.MaxLength = 11;
            this.checkincode_phone.Name = "checkincode_phone";
            this.checkincode_phone.Size = new System.Drawing.Size(142, 32);
            this.checkincode_phone.TabIndex = 2;
            this.checkincode_phone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // checkincode_label1
            // 
            this.checkincode_label1.AutoSize = true;
            this.checkincode_label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkincode_label1.Location = new System.Drawing.Point(53, 92);
            this.checkincode_label1.Name = "checkincode_label1";
            this.checkincode_label1.Size = new System.Drawing.Size(74, 21);
            this.checkincode_label1.TabIndex = 1;
            this.checkincode_label1.Text = "手机号码";
            // 
            // checkincode_title_panel
            // 
            this.checkincode_title_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(108)))), ((int)(((byte)(117)))));
            this.checkincode_title_panel.Controls.Add(this.checkincode_close_btn);
            this.checkincode_title_panel.Controls.Add(this.checkincode_title);
            this.checkincode_title_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkincode_title_panel.Location = new System.Drawing.Point(0, 0);
            this.checkincode_title_panel.Name = "checkincode_title_panel";
            this.checkincode_title_panel.Size = new System.Drawing.Size(375, 42);
            this.checkincode_title_panel.TabIndex = 0;
            this.checkincode_title_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkincode_title_panel_MouseDown);
            // 
            // checkincode_close_btn
            // 
            this.checkincode_close_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("checkincode_close_btn.BackgroundImage")));
            this.checkincode_close_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkincode_close_btn.FlatAppearance.BorderSize = 0;
            this.checkincode_close_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkincode_close_btn.Location = new System.Drawing.Point(338, 11);
            this.checkincode_close_btn.Name = "checkincode_close_btn";
            this.checkincode_close_btn.Size = new System.Drawing.Size(20, 20);
            this.checkincode_close_btn.TabIndex = 1;
            this.checkincode_close_btn.UseVisualStyleBackColor = true;
            this.checkincode_close_btn.Click += new System.EventHandler(this.checkincode_close_btn_Click);
            // 
            // checkincode_title
            // 
            this.checkincode_title.AutoSize = true;
            this.checkincode_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkincode_title.ForeColor = System.Drawing.SystemColors.Window;
            this.checkincode_title.Location = new System.Drawing.Point(12, 12);
            this.checkincode_title.Margin = new System.Windows.Forms.Padding(0);
            this.checkincode_title.Name = "checkincode_title";
            this.checkincode_title.Size = new System.Drawing.Size(74, 21);
            this.checkincode_title.TabIndex = 0;
            this.checkincode_title.Text = "口令签到";
            this.checkincode_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkincode_title_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(53, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "(可填写后4位)";
            // 
            // CheckinCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(375, 337);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkincode_submit_btn);
            this.Controls.Add(this.checkincode_smstoken);
            this.Controls.Add(this.checkincode_label2);
            this.Controls.Add(this.checkincode_phone);
            this.Controls.Add(this.checkincode_label1);
            this.Controls.Add(this.checkincode_title_panel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "CheckinCode";
            this.Text = "CheckinCode";
            this.Load += new System.EventHandler(this.CheckinCode_Load);
            this.checkincode_title_panel.ResumeLayout(false);
            this.checkincode_title_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel checkincode_title_panel;
        private System.Windows.Forms.Button checkincode_close_btn;
        private System.Windows.Forms.Label checkincode_title;
        private System.Windows.Forms.Label checkincode_label1;
        private System.Windows.Forms.TextBox checkincode_phone;
        private System.Windows.Forms.Label checkincode_label2;
        private System.Windows.Forms.TextBox checkincode_smstoken;
        private System.Windows.Forms.Button checkincode_submit_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}