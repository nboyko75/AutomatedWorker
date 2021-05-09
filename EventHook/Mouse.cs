using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Interceptor;

namespace EventHook
{
    public class Mouse
    {
        private Input simulator;
        private static int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private static int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        #region user consts and structs
        public const int WAIT_FOR_CLICK = 1000;
        public const int InputMouse = 0;

        public struct MousePoint
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public enum MouseSpeed { Instant, SuperSlow, Slow, Natural = 3, Fast = 5, SuperFast = 8 };
        #endregion

        public Mouse()
        {
            simulator = new Input();
            simulator.KeyboardFilterMode = KeyboardFilterMode.All;
            simulator.Load();
        }

        #region static methods: click
        public void Click_Left()
        {
            simulator.SendLeftClick();
        }

        public void Click_Right()
        {
            simulator.SendRightClick();
        }
        #endregion

        #region static methods: move
        public void MoveTo(Point p)
        {
            moveTo(p, MouseSpeed.Natural);
        }

        public void MoveTo(Point p, MouseSpeed speed)
        {
            switch (speed)
            {
                case MouseSpeed.Instant:
                    MoveTo(p);
                    break;
                default:
                    moveTo(p, speed);
                    break;
            }
        }

        public void MoveTo(int x, int y)
        {
            MoveTo(new Point(x, y));
        }

        public void MoveTo(int x, int y, MouseSpeed speed)
        {
            MoveTo(new Point(x, y), speed);
        }

        private void moveTo(Point p)
        {
            Point ap = getAbsoluteCoord(p);
            simulator.MoveMouseTo(ap.X, ap.Y, true);
        }

        private void moveTo(Point p, MouseSpeed speed)
        {
            List<Point> wayPoints = new List<Point>();
            switch (speed)
            {
                case MouseSpeed.Instant:
                    moveTo(p);
                    return;
                default:
                    getWayPoints(Cursor.Position, p, ref wayPoints, (int)speed);
                    foreach (Point waypoint in wayPoints)
                    {
                        moveTo(waypoint);
                        Thread.Sleep(5);
                    }
                    return;
            }
        }

        private void getWayPoints(Point from, Point to, ref List<Point> points, int divider)
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

        public static Point getAbsoluteCoord(Point p)
        {
            Point res = new Point();
            res.X = (p.X * 65535) / screenWidth;
            res.Y = (p.Y * 65535) / screenHeight;
            return res;
        }
        #endregion
    }
}
