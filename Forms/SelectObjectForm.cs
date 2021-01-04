using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AutomatedWorker.Base;
using AutomatedWorker.Data;

namespace AutomatedWorker.Forms
{
    public partial class SelectObjectForm : Form
    {
        public string SelectedObjectName { get { return cmbObject.Text; } }

        public ActObject SelectedObject { get { return objectList[cmbObject.SelectedIndex]; } }

        public List<ActObject> ObjectList { get { return objectList; } }

        private Config config;
        private ObjectManager objectManager;
        private List<ActObject> objectList;

        public SelectObjectForm()
        {
            InitializeComponent();
            config = new Config();
            objectManager = new ObjectManager();
            FillObjects();
        }

        public void FillObjects()
        {
            cmbObject.Items.Clear();
            objectList = objectManager.Objects.GetSortedItems<ActObject>();
            foreach (ActObject o in objectList)
            {
                cmbObject.Items.Add(o.Name);
            }
        }
    }
}
