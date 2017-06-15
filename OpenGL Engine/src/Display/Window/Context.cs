using System;

namespace OpenEngine
{
    public static class Context
    {

        #region FIELDS

        private static bool active = false;

        #endregion

        #region PROPERTIES

        public static bool Active
        {
            get { return active; }
            private set { active = value; }
        }

        public static Window Window
        {
            get; set;
        }

        #endregion

        #region PUBLIC METHODS

        public static void Initialise(Window window)
        {
            Window = window;
            Active = true;
        }

        public static void Destroy()
        {
            Active = false;
        }

        public static Vector2 Convert(Vector2 coordinate, CoordinateType format, CoordinateType desired)
        {
            return FromNDC(ToNDC(coordinate, format), desired);
        }

        public static Vector2 ToNDC(Vector2 coordinate, CoordinateType format)
        {
            if (format == CoordinateType.NDC)
            {
                return coordinate;
            }
            else if (format == CoordinateType.Orthographic)
            {
                return coordinate / Window.Resolution * 2;
            }
            else if (format == CoordinateType.TopLeft)
            {
                Vector2 coords = coordinate / Window.Resolution;
                return (coords * 2 - 1f) * new Vector2(1, -1);
            }
            else // bottom left
            {
                Vector2 coords = coordinate / Window.Resolution;
                return (coords * 2 - 1f);
            }
        }

        public static Vector2 FromNDC(Vector2 coordinate, CoordinateType format)
        {
            if (format == CoordinateType.NDC)
            {
                return coordinate;
            }
            else if (format == CoordinateType.Orthographic)
            {
                return coordinate * Window.Resolution / 2f;
            }
            else if (format == CoordinateType.TopLeft)
            {
                Vector2 coords = coordinate * Window.Resolution / 2f;
                return (coords + Window.Resolution) * new Vector2(1, -1);
            }
            else // bottom left
            {
                Vector2 coords = coordinate * Window.Resolution / 2f;
                return (coords + Window.Resolution);
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
