using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using JsonFlatFileDataStore;

namespace EventHook.Tools
{
    public class ImageUtils
    {
        public static void SaveImageToFile(BitmapSource image, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }

        public static void SaveImageToFile(Bitmap image, string filePath)
        {
            BitmapSource imageSrc = SourceFromBitmap(image);
            SaveImageToFile(imageSrc, filePath);
        }

        public static Byte[] LoadImageFromFile(string filePath)
        {
            Bitmap img = new Bitmap(filePath);
            byte[] res;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                res = ms.ToArray();
            }
            return res;
        }

        public static BitmapSource SourceFromBitmap(Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(),
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var ms = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(ms);
                bitmap = new Bitmap(ms);
            }
            return bitmap;
        }

        public static Byte[] ByteArrayFromSource(BitmapSource bitmapsource)
        {
            Byte[] result;
            using (var ms = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(ms);
                result = ms.ToArray();
            }
            return result;
        }

        public static Byte[] BitmapToByteArray(Bitmap source) 
        {
            Byte[] result = null;
            if (source != null)
            {
                MemoryStream ms = new MemoryStream();
                source.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                result = ms.ToArray();
            }
            return result;
        }

        public static Bitmap ByteArrayToBitmap(Byte[] source) 
        {
            Bitmap result = null;
            if (source != null)
            {
                MemoryStream ms = new MemoryStream(source);
                result = new Bitmap(Image.FromStream(ms));
            }
            return result;
        }

        public static BitmapImage BitmapImageFromSource(BitmapSource bitmapsource)
        {
            BitmapImage img = new BitmapImage();
            MemoryStream ms = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(bitmapsource));
            enc.Save(ms);

            img.BeginInit();
            img.StreamSource = ms;
            img.EndInit();

            ms.Close();
            return img;
        }

        public static int[][] GetPixelArray(Bitmap bitmap)
        {
            var result = new int[bitmap.Height][];
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            for (int y = 0; y < bitmap.Height; ++y)
            {
                result[y] = new int[bitmap.Width];
                System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0 + y * bitmapData.Stride, result[y], 0, result[y].Length);
            }

            bitmap.UnlockBits(bitmapData);

            return result;
        }

        public static void SavePixelArrayToFile(Bitmap bitmap, string filePath)
        {
            int[][] array = GetPixelArray(bitmap);
            string json = JsonUtils.GetJsonOfObject(array);
            File.WriteAllText($"{filePath}.json", json);
        }
    }
}
