using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JobData;

namespace AutomatedWorker.Forms
{
    public partial class SelectObjectForm : Form
    {
        public string SelectedObjectName { get { return lstFragments.SelectedItem.ToString(); } }

        public ActObject SelectedObject { get { return hasSelection() ? objectList[lstFragments.SelectedIndex] : null; } }

        public List<ActObject> ObjectList { get { return objectList; } }

        private Config config;
        private ObjectManager objectManager;
        private List<ActObject> objectList;

        public SelectObjectForm()
        {
            InitializeComponent();
            config = new Config();
            objectManager = new ObjectManager();
            PopulateObjects();
        }

        public void PopulateObjects()
        {
            lstFragments.Items.Clear();
            objectList = objectManager.Objects.GetSortedItems<ActObject>();
            foreach (ActObject o in objectList)
            {
                lstFragments.Items.Add(o.Name);
            }
        }


        protected void SelectObjectForm_Load(object sender, EventArgs e)
        {
            System.Drawing.Point savedLocation = Properties.Settings.Default.SelectObjectForm_Location;
            if (savedLocation.X > 0 || savedLocation.Y > 0)
            {
                Location = savedLocation;
            }
            System.Drawing.Size savedSize = Properties.Settings.Default.SelectObjectForm_Size;
            if (savedSize.Width > 0 || savedSize.Height > 0)
            {
                Size = savedSize;
            }
        }

        protected void SelectObjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.SelectObjectForm_Location = Location;
            Properties.Settings.Default.SelectObjectForm_Size = Size;
            Properties.Settings.Default.Save();
        }

        protected void lstFragments_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = true;
            btnDelete.Enabled = true;
        }

        protected void lstFragments_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private bool hasSelection() 
        {
            return lstFragments.SelectedIndex >= 0;
        }
    }
}
