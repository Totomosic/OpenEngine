using System;

namespace OpenEngine.Components
{
    public class MeshColor : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public MeshColor(Color color, Color secondaryColor = new Color())
        {
            Color = color;
            SecondaryColor = secondaryColor;
        }

        public MeshColor() : this(Color.White)
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

        public override Component Clone()
        {
            MeshColor color = new MeshColor();
            color.Color = Color;
            color.SecondaryColor = SecondaryColor;
            return color;
        }

    }
}
