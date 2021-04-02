namespace AutomatedWorker.Forms
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.dsOperations = new System.Data.DataSet();
            this.tblOperations = new System.Data.DataTable();
            this.tclId = new System.Data.DataColumn();
            this.tclName = new System.Data.DataColumn();
            this.tclMouseX = new System.Data.DataColumn();
            this.tclMouseY = new System.Data.DataColumn();
            this.tclKeyboardText = new System.Data.DataColumn();
            this.tclImage = new System.Data.DataColumn();
            this.tclClickTypeId = new System.Data.DataColumn();
            this.tblMouseClickType = new System.Data.DataTable();
            this.mclId = new System.Data.DataColumn();
            this.mclCode = new System.Data.DataColumn();
            this.mclName = new System.Data.DataColumn();
            this.grdOperations = new System.Windows.Forms.DataGridView();
            this.clDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMouseX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMouseY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clClickTypeId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clKeyboardText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblJobCaption = new System.Windows.Forms.Label();
            this.pnlJob = new System.Windows.Forms.GroupBox();
            this.pnlOperationsHeader = new System.Windows.Forms.Panel();
            this.lblKeyboardText = new System.Windows.Forms.Label();
            this.lblClickType = new System.Windows.Forms.Label();
            this.lblMouseY = new System.Windows.Forms.Label();
            this.lblMouseX = new System.Windows.Forms.Label();
            this.lblMouseCaption = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblImage = new System.Windows.Forms.Label();
            this.pnlNewOperation = new System.Windows.Forms.GroupBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tbMain = new System.Windows.Forms.ToolStrip();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewFragment = new System.Windows.Forms.ToolStripButton();
            this.btnExistedFragment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dsOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMouseClickType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperations)).BeginInit();
            this.pnlJob.SuspendLayout();
            this.pnlOperationsHeader.SuspendLayout();
            this.tbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(72, 28);
            this.txtJobName.MaxLength = 100;
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(207, 20);
            this.txtJobName.TabIndex = 8;
            // 
            // dsOperations
            // 
            this.dsOperations.DataSetName = "NewDataSet";
            this.dsOperations.EnforceConstraints = false;
            this.dsOperations.Relations.AddRange(new System.Data.DataRelation[] {
            new System.Data.DataRelation("rltMouseClick", "Operations", "MouseClickType", new string[] {
                        "ClickTypeId"}, new string[] {
                        "Id"}, false)});
            this.dsOperations.Tables.AddRange(new System.Data.DataTable[] {
            this.tblOperations,
            this.tblMouseClickType});
            // 
            // tblOperations
            // 
            this.tblOperations.Columns.AddRange(new System.Data.DataColumn[] {
            this.tclId,
            this.tclName,
            this.tclMouseX,
            this.tclMouseY,
            this.tclKeyboardText,
            this.tclImage,
            this.tclClickTypeId});
            this.tblOperations.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "Id"}, true),
            new System.Data.UniqueConstraint("Constraint2", new string[] {
                        "ClickTypeId"}, false)});
            this.tblOperations.PrimaryKey = new System.Data.DataColumn[] {
        this.tclId};
            this.tblOperations.TableName = "Operations";
            this.tblOperations.RowChanged += new System.Data.DataRowChangeEventHandler(this.tblOperations_RowChanged);
            // 
            // tclId
            // 
            this.tclId.AllowDBNull = false;
            this.tclId.ColumnName = "Id";
            this.tclId.DataType = typeof(int);
            // 
            // tclName
            // 
            this.tclName.ColumnName = "Name";
            // 
            // tclMouseX
            // 
            this.tclMouseX.Caption = "Mouse X";
            this.tclMouseX.ColumnName = "MouseX";
            this.tclMouseX.DataType = typeof(int);
            // 
            // tclMouseY
            // 
            this.tclMouseY.Caption = "Mouse Y";
            this.tclMouseY.ColumnName = "MouseY";
            this.tclMouseY.DataType = typeof(int);
            // 
            // tclKeyboardText
            // 
            this.tclKeyboardText.Caption = "Keyboard text";
            this.tclKeyboardText.ColumnName = "KeyboardText";
            // 
            // tclImage
            // 
            this.tclImage.ColumnName = "Image";
            this.tclImage.DataType = typeof(byte[]);
            // 
            // tclClickTypeId
            // 
            this.tclClickTypeId.ColumnName = "ClickTypeId";
            this.tclClickTypeId.DataType = typeof(int);
            // 
            // tblMouseClickType
            // 
            this.tblMouseClickType.Columns.AddRange(new System.Data.DataColumn[] {
            this.mclId,
            this.mclCode,
            this.mclName});
            this.tblMouseClickType.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("rltMouseClick", "Operations", new string[] {
                        "ClickTypeId"}, new string[] {
                        "Id"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.SetNull)});
            this.tblMouseClickType.TableName = "MouseClickType";
            // 
            // mclId
            // 
            this.mclId.ColumnName = "Id";
            this.mclId.DataType = typeof(int);
            // 
            // mclCode
            // 
            this.mclCode.ColumnName = "Code";
            this.mclCode.MaxLength = 20;
            // 
            // mclName
            // 
            this.mclName.ColumnName = "Name";
            this.mclName.MaxLength = 100;
            // 
            // grdOperations
            // 
            this.grdOperations.AllowUserToAddRows = false;
            this.grdOperations.AllowUserToDeleteRows = false;
            this.grdOperations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdOperations.AutoGenerateColumns = false;
            this.grdOperations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdOperations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clDel,
            this.clImage,
            this.clName,
            this.clMouseX,
            this.clMouseY,
            this.clClickTypeId,
            this.clKeyboardText});
            this.grdOperations.DataMember = "Operations";
            this.grdOperations.DataSource = this.dsOperations;
            this.grdOperations.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdOperations.Location = new System.Drawing.Point(12, 414);
            this.grdOperations.Name = "grdOperations";
            this.grdOperations.Size = new System.Drawing.Size(715, 148);
            this.grdOperations.TabIndex = 9;
            this.grdOperations.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdOperations_CellContentClick);
            this.grdOperations.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdOperations_CellEndEdit);
            // 
            // clDel
            // 
            this.clDel.HeaderText = "";
            this.clDel.Name = "clDel";
            this.clDel.ReadOnly = true;
            this.clDel.Text = "Delete";
            this.clDel.UseColumnTextForButtonValue = true;
            this.clDel.Width = 50;
            // 
            // clImage
            // 
            this.clImage.DataPropertyName = "Image";
            this.clImage.HeaderText = "Image";
            this.clImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.clImage.Name = "clImage";
            this.clImage.Width = 50;
            // 
            // clName
            // 
            this.clName.DataPropertyName = "Name";
            this.clName.FillWeight = 200F;
            this.clName.HeaderText = "Fragment name";
            this.clName.MaxInputLength = 100;
            this.clName.Name = "clName";
            this.clName.Width = 200;
            // 
            // clMouseX
            // 
            this.clMouseX.DataPropertyName = "MouseX";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.clMouseX.DefaultCellStyle = dataGridViewCellStyle5;
            this.clMouseX.HeaderText = "Mouse X";
            this.clMouseX.MaxInputLength = 5;
            this.clMouseX.Name = "clMouseX";
            this.clMouseX.Width = 60;
            // 
            // clMouseY
            // 
            this.clMouseY.DataPropertyName = "MouseY";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.clMouseY.DefaultCellStyle = dataGridViewCellStyle6;
            this.clMouseY.FillWeight = 60F;
            this.clMouseY.HeaderText = "Mouse Y";
            this.clMouseY.MaxInputLength = 5;
            this.clMouseY.Name = "clMouseY";
            this.clMouseY.Width = 60;
            // 
            // clClickTypeId
            // 
            this.clClickTypeId.DataPropertyName = "ClickTypeId";
            this.clClickTypeId.DataSource = this.dsOperations;
            this.clClickTypeId.DisplayMember = "MouseClickType.Name";
            this.clClickTypeId.HeaderText = "Click type";
            this.clClickTypeId.Name = "clClickTypeId";
            this.clClickTypeId.ValueMember = "MouseClickType.Id";
            // 
            // clKeyboardText
            // 
            this.clKeyboardText.DataPropertyName = "KeyboardText";
            this.clKeyboardText.HeaderText = "KeyboardText";
            this.clKeyboardText.MaxInputLength = 1024;
            this.clKeyboardText.Name = "clKeyboardText";
            this.clKeyboardText.Width = 150;
            // 
            // lblJobCaption
            // 
            this.lblJobCaption.AutoSize = true;
            this.lblJobCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblJobCaption.Location = new System.Drawing.Point(26, 31);
            this.lblJobCaption.Name = "lblJobCaption";
            this.lblJobCaption.Size = new System.Drawing.Size(40, 13);
            this.lblJobCaption.TabIndex = 15;
            this.lblJobCaption.Text = "Macro:";
            // 
            // pnlJob
            // 
            this.pnlJob.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlJob.Controls.Add(this.pnlOperationsHeader);
            this.pnlJob.Location = new System.Drawing.Point(12, 54);
            this.pnlJob.Name = "pnlJob";
            this.pnlJob.Size = new System.Drawing.Size(715, 255);
            this.pnlJob.TabIndex = 19;
            this.pnlJob.TabStop = false;
            this.pnlJob.Text = "Macro operations";
            // 
            // pnlOperationsHeader
            // 
            this.pnlOperationsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOperationsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOperationsHeader.Controls.Add(this.lblKeyboardText);
            this.pnlOperationsHeader.Controls.Add(this.lblClickType);
            this.pnlOperationsHeader.Controls.Add(this.lblMouseY);
            this.pnlOperationsHeader.Controls.Add(this.lblMouseX);
            this.pnlOperationsHeader.Controls.Add(this.lblMouseCaption);
            this.pnlOperationsHeader.Controls.Add(this.lblName);
            this.pnlOperationsHeader.Controls.Add(this.lblImage);
            this.pnlOperationsHeader.Location = new System.Drawing.Point(6, 19);
            this.pnlOperationsHeader.Name = "pnlOperationsHeader";
            this.pnlOperationsHeader.Size = new System.Drawing.Size(703, 44);
            this.pnlOperationsHeader.TabIndex = 7;
            // 
            // lblKeyboardText
            // 
            this.lblKeyboardText.AutoSize = true;
            this.lblKeyboardText.Location = new System.Drawing.Point(524, 17);
            this.lblKeyboardText.Name = "lblKeyboardText";
            this.lblKeyboardText.Size = new System.Drawing.Size(72, 13);
            this.lblKeyboardText.TabIndex = 13;
            this.lblKeyboardText.Text = "Keyboard text";
            // 
            // lblClickType
            // 
            this.lblClickType.AutoSize = true;
            this.lblClickType.Location = new System.Drawing.Point(439, 17);
            this.lblClickType.Name = "lblClickType";
            this.lblClickType.Size = new System.Drawing.Size(53, 13);
            this.lblClickType.TabIndex = 12;
            this.lblClickType.Text = "Click type";
            // 
            // lblMouseY
            // 
            this.lblMouseY.AutoSize = true;
            this.lblMouseY.Location = new System.Drawing.Point(370, 28);
            this.lblMouseY.Name = "lblMouseY";
            this.lblMouseY.Size = new System.Drawing.Size(14, 13);
            this.lblMouseY.TabIndex = 11;
            this.lblMouseY.Text = "Y";
            // 
            // lblMouseX
            // 
            this.lblMouseX.AutoSize = true;
            this.lblMouseX.Location = new System.Drawing.Point(334, 28);
            this.lblMouseX.Name = "lblMouseX";
            this.lblMouseX.Size = new System.Drawing.Size(14, 13);
            this.lblMouseX.TabIndex = 10;
            this.lblMouseX.Text = "X";
            // 
            // lblMouseCaption
            // 
            this.lblMouseCaption.AutoSize = true;
            this.lblMouseCaption.Location = new System.Drawing.Point(314, 6);
            this.lblMouseCaption.Name = "lblMouseCaption";
            this.lblMouseCaption.Size = new System.Drawing.Size(97, 13);
            this.lblMouseCaption.TabIndex = 9;
            this.lblMouseCaption.Text = "Mouse coordinates";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(131, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Name";
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(44, 16);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(36, 13);
            this.lblImage.TabIndex = 7;
            this.lblImage.Text = "Image";
            // 
            // pnlNewOperation
            // 
            this.pnlNewOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNewOperation.Location = new System.Drawing.Point(12, 315);
            this.pnlNewOperation.Name = "pnlNewOperation";
            this.pnlNewOperation.Size = new System.Drawing.Size(715, 93);
            this.pnlNewOperation.TabIndex = 20;
            this.pnlNewOperation.TabStop = false;
            this.pnlNewOperation.Text = "New operation";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // tbMain
            // 
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(739, 25);
            this.tbMain.TabIndex = 21;
            this.tbMain.Text = "";
            this.tbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.btnLoad,
                this.btnSave,
                this.toolStripSeparator1,
                this.btnRun,
                this.toolStripSeparator2,
                this.btnNewFragment,
                this.btnExistedFragment,
                this.toolStripSeparator3,
                this.btnClose
            });
            // 
            // btnLoad
            // 
            this.btnLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoad.Image = global::AutomatedWorker.Properties.Resources.open;
            this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(23, 22);
            this.btnLoad.Text = "Load macro";
            this.btnLoad.ToolTipText = "Load macro";
            this.btnLoad.Click += new System.EventHandler(this.btnLoadJob_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::AutomatedWorker.Properties.Resources.save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save macro";
            this.btnSave.ToolTipText = "Save macro";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRun
            // 
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRun.Image = global::AutomatedWorker.Properties.Resources.run;
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(23, 22);
            this.btnRun.Text = "Run macro";
            this.btnRun.ToolTipText = "Run macro";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnNewFragment
            // 
            this.btnNewFragment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewFragment.Image = global::AutomatedWorker.Properties.Resources.plus;
            this.btnNewFragment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewFragment.Name = "btnNewFragment";
            this.btnNewFragment.Size = new System.Drawing.Size(23, 22);
            this.btnNewFragment.Text = "New fragment";
            this.btnNewFragment.ToolTipText = "New fragment";
            this.btnNewFragment.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnExistedFragment
            // 
            this.btnExistedFragment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExistedFragment.Image = global::AutomatedWorker.Properties.Resources.openForAdd;
            this.btnExistedFragment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExistedFragment.Name = "btnExistedFragment";
            this.btnExistedFragment.Size = new System.Drawing.Size(23, 22);
            this.btnExistedFragment.Text = "Select fragment";
            this.btnExistedFragment.ToolTipText = "Select fragment";
            this.btnExistedFragment.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = global::AutomatedWorker.Properties.Resources.exit;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "Exit";
            this.btnClose.ToolTipText = "Exit";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 565);
            this.Controls.Add(this.tbMain);
            this.Controls.Add(this.pnlNewOperation);
            this.Controls.Add(this.pnlJob);
            this.Controls.Add(this.grdOperations);
            this.Controls.Add(this.lblJobCaption);
            this.Controls.Add(this.txtJobName);
            this.Name = "MainForm";
            this.Text = "Automated worker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMouseClickType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperations)).EndInit();
            this.pnlJob.ResumeLayout(false);
            this.pnlOperationsHeader.ResumeLayout(false);
            this.pnlOperationsHeader.PerformLayout();
            this.tbMain.ResumeLayout(false);
            this.tbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtJobName;
        private System.Data.DataSet dsOperations;
        private System.Windows.Forms.DataGridView grdOperations;
        private System.Data.DataTable tblOperations;
        private System.Data.DataColumn tclId;
        private System.Data.DataColumn tclName;
        private System.Data.DataColumn tclMouseX;
        private System.Data.DataColumn tclMouseY;
        private System.Data.DataColumn tclKeyboardText;
        private System.Data.DataColumn tclImage;
        private System.Windows.Forms.Label lblJobCaption;
        private System.Data.DataColumn tclClickTypeId;
        private System.Data.DataTable tblMouseClickType;
        private System.Data.DataColumn mclId;
        private System.Data.DataColumn mclCode;
        private System.Data.DataColumn mclName;
        private System.Windows.Forms.DataGridViewButtonColumn clDel;
        private System.Windows.Forms.DataGridViewImageColumn clImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMouseX;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMouseY;
        private System.Windows.Forms.DataGridViewComboBoxColumn clClickTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clKeyboardText;
        private System.Windows.Forms.GroupBox pnlJob;
        private System.Windows.Forms.GroupBox pnlNewOperation;
        private System.Windows.Forms.Panel pnlOperationsHeader;
        private System.Windows.Forms.Label lblKeyboardText;
        private System.Windows.Forms.Label lblClickType;
        private System.Windows.Forms.Label lblMouseY;
        private System.Windows.Forms.Label lblMouseX;
        private System.Windows.Forms.Label lblMouseCaption;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip tbMain;
        private System.Windows.Forms.ToolStripButton btnLoad;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnNewFragment;
        private System.Windows.Forms.ToolStripButton btnExistedFragment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnClose;
    }
}