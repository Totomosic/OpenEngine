using System;
using Pencil.Gaming;

namespace OpenEngine.Components
{
    public class CKeyboardRotation : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CKeyboardRotation(float angle, AngleType angleType = AngleType.Radians, Space xSpace = Space.Local, Space ySpace = Space.World, Key rightkey = Key.Right, Key leftKey = Key.Left, Key upKey = Key.Up, Key downKey = Key.Down)
        {
            Angle = angle;
            AngleType = angleType;
            XSpace = xSpace;
            YSpace = ySpace;
            RightKey = rightkey;
            LeftKey = leftKey;
            UpKey = upKey;
            DownKey = downKey;
        }

        public CKeyboardRotation() : this(60, AngleType.Degrees)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual float Angle
        {
            get; set;
        }

        public virtual AngleType AngleType
        {
            get; set;
        }

        public virtual Space XSpace
        {
            get; set;
        }

        public virtual Space YSpace
        {
            get; set;
        }

        public virtual Key RightKey
        {
            get; set;
        }

        public virtual Key LeftKey
        {
            get; set;
        }

        public virtual Key UpKey
        {
            get; set;
        }

        public virtual Key DownKey
        {
            get; set;
        }

        #endregion

    }
}
