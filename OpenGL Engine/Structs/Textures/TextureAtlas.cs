using System;
using System.Collections.Generic;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class TextureAtlas : Texture
    {

        #region FIELDS

        private int columns;
        private int rows;
        private int totalSubImages;
        private int currentIndex;
        private List<SubImage> subImages;

        #endregion

        #region CONSTRUCTORS

        public TextureAtlas(int id, Vector2 size, int cols_, int rows_, bool transparent) : base(id, TextureTarget.Texture2D, size, transparent)
        {
            columns = cols_;
            rows = rows_;
            totalSubImages = columns * rows;
            subImages = new List<SubImage>();
            currentIndex = totalSubImages;
            CalculateSubImages();
        }

        public TextureAtlas() : this(0, new Vector2(), 0, 0, false)
        {

        }

        #endregion

        #region PROPERTIES

        public int Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        public int ImageCount
        {
            get { return columns * rows; }
        }

        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public SubImage GetSubImage(int index = 0)
        {
            return subImages[index];
        }

        public SubImage GetNextImage()
        {
            currentIndex %= ImageCount;
            SubImage image = subImages[currentIndex];
            currentIndex++;
            return image;
        }

        #endregion

        #region PRIVATE METHDOS

        private void CalculateSubImages()
        {
            float rowSize = 1f / rows;
            float colSize = 1f / columns;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    subImages.Add(new SubImage(this, new Vector2[] { new Vector2(colSize * j, 1 - rowSize * i), new Vector2(colSize * j, 1 - rowSize * (i + 1)), new Vector2(colSize * (j + 1), 1 - rowSize * (i + 1)), new Vector2(colSize * (j + 1), 1 - rowSize * i) }));
                }
            }

        }

        #endregion

    }
}
