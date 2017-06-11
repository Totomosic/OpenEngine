using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class Viewport
    {

        #region FIELDS

        private int xOffset;
        private int yOffset;
        private int width;
        private int height;

        #endregion

        #region CONSTRUCTORS

        public Viewport(int xOffset, int yOffset, int width, int height)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.width = width;
            this.height = height;
        }

        #endregion

        #region PROPERTIES

        public int X
        {
            get { return xOffset; }
            set { xOffset = value; }
        }

        public int Y
        {
            get { return yOffset; }
            set { yOffset = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public float Aspect
        {
            get { return (float)Width / (float)Height; }
        }

        #endregion

        #region PUBLIC METHODS

        public void Bind()
        {
            GL.Viewport(X, Y, Width, Height);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
