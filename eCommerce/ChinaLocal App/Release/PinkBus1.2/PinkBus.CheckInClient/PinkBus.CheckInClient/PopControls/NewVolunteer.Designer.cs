namespace PinkBus.CheckInClient.PopControls
{
    partial class NewVolunteer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewVolunteer));
            this.label12 = new System.Windows.Forms.Label();
            this.errMsg = new System.Windows.Forms.Label();
            this.leftNormalCount_Text = new System.Windows.Forms.Label();
            this.leftVIPCount_Text = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.normalTicketCount_Text = new System.Windows.Forms.TextBox();
            this.vipTicketCount_text = new System.Windows.Forms.TextBox();
            this.residenceId_Text = new System.Windows.Forms.TextBox();
            this.level_text = new System.Windows.Forms.TextBox();
            this.phone_text = new System.Windows.Forms.TextBox();
            this.SellerId_Text = new System.Windows.Forms.TextBox();
            this.name_Text = new System.Windows.Forms.TextBox();
            this.volunteerSave_btn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.county_Combox = new System.Windows.Forms.ComboBox();
            this.city_Combox = new System.Windows.Forms.ComboBox();
            this.province_Combox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.county_text = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(58, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 20);
            this.label12.TabIndex = 26;
            this.label12.Text = "*";
            // 
            // errMsg
            // 
            this.errMsg.AutoSize = true;
            this.errMsg.ForeColor = System.Drawing.Color.Red;
            this.errMsg.Location = new System.Drawing.Point(150, 426);
            this.errMsg.Name = "errMsg";
            this.errMsg.Size = new System.Drawing.Size(0, 12);
            this.errMsg.TabIndex = 25;
            // 
            // leftNormalCount_Text
            // 
            this.leftNormalCount_Text.AutoSize = true;
            this.leftNormalCount_Text.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.leftNormalCount_Text.ForeColor = System.Drawing.Color.Gray;
            this.leftNormalCount_Text.Location = new System.Drawing.Point(261, 380);
            this.leftNormalCount_Text.Name = "leftNormalCount_Text";
            this.leftNormalCount_Text.Size = new System.Drawing.Size(52, 20);
            this.leftNormalCount_Text.TabIndex = 24;
            this.leftNormalCount_Text.Text = "剩余: 0";
            // 
            // leftVIPCount_Text
            // 
            this.leftVIPCount_Text.AutoSize = true;
            this.leftVIPCount_Text.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.leftVIPCount_Text.ForeColor = System.Drawing.Color.Gray;
            this.leftVIPCount_Text.Location = new System.Drawing.Point(262, 340);
            this.leftVIPCount_Text.Name = "leftVIPCount_Text";
            this.leftVIPCount_Text.Size = new System.Drawing.Size(52, 20);
            this.leftVIPCount_Text.TabIndex = 23;
            this.leftVIPCount_Text.Text = "剩余: 0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(108)))), ((int)(((byte)(117)))));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 45);
            this.panel1.TabIndex = 20;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDowns);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(484, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "填写志愿者信息";
            this.label9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Drag_MouseDowns);
            // 
            // normalTicketCount_Text
            // 
            this.normalTicketCount_Text.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.normalTicketCount_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.normalTicketCount_Text.Location = new System.Drawing.Point(145, 375);
            this.normalTicketCount_Text.Name = "normalTicketCount_Text";
            this.normalTicketCount_Text.Size = new System.Drawing.Size(110, 29);
            this.normalTicketCount_Text.TabIndex = 19;
            this.normalTicketCount_Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // vipTicketCount_text
            // 
            this.vipTicketCount_text.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vipTicketCount_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vipTicketCount_text.Location = new System.Drawing.Point(145, 335);
            this.vipTicketCount_text.Name = "vipTicketCount_text";
            this.vipTicketCount_text.Size = new System.Drawing.Size(110, 29);
            this.vipTicketCount_text.TabIndex = 18;
            this.vipTicketCount_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // residenceId_Text
            // 
            this.residenceId_Text.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.residenceId_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.residenceId_Text.Location = new System.Drawing.Point(145, 251);
            this.residenceId_Text.MaxLength = 18;
            this.residenceId_Text.Name = "residenceId_Text";
            this.residenceId_Text.Size = new System.Drawing.Size(202, 29);
            this.residenceId_Text.TabIndex = 17;
            this.residenceId_Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.residenceId_Text_KeyPress);
            // 
            // level_text
            // 
            this.level_text.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.level_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.level_text.Location = new System.Drawing.Point(145, 209);
            this.level_text.MaxLength = 2;
            this.level_text.Name = "level_text";
            this.level_text.Size = new System.Drawing.Size(202, 29);
            this.level_text.TabIndex = 16;
            this.level_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // phone_text
            // 
            this.phone_text.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.phone_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.phone_text.Location = new System.Drawing.Point(145, 168);
            this.phone_text.MaxLength = 11;
            this.phone_text.Name = "phone_text";
            this.phone_text.Size = new System.Drawing.Size(202, 29);
            this.phone_text.TabIndex = 15;
            this.phone_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // SellerId_Text
            // 
            this.SellerId_Text.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SellerId_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SellerId_Text.Location = new System.Drawing.Point(145, 118);
            this.SellerId_Text.MaxLength = 12;
            this.SellerId_Text.Name = "SellerId_Text";
            this.SellerId_Text.Size = new System.Drawing.Size(202, 29);
            this.SellerId_Text.TabIndex = 14;
            this.SellerId_Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressNumber);
            // 
            // name_Text
            // 
            this.name_Text.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.name_Text.Location = new System.Drawing.Point(145, 65);
            this.name_Text.MaxLength = 20;
            this.name_Text.Name = "name_Text";
            this.name_Text.Size = new System.Drawing.Size(202, 29);
            this.name_Text.TabIndex = 13;
            // 
            // volunteerSave_btn
            // 
            this.volunteerSave_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but16;
            this.volunteerSave_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.volunteerSave_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.volunteerSave_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.volunteerSave_btn.FlatAppearance.BorderSize = 0;
            this.volunteerSave_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.volunteerSave_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.volunteerSave_btn.ForeColor = System.Drawing.Color.White;
            this.volunteerSave_btn.Location = new System.Drawing.Point(145, 444);
            this.volunteerSave_btn.Name = "volunteerSave_btn";
            this.volunteerSave_btn.Size = new System.Drawing.Size(222, 44);
            this.volunteerSave_btn.TabIndex = 12;
            this.volunteerSave_btn.UseVisualStyleBackColor = false;
            this.volunteerSave_btn.Click += new System.EventHandler(this.volunteerSave_btn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(70, 380);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "来宾券数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(70, 340);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "贵宾券数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(42, 297);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "省、市、区县";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(70, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "身份证号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(70, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "专业职称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(70, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "手机号码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(56, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "直销员编号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(98, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "姓名";
            // 
            // county_Combox
            // 
            this.county_Combox.DisplayMember = "County";
            this.county_Combox.DropDownHeight = 180;
            this.county_Combox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.county_Combox.DropDownWidth = 120;
            this.county_Combox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.county_Combox.FormattingEnabled = true;
            this.county_Combox.IntegralHeight = false;
            this.county_Combox.Location = new System.Drawing.Point(368, 292);
            this.county_Combox.Name = "county_Combox";
            this.county_Combox.Size = new System.Drawing.Size(100, 27);
            this.county_Combox.TabIndex = 2;
            this.county_Combox.ValueMember = "CountyId";
            this.county_Combox.DropDownClosed += new System.EventHandler(this.Combox_DropDownClosed);
            this.county_Combox.SelectedValueChanged += new System.EventHandler(this.county_Combox_SelectedValueChanged);
            // 
            // city_Combox
            // 
            this.city_Combox.DropDownHeight = 180;
            this.city_Combox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.city_Combox.DropDownWidth = 120;
            this.city_Combox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.city_Combox.FormattingEnabled = true;
            this.city_Combox.IntegralHeight = false;
            this.city_Combox.Location = new System.Drawing.Point(262, 292);
            this.city_Combox.Name = "city_Combox";
            this.city_Combox.Size = new System.Drawing.Size(100, 27);
            this.city_Combox.TabIndex = 1;
            this.city_Combox.DropDownClosed += new System.EventHandler(this.Combox_DropDownClosed);
            this.city_Combox.SelectedValueChanged += new System.EventHandler(this.city_Combox_SelectedValueChanged);
            // 
            // province_Combox
            // 
            this.province_Combox.BackColor = System.Drawing.Color.White;
            this.province_Combox.DisplayMember = "Province";
            this.province_Combox.DropDownHeight = 180;
            this.province_Combox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.province_Combox.DropDownWidth = 140;
            this.province_Combox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.province_Combox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.province_Combox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.province_Combox.IntegralHeight = false;
            this.province_Combox.Location = new System.Drawing.Point(145, 292);
            this.province_Combox.MaxDropDownItems = 5;
            this.province_Combox.Name = "province_Combox";
            this.province_Combox.Size = new System.Drawing.Size(110, 27);
            this.province_Combox.TabIndex = 0;
            this.province_Combox.ValueMember = "ProvinceId";
            this.province_Combox.DropDownClosed += new System.EventHandler(this.Combox_DropDownClosed);
            this.province_Combox.SelectedValueChanged += new System.EventHandler(this.Province_Combox_SelectedValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(86, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(44, 121);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 20);
            this.label11.TabIndex = 22;
            this.label11.Text = "*";
            // 
            // county_text
            // 
            this.county_text.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.county_text.Location = new System.Drawing.Point(368, 292);
            this.county_text.Name = "county_text";
            this.county_text.Size = new System.Drawing.Size(100, 27);
            this.county_text.TabIndex = 27;
            // 
            // NewVolunteer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(518, 520);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.errMsg);
            this.Controls.Add(this.leftNormalCount_Text);
            this.Controls.Add(this.leftVIPCount_Text);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.normalTicketCount_Text);
            this.Controls.Add(this.vipTicketCount_text);
            this.Controls.Add(this.residenceId_Text);
            this.Controls.Add(this.level_text);
            this.Controls.Add(this.phone_text);
            this.Controls.Add(this.SellerId_Text);
            this.Controls.Add(this.name_Text);
            this.Controls.Add(this.volunteerSave_btn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.city_Combox);
            this.Controls.Add(this.province_Combox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.county_Combox);
            this.Controls.Add(this.county_text);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "NewVolunteer";
            this.Text = "NewVolunteer";
            this.Load += new System.EventHandler(this.NewVolunteer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox province_Combox;
        private System.Windows.Forms.ComboBox city_Combox;
        private System.Windows.Forms.ComboBox county_Combox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button volunteerSave_btn;
        private System.Windows.Forms.TextBox name_Text;
        private System.Windows.Forms.TextBox SellerId_Text;
        private System.Windows.Forms.TextBox phone_text;
        private System.Windows.Forms.TextBox level_text;
        private System.Windows.Forms.TextBox residenceId_Text;
        private System.Windows.Forms.TextBox vipTicketCount_text;
        private System.Windows.Forms.TextBox normalTicketCount_Text;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label leftVIPCount_Text;
        private System.Windows.Forms.Label leftNormalCount_Text;
        private System.Windows.Forms.Label errMsg;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox county_text;
    }
}