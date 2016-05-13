namespace PinkBus.OfflineCustomer.UserControls
{
    partial class AssignCustomer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignCustomer));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.unAssignCustomerTotal = new System.Windows.Forms.Label();
            this.emptyData_Panel = new System.Windows.Forms.Panel();
            this.process_Label = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.consultant_GridView = new System.Windows.Forms.DataGridView();
            this.Import = new System.Windows.Forms.Button();
            this.Confirm = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Close = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.emptyData_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.consultant_GridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Worksheets 2007(*.xlsx)|*.xlsx";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // unAssignCustomerTotal
            // 
            this.unAssignCustomerTotal.AutoSize = true;
            this.unAssignCustomerTotal.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.unAssignCustomerTotal.Location = new System.Drawing.Point(121, 64);
            this.unAssignCustomerTotal.Name = "unAssignCustomerTotal";
            this.unAssignCustomerTotal.Size = new System.Drawing.Size(0, 20);
            this.unAssignCustomerTotal.TabIndex = 9;
            // 
            // emptyData_Panel
            // 
            this.emptyData_Panel.BackColor = System.Drawing.Color.White;
            this.emptyData_Panel.Controls.Add(this.process_Label);
            this.emptyData_Panel.Controls.Add(this.progressBar1);
            this.emptyData_Panel.Controls.Add(this.pictureBox2);
            this.emptyData_Panel.Controls.Add(this.label3);
            this.emptyData_Panel.Location = new System.Drawing.Point(16, 149);
            this.emptyData_Panel.Name = "emptyData_Panel";
            this.emptyData_Panel.Size = new System.Drawing.Size(585, 266);
            this.emptyData_Panel.TabIndex = 8;
            this.emptyData_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.emptyData_Panel_Paint);
            // 
            // process_Label
            // 
            this.process_Label.AutoSize = true;
            this.process_Label.Location = new System.Drawing.Point(255, 153);
            this.process_Label.Name = "process_Label";
            this.process_Label.Size = new System.Drawing.Size(41, 12);
            this.process_Label.TabIndex = 3;
            this.process_Label.Text = "label2";
            this.process_Label.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(231, 123);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PinkBus.OfflineCustomer.Properties.Resources.empty;
            this.pictureBox2.Location = new System.Drawing.Point(255, 60);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 59);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.label3.Location = new System.Drawing.Point(177, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "请先导入当天的签到志愿者名单";
            // 
            // consultant_GridView
            // 
            this.consultant_GridView.AllowUserToAddRows = false;
            this.consultant_GridView.AllowUserToDeleteRows = false;
            this.consultant_GridView.AllowUserToResizeColumns = false;
            this.consultant_GridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.consultant_GridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.consultant_GridView.BackgroundColor = System.Drawing.Color.White;
            this.consultant_GridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.consultant_GridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.consultant_GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.consultant_GridView.ColumnHeadersHeight = 45;
            this.consultant_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.consultant_GridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.consultant_GridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.consultant_GridView.GridColor = System.Drawing.Color.White;
            this.consultant_GridView.Location = new System.Drawing.Point(12, 101);
            this.consultant_GridView.MultiSelect = false;
            this.consultant_GridView.Name = "consultant_GridView";
            this.consultant_GridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Red;
            this.consultant_GridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.consultant_GridView.RowHeadersWidth = 10;
            this.consultant_GridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.consultant_GridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.consultant_GridView.RowTemplate.DividerHeight = 1;
            this.consultant_GridView.RowTemplate.Height = 35;
            this.consultant_GridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consultant_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.consultant_GridView.ShowEditingIcon = false;
            this.consultant_GridView.Size = new System.Drawing.Size(596, 320);
            this.consultant_GridView.TabIndex = 3;
            this.consultant_GridView.VirtualMode = true;
            this.consultant_GridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.consultant_GridView_CellClick);
            // 
            // Import
            // 
            this.Import.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Import.FlatAppearance.BorderSize = 0;
            this.Import.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Import.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Import.Image = global::PinkBus.OfflineCustomer.Properties.Resources.but4;
            this.Import.Location = new System.Drawing.Point(456, 52);
            this.Import.Margin = new System.Windows.Forms.Padding(0);
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(135, 39);
            this.Import.TabIndex = 7;
            this.Import.UseVisualStyleBackColor = true;
            this.Import.Click += new System.EventHandler(this.Import_Click);
            // 
            // Confirm
            // 
            this.Confirm.Enabled = false;
            this.Confirm.FlatAppearance.BorderSize = 0;
            this.Confirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Confirm.Image = ((System.Drawing.Image)(resources.GetObject("Confirm.Image")));
            this.Confirm.Location = new System.Drawing.Point(332, 443);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(125, 37);
            this.Confirm.TabIndex = 6;
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // Cancel
            // 
            this.Cancel.FlatAppearance.BorderSize = 0;
            this.Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
            this.Cancel.Location = new System.Drawing.Point(120, 443);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(125, 37);
            this.Cancel.TabIndex = 5;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(18, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "未分配新顾客:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(108)))), ((int)(((byte)(117)))));
            this.panel1.Controls.Add(this.Close);
            this.panel1.Controls.Add(this.Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 45);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // Close
            // 
            this.Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Close.BackgroundImage")));
            this.Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Close.FlatAppearance.BorderSize = 0;
            this.Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close.Location = new System.Drawing.Point(588, 11);
            this.Close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(20, 24);
            this.Close.TabIndex = 2;
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(18, 14);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(90, 21);
            this.Title.TabIndex = 0;
            this.Title.Text = "分配新顾客";
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lable1_MouseDown);
            // 
            // AssignCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(620, 497);
            this.Controls.Add(this.unAssignCustomerTotal);
            this.Controls.Add(this.emptyData_Panel);
            this.Controls.Add(this.consultant_GridView);
            this.Controls.Add(this.Import);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "AssignCustomer";
            this.Text = "分配顾客";
            this.Load += new System.EventHandler(this.AssignCustomer_Load);
            this.emptyData_Panel.ResumeLayout(false);
            this.emptyData_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.consultant_GridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.Button Import;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView consultant_GridView;
        private System.Windows.Forms.Panel emptyData_Panel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label unAssignCustomerTotal;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label process_Label;
    }
}