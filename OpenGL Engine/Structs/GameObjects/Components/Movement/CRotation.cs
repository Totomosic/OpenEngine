using System;

namespace OpenEngine.Components
{
    public class CRotation : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CRotation(float angle, Vector4 axis, Space space = Space.Local, AngleType type = AngleType.Radians)
        {
            Angle = angle;
            Axis = axis;
            Space = space;
            Type = type;
        }

        public CRotation() : this(30, World.ZAxis, Space.Local, AngleType.Degrees)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual float Angle
        {
            get; set;
        }

        public virtual Vector4 Axis
        {
            get; set;
        }

        public virtual Space Space
        {
            get; set;
        }

        public virtual AngleType Type
        {
            get; set;
        }

        #endregion

    }
}
