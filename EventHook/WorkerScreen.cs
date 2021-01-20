﻿using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace EventHook
{
    public class WorkerScreen
    {
        #region Public methods
        public static Point? GetFragmentPoint(Bitmap searchBitmap) 
        {
            Bitmap screen = GetScreenBitmap();
            return Find(screen, searchBitmap);
        }

        public static Bitmap GetScreenBitmap() 
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            Bitmap res = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(res))
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            }
            return res;
        }

        public static Point? Find(Bitmap withinBitmap, Bitmap searchBitmap)
        {
            if (null == withinBitmap || null == searchBitmap)
            {
                return null;
            }
            if (withinBitmap.Width < searchBitmap.Width || withinBitmap.Height < searchBitmap.Height)
            {
                return null;
            }

            int[][] withinArray = GetPixelArray(withinBitmap);
            int[][] searchArray = GetPixelArray(searchBitmap);

            foreach (var firstLineMatchPoint in FindMatch(withinArray.Take(withinBitmap.Height - searchBitmap.Height), searchArray[0]))
            {
                if (SearchAtLocation(withinArray, searchArray, firstLineMatchPoint, 1))
                {
                    return firstLineMatchPoint;
                }
            }

            return null;
        }
        #endregion

        #region Private methods
        private static int[][] GetPixelArray(Bitmap bitmap)
        {
            var result = new int[bitmap.Height][];
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            for (int y = 0; y < bitmap.Height; ++y)
            {
                result[y] = new int[bitmap.Width];
                Marshal.Copy(bitmapData.Scan0 + y * bitmapData.Stride, result[y], 0, result[y].Length);
            }

            bitmap.UnlockBits(bitmapData);

            return result;
        }

        private static IEnumerable<Point> FindMatch(IEnumerable<int[]> withinLines, int[] searchLine)
        {
            var y = 0;
            foreach (var withinLine in withinLines)
            {
                for (int x = 0, n = withinLine.Length - searchLine.Length; x < n; ++x)
                {
                    if (ContainSameElements(withinLine, x, searchLine, 0, searchLine.Length))
                    {
                        yield return new Point(x, y);
                    }
                }
                y += 1;
            }
        }

        private static bool ContainSameElements(int[] first, int firstStart, int[] second, int secondStart, int length)
        {
            int firstMaxIdx = first.Length - 1;
            int secondMaxIdx = second.Length - 1;
            for (int i = 0; i < length; ++i)
            {
                int firstIdx = i + firstStart;
                int secondIdx = i + secondStart;
                if (firstIdx > firstMaxIdx || secondIdx > secondMaxIdx) 
                {
                    return false;
                }
                if (first[firstIdx] != second[secondIdx])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool SearchAtLocation(int[][] withinArray, int[][] searchArray, Point point, int alreadyVerified)
        {
            //we already know that "alreadyVerified" lines already match, so skip them
            for (int y = alreadyVerified; y < searchArray.Length; ++y)
            {
                if (!ContainSameElements(withinArray[y + point.Y], point.X, searchArray[y], 0, searchArray[y].Length))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
