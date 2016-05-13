namespace PinkBus.CheckInClient.PopControls
{
    partial class DownloadConfirm
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.messageshow_title = new System.Windows.Forms.Label();
            this.messageshow_msg = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::PinkBus.CheckInClient.Properties.Resources.but3;
            this.button1.Location = new System.Drawing.Point(94, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 37);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(108)))), ((int)(((byte)(117)))));
            this.panel1.Controls.Add(this.messageshow_title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(311, 45);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // messageshow_title
            // 
            this.messageshow_title.AutoSize = true;
            this.messageshow_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.messageshow_title.ForeColor = System.Drawing.Color.White;
            this.messageshow_title.Location = new System.Drawing.Point(28, 14);
            this.messageshow_title.Name = "messageshow_title";
            this.messageshow_title.Size = new System.Drawing.Size(74, 21);
            this.messageshow_title.TabIndex = 0;
            this.messageshow_title.Text = "错误信息";
            this.messageshow_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.messageshow_title_MouseDown);
            // 
            // messageshow_msg
            // 
            this.messageshow_msg.AutoSize = true;
            this.messageshow_msg.Location = new System.Drawing.Point(133, 117);
            this.messageshow_msg.Name = "messageshow_msg";
            this.messageshow_msg.Size = new System.Drawing.Size(41, 12);
            this.messageshow_msg.TabIndex = 2;
            this.messageshow_msg.Text = "label2";
            // 
            // DownloadConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(311, 262);
            this.Controls.Add(this.messageshow_msg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DownloadConfirm";
            this.Text = "MessageShow";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label messageshow_title;
        private System.Windows.Forms.Label messageshow_msg;
    }
}