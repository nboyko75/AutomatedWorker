using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace EventHook
{
    public class Mouse
    {
        #region private fields
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern int SetCursorPos(int x, int y);

        // [DllImport("user32.dll", EntryPoint = "keybd_event")]
        // public static extern void keybd_event(byte bVk, byte bScan, KeyEventFlag dwFlags, int dwExtraInfo);

        public const int WAIT_FOR_CLICK = 1000;

        private const uint WM_MOUSELEFTDOWN = 0x02;
        private const uint WM_MOUSELEFTUP = 0x04;
        private const uint WM_MOUSERIGHTDOWN = 0x08;
        private const uint WM_MOUSERIGHTUP = 0x10;
        private const uint WM_MOUSEMOVE = 0x0200;

        public struct MousePoint
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public enum MouseSpeed { Instant, SuperSlow, Slow, Natural = 3, Fast = 5, SuperFast = 8 };
        #endregion

        #region static methods: click
        private static int MAKELPARAM(int p, int p_2)
        {
            return ((p_2 << 16) | (p & 0xFFFF));
        }

        static public void Click_Left()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            if (ApplicationIsActivated()) 
            {
                mouse_event(WM_MOUSELEFTDOWN | WM_MOUSELEFTUP, (uint)X, (uint)Y, 0, 0);
            }
            else 
            {
                PostMessage(GetForegroundWindow(), WM_MOUSELEFTDOWN | WM_MOUSELEFTUP, (IntPtr)0, (IntPtr)MAKELPARAM(X, Y));
            }
        }

        static public void Click_Right()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            if (ApplicationIsActivated())
            {
                mouse_event(WM_MOUSERIGHTDOWN | WM_MOUSERIGHTUP, (uint)X, (uint)Y, 0, 0);
            }
            else
            {
                PostMessage(GetForegroundWindow(), WM_MOUSERIGHTDOWN | WM_MOUSERIGHTUP, (IntPtr)0, (IntPtr)MAKELPARAM(X, Y));
            }
        }

        static public void Click_DoubleClick()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            if (ApplicationIsActivated())
            {
                mouse_event(WM_MOUSELEFTDOWN | WM_MOUSELEFTUP, (uint)X, (uint)Y, 0, 0);
                Thread.Sleep(150);
                mouse_event(WM_MOUSELEFTDOWN | WM_MOUSELEFTUP, (uint)X, (uint)Y, 0, 0);
            }
            else
            {
                PostMessage(GetForegroundWindow(), WM_MOUSELEFTDOWN | WM_MOUSELEFTUP, (IntPtr)0, (IntPtr)MAKELPARAM(X, Y));
                Thread.Sleep(150);
                PostMessage(GetForegroundWindow(), WM_MOUSELEFTDOWN | WM_MOUSELEFTUP, (IntPtr)0, (IntPtr)MAKELPARAM(X, Y));
            }
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
            // MessageBox.Show($"Mouse moveTo x = {p.X}, y ={p.Y}");
            if (ApplicationIsActivated())
            {
                SetCursorPos(p.X, p.Y);
                // Cursor.Position = p;
            }
            else 
            {
                SendMessage(GetForegroundWindow(), WM_MOUSEMOVE, (IntPtr)0, (IntPtr)MAKELPARAM(p.X, p.Y));
                // PostMessage(GetForegroundWindow(), WM_MOUSEMOVE, (IntPtr)0, (IntPtr)MAKELPARAM(p.X, p.Y));
            }
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
        public static bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false; // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }

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
