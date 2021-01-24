using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EventHook.Tools
{
    public class ImageUtils
    {
        public static void SaveImageToFile(BitmapSource image, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
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

        public static BitmapSource ConvertBitmap(Bitmap source)
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
    }
}
