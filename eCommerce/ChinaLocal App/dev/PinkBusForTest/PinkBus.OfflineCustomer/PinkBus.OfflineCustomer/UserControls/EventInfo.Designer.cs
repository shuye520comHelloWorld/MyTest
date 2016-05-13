namespace PinkBus.OfflineCustomer.UserControls
{
    partial class EventInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventInfo));
            this.eventinfo_EvenTtitle = new System.Windows.Forms.Label();
            this.eventinfo_SyncStatus = new System.Windows.Forms.Label();
            this.eventinfo_SessionDate = new System.Windows.Forms.Label();
            this.eventinfo_Excel_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // eventinfo_EvenTtitle
            // 
            this.eventinfo_EvenTtitle.AutoSize = true;
            this.eventinfo_EvenTtitle.BackColor = System.Drawing.Color.Transparent;
            this.eventinfo_EvenTtitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventinfo_EvenTtitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eventinfo_EvenTtitle.Location = new System.Drawing.Point(147, 24);
            this.eventinfo_EvenTtitle.Name = "eventinfo_EvenTtitle";
            this.eventinfo_EvenTtitle.Size = new System.Drawing.Size(210, 25);
            this.eventinfo_EvenTtitle.TabIndex = 0;
            this.eventinfo_EvenTtitle.Text = "粉巴迅游-长春终极会议";
            this.eventinfo_EvenTtitle.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.eventinfo_EvenTtitle.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            // 
            // eventinfo_SyncStatus
            // 
            this.eventinfo_SyncStatus.AutoSize = true;
            this.eventinfo_SyncStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eventinfo_SyncStatus.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventinfo_SyncStatus.ForeColor = System.Drawing.Color.White;
            this.eventinfo_SyncStatus.Location = new System.Drawing.Point(71, 27);
            this.eventinfo_SyncStatus.Name = "eventinfo_SyncStatus";
            this.eventinfo_SyncStatus.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.eventinfo_SyncStatus.Size = new System.Drawing.Size(61, 20);
            this.eventinfo_SyncStatus.TabIndex = 1;
            this.eventinfo_SyncStatus.Text = "未上传";
            this.eventinfo_SyncStatus.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.eventinfo_SyncStatus.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            // 
            // eventinfo_SessionDate
            // 
            this.eventinfo_SessionDate.AutoSize = true;
            this.eventinfo_SessionDate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventinfo_SessionDate.Location = new System.Drawing.Point(148, 74);
            this.eventinfo_SessionDate.Name = "eventinfo_SessionDate";
            this.eventinfo_SessionDate.Size = new System.Drawing.Size(344, 21);
            this.eventinfo_SessionDate.TabIndex = 2;
            this.eventinfo_SessionDate.Text = "2015年10月1日 9：30-2015年10月3日 16：00";
            this.eventinfo_SessionDate.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.eventinfo_SessionDate.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            // 
            // eventinfo_Excel_Btn
            // 
            this.eventinfo_Excel_Btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("eventinfo_Excel_Btn.BackgroundImage")));
            this.eventinfo_Excel_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.eventinfo_Excel_Btn.FlatAppearance.BorderSize = 0;
            this.eventinfo_Excel_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eventinfo_Excel_Btn.Location = new System.Drawing.Point(845, 37);
            this.eventinfo_Excel_Btn.Name = "eventinfo_Excel_Btn";
            this.eventinfo_Excel_Btn.Size = new System.Drawing.Size(104, 40);
            this.eventinfo_Excel_Btn.TabIndex = 3;
            this.eventinfo_Excel_Btn.UseVisualStyleBackColor = true;
            this.eventinfo_Excel_Btn.Click += new System.EventHandler(this.eventinfo_Excel_btn_Click);
            this.eventinfo_Excel_Btn.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.eventinfo_Excel_Btn.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            // 
            // EventInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.eventinfo_Excel_Btn);
            this.Controls.Add(this.eventinfo_SessionDate);
            this.Controls.Add(this.eventinfo_SyncStatus);
            this.Controls.Add(this.eventinfo_EvenTtitle);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(27, 10, 10, 10);
            this.Name = "EventInfo";
            this.Size = new System.Drawing.Size(1040, 117);
            this.Tag = "";
            this.Load += new System.EventHandler(this.EventInfo_Load);
            this.Click += new System.EventHandler(this.EventInfo_Click);
            this.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label eventinfo_EvenTtitle;
        private System.Windows.Forms.Label eventinfo_SyncStatus;
        private System.Windows.Forms.Label eventinfo_SessionDate;
        private System.Windows.Forms.Button eventinfo_Excel_Btn;
    }
}
