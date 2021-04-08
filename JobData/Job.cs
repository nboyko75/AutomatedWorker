using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using EventHook.Tools;

namespace JobData
{
    public class Job : StoreObject
    {
        private List<Panel> panels;
        public List<Panel> Panels
        {
            get { return panels; }
        }

        public Job(string ObjectName, string DataDir) : base(ObjectName, DataDir)
        {
            panels = new List<Panel>();
        }

        public void Load() 
        {
            panels.Clear();
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
                    Image = new Bitmap(op.Actor.ImageSrc),
                    Location = new Point(10, padding),
                    Name = $"image{op.Id}",
                    Size = new Size(30, 30),
                    TabIndex = i++,
                    TabStop = false
                };
                opPanel.Controls.Add(opImage);

                TextBox txtName = new TextBox
                {
                    Location = new Point(50, padding),
                    Name = $"txtName{op.Id}",
                    Size = new Size(180, 20),
                    Text = op.Name,
                    TabIndex = i++
                };
                opPanel.Controls.Add(txtName);

                NumericUpDown numMouseX = new NumericUpDown
                {
                    Location = new Point(240, padding),
                    Maximum = new decimal(new int[] { 99999, 0, 0, 0 }),
                    Name = $"numMouseX{op.Id}",
                    Size = new Size(50, 20),
                    Value = op.Action.ActPoint.X,
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
                    Value = op.Action.ActPoint.Y,
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
                int ClickType = (int)op.Action.ClickType;
                if (ClickType > 0)
                {
                    cmbClickType.SelectedIndex = ClickType - 1;
                }
                opPanel.Controls.Add(cmbClickType);
               
                TextBox txtKeyboardText = new TextBox
                {
                    Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right),
                    Location = new Point(490, padding),
                    Name = $"txtKeyboardText{op.Id}",
                    Size = new Size(210, 20),
                    Text = op.Action.KeyboardText,
                    TabIndex = i++
                };
                opPanel.Controls.Add(txtKeyboardText);

                panels.Add(opPanel);
            }
        }
    }
}
