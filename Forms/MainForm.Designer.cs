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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.lblJobCaption = new System.Windows.Forms.Label();
            this.pnlJob = new System.Windows.Forms.GroupBox();
            this.pnlOperations = new System.Windows.Forms.Panel();
            this.pnlOperationsHeader = new System.Windows.Forms.Panel();
            this.lblKeyboardText = new System.Windows.Forms.Label();
            this.lblClickType = new System.Windows.Forms.Label();
            this.lblMouseY = new System.Windows.Forms.Label();
            this.lblMouseX = new System.Windows.Forms.Label();
            this.lblMouseCaption = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblImage = new System.Windows.Forms.Label();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tbMain = new System.Windows.Forms.ToolStrip();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewFragment = new System.Windows.Forms.ToolStripButton();
            this.btnExistedFragment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.lblContinue = new System.Windows.Forms.Label();
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
            this.pnlJob.Controls.Add(this.pnlOperations);
            this.pnlJob.Controls.Add(this.pnlOperationsHeader);
            this.pnlJob.Location = new System.Drawing.Point(12, 54);
            this.pnlJob.Name = "pnlJob";
            this.pnlJob.Size = new System.Drawing.Size(810, 365);
            this.pnlJob.TabIndex = 19;
            this.pnlJob.TabStop = false;
            this.pnlJob.Text = "Macro operations";
            // 
            // pnlOperations
            // 
            this.pnlOperations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOperations.Location = new System.Drawing.Point(6, 65);
            this.pnlOperations.Name = "pnlOperations";
            this.pnlOperations.Size = new System.Drawing.Size(798, 294);
            this.pnlOperations.TabIndex = 8;
            // 
            // pnlOperationsHeader
            // 
            this.pnlOperationsHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOperationsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOperationsHeader.Controls.Add(this.lblContinue);
            this.pnlOperationsHeader.Controls.Add(this.lblKeyboardText);
            this.pnlOperationsHeader.Controls.Add(this.lblClickType);
            this.pnlOperationsHeader.Controls.Add(this.lblMouseY);
            this.pnlOperationsHeader.Controls.Add(this.lblMouseX);
            this.pnlOperationsHeader.Controls.Add(this.lblMouseCaption);
            this.pnlOperationsHeader.Controls.Add(this.lblName);
            this.pnlOperationsHeader.Controls.Add(this.lblImage);
            this.pnlOperationsHeader.Location = new System.Drawing.Point(6, 19);
            this.pnlOperationsHeader.Name = "pnlOperationsHeader";
            this.pnlOperationsHeader.Size = new System.Drawing.Size(798, 40);
            this.pnlOperationsHeader.TabIndex = 7;
            // 
            // lblKeyboardText
            // 
            this.lblKeyboardText.AutoSize = true;
            this.lblKeyboardText.Location = new System.Drawing.Point(565, 11);
            this.lblKeyboardText.Name = "lblKeyboardText";
            this.lblKeyboardText.Size = new System.Drawing.Size(72, 13);
            this.lblKeyboardText.TabIndex = 13;
            this.lblKeyboardText.Text = "Keyboard text";
            // 
            // lblClickType
            // 
            this.lblClickType.AutoSize = true;
            this.lblClickType.Location = new System.Drawing.Point(418, 11);
            this.lblClickType.Name = "lblClickType";
            this.lblClickType.Size = new System.Drawing.Size(53, 13);
            this.lblClickType.TabIndex = 12;
            this.lblClickType.Text = "Click type";
            // 
            // lblMouseY
            // 
            this.lblMouseY.AutoSize = true;
            this.lblMouseY.Location = new System.Drawing.Point(340, 17);
            this.lblMouseY.Name = "lblMouseY";
            this.lblMouseY.Size = new System.Drawing.Size(14, 13);
            this.lblMouseY.TabIndex = 11;
            this.lblMouseY.Text = "Y";
            // 
            // lblMouseX
            // 
            this.lblMouseX.AutoSize = true;
            this.lblMouseX.Location = new System.Drawing.Point(287, 17);
            this.lblMouseX.Name = "lblMouseX";
            this.lblMouseX.Size = new System.Drawing.Size(14, 13);
            this.lblMouseX.TabIndex = 10;
            this.lblMouseX.Text = "X";
            // 
            // lblMouseCaption
            // 
            this.lblMouseCaption.AutoSize = true;
            this.lblMouseCaption.Location = new System.Drawing.Point(278, 0);
            this.lblMouseCaption.Name = "lblMouseCaption";
            this.lblMouseCaption.Size = new System.Drawing.Size(97, 13);
            this.lblMouseCaption.TabIndex = 9;
            this.lblMouseCaption.Text = "Mouse coordinates";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(128, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Name";
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(40, 11);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(36, 13);
            this.lblImage.TabIndex = 7;
            this.lblImage.Text = "Image";
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
            this.tbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoad,
            this.btnSave,
            this.btnClear,
            this.toolStripSeparator1,
            this.btnRun,
            this.toolStripSeparator2,
            this.btnNewFragment,
            this.btnExistedFragment,
            this.toolStripSeparator3,
            this.btnClose});
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(834, 25);
            this.tbMain.TabIndex = 21;
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
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClear.Image = global::AutomatedWorker.Properties.Resources.clear;
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(23, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
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
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "Exit";
            this.btnClose.ToolTipText = "Exit";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblContinue
            // 
            this.lblContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContinue.AutoSize = true;
            this.lblContinue.Location = new System.Drawing.Point(703, 4);
            this.lblContinue.MaximumSize = new System.Drawing.Size(100, 0);
            this.lblContinue.Name = "lblContinue";
            this.lblContinue.Size = new System.Drawing.Size(91, 26);
            this.lblContinue.TabIndex = 14;
            this.lblContinue.Text = "Continue if image isn\'t found";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 431);
            this.Controls.Add(this.tbMain);
            this.Controls.Add(this.pnlJob);
            this.Controls.Add(this.lblJobCaption);
            this.Controls.Add(this.txtJobName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(850, 300);
            this.Name = "MainForm";
            this.Text = "Automated worker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private System.Windows.Forms.Label lblJobCaption;
        private System.Windows.Forms.GroupBox pnlJob;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel pnlOperations;
        private System.Windows.Forms.Panel pnlOperationsHeader;
        private System.Windows.Forms.Label lblKeyboardText;
        private System.Windows.Forms.Label lblClickType;
        private System.Windows.Forms.Label lblMouseY;
        private System.Windows.Forms.Label lblMouseX;
        private System.Windows.Forms.Label lblMouseCaption;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblImage;
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
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.Label lblContinue;
    }
}