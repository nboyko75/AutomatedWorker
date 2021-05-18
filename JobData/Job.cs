using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;
using EventHook;
using JobData.Forms;

namespace JobData
{
    public class Job : StoreObject
    {
        private const int padding = 2;
        private const int marginY = padding + 5;
        private const int panelHeight = 30 + 2 * padding;
        private const int panelWidth = 795;

        private Control ownerControl;
        private List<Panel> panels;
        private Dictionary<int, Panel> operationToPanel;
        private ResourceManager resources;

        public List<Panel> Panels
        {
            get { return panels; }
        }

        public Job(string ObjectName, string DataDir) : base(ObjectName, DataDir)
        {
            panels = new List<Panel>();
            operationToPanel = new Dictionary<int, Panel>();
            resources = JobData.Properties.Resources.ResourceManager;
        }

        public void setOwnerControl(Control ownerCtrl)
        {
            ownerControl = ownerCtrl;
        }

        public void Load() 
        {
            Clear();
            foreach (Operation op in GetItems<Operation>())
            {
                Panel opPanel = getNewOperationPanel(op);
                AddControlsToPanel(opPanel, op);
            }
            if (ownerControl != null) 
            {
                ownerControl.Controls.Clear();
                ownerControl.Controls.AddRange(panels.ToArray());
            }
        }

        public void Save()
        {
            List<Operation> operations = GetItems<Operation>();
            DeleteAll<Operation>();
            foreach (KeyValuePair<int, Panel> dictPair in operationToPanel)
            {
                Operation op = operations.Find(o => o.Id == dictPair.Key);
                if (op != null) 
                {
                    Bind(op, false);
                    Add<Operation>(op);
                }
            }
        }

        protected void OnImageDblClick(object sender, System.Windows.Forms.MouseEventArgs e) 
        {
            Control pctBox = (Control)sender;
            Operation op = GetItem<Operation>((int)pctBox.Tag);
            Mouse.MousePoint mousePoint = Mouse.GetScreenCenterPoint();
            if (op.Actor != null && op.Actor.ImageSrc.Length > 0)
            {
                ImageView imageView = new ImageView();
                imageView.LoadImage(op.Actor.ImageSrc);
                Mouse.MousePoint formLocation = Mouse.GetAppropriatedFormPoint(mousePoint, imageView.Size.Width, imageView.Size.Height);
                imageView.Location = new System.Drawing.Point(formLocation.X, formLocation.Y);
                imageView.Show();
            }
        }

        public void Add(ActObject obj, int x, int y) 
        {
            Act newAct = new Act { ActPoint = new Mouse.MousePoint { X = x, Y = y }, ClickType = Config.DEFMOUSE_CLICKTYPE };
            int newActId = GetCount<Operation>() + 1;
            Operation newOp = new Operation { Id = newActId, Name = obj.Name, Actor = obj, Action = newAct, ToContinue = false };
            Add<Operation>(newOp);
            AddOperationPanel(newOp);
        }

        public void Clear() 
        {
            panels.Clear();
            operationToPanel.Clear();
        }

        protected void btnDelete_Click(object sender, EventArgs e) 
        {
            DialogResult confirmRes = MessageBox.Show(resources.GetString("DeleteOperationConfirmation"),
                resources.GetString("DeleteConfirmationTitle"), MessageBoxButtons.YesNo);
            if (confirmRes == DialogResult.Yes)
            {
                Button btn = (Button)sender;
                if (Int32.TryParse(btn.Tag.ToString(), out int opId))
                {
                    Operation op = GetItem<Operation>(opId);
                    if (op != null)
                    {
                        DeleteOperationPanel(op);
                    }
                }
            }
        }

        private void AddControlsToPanel(Panel opPanel, Operation op)
        {
            int i = 0;
            int x = 10;
            Button btnDelete = new Button()
            {
                Location = new Point(10, padding + 3),
                Name = $"btnDelete{op.Id}",
                Size = new Size(24, 24),
                Image = (Image)resources.GetObject("delete"),
                
                TabIndex = i++,
                Tag = op.Id
            };
            btnDelete.Click += new EventHandler(btnDelete_Click);
            ToolTip btnDeleteToolTip = new ToolTip();
            btnDeleteToolTip.SetToolTip(btnDelete, resources.GetString("DeleteRow"));
            opPanel.Controls.Add(btnDelete);
            x += 30;

            PictureBox opImage = new PictureBox
            {
                Location = new Point(x, padding),
                Name = $"image{op.Id}",
                Size = new Size(30, 30),
                TabStop = false,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = op.Id
            };
            opImage.MouseDoubleClick += OnImageDblClick;
            ToolTip imgToolTip = new ToolTip();
            imgToolTip.SetToolTip(opImage, resources.GetString("ImageHint"));
            opPanel.Controls.Add(opImage);
            x += 40;

            TextBox txtName = new TextBox
            {
                Location = new Point(x, marginY),
                Name = $"txtName{op.Id}",
                Size = new Size(180, 20),
                TabIndex = i++
            };
            opPanel.Controls.Add(txtName);
            x += 190;

            NumericUpDown numMouseX = new NumericUpDown
            {
                Location = new Point(x, marginY),
                Maximum = new decimal(new int[] { 99999, 0, 0, 0 }),
                Name = $"numMouseX{op.Id}",
                Size = new Size(50, 20),
                TabIndex = i++,
                TextAlign = HorizontalAlignment.Center
            };
            opPanel.Controls.Add(numMouseX);
            x += 60;

            NumericUpDown numMouseY = new NumericUpDown
            {
                Location = new Point(x, marginY),
                Maximum = new decimal(new int[] { 99999, 0, 0, 0 }),
                Name = $"numMouseY{op.Id}",
                Size = new Size(50, 20),
                TabIndex = i++,
                TextAlign = HorizontalAlignment.Center
            };
            opPanel.Controls.Add(numMouseY);
            x += 60;

            ComboBox cmbClickType = new ComboBox
            {
                FormattingEnabled = true,
                Location = new Point(x, marginY),
                Name = $"cmbClickType{op.Id}",
                Size = new Size(120, 20),
                TabIndex = i++
            };
            cmbClickType.Items.AddRange(new object[] {
                    JobData.Properties.Resources.LeftButtonClick,
                    JobData.Properties.Resources.RightButtonClick,
                    JobData.Properties.Resources.DoubleButtonClick
                });
            opPanel.Controls.Add(cmbClickType);
            x += 130;

            TextBox txtKeyboardText = new TextBox
            {
                Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right),
                Location = new Point(x, marginY),
                Name = $"txtKeyboardText{op.Id}",
                Size = new Size(180, 20),
                TabIndex = i++
            };
            opPanel.Controls.Add(txtKeyboardText);
            x += 190;

