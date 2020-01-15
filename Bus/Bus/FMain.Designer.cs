namespace Bus
{
    partial class FMain
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
            this.components = new System.ComponentModel.Container();
            this.ivMain = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ticketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.numberCarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editRoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btSearch = new System.Windows.Forms.Button();
            this.tmrDate = new System.Windows.Forms.Timer(this.components);
            this.cbbSear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblMName = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.CbbCheck = new System.Windows.Forms.ComboBox();
            this.mkIDCard = new System.Windows.Forms.MaskedTextBox();
            this.dtpMD = new System.Windows.Forms.DateTimePicker();
            this.tmrRID = new System.Windows.Forms.Timer(this.components);
            this.tmrReadID = new System.Windows.Forms.Timer(this.components);
            this.btcRead = new System.Windows.Forms.Button();
            this.mkcIDCard = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ivMain
            // 
            this.ivMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ivMain.ContextMenuStrip = this.contextMenuStrip1;
            this.ivMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ivMain.FullRowSelect = true;
            this.ivMain.GridLines = true;
            this.ivMain.Location = new System.Drawing.Point(4, 65);
            this.ivMain.MultiSelect = false;
            this.ivMain.Name = "ivMain";
            this.ivMain.Size = new System.Drawing.Size(813, 366);
            this.ivMain.TabIndex = 0;
            this.ivMain.UseCompatibleStateImageBehavior = false;
            this.ivMain.View = System.Windows.Forms.View.Details;
            this.ivMain.SelectedIndexChanged += new System.EventHandler(this.ivMain_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ticketToolStripMenuItem,
            this.printToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(144, 70);
            // 
            // ticketToolStripMenuItem
            // 
            this.ticketToolStripMenuItem.Name = "ticketToolStripMenuItem";
            this.ticketToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.ticketToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.ticketToolStripMenuItem.Text = "&Ticket";
            this.ticketToolStripMenuItem.Click += new System.EventHandler(this.ticketToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.printToolStripMenuItem.Text = "&Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(821, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memberToolStripMenuItem1,
            this.numberCarToolStripMenuItem1,
            this.editRoundToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // memberToolStripMenuItem1
            // 
            this.memberToolStripMenuItem1.Name = "memberToolStripMenuItem1";
            this.memberToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.memberToolStripMenuItem1.Text = "&Member";
            this.memberToolStripMenuItem1.Click += new System.EventHandler(this.memberToolStripMenuItem_Click);
            // 
            // numberCarToolStripMenuItem1
            // 
            this.numberCarToolStripMenuItem1.Name = "numberCarToolStripMenuItem1";
            this.numberCarToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.numberCarToolStripMenuItem1.Text = "&Number Car";
            this.numberCarToolStripMenuItem1.Click += new System.EventHandler(this.numberCarToolStripMenuItem_Click);
            // 
            // editRoundToolStripMenuItem
            // 
            this.editRoundToolStripMenuItem.Name = "editRoundToolStripMenuItem";
            this.editRoundToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.editRoundToolStripMenuItem.Text = "&Edit Round";
            this.editRoundToolStripMenuItem.Click += new System.EventHandler(this.editRoundToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "&Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(28, 20);
            this.toolStripMenuItem1.Text = "...";
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(332, 35);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(63, 24);
            this.btSearch.TabIndex = 3;
            this.btSearch.Text = "ค้นหา";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // tmrDate
            // 
            this.tmrDate.Enabled = true;
            this.tmrDate.Tick += new System.EventHandler(this.tmrDate_Tick);
            // 
            // cbbSear
            // 
            this.cbbSear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbSear.FormattingEnabled = true;
            this.cbbSear.Items.AddRange(new object[] {
            "Date",
            "IDCard",
            "Name",
            "ใบรับเงิน"});
            this.cbbSear.Location = new System.Drawing.Point(50, 35);
            this.cbbSear.Name = "cbbSear";
            this.cbbSear.Size = new System.Drawing.Size(135, 24);
            this.cbbSear.TabIndex = 1;
            this.cbbSear.Text = "Date";
            this.cbbSear.SelectedIndexChanged += new System.EventHandler(this.cbbSear_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(8, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "ค้นหา :";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtName.Location = new System.Drawing.Point(191, 35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 24);
            this.txtName.TabIndex = 6;
            this.txtName.Visible = false;
            this.txtName.Click += new System.EventHandler(this.txtName_Click);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // lblMName
            // 
            this.lblMName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMName.Location = new System.Drawing.Point(663, 38);
            this.lblMName.Name = "lblMName";
            this.lblMName.Size = new System.Drawing.Size(154, 18);
            this.lblMName.TabIndex = 8;
            this.lblMName.Text = "...";
            this.lblMName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDate.Location = new System.Drawing.Point(721, 3);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(96, 18);
            this.lblDate.TabIndex = 12;
            this.lblDate.Text = "...";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CbbCheck
            // 
            this.CbbCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CbbCheck.FormattingEnabled = true;
            this.CbbCheck.Items.AddRange(new object[] {
            "ออกแล้ว",
            "ยังไม่ได้ออก"});
            this.CbbCheck.Location = new System.Drawing.Point(191, 35);
            this.CbbCheck.Name = "CbbCheck";
            this.CbbCheck.Size = new System.Drawing.Size(135, 24);
            this.CbbCheck.TabIndex = 2;
            this.CbbCheck.Text = "ออกแล้ว";
            this.CbbCheck.Visible = false;
            // 
            // mkIDCard
            // 
            this.mkIDCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mkIDCard.Location = new System.Drawing.Point(191, 35);
            this.mkIDCard.Mask = "0-0000-00000-00-0";
            this.mkIDCard.Name = "mkIDCard";
            this.mkIDCard.PromptChar = ' ';
            this.mkIDCard.Size = new System.Drawing.Size(135, 24);
            this.mkIDCard.TabIndex = 2;
            this.mkIDCard.Visible = false;
            this.mkIDCard.Click += new System.EventHandler(this.mkIDCard_Click);
            this.mkIDCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mkIDCard_KeyPress);
            // 
            // dtpMD
            // 
            this.dtpMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtpMD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMD.Location = new System.Drawing.Point(191, 35);
            this.dtpMD.Name = "dtpMD";
            this.dtpMD.Size = new System.Drawing.Size(135, 24);
            this.dtpMD.TabIndex = 13;
            // 
            // tmrRID
            // 
            this.tmrRID.Enabled = true;
            this.tmrRID.Interval = 5000;
            this.tmrRID.Tick += new System.EventHandler(this.tmrRID_Tick);
            // 
            // tmrReadID
            // 
            this.tmrReadID.Enabled = true;
            this.tmrReadID.Interval = 3000;
            this.tmrReadID.Tick += new System.EventHandler(this.tmrReadID_Tick);
            // 
            // btcRead
            // 
            this.btcRead.Location = new System.Drawing.Point(594, 35);
            this.btcRead.Name = "btcRead";
            this.btcRead.Size = new System.Drawing.Size(63, 24);
            this.btcRead.TabIndex = 15;
            this.btcRead.Text = "เพิ่ม";
            this.btcRead.UseVisualStyleBackColor = true;
            this.btcRead.Click += new System.EventHandler(this.btcRead_Click);
            // 
            // mkcIDCard
            // 
            this.mkcIDCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mkcIDCard.Location = new System.Drawing.Point(453, 35);
            this.mkcIDCard.Mask = "0-0000-00000-00-0";
            this.mkcIDCard.Name = "mkcIDCard";
            this.mkcIDCard.PromptChar = ' ';
            this.mkcIDCard.Size = new System.Drawing.Size(135, 24);
            this.mkcIDCard.TabIndex = 14;
            this.mkcIDCard.Click += new System.EventHandler(this.mkcIDCard_Click);
            this.mkcIDCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mkcIDCard_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(400, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "เพิ่มรอบ :";
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 434);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mkcIDCard);
            this.Controls.Add(this.btcRead);
            this.Controls.Add(this.dtpMD);
            this.Controls.Add(this.mkIDCard);
            this.Controls.Add(this.CbbCheck);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblMName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbSear);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.ivMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "โปรแกรมระบบจัดการบริหารการเดินรถสาธารณะประจำทางเอกชน";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FMain_FormClosed);
            this.Load += new System.EventHandler(this.FMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Timer tmrDate;
        private System.Windows.Forms.ComboBox cbbSear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.ListView ivMain;
        private System.Windows.Forms.Label lblMName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ticketToolStripMenuItem;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox CbbCheck;
        private System.Windows.Forms.MaskedTextBox mkIDCard;
        private System.Windows.Forms.DateTimePicker dtpMD;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Timer tmrRID;
        private System.Windows.Forms.Timer tmrReadID;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem numberCarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editRoundToolStripMenuItem;
        private System.Windows.Forms.Button btcRead;
        private System.Windows.Forms.MaskedTextBox mkcIDCard;
        private System.Windows.Forms.Label label2;

    }
}



