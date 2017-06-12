using System;
using Pencil.Gaming;

namespace OpenEngine
{
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

        public Vector3 Position
        {
            get { return position; }
        }

        public Vector2 GLPosition
        {
            get { return new Vector2(position.X, window.Height - position.Y); }
        }

        public Vector2 NormalisedPosition
        {
            get { return GLPosition / window.Resolution * 2 - 1; }
        }

        public ButtonState State
        {
            get { return state; }
        }

        public Vector3 RelativePosition
        {
            get { return relPosition; }
        }

        public Vector2 RelativeScroll
        {
            get { return relScroll; }
        }

        #endregion

        #region PUBLIC METHODS

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

        public void Capture()
        {
            Glfw.SetInputMode(window.WindowPtr, InputMode.CursorMode, CursorMode.CursorCaptured);
            captured = true;
        }

        public void Uncapture()
        {
            Glfw.SetInputMode(window.WindowPtr, InputMode.CursorMode, CursorMode.CursorNormal);
            if (!shown)
            {
                Hide();
            }
            captured = false;
        }

        public void Show()
        {
            Glfw.SetInputMode(window.WindowPtr, InputMode.CursorMode, CursorMode.CursorNormal);
            if (captured)
            {
                Capture();
            }
            shown = true;
        }

        public void Hide()
        {
            Glfw.SetInputMode(window.WindowPtr, InputMode.CursorMode, CursorMode.CursorHidden);
            shown = false;
        }

        public Vector2 GetNormalisedPosition()
        {
            return new Vector2(Position.X / window.Width * 2f - 1f, GLPosition.Y / window.Height * 2f - 1f);
        }

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
