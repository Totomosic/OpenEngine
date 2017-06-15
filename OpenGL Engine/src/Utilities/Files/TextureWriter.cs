using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace OpenEngine
{
    public static class TextureWriter
    {

        public static void WriteTexture(Texture texture, string filename)
        {
            byte[] data = texture.GetPixels();
            Bitmap image = new Bitmap(texture.Width, texture.Height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb );
            Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);
            image.UnlockBits(bitmapData);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            image.Save(filename);
            image.Dispose();
        }

    }
}
