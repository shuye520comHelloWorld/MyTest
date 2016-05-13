namespace PinkBus.OfflineCustomer.UserControls
{
    partial class EventForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventForm));
            this.eventPannel = new System.Windows.Forms.Panel();
            this.downloadPanel = new System.Windows.Forms.Panel();
            this.downLoad_Btn = new System.Windows.Forms.Button();
            this.downLoad_Label = new System.Windows.Forms.Label();
            this.flowLayout_Panel = new System.Windows.Forms.Panel();
            this.eventPannel.SuspendLayout();
            this.downloadPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // eventPannel
            // 
            this.eventPannel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventPannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.eventPannel.Controls.Add(this.downloadPanel);
            this.eventPannel.Controls.Add(this.flowLayout_Panel);
            this.eventPannel.Location = new System.Drawing.Point(-114, 13);
            this.eventPannel.Margin = new System.Windows.Forms.Padding(0);
            this.eventPannel.Name = "eventPannel";
            this.eventPannel.Size = new System.Drawing.Size(1100, 580);
            this.eventPannel.TabIndex = 4;
            // 
            // downloadPanel
            // 
            this.downloadPanel.BackColor = System.Drawing.Color.White;
            this.downloadPanel.Controls.Add(this.downLoad_Btn);
            this.downloadPanel.Controls.Add(this.downLoad_Label);
            this.downloadPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.downloadPanel.Location = new System.Drawing.Point(0, 0);
            this.downloadPanel.Name = "downloadPanel";
            this.downloadPanel.Size = new System.Drawing.Size(1100, 60);
            this.downloadPanel.TabIndex = 1;
            // 
            // downLoad_Btn
            // 
            this.downLoad_Btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("downLoad_Btn.BackgroundImage")));
            this.downLoad_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.downLoad_Btn.FlatAppearance.BorderSize = 0;
            this.downLoad_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downLoad_Btn.Location = new System.Drawing.Point(941, 9);
            this.downLoad_Btn.Name = "downLoad_Btn";
            this.downLoad_Btn.Size = new System.Drawing.Size(120, 42);
            this.downLoad_Btn.TabIndex = 1;
            this.downLoad_Btn.UseVisualStyleBackColor = true;
            // 
            // downLoad_Label
            // 
            this.downLoad_Label.AutoSize = true;
            this.downLoad_Label.Font = new System.Drawing.Font("Microsoft YaHei", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downLoad_Label.Location = new System.Drawing.Point(24, 20);
            this.downLoad_Label.Name = "downLoad_Label";
            this.downLoad_Label.Size = new System.Drawing.Size(136, 24);
            this.downLoad_Label.TabIndex = 0;
            this.downLoad_Label.Text = "请选择活动信息";
            // 
            // flowLayout_Panel
            // 
            this.flowLayout_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayout_Panel.AutoScroll = true;
            this.flowLayout_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.flowLayout_Panel.Location = new System.Drawing.Point(0, 80);
            this.flowLayout_Panel.Name = "flowLayout_Panel";
            this.flowLayout_Panel.Size = new System.Drawing.Size(1084, 530);
            this.flowLayout_Panel.TabIndex = 0;
            // 
            // EventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 607);
            this.Controls.Add(this.eventPannel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EventForm";
            this.Text = "EventForm";
            this.Load += new System.EventHandler(this.EventForm_Load);
            this.eventPannel.ResumeLayout(false);
            this.downloadPanel.ResumeLayout(false);
            this.downloadPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel eventPannel;
        private System.Windows.Forms.Panel downloadPanel;
        private System.Windows.Forms.Button downLoad_Btn;
        private System.Windows.Forms.Label downLoad_Label;
        private System.Windows.Forms.Panel flowLayout_Panel;

    }
}