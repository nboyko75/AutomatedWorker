using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using EventHook;
using JobData;
using JobRunner.Properties;

namespace JobRunner
{
    public class Runner
    {
        #region Consts
        public const int CHECKIMG_REPEAT_DELAY = 1000;
        public const int CLICK_DELAY = 200;
        #endregion

        private Mouse mouse;
        private ResourceManager resManager;

        public Runner() 
        {
            mouse = new Mouse();
            resManager = new ResourceManager("JobRunner.Properties.Resources", typeof(Resources).Assembly);
        }

        public void Run(Job job) 
        {
            List<Operation> ops = job.GetItems<Operation>();
            if (ops.Count == 0)
            {
                return;
            }
            
            for (int i = 0; i < ops.Count; i++)
            {
                Operation op = ops[i];
                bool isLast = i == ops.Count - 1;
                if (!runOperation(op, isLast)) 
                {
                    break;
                }
            }
        }

        private bool runOperation(Operation op, bool isLast)
        {
            Bitmap img = new Bitmap(op.Actor.ImageSrc);
            Thread.Sleep(CHECKIMG_REPEAT_DELAY);
            WorkerScreen screen = new WorkerScreen();
            System.Drawing.Point? imgPoint = screen.GetFragmentPoint(img);
            if (!imgPoint.HasValue)
            {
                if (!op.ToContinue)
                {
                    MessageBox.Show(String.Format(resManager.GetString("ErrFragmentIsNotFound"), op.Name), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else 
                {
                    return true;
                }
            }
            mouse.MoveTo(imgPoint.Value.X + op.Action.ActPoint.X, imgPoint.Value.Y + op.Action.ActPoint.Y);

            Thread.Sleep(CLICK_DELAY);
            switch (op.Action.ClickType)
            {
                case MouseClickType.LEFTCLICK:
                    mouse.Click_Left();
                    break;
                case MouseClickType.RIGHTCLICK:
                    mouse.Click_Right();
                    break;
                case MouseClickType.DOUBLECLICK:
                    mouse.Click_Left();
                    mouse.Click_Right();
                    break;
            }
            return true;
        }
    }
}
