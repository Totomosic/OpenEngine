using System;
using System.Collections.Generic;
using System.Drawing;

namespace OpenEngine
{
    public class Table<T>
    {

        #region FIELDS

        private OutOfRange outOfRange;
        private int width;
        private int height;
        private T @default;

        private List<T> data;

        #endregion

        #region CONSTRUCTORS

        public Table(int w, int h, T[] initData, T defaultVal, OutOfRange handler = OutOfRange.ThrowException)
        {
            @default = defaultVal;
            width = w;
            height = h;
            outOfRange = handler;

            data = new List<T>(initData);
            // Initialise data
            if (initData == null || initData.Length > 0)
            {
                for (int i = 0; i < Total; i++)
                {
                    data.Add(@default);
                }
            }
        }

        public Table(int width, int height, T defaultVal, OutOfRange handler = OutOfRange.ThrowException) : this(width, height, null, defaultVal, handler)
        {

        }

        #region STATIC CONSTRUCTORS

        public static Table<float> FromImageBrightness(Bitmap image, float min = 0, float max = 1, OutOfRange handler = OutOfRange.ThrowException)
        {
            float[] data = new float[image.Width * image.Height];
            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    data[i + j * image.Width] = Utilities.Map(image.GetPixel(i, j).GetBrightness(), 0, 1, min, max);
                }
            }
            return new Table<float>(image.Width, image.Height, data, 0, handler);
        }

        #endregion

        #endregion

        #region PROPERTIES

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Total
        {
            get { return width * height; }
        }

        public T Default
        {
            get { return @default; }
            set { @default = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public T[] ToArray()
        {
            return data.ToArray();
        }

        public void Clear()
        {
            data.Clear();
            for (int i = 0; i < Total; i++)
            {
                data.Add(@default);
            }
        }

        public void SetValue(int x, int y, T value)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                data[From2DIndex(x, y)] = value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public T GetValue(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return data[From2DIndex(x, y)];
            }
            else
            {
                if (outOfRange == OutOfRange.ReturnDefault)
                {
                    return @default;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion

        #region PRIVATE METHODS

        private int From2DIndex(int x, int y)
        {
            return x + y * width;
        }

        #endregion

    }
}
