using System;

using Pencil.Gaming;

namespace OpenEngine
{
    public class ButtonState
    {

        #region FIELDS

        private Window context;
        private bool mouse1;
        private bool mouse2;
        private bool mouse3;

        #endregion

        #region CONSTRUCTORS

        public ButtonState(Window window)
        {
            context = window;
            Update();
        }

        #endregion

        #region PROPERTIES

        public bool LeftButton
        {
            get { return mouse1; }
        }

        public bool MiddleButton
        {
            get { return mouse2; }
        }

        public bool RightButton
        {
            get { return mouse3; }
        }

        #endregion

        #region PUBLIC METHODS

        public void Update()
        {
            mouse1 = Glfw.GetMouseButton(context.WindowPtr, MouseButton.LeftButton);
            mouse2 = Glfw.GetMouseButton(context.WindowPtr, MouseButton.MiddleButton);
            mouse3 = Glfw.GetMouseButton(context.WindowPtr, MouseButton.RightButton);
        }

        #endregion

        #region PRIVATE METHODS 

        #endregion

    }
}
