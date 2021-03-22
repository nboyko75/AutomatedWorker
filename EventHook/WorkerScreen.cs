using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using EventHook.Tools;

namespace EventHook
{
    public class WorkerScreen
    {
        private ScreenshotMaker screenshotMaker;

        public WorkerScreen() 
        {
            screenshotMaker = new ScreenshotMaker();
        }

        #region Public methods
        public Point? GetFragmentPoint(Bitmap searchBitmap) 
        {
            Bitmap screen = GetScreenBitmap();
            return Find(screen, searchBitmap);
        }

        public Bitmap GetScreenBitmap() 
        {
            return screenshotMaker.GetBitmapRect(ImageFormat.Bmp);
        }

        public void SaveScreenBitmapToFile(string filePath)
        {
            Bitmap screenBmp = GetScreenBitmap();
            BitmapSource screenSrc = ImageUtils.SourceFromBitmap(screenBmp);
            ImageUtils.SaveImageToFile(screenSrc, filePath);
        }

        public Point? Find(Bitmap withinBitmap, Bitmap searchBitmap)
        {
            if (null == withinBitmap || null == searchBitmap)
            {
                return null;
            }
            if (withinBitmap.Width < searchBitmap.Width || withinBitmap.Height < searchBitmap.Height)
            {
                return null;
            }

            int[][] withinArray = ImageUtils.GetPixelArray(withinBitmap);
            int[][] searchArray = ImageUtils.GetPixelArray(searchBitmap);

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
        private int[][] GetPixelArray(Bitmap bitmap)
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

        private IEnumerable<Point> FindMatch(IEnumerable<int[]> withinLines, int[] searchLine)
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

        private bool ContainSameElements(int[] first, int firstStart, int[] second, int secondStart, int length)
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

        private bool SearchAtLocation(int[][] withinArray, int[][] searchArray, Point point, int alreadyVerified)
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