            CheckBox chkContinue = new CheckBox
            {
                Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Right),
                Location = new Point(x + 30, marginY),
                Name = $"chkContinue{op.Id}",
                TabIndex = i++
            };
            opPanel.Controls.Add(chkContinue);
            x += 110;

            panels.Add(opPanel);
            operationToPanel.Add(op.Id.Value, opPanel);
            Bind(op, true);
        }

        private void Bind (Operation op, bool toControls) 
        {
            Panel pnl = operationToPanel[op.Id.Value];
            if (pnl == null) 
            {
                return;
            }
            Mouse.MousePoint point = new Mouse.MousePoint { X = 0, Y = 0 };
            foreach (Control ctrl in pnl.Controls)
            {
                string name = ctrl.Name;
                if (name.StartsWith("image"))
                {
                    if (toControls)
                    {
                        try
                        {
                            ((PictureBox)ctrl).Image = new Bitmap(op.Actor.ImageSrc);
                        }
                        catch (Exception e) 
                        {
                            MessageBox.Show($"Cannot read the file '{op.Actor.ImageSrc}'");
                        }
                    }
                }
                else if (name.StartsWith("txtName"))
                {
                    if (toControls)
                    {
                        ctrl.Text = op.Name;
                    }
                    else
                    {
                        op.Name = ctrl.Text;
                    }
                }
                else if (name.StartsWith("numMouseX"))
                {
                    if (toControls)
                    {
                        ((NumericUpDown)ctrl).Value = op.Action.ActPoint.X;
                    }
                    else
                    {
                        point.X = (int)((NumericUpDown)ctrl).Value;
                    }
                }
                else if (name.StartsWith("numMouseY"))
                {
                    if (toControls)
                    {
                        ((NumericUpDown)ctrl).Value = op.Action.ActPoint.Y;
                    }
                    else
                    {
                        point.Y = (int)((NumericUpDown)ctrl).Value;
                    }
                }
                else if (name.StartsWith("cmbClickType"))
                {
                    if (toControls)
                    {
                        ((ComboBox)ctrl).SelectedIndex = (int)op.Action.ClickType - 1;
                    }
                    else
                    {
                        op.Action.ClickType = (MouseClickType)((ComboBox)ctrl).SelectedIndex + 1;
                    }
                }
                else if (name.StartsWith("txtKeyboardText"))
                {
                    if (toControls)
                    {
                        ctrl.Text = op.Action.KeyboardText;
                    }
                    else
                    {
                        op.Action.KeyboardText = ctrl.Text;
                    }
                }
                else if (name.StartsWith("chkContinue"))
                {
                    CheckBox chkContinue = (CheckBox)ctrl;
                    if (toControls)
                    {
                        chkContinue.Checked = op.ToContinue;
                    }
                    else
                    {
                        op.ToContinue = chkContinue.Checked;
                    }
                }
            }
            if (!toControls)
            {
                op.Action.ActPoint = point;
            }
        }

        private Panel getNewOperationPanel(Operation op) 
        {
            return new Panel
            {
                Name = $"panel{op.Id}",
                Location = new Point(0, panels.Count * panelHeight),
                Size = new Size(panelWidth, panelHeight),
                Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right)
            };
        }

        private void AddOperationPanel(Operation op)
        {
            Panel opPanel = getNewOperationPanel(op);
            if (ownerControl != null)
            {
                AddControlsToPanel(opPanel, op);
                ownerControl.Controls.Add(opPanel);
            }
        }

        private void DeleteOperationPanel(Operation op)
        {
            Panel opPanel = operationToPanel[op.Id.Value];
            panels.Remove(opPanel);
            operationToPanel.Remove(op.Id.Value);
            if (ownerControl != null) 
            {
                ownerControl.Controls.Remove(opPanel);
            }
        }
    }
}
