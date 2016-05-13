namespace PinkBus.CheckInClient.PartialControls
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
            this.eventinfo_eventtitle = new System.Windows.Forms.Label();
            this.eventinfo_syncstatus = new System.Windows.Forms.Label();
            this.eventinfo_sessiondate = new System.Windows.Forms.Label();
            this.eventinfo_Excel_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // eventinfo_eventtitle
            // 
            this.eventinfo_eventtitle.AutoSize = true;
            this.eventinfo_eventtitle.BackColor = System.Drawing.Color.Transparent;
            this.eventinfo_eventtitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.eventinfo_eventtitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventinfo_eventtitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eventinfo_eventtitle.Location = new System.Drawing.Point(147, 24);
            this.eventinfo_eventtitle.Name = "eventinfo_eventtitle";
            this.eventinfo_eventtitle.Size = new System.Drawing.Size(210, 25);
            this.eventinfo_eventtitle.TabIndex = 0;
            this.eventinfo_eventtitle.Text = "粉巴迅游-长春终极会议";
            this.eventinfo_eventtitle.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.eventinfo_eventtitle.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            // 
            // eventinfo_syncstatus
            // 
            this.eventinfo_syncstatus.AutoSize = true;
            this.eventinfo_syncstatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eventinfo_syncstatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.eventinfo_syncstatus.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventinfo_syncstatus.ForeColor = System.Drawing.Color.White;
            this.eventinfo_syncstatus.Location = new System.Drawing.Point(71, 27);
            this.eventinfo_syncstatus.Name = "eventinfo_syncstatus";
            this.eventinfo_syncstatus.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.eventinfo_syncstatus.Size = new System.Drawing.Size(61, 20);
            this.eventinfo_syncstatus.TabIndex = 1;
            this.eventinfo_syncstatus.Text = "未上传";
            this.eventinfo_syncstatus.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.eventinfo_syncstatus.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            // 
            // eventinfo_sessiondate
            // 
            this.eventinfo_sessiondate.AutoSize = true;
            this.eventinfo_sessiondate.Cursor = System.Windows.Forms.Cursors.Default;
            this.eventinfo_sessiondate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventinfo_sessiondate.Location = new System.Drawing.Point(148, 74);
            this.eventinfo_sessiondate.Name = "eventinfo_sessiondate";
            this.eventinfo_sessiondate.Size = new System.Drawing.Size(344, 21);
            this.eventinfo_sessiondate.TabIndex = 2;
            this.eventinfo_sessiondate.Text = "2015年10月1日 9：30-2015年10月3日 16：00";
            this.eventinfo_sessiondate.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.eventinfo_sessiondate.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            // 
            // eventinfo_Excel_btn
            // 
            this.eventinfo_Excel_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but2;
            this.eventinfo_Excel_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.eventinfo_Excel_btn.FlatAppearance.BorderSize = 0;
            this.eventinfo_Excel_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eventinfo_Excel_btn.Location = new System.Drawing.Point(845, 37);
            this.eventinfo_Excel_btn.Name = "eventinfo_Excel_btn";
            this.eventinfo_Excel_btn.Size = new System.Drawing.Size(104, 40);
            this.eventinfo_Excel_btn.TabIndex = 3;
            this.eventinfo_Excel_btn.UseVisualStyleBackColor = true;
            this.eventinfo_Excel_btn.Click += new System.EventHandler(this.eventinfo_Excel_btn_Click);
            this.eventinfo_Excel_btn.MouseEnter += new System.EventHandler(this.EventInfo_MouseEnter);
            this.eventinfo_Excel_btn.MouseLeave += new System.EventHandler(this.EventInfo_MouseLeave);
            // 
            // EventInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.eventinfo_Excel_btn);
            this.Controls.Add(this.eventinfo_sessiondate);
            this.Controls.Add(this.eventinfo_syncstatus);
            this.Controls.Add(this.eventinfo_eventtitle);
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

        private System.Windows.Forms.Label eventinfo_eventtitle;
        private System.Windows.Forms.Label eventinfo_syncstatus;
        private System.Windows.Forms.Label eventinfo_sessiondate;
        private System.Windows.Forms.Button eventinfo_Excel_btn;
    }
}
