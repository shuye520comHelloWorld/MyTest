namespace PinkBus.OfflineCustomer.UserControls
{
    partial class NewCustomerInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewCustomerInfo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.end_Btn = new System.Windows.Forms.Button();
            this.continue_Btn = new System.Windows.Forms.Button();
            this.新顾客添加成功 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.end_Btn);
            this.panel1.Controls.Add(this.continue_Btn);
            this.panel1.Controls.Add(this.新顾客添加成功);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(314, 157);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PinkBus.OfflineCustomer.Properties.Resources.pic;
            this.pictureBox1.Location = new System.Drawing.Point(77, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // end_Btn
            // 
            this.end_Btn.FlatAppearance.BorderSize = 0;
            this.end_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.end_Btn.Image = global::PinkBus.OfflineCustomer.Properties.Resources.but9;
            this.end_Btn.Location = new System.Drawing.Point(44, 109);
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
            this.continue_Btn.Image = global::PinkBus.OfflineCustomer.Properties.Resources.but7;
            this.continue_Btn.Location = new System.Drawing.Point(179, 108);
            this.continue_Btn.Name = "continue_Btn";
            this.continue_Btn.Size = new System.Drawing.Size(94, 31);
            this.continue_Btn.TabIndex = 3;
            this.continue_Btn.UseVisualStyleBackColor = true;
            this.continue_Btn.Click += new System.EventHandler(this.continue_Btn_Click);
            // 
            // 新顾客添加成功
            // 
            this.新顾客添加成功.AutoSize = true;
            this.新顾客添加成功.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Bold);
            this.新顾客添加成功.Location = new System.Drawing.Point(122, 46);
            this.新顾客添加成功.Name = "新顾客添加成功";
            this.新顾客添加成功.Size = new System.Drawing.Size(129, 19);
            this.新顾客添加成功.TabIndex = 1;
            this.新顾客添加成功.Text = "新顾客添加成功！";
            // 
            // NewCustomerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(314, 157);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "NewCustomerInfo";
            this.Text = "MessageShow";
            this.Load += new System.EventHandler(this.AssignCustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label 新顾客添加成功;
        private System.Windows.Forms.Button end_Btn;
        private System.Windows.Forms.Button continue_Btn;
        private System.Windows.Forms.Panel panel1;

    }
}