using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
using PinkBus.CheckInClient.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinkBus.CheckInClient.PopControls
{
    public partial class OutputVolunteer : BaseForm
    {
        public OutputVolunteer()
            : base()
        {
            //SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            InitializeComponent();
            VolunteerCheckinList();
        }

        private void VolunteerCheckinList()
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void outputvolunteer_title_panel_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void outputvolunteer_title_label_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void OutputVolunteer_Load(object sender, EventArgs e)
        {
           DataTable dt = EventDAL.QueryVolunteerExcels((this.Owner as MainForm).SelectEventKey);
            foreach (DataRow row in dt.Rows)
            {
                RadioButton v = new RadioButton();
                v.Text = DateTime.Parse(row[0].ToString()).ToString("yyyy-MM-dd") + " 志愿者签到" + row[1] + "人";
                v.Cursor = System.Windows.Forms.Cursors.Hand;
                v.AutoSize = true;
                v.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                v.Size = new System.Drawing.Size(227, 25);
                v.TabStop = true;
                v.UseVisualStyleBackColor = true;
                v.Tag = DateTime.Parse(row[0].ToString());
                v.Margin = new System.Windows.Forms.Padding(15, 15, 0, 15);
                v.UseVisualStyleBackColor = true;
                this.flowLayoutPanel1.Controls.Add(v);
            }

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 

        }

        private void outputvolunteer_submit_btn_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.MinValue;
            string fileName = ""; 

            foreach (var control in flowLayoutPanel1.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton r = control as RadioButton;
                    if (r.Checked)
                    {
                        date = (DateTime)r.Tag;
                        fileName = r.Text;
                    }
                }
            }

            if (date == DateTime.MinValue)
            {
                MessageBox.Show("请选中一场活动！");
                return;
            }


            List<VolunteerForExcel> cons = EventDAL.QueryConsultantsForExcel((this.Owner as MainForm).SelectEventKey, date);
    
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择excel导出的目标文件夹";
            var result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var folderName = folderBrowserDialog.SelectedPath;
                fileName = folderName + "\\" + fileName + ".xlsx";
                NPOIHelper.ExportVolunteers(fileName, cons);
                MessageBox.Show("导出成功，文件路径为：" + fileName);
                this.Close();
            }
          
        }

    }
}
