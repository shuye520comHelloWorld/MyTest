namespace PinkBus.CheckInClient
{
    partial class ExtendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtendForm));
            this.eventTitle = new System.Windows.Forms.Label();
            this.CustomerName = new System.Windows.Forms.Label();
            this.welcome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // eventTitle
            // 
            this.eventTitle.AutoSize = true;
            this.eventTitle.BackColor = System.Drawing.Color.Transparent;
            this.eventTitle.Font = new System.Drawing.Font("Microsoft YaHei", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.eventTitle.Location = new System.Drawing.Point(129, 187);
            this.eventTitle.MaximumSize = new System.Drawing.Size(1200, 190);
            this.eventTitle.Name = "eventTitle";
            this.eventTitle.Size = new System.Drawing.Size(1155, 190);
            this.eventTitle.TabIndex = 0;
            this.eventTitle.Text = "粉巴中国行--长春终极会长春终极会长春终极会";
            this.eventTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.eventTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.eventTitle_Paint);
            // 
            // CustomerName
            // 
            this.CustomerName.AutoSize = true;
            this.CustomerName.BackColor = System.Drawing.Color.Transparent;
            this.CustomerName.Font = new System.Drawing.Font("Microsoft YaHei", 55F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.CustomerName.Location = new System.Drawing.Point(351, 428);
            this.CustomerName.MaximumSize = new System.Drawing.Size(1000, 100);
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Size = new System.Drawing.Size(432, 95);
            this.CustomerName.TabIndex = 1;
            this.CustomerName.Text = "贵宾 瘦先生";
            this.CustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CustomerName.TextChanged += new System.EventHandler(this.CustomerName_TextChanged);
            this.CustomerName.Paint += new System.Windows.Forms.PaintEventHandler(this.CustomerName_Paint);
            // 
            // welcome
            // 
            this.welcome.AutoSize = true;
            this.welcome.BackColor = System.Drawing.Color.Transparent;
            this.welcome.Font = new System.Drawing.Font("Microsoft YaHei", 55F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.welcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.welcome.Location = new System.Drawing.Point(651, 546);
            this.welcome.Name = "welcome";
            this.welcome.Size = new System.Drawing.Size(336, 95);
            this.welcome.TabIndex = 2;
            this.welcome.Text = "欢迎您！";
            // 
            // ExtendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1084, 742);
            this.Controls.Add(this.welcome);
            this.Controls.Add(this.CustomerName);
            this.Controls.Add(this.eventTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExtendForm";
            this.Text = "ExternalForm";
            this.Load += new System.EventHandler(this.ExtendForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label eventTitle;
        private System.Windows.Forms.Label CustomerName;
        private System.Windows.Forms.Label welcome;
    }
}