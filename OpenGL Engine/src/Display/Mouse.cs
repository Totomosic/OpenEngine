using System;
using Pencil.Gaming;

namespace OpenEngine
{
    /// <summary>
    /// Class that represents the cursor
    /// </summary>
    public class Mouse
    {

        #region FIELDS

        private Window window;
        private Vector3 position;
        private ButtonState state;
        private Vector3 prevPosition;
        private Vector3 relPosition;
        private Vector2 relScroll;

        private bool shown;
        private bool captured;

        #endregion

        #region CONSTRUCTORS

        public Mouse(Window context)
        {
            window = context;
            position = new Vector3(GetMousePos(), 0);
            state = new ButtonState(window);
            prevPosition = position;
            relPosition = new Vector3();
            relScroll = new Vector2();

            shown = true;
            captured = false;
        }

        #endregion

        #region PROPERTIES

        public bool Shown
        {
            get { return shown; }
        }

        public bool Captured
        {
            get { return captured; }
        }

        /// <summary>
        /// Get the position specified from the top left corner
        /// </summary>
        public Vector3 Position
        {
            get { return position; }
        }

        /// <summary>
        /// Get the position specified from the bottom left corner
        /// </summary>
        public Vector2 GLPosition
        {
            get { return new Vector2(position.X, window.Height - position.Y); }
        }

        /// <summary>
        /// Get the position in NDC coordinates (-1 to 1)
        /// </summary>
        public Vector2 NormalisedPosition
        {
            get { return GLPosition / window.Resolution * 2 - 1; }
        }

        /// <summary>
        /// State of mouse buttons
        /// </summary>
        public ButtonState State
        {
            get { return state; }
        }

        /// <summary>
        /// Position moved during this frame
        /// </summary>
        public Vector3 RelativePosition
        {
            get { return relPosition; }
        }

        /// <summary>
        /// Amount scrolled this frame in an x and y component
        /// </summary>
        public Vector2 RelativeScroll
        {
            get { return relScroll; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Update position of mouse
        /// </summary>
        public void Update()
        {
            position = new Vector3(GetMousePos(), 0);
            state.Update();
            relPosition = position - prevPosition;
            prevPosition = position;

            bool foundEvent = false;
            foreach (Event e in window.GetEvents())
            {
                if (e.Type == EventType.MouseScroll)
                {
                    relScroll = new Vector2(e.XScroll, e.YScroll);
                    foundEvent = true;
                }
            }
            if (!foundEvent)
            {
                relScroll = new Vector2();
            }

        }

        /// <summary>
        /// Hide the cursor and keep inside the window
        /// </summary>
        public void Capture()
        {
            Glfw.SetInputMode(window.WindowPtr, InputMode.CursorMode, CursorMode.CursorCaptured);
            captured = true;
        }

        /// <summary>
        /// Show and release the cursor
        /// </summary>
        public void Uncapture()
        {
            Glfw.SetInputMode(window.WindowPtr, InputMode.CursorMode, CursorMode.CursorNormal);
            if (!shown)
            {
                Hide();
            }
            captured = false;
        }

        /// <summary>
        /// Show cursor
        /// </summary>
        public void Show()
        {
            Glfw.SetInputMode(window.WindowPtr, InputMode.CursorMode, CursorMode.CursorNormal);
            if (captured)
            {
                Capture();
            }
            shown = true;
        }

        /// <summary>
        /// Hide cursor
        /// </summary>
        public void Hide()
        {
            Glfw.SetInputMode(window.WindowPtr, InputMode.CursorMode, CursorMode.CursorHidden);
            shown = false;
        }

        /// <summary>
        /// Gets position in NDC coordinates
        /// </summary>
        /// <returns></returns>
        public Vector2 GetNormalisedPosition()
        {
            return new Vector2(Position.X / window.Width * 2f - 1f, GLPosition.Y / window.Height * 2f - 1f);
        }

        /// <summary>
        /// Gets position from center eg. (0, 0) is center of screen
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCenteredPosition()
        {
            return new Vector2(Position.X - window.Width * 0.5f, GLPosition.Y - window.Height * 0.5f);
        }

        #endregion

        #region PRIVATE METHODS

        private Vector2 GetMousePos()
        {
            double x = 0;
            double y = 0;
            Glfw.GetCursorPos(window.WindowPtr, out x, out y);
            return new Vector2((float)x, (float)y);
        }

        #endregion

    }
}
