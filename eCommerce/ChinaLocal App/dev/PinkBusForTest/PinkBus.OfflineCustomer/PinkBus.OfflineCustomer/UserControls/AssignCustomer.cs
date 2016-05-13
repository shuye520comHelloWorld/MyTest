using PinkBus.OfflineCustomer.DAL;
using PinkBus.OfflineCustomer.Entity;
using PinkBus.OfflineCustomer.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinkBus.OfflineCustomer.UserControls
{
    public partial class AssignCustomer : BaseForm
    {
        public Guid EventKey { get; set; }

        private List<Volunteer> Volunteers { get; set; }


        private List<Customer> UnAssignedCustomers { get; set; }

        private List<Customer> AssignedCustomers { get; set; }

        private List<Customer> AwardAssignedCustomers { get; set; }

        /// <summary>
        /// continue add customer button click handler
        /// </summary>
        public EventHandler SetUnassignedCustomer;

        public AssignCustomer(Guid eventKey)
            : base()
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            this.EventKey = eventKey;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
            this.panel1.Focus();
        }

        private void lable1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void AssignCustomer_Load(object sender, EventArgs e)
            {
            UnAssignedCustomers = OfflineCustomerHelper.QueryUnAssignedCustomers(this.EventKey.ToString());
            if (UnAssignedCustomers == null)
            {
                //TODO
            }

            unAssignCustomerTotal.Text = UnAssignedCustomers.Count().ToString();
            this.emptyData_Panel.Visible = true;


            this.consultant_GridView.AutoGenerateColumns = false;
            var name = new DataGridViewTextBoxColumn
            {
                Name = "ConsultantName",
                DataPropertyName = "ConsultantName",
                HeaderText = "姓名",
                Visible = true,
                Width = 150,
                ReadOnly = true
            };
            var directSeller = new DataGridViewTextBoxColumn
            {
                Name = "DirectSellerId",
                DataPropertyName = "DirectSellerId",
                HeaderText = "编号",
                Visible = true,
                Width = 150,
                ReadOnly = true
            };
            var consultantLevel = new DataGridViewTextBoxColumn
            {
                Name = "ConsultantLevel",
                DataPropertyName = "ConsultantLevel",
                HeaderText = "职级",
                Visible = true,
                Width = 150,
                ReadOnly = true
            };
            var quantity = new DataGridViewTextBoxColumn
            {
                Name = "CustomerQuantity",
                DataPropertyName = "CustomerQuantity",
                HeaderText = "奖励数量",
                Visible = true,
                Width = 145
            };

            consultant_GridView.Columns.Clear();
            consultant_GridView.Columns.AddRange(name, directSeller, consultantLevel, quantity);
            consultant_GridView.Visible = true;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁
        }

        private void AssignAwardCustomer(List<Volunteer> awardVolunteers)
        {
            if (AssignedCustomers == null)
                AssignedCustomers = new List<Customer>();
            awardVolunteers.ForEach((volunteer =>
            {
                int assignQuantity = volunteer.CustomerQuantity;
                List<Customer> sameCounty = UnAssignedCustomers.Where(p => p.County == volunteer.County).ToList();
                if (sameCounty.Count > 0)
                {
                    int actual = 0;
                    if (sameCounty.Count < assignQuantity)
                        actual = sameCounty.Count;
                    else
                        actual = assignQuantity;

                    for (int i = 0; i < actual; i++)
                    {
                        sameCounty[i].DirectSellerId = volunteer.DirectSellerId;
                        AssignedCustomers.Add(sameCounty[i]);
                    }

                }

                UnAssignedCustomers.RemoveAll(p => !string.IsNullOrEmpty(p.DirectSellerId));
                int leftCount = 0;
                if (assignQuantity > sameCounty.Count)
                {
                    leftCount = assignQuantity - sameCounty.Count;
                    foreach (var item in UnAssignedCustomers)
                    {
                        if (string.IsNullOrEmpty(item.DirectSellerId))
                        {
                            item.DirectSellerId = volunteer.DirectSellerId;
                            AssignedCustomers.Add(item);
                            leftCount--;
                        }

                        if (leftCount <= 0)
                            break;
                    }
                }
                UnAssignedCustomers.RemoveAll(p => !string.IsNullOrEmpty(p.DirectSellerId));
            }));

        }

        private void AssignAverageCustomer(List<Volunteer> allVolunteers, int assignQuantity)
        {
            if (AssignedCustomers == null)
                AssignedCustomers = new List<Customer>();

            //分配顾客，一轮分配一个
            int loop = assignQuantity;
            for (int i = 0; i < assignQuantity; i++)
            {
                List<Customer> studentCustomer = UnAssignedCustomers.Where(p => p.CustomerType == "2").ToList();
                if (studentCustomer != null && studentCustomer.Count > 0)
                {
                    UnAssignedCustomers.RemoveAll(p => p.CustomerType == "2");
                    //studentCustomer.ForEach((student) => { 
                    UnAssignedCustomers.InsertRange(0, studentCustomer);
                    //});
                }
             
                allVolunteers.ForEach((volunteer =>
               {
                   //county 优先匹配
                   Customer sameCountyCustomer = UnAssignedCustomers.Where(p => p.County == volunteer.County).FirstOrDefault();
                   if (sameCountyCustomer != null)
                   {
                       sameCountyCustomer.DirectSellerId = volunteer.DirectSellerId;
                       AssignedCustomers.Add(sameCountyCustomer);

                   }
                   else
                   {
                       foreach (var item in UnAssignedCustomers)
                       {
                           if (string.IsNullOrEmpty(item.DirectSellerId))
                           {

                               // var studentCustomer = AssignedCustomers.Where(p => p.DirectSellerId == volunteer.DirectSellerId).FirstOrDefault();
                               // if (studentCustomer == null)
                               /// {
                               item.DirectSellerId = volunteer.DirectSellerId;
                               AssignedCustomers.Add(item);
                               break;
                               
                           }
                       }
                   }
                   UnAssignedCustomers.RemoveAll(p => !string.IsNullOrEmpty(p.DirectSellerId));
               }));
            }


        }
        //private void AssignAverageCustomer(List<Volunteer> allVolunteers, int assignQuantity)
        //{
        //    AssignedCustomers = new List<Customer>();
        //    allVolunteers.ForEach((volunteer =>
        //     {
        //         //county 优先匹配
        //         List<Customer> sameCounty = UnAssignedCustomers.Where(p => p.County == volunteer.County).ToList();
        //         if (sameCounty.Count > 0)
        //         {
        //             int actual = 0;
        //             if (sameCounty.Count < assignQuantity)
        //                 actual = sameCounty.Count;
        //             else
        //                 actual = assignQuantity;

        //             for (int i = 0; i < actual; i++)
        //             {
        //                 sameCounty[i].DirectSellerId = volunteer.DirectSellerId;
        //                 AssignedCustomers.Add(sameCounty[i]);
        //             }
        //         }
        //         UnAssignedCustomers.RemoveAll(p => !string.IsNullOrEmpty(p.DirectSellerId));
        //         int leftCount = 0;
        //         if (assignQuantity > sameCounty.Count)
        //         {
        //             leftCount = assignQuantity - sameCounty.Count;
        //             int counts = UnAssignedCustomers.Count;
        //             foreach (var item in UnAssignedCustomers)
        //             {
        //                 if (string.IsNullOrEmpty(item.DirectSellerId))
        //                 {
        //                     item.DirectSellerId = volunteer.DirectSellerId;
        //                     AssignedCustomers.Add(item);
        //                     leftCount--;
        //                 }

        //                 if (leftCount <= 0)
        //                     break;
        //             }
        //         }
        //         UnAssignedCustomers.RemoveAll(p => !string.IsNullOrEmpty(p.DirectSellerId));
        //     }));

        //}
        /// <summary>
        /// to do 分配顾客
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, EventArgs e)
        {
            List<Volunteer> volunteers = this.consultant_GridView.DataSource as List<Volunteer>;
            int awardTotal = volunteers.Sum(p => p.CustomerQuantity);
            int unAssignedTotal = UnAssignedCustomers.Count;

            if (unAssignedTotal < awardTotal || unAssignedTotal == 0)
            {
                MessageBox.Show("当前可分配的新顾客数量不足");
                return;
            }

            Volunteers = volunteers;
            //customer type is student
            //List<Customer> sutdent = UnAssignedCustomers.Where(p => p.CustomerType == "2").ToList();
            this.progressBar1.Visible = true;
        
            this.backgroundWorker1.RunWorkerAsync(10);
            this.SetUnassignedCustomer += new EventHandler((this.Owner as MainForm).SetUnAssinged);
            this.SetUnassignedCustomer(sender, e);
        }

        void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int awardTotal = Volunteers.Sum(p => p.CustomerQuantity);
            int unAssignedTotal = UnAssignedCustomers.Count;
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(5);

            List<Volunteer> awardVolunteers = Volunteers.Where(p => p.CustomerQuantity > 0).ToList();
            int leftTotal = unAssignedTotal - awardTotal;

            //step 1: 分配顾客 给设置奖励数量的志愿者
            if (awardVolunteers.Count > 0)
            {
                AssignAwardCustomer(awardVolunteers);
            }
            worker.ReportProgress(20);
            //step 2: 分配顾客，平均分配
            int averageQuantity = 0;
            if (leftTotal % Volunteers.Count == 0)
                averageQuantity = (int)leftTotal / Volunteers.Count;
            else
                averageQuantity = (int)leftTotal / Volunteers.Count + 1;

            AssignAverageCustomer(Volunteers, averageQuantity);

            worker.ReportProgress(50);
            //step3：保存 assigned customer                
           // OfflineCustomerHelper.UpdateCustomersByBackgroundWorker(this.backgroundWorker1, UnAssignedCustomers);
            OfflineCustomerHelper.UpdateCustomersByBackgroundWorker(this.backgroundWorker1, AssignedCustomers,this.EventKey);
            
        }

        void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar1.Value < e.ProgressPercentage)
            {
                this.progressBar1.Value = e.ProgressPercentage;
                this.process_Label.Text = e.ProgressPercentage.ToString();
            }
        }

        void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                this.unAssignCustomerTotal.Text = "0";
                MessageBox.Show("分配完成");
                this.Close();
                MainForm mf = this.Owner as MainForm;
                mf.CustomerListDataBind();
            }

        }


        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// import consultant list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Import_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog1.ShowDialog();
            string fileName = string.Empty;
            if (result == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    List<Volunteer> volunteers = NPOIHelper.GetVolunteers(fileName);
                    if (volunteers == null || volunteers.Count == 0)
                        return;
                    this.emptyData_Panel.Visible = false;
                    this.consultant_GridView.Visible = true;
                    this.Confirm.Enabled = true;
                    this.consultant_GridView.DataSource = volunteers;

                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void consultant_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.ColumnIndex!=3)
            //    e.
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void emptyData_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
