using System;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class WindowInfo
    {

        #region FIELDS

        private GlfwWindowPtr displayPtr;

        private Vector2 position;
        private Vector2 size;
        private readonly Vector2 originalSize;
        private string title;

        #endregion

        #region CONSTRUCTORS

        public WindowInfo(GlfwWindowPtr window, Vector2 size, string title, Vector2 position = new Vector2())
        {
            displayPtr = window;
            this.size = size;
            originalSize = size;
            this.position = position;
            this.title = title;
        }

        #endregion

        #region PROPERTIES

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public Vector2 OriginalSize
        {
            get { return originalSize; }
        }

        public Vector2 ScreenPosition
        {
            get { return position; }
            set { position = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public GlfwWindowPtr WindowPtr
        {
            get { return displayPtr; }
            set { displayPtr = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public void SetSize(int w, int h)
        {
            Size = new Vector2(w, h);
            Glfw.SetWindowSize(displayPtr, w, h);
            GL.Viewport(0, 0, w, h);
        }

        public void SetTitle(string title)
        {
            this.title = title;
            Glfw.SetWindowTitle(displayPtr, title);
        }

        public void SetPosition(int x, int y)
        {
            ScreenPosition = new Vector2(x, y);
            Glfw.SetWindowPos(displayPtr, x, y);
        }

        public void CalculateRealPosition()
        {
            int x;
            int y;
            Glfw.GetWindowPos(displayPtr, out x, out y);
            position = new Vector2(x, y);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
