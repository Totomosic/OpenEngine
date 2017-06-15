using System;
using System.Linq;
using System.Collections.Generic;

namespace OpenEngine
{
    public class SubImage
    {

        #region FIELDS

        private Texture texture;
        private Vector2[] textureCoords;
        private Vector2 size;

        #endregion

        #region CONSTRUCTORS

        public SubImage(Texture image, Vector2[] texCoords)
        {
            texture = image;
            textureCoords = texCoords;
            CalcSize();
        }

        public SubImage(Texture image, Vector2 botLeft, Vector2 dimensions)
        {
            texture = image;

            Vector2 tl = new Vector2(botLeft.X, botLeft.Y + dimensions.Y);
            Vector2 bl = new Vector2(botLeft.X, botLeft.Y);
            Vector2 br = new Vector2(botLeft.X + dimensions.X, botLeft.Y);
            Vector2 tr = new Vector2(botLeft.X + dimensions.X, botLeft.Y + dimensions.Y);
            textureCoords = new Vector2[] { tl, bl, br, tr };
            CalcSize();
        }

        #endregion

        #region PROPERTIES

        public Texture Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2[] TextureCoordinates
        {
            get { return textureCoords; }
            set { textureCoords = value; }
        }

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public Vector2 TopLeft
        {
            get { return textureCoords[0]; }
        }

        public Vector2 BottomLeft
        {
            get { return textureCoords[1]; }
        }

        public Vector2 BottomRight
        {
            get { return textureCoords[2]; }
        }

        public Vector2 TopRight
        {
            get { return textureCoords[3]; }
        }

        #endregion

        #region PUBLIC METHODS

        public void Bind(int unit = 0)
        {
            texture.Bind(unit);
        }

        public void Unbind(int unit = 0)
        {
            texture.Unbind(unit);
        }

        public float[] GetTexCoordArray()
        {
            return new float[] { TopLeft.X, TopLeft.Y, BottomLeft.X, BottomLeft.Y, BottomRight.X, BottomRight.Y, TopRight.X, TopRight.Y };
        }

        public float[] GetRectangleTexArray()
        {
            float[] array = GetTexCoordArray();
            float[] floats = new float[] { array[0], array[1], array[2], array[3], array[4], array[5], array[0], array[1], array[4], array[5], array[6], array[7] };
            return floats;
        }

        public float[] GetCuboidTexArray()
        {
            return GetRectangleTexArray().Concat(GetRectangleTexArray()).Concat(GetRectangleTexArray()).Concat(GetRectangleTexArray()).Concat(GetRectangleTexArray()).Concat(GetRectangleTexArray()).ToArray();
        }

        #endregion

        #region PRIVATE METHODS

        private void CalcSize()
        {
            float minX = textureCoords[0].X;
            float maxX = textureCoords[3].X;
            float minY = textureCoords[1].Y;
            float maxY = textureCoords[0].Y;
            size = new Vector2((maxX - minX) * texture.Width, (maxY - minY) * texture.Height);
        }

        #endregion

    }
}
