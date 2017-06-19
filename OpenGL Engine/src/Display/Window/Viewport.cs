using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    /// <summary>
    /// Class that represents a viewport
    /// </summary>
    public class Viewport
    {

        #region FIELDS

        private int xOffset;
        private int yOffset;
        private int width;
        private int height;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructs a new viewport
        /// </summary>
        /// <param name="xOffset">Offset of the bottom left corner of the viewport specified in pixels</param>
        /// <param name="yOffset">Offset of the bottom left corner of the viewport specified in pixels</param>
        /// <param name="width">Width of viewport</param>
        /// <param name="height">Height of viewport</param>
        public Viewport(int xOffset, int yOffset, int width, int height)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.width = width;
            this.height = height;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// X offset
        /// </summary>
        public int X
        {
            get { return xOffset; }
            set { xOffset = value; }
        }

        /// <summary>
        /// Y offset
        /// </summary>
        public int Y
        {
            get { return yOffset; }
            set { yOffset = value; }
        }

        /// <summary>
        /// Width of viewport
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Height of viewport
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Aspect ratio of viewport (Width / Height)
        /// </summary>
        public float Aspect
        {
            get { return (float)Width / (float)Height; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Set viewport as current viewport
        /// </summary>
        public void Bind()
        {
            GL.Viewport(X, Y, Width, Height);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
