using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using EventHook;
using JobData.Forms;

namespace JobData
{
    public class Job : StoreObject
    {
        private List<Panel> panels;
        private Dictionary<int, Panel> operationToPanel;

        public List<Panel> Panels
        {
            get { return panels; }
        }

        public Job(string ObjectName, string DataDir) : base(ObjectName, DataDir)
        {
            panels = new List<Panel>();
            operationToPanel = new Dictionary<int, Panel>();
        }

        public void Load() 
        {
            panels.Clear();
            operationToPanel.Clear();
            int panelIdx = 0;
            int padding = 2;
            int panelHeight = 30 + 2 * padding;
            int panelWidth = 735;
            foreach (Operation op in GetItems<Operation>())
            {
                int i = 0;
                Panel opPanel = new Panel
                {
                    Name = $"panel{op.Id}",
                    Location = new Point(0, (panelIdx++) * panelHeight),
                    Size = new Size(panelWidth, panelHeight),
                    Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right)
                };

                PictureBox opImage = new PictureBox
                {
                    Location = new Point(10, padding),
                    Name = $"image{op.Id}",
                    Size = new Size(30, 30),
                    TabIndex = i++,
                    TabStop = false,
                    Tag = op.Id
                };
                opImage.MouseDoubleClick += OnImageDblClick;
                opPanel.Controls.Add(opImage);

                TextBox txtName = new TextBox
                {
                    Location = new Point(50, padding),
                    Name = $"txtName{op.Id}",
                    Size = new Size(180, 20),
                    TabIndex = i++
                };
                opPanel.Controls.Add(txtName);

                NumericUpDown numMouseX = new NumericUpDown
                {
                    Location = new Point(240, padding),
                    Maximum = new decimal(new int[] { 99999, 0, 0, 0 }),
                    Name = $"numMouseX{op.Id}",
                    Size = new Size(50, 20),
                    TabIndex = i++,
                    TextAlign = HorizontalAlignment.Center
                };
                opPanel.Controls.Add(numMouseX);

                NumericUpDown numMouseY = new NumericUpDown
                {
                    Location = new Point(300, padding),
                    Maximum = new decimal(new int[] { 99999, 0, 0, 0 }),
                    Name = $"numMouseY{op.Id}",
                    Size = new Size(50, 20),
                    TabIndex = i++,
                    TextAlign = HorizontalAlignment.Center
                };
                opPanel.Controls.Add(numMouseY);

                ComboBox cmbClickType = new ComboBox
                {
                    FormattingEnabled = true,
                    Location = new Point(360, padding),
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
               
                TextBox txtKeyboardText = new TextBox
                {
                    Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right),
                    Location = new Point(490, padding),
                    Name = $"txtKeyboardText{op.Id}",
                    Size = new Size(210, 20),
                    TabIndex = i++
                };
                opPanel.Controls.Add(txtKeyboardText);

                panels.Add(opPanel);
                operationToPanel.Add(op.Id.Value, opPanel);
                Bind(op, true);
            }
        }

        public void Save()
        {
            foreach (Operation op in GetItems<Operation>())
            {
                Bind(op, false);
                Edit<Operation>(op);
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
            Operation newOp = new Operation { Id = newActId, Actor = obj, Action = newAct };
            Add<Operation>(newOp);
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
                        ((PictureBox)ctrl).Image = new Bitmap(op.Actor.ImageSrc);
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
            }
            if (!toControls)
            {
                op.Action.ActPoint = point;
            }
        }
    }
}
