using System;

namespace OpenEngine.Components
{
    public class CColor : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CColor(Color color, Color secondaryColor = new Color())
        {
            Color = color;
            SecondaryColor = secondaryColor;
        }

        public CColor() : this(Color.White)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Color Color
        {
            get; set;
        }

        public virtual Color SecondaryColor
        {
            get; set;
        }

        #endregion

    }
}
