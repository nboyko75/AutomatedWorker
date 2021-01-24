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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new System.Windows.Forms.Button();
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
            this.btnNew = new System.Windows.Forms.Button();
            this.btnLoadJob = new System.Windows.Forms.Button();
            this.lblJobCaption = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dsOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOperations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMouseClickType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperations)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(651, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(79, 12);
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
            this.grdOperations.Location = new System.Drawing.Point(12, 40);
            this.grdOperations.Name = "grdOperations";
            this.grdOperations.Size = new System.Drawing.Size(715, 278);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.clMouseX.DefaultCellStyle = dataGridViewCellStyle1;
            this.clMouseX.HeaderText = "Mouse X";
            this.clMouseX.MaxInputLength = 5;
            this.clMouseX.Name = "clMouseX";
            this.clMouseX.Width = 60;
            // 
            // clMouseY
            // 
            this.clMouseY.DataPropertyName = "MouseY";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.clMouseY.DefaultCellStyle = dataGridViewCellStyle2;
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
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Location = new System.Drawing.Point(103, 324);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(87, 23);
            this.btnNew.TabIndex = 11;
            this.btnNew.Text = "New fragment";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnLoadJob
            // 
            this.btnLoadJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadJob.Location = new System.Drawing.Point(22, 324);
            this.btnLoadJob.Name = "btnLoadJob";
            this.btnLoadJob.Size = new System.Drawing.Size(75, 23);
            this.btnLoadJob.TabIndex = 14;
            this.btnLoadJob.Text = "Load macro";
            this.btnLoadJob.UseVisualStyleBackColor = true;
            this.btnLoadJob.Click += new System.EventHandler(this.btnLoadJob_Click);
            // 
            // lblJobCaption
            // 
            this.lblJobCaption.AutoSize = true;
            this.lblJobCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblJobCaption.Location = new System.Drawing.Point(28, 15);
            this.lblJobCaption.Name = "lblJobCaption";
            this.lblJobCaption.Size = new System.Drawing.Size(40, 13);
            this.lblJobCaption.TabIndex = 15;
            this.lblJobCaption.Text = "Macro:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(196, 324);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(115, 23);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Add existed fragment";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRun
            // 
            this.btnRun.Image = global::AutomatedWorker.Properties.Resources.run;
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRun.Location = new System.Drawing.Point(302, 9);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(53, 23);
            this.btnRun.TabIndex = 17;
            this.btnRun.Text = "Run";
            this.btnRun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 359);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.grdOperations);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblJobCaption);
            this.Controls.Add(this.btnLoadJob);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtJobName);
            this.Controls.Add(this.btnClose);
            this.Name = "MainForm";
            this.Text = "Automated worker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblOperations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMouseClickType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtJobName;
        private System.Data.DataSet dsOperations;
        private System.Windows.Forms.DataGridView grdOperations;
        private System.Windows.Forms.Button btnNew;
        private System.Data.DataTable tblOperations;
        private System.Data.DataColumn tclId;
        private System.Data.DataColumn tclName;
        private System.Data.DataColumn tclMouseX;
        private System.Data.DataColumn tclMouseY;
        private System.Data.DataColumn tclKeyboardText;
        private System.Windows.Forms.Button btnLoadJob;
        private System.Data.DataColumn tclImage;
        private System.Windows.Forms.Label lblJobCaption;
        private System.Data.DataColumn tclClickTypeId;
        private System.Data.DataTable tblMouseClickType;
        private System.Data.DataColumn mclId;
        private System.Data.DataColumn mclCode;
        private System.Data.DataColumn mclName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewButtonColumn clDel;
        private System.Windows.Forms.DataGridViewImageColumn clImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMouseX;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMouseY;
        private System.Windows.Forms.DataGridViewComboBoxColumn clClickTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clKeyboardText;
        private System.Windows.Forms.Button btnRun;
    }
}