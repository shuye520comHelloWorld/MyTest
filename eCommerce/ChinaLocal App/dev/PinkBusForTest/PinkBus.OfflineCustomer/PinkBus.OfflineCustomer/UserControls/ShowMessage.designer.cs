namespace PinkBus.OfflineCustomer.UserControls
{
    partial class ShowMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowMessage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.message_lable = new System.Windows.Forms.Label();
            this.end_Btn = new System.Windows.Forms.Button();
            this.continue_Btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.message_lable);
            this.panel1.Controls.Add(this.end_Btn);
            this.panel1.Controls.Add(this.continue_Btn);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 160);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // message_lable
            // 
            this.message_lable.AutoSize = true;
            this.message_lable.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.message_lable.Location = new System.Drawing.Point(23, 54);
            this.message_lable.Name = "message_lable";
            this.message_lable.Size = new System.Drawing.Size(302, 21);
            this.message_lable.TabIndex = 4;
            this.message_lable.Text = "请先分配新顾客,未分配的顾客不进行上传";
            // 
            // end_Btn
            // 
            this.end_Btn.FlatAppearance.BorderSize = 0;
            this.end_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.end_Btn.Image = global::PinkBus.OfflineCustomer.Properties.Resources.but11;
            this.end_Btn.Location = new System.Drawing.Point(50, 108);
            this.end_Btn.Name = "end_Btn";
            this.end_Btn.Size = new System.Drawing.Size(94, 31);
            this.end_Btn.TabIndex = 2;
            this.end_Btn.UseVisualStyleBackColor = true;
            this.end_Btn.Click += new System.EventHandler(this.end_Btn_Click);
            // 
            // continue_Btn
            // 
            this.continue_Btn.FlatAppearance.BorderSize = 0;
            this.continue_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continue_Btn.Image = global::PinkBus.OfflineCustomer.Properties.Resources.but10;
            this.continue_Btn.Location = new System.Drawing.Point(192, 108);
            this.continue_Btn.Name = "continue_Btn";
            this.continue_Btn.Size = new System.Drawing.Size(94, 31);
            this.continue_Btn.TabIndex = 3;
            this.continue_Btn.UseVisualStyleBackColor = true;
            this.continue_Btn.Click += new System.EventHandler(this.continue_Btn_Click);
            // 
            // ShowMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 157);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ShowMessage";
            this.Text = "MessageShow";
            this.Load += new System.EventHandler(this.AssignCustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button end_Btn;
        private System.Windows.Forms.Button continue_Btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label message_lable;

    }
}