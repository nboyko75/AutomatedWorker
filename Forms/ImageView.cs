using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutomatedWorker.Forms
{
    public partial class ImageView : Form
    {
        public ImageView()
        {
            InitializeComponent();
        }

        public void LoadImage(string imageSrc) 
        {
            imageBox.Image = new Bitmap(imageSrc);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
