using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class TextureData : DataObject
    {

        #region FIELDS

        private TextureWrapMode wrapS;
        private TextureWrapMode wrapT;
        private TextureWrapMode wrapR;
        private TextureMinFilter minFilter;
        private TextureMagFilter magFilter;

        private int columns;
        private int rows;
        private bool invertYAxis;

        #endregion

        #region CONSTRUCTORS

        public TextureData(int cols_ = 1, int rows_ = 1, bool invertY = true, TextureWrapMode sWrap = TextureWrapMode.Repeat, TextureWrapMode tWrap = TextureWrapMode.Repeat, TextureWrapMode rWrap = TextureWrapMode.Repeat, TextureMinFilter min = TextureMinFilter.LinearMipmapLinear, TextureMagFilter mag = TextureMagFilter.Linear)
        {
            wrapS = sWrap;
            wrapT = tWrap;
            wrapR = rWrap;
            minFilter = min;
            magFilter = mag;

            columns = cols_;
            rows = rows_;
            invertYAxis = invertY;
        }

        #endregion

        #region PROPERTIES

        public TextureWrapMode WrapS
        {
            get { return wrapS; }
        }

        public TextureWrapMode WrapT
        {
            get { return wrapT; }
        }

        public TextureWrapMode WrapR
        {
            get { return wrapR; }
        }

        public TextureMinFilter MinFilter
        {
            get { return minFilter; }
        }

        public TextureMagFilter MagFilter
        {
            get { return magFilter; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public int Rows
        {
            get { return rows; }
        }

        public bool InvertY
        {
            get { return invertYAxis; }
        }

        #endregion

    }
}
