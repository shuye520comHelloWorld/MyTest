namespace PinkBus.CheckInClient.PartialControls
{
    partial class VolunteerCheckin
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Volunteercheckin_ratio = new System.Windows.Forms.RadioButton();
            this.volunteerexcel_title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Volunteercheckin_ratio
            // 
            this.Volunteercheckin_ratio.AutoSize = true;
            this.Volunteercheckin_ratio.Location = new System.Drawing.Point(43, 13);
            this.Volunteercheckin_ratio.Name = "Volunteercheckin_ratio";
            this.Volunteercheckin_ratio.Size = new System.Drawing.Size(65, 16);
            this.Volunteercheckin_ratio.TabIndex = 0;
            this.Volunteercheckin_ratio.TabStop = true;
            this.Volunteercheckin_ratio.Tag = "";
            this.Volunteercheckin_ratio.Text = "ddddddd";
            this.Volunteercheckin_ratio.UseVisualStyleBackColor = true;
            this.Volunteercheckin_ratio.CheckedChanged += new System.EventHandler(this.Volunteercheckin_ratio_CheckedChanged);
            // 
            // volunteerexcel_title
            // 
            this.volunteerexcel_title.AutoSize = true;
            this.volunteerexcel_title.Cursor = System.Windows.Forms.Cursors.Hand;
            this.volunteerexcel_title.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.volunteerexcel_title.Location = new System.Drawing.Point(111, 13);
            this.volunteerexcel_title.Name = "volunteerexcel_title";
            this.volunteerexcel_title.Size = new System.Drawing.Size(209, 21);
            this.volunteerexcel_title.TabIndex = 1;
            this.volunteerexcel_title.Text = "2015.09.28 志愿者签到40人";
            this.volunteerexcel_title.Click += new System.EventHandler(this.volunteerexcel_title_Click);
            // 
            // VolunteerCheckin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.volunteerexcel_title);
            this.Controls.Add(this.Volunteercheckin_ratio);
            this.Name = "VolunteerCheckin";
            this.Size = new System.Drawing.Size(323, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton Volunteercheckin_ratio;
        private System.Windows.Forms.Label volunteerexcel_title;
    }
}
