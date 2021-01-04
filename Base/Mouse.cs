using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using AutomatedWorker.Data;

namespace AutomatedWorker.Action
{
    public class Mouse
    {
        #region private fields
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        public enum MouseSpeed { Instant, SuperSlow, Slow, Natural = 3, Fast = 5, SuperFast = 8 };
        #endregion

        #region static methods: click
        static public void Click_Left()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        static public void Click_Right()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }

        static public void Click_DoubleClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            Thread.Sleep(150);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
        #endregion

        #region static methods: move
        static public void MoveTo(Point p)
        {
            Mouse.moveTo(p, MouseSpeed.Natural);
        }

        static public void MoveTo(Point p, MouseSpeed speed)
        {
            switch (speed)
            {
                case MouseSpeed.Instant:
                    Mouse.MoveTo(p);
                    break;
                default:
                    Mouse.moveTo(p, speed);
                    break;
            }
        }

        static public void MoveTo(int x, int y)
        {
            Mouse.MoveTo(new Point(x, y));
        }

        static public void MoveTo(int x, int y, MouseSpeed speed)
        {
            Mouse.MoveTo(new Point(x, y), speed);
        }

        private static void moveTo(Point p)
        {
            Cursor.Position = p;
        }

        private static void moveTo(Point p, MouseSpeed speed)
        {
            List<Point> wayPoints = new List<Point>();
            switch (speed)
            {
                case MouseSpeed.Instant:
                    Mouse.moveTo(p);
                    return;
                default:
                    getWayPoints(Cursor.Position, p, ref wayPoints, (int)speed);
                    foreach (Point waypoint in wayPoints)
                    {
                        Mouse.moveTo(waypoint);
                        Thread.Sleep(5);
                    }
                    return;
            }
        }

        private static void getWayPoints(Point from, Point to, ref List<Point> points, int divider)
        {
            lock (points)
            {
                int fromX, fromY, toX, toY, distanceX, distanceY, directionX, directionY, howMany;
                double intervalX, intervalY;
                fromX = from.X;
                fromY = from.Y;
                toX = to.X;
                toY = to.Y;
                distanceX = Math.Abs(fromX - toX);
                distanceY = Math.Abs(fromY - toY);
                directionX = fromX > toX ? -1 : 1;
                directionY = fromY > toY ? -1 : 1;
                howMany = ((distanceX > distanceY) ? distanceX : distanceY) / (6 * divider);
                intervalX = 1.000 * distanceX / howMany;
                intervalY = 1.000 * distanceY / howMany;
                points.Clear();
                for (int i = 1; i <= howMany; i++)
                {
                    points.Add(new Point(
                        fromX + (int)(intervalX * i * directionX),
                        fromY + (int)(intervalY * i * directionY)
                        ));
                }
            }
        }
        #endregion

        #region static utils
        public static MousePoint GetScreenCenterPoint()
        {
            return new MousePoint { X = Screen.PrimaryScreen.Bounds.Width / 2, Y = Screen.PrimaryScreen.Bounds.Height / 2 };
        }

        public static MousePoint GetAppropriatedFormPoint(MousePoint currentPoint, int formWidth, int formHeight) 
        {
            const int identX = 10;
            const int identY = 10;
            int pointX = currentPoint.X - formWidth - identX;
            if (pointX < identX) 
            {
                pointX = currentPoint.X + identX;
            }
            int pointY = currentPoint.Y - formHeight - identY;
            if (pointY < identY)
            {
                pointY = identY;
            }
            return new MousePoint { X = pointX, Y = pointY };
        }
        #endregion
    }
}
