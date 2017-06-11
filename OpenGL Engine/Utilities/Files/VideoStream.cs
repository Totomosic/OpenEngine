using System;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using SharpAvi;
using SharpAvi.Output;
using System.Collections.Generic;
using System.Threading;

namespace OpenEngine
{
    public class VideoStream
    {

        #region FIELDS

        private int framecount;
        private AviWriter writer;
        private IAviVideoStream vStream;
        private string filename;
        private int fps;
        private Vector2 size;

        private List<byte[]> frames;

        #endregion

        #region CONSTRUCTORS

        public VideoStream(string filenme, int width, int height, int framesps = 30, bool useVideoPath = true)
        {
            filename = (useVideoPath) ? Paths.VideoPath + filenme : filenme;
            fps = framesps;
            size = new Vector2();
            frames = new List<byte[]>();
            writer = new AviWriter(filename)
            {
                FramesPerSecond = fps,
                EmitIndex1 = true
            };
            vStream = writer.AddVideoStream();
            vStream.Width = width;
            vStream.Height = height;
            vStream.BitsPerPixel = BitsPerPixel.Bpp32;
            vStream.Codec = KnownFourCCs.Codecs.Uncompressed;
            framecount = 0;
        }

        #endregion

        #region PROPERTIES

        public int Framecount
        {
            get { return framecount; }
        }

        public int FPS
        {
            get { return fps; }
        }

        public Vector2 Size
        {
            get { return size; }
        }

        #endregion

        #region PUBLIC METHODS

        public void ProcessFrames()
        {
            foreach (byte[] data in frames)
            {
                vStream.WriteFrame(true, data, 0, data.Length);
                framecount++;
            }
            frames.Clear();
        }

        public void AddFrame(Texture frame)
        {
            frames.Add(frame.GetPixels());
        }

        public void Finish()
        {
            ProcessFrames();
            writer.Close();
        }

        public static void FromDirectory(string filename, string directoryname, int fps = 30, bool useRootDir = true)
        {
            if (useRootDir)
            {
                directoryname = Paths.RootDirectory + directoryname;
            }
            AviWriter writer = new AviWriter(Paths.RootDirectory + filename)
            {
                FramesPerSecond = fps,
                EmitIndex1 = true
            };
            IAviVideoStream stream = writer.AddVideoStream();
            stream.Codec = KnownFourCCs.Codecs.Uncompressed;
            stream.BitsPerPixel = BitsPerPixel.Bpp32;
            bool first = true;

            foreach (FileInfo file in Directory.GetFiles(directoryname).Select(fn => new FileInfo(fn)).OrderBy(f => f.CreationTime))
            {
                using (Bitmap image = new Bitmap(file.FullName))
                using (Bitmap newImage = new Bitmap(image))
                using (Bitmap target = newImage.Clone(new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), PixelFormat.Format32bppArgb))
                {
                    byte[] data = RefineArray(ToByteArray(image));
                    if (first)
                    {
                        stream.Width = image.Width;
                        stream.Height = image.Height;
                    }
                    stream.WriteFrame(true, data, 0, data.Length);
                    first = false;
                }
            }
            writer.Close();
        }

        #endregion

        #region PRIVATE METHODS

        private static byte[] ToByteArray(Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);
                stream.Position = 0;
                return stream.ToArray();
            }
        }

        private static byte[] RefineArray(byte[] array)
        {
            byte[] temp = array;
            byte[] data = new byte[temp.Length];
            for (int i = 0; i < temp.Length - 4; i += 4)
            {
                byte b = temp[i];
                byte a = temp[i + 1];
                byte r = temp[i + 2];
                byte g = temp[i + 3];

                data[i] = r;
                data[i + 1] = g;
                data[i + 2] = b;
                data[i + 3] = a;
            }
            return data;
        }

        #endregion

    }
}
