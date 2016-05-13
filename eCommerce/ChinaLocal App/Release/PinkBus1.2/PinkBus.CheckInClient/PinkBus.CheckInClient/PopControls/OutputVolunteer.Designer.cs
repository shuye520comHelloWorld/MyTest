namespace PinkBus.CheckInClient.PopControls
{
    partial class OutputVolunteer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputVolunteer));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.outputvolunteer_submit_btn = new System.Windows.Forms.Button();
            this.outputvolunteer_title_panel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.outputvolunteer_title_label = new System.Windows.Forms.Label();
            this.outputvolunteer_title_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 62);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(284, 147);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // outputvolunteer_submit_btn
            // 
            this.outputvolunteer_submit_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but13;
            this.outputvolunteer_submit_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.outputvolunteer_submit_btn.FlatAppearance.BorderSize = 0;
            this.outputvolunteer_submit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.outputvolunteer_submit_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.outputvolunteer_submit_btn.Location = new System.Drawing.Point(42, 233);
            this.outputvolunteer_submit_btn.Name = "outputvolunteer_submit_btn";
            this.outputvolunteer_submit_btn.Size = new System.Drawing.Size(222, 44);
            this.outputvolunteer_submit_btn.TabIndex = 2;
            this.outputvolunteer_submit_btn.UseVisualStyleBackColor = true;
            this.outputvolunteer_submit_btn.Click += new System.EventHandler(this.outputvolunteer_submit_btn_Click);
            // 
            // outputvolunteer_title_panel
            // 
            this.outputvolunteer_title_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(108)))), ((int)(((byte)(117)))));
            this.outputvolunteer_title_panel.Controls.Add(this.button1);
            this.outputvolunteer_title_panel.Controls.Add(this.outputvolunteer_title_label);
            this.outputvolunteer_title_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.outputvolunteer_title_panel.Location = new System.Drawing.Point(0, 0);
            this.outputvolunteer_title_panel.Name = "outputvolunteer_title_panel";
            this.outputvolunteer_title_panel.Size = new System.Drawing.Size(306, 45);
            this.outputvolunteer_title_panel.TabIndex = 0;
            this.outputvolunteer_title_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.outputvolunteer_title_panel_MouseDown);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(268, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // outputvolunteer_title_label
            // 
            this.outputvolunteer_title_label.AutoSize = true;
            this.outputvolunteer_title_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outputvolunteer_title_label.ForeColor = System.Drawing.Color.White;
            this.outputvolunteer_title_label.Location = new System.Drawing.Point(12, 12);
            this.outputvolunteer_title_label.Name = "outputvolunteer_title_label";
            this.outputvolunteer_title_label.Size = new System.Drawing.Size(122, 21);
            this.outputvolunteer_title_label.TabIndex = 0;
            this.outputvolunteer_title_label.Text = "导出签到志愿者";
            this.outputvolunteer_title_label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.outputvolunteer_title_label_MouseDown);
            // 
            // OutputVolunteer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(306, 299);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.outputvolunteer_submit_btn);
            this.Controls.Add(this.outputvolunteer_title_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "OutputVolunteer";
            this.Text = "OutputVolunteer";
            this.Load += new System.EventHandler(this.OutputVolunteer_Load);
            this.outputvolunteer_title_panel.ResumeLayout(false);
            this.outputvolunteer_title_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel outputvolunteer_title_panel;
        private System.Windows.Forms.Label outputvolunteer_title_label;
        private System.Windows.Forms.Button outputvolunteer_submit_btn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
    }
}