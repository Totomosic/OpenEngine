using System;
using Pencil.Gaming;

namespace OpenEngine.Components
{
    public class CKeyboardMotion : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CKeyboardMotion(float speed, Key forwardKey = Key.W, Key backwardKey = Key.S, Key rightKey = Key.D, Key leftKey = Key.A, Key upKey = Key.Space, Key downKey = Key.LeftShift)
        {

            Speed = speed;

            ForwardKey = forwardKey;
            BackwardKey = backwardKey;
            RightKey = rightKey;
            LeftKey = leftKey;
            UpKey = upKey;
            DownKey = downKey;

            ForwardBackEnabled = true;
            LeftRightEnabled = true;
            UpDownEnabled = true;
        }

        public CKeyboardMotion(float speed, bool forwardBack, bool leftRight = true, bool upDown = true) : this(speed)
        {
            ForwardBackEnabled = forwardBack;
            LeftRightEnabled = leftRight;
            UpDownEnabled = upDown;
        }

        public CKeyboardMotion() : this(50)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual float Speed
        {
            get; set;
        }

        public virtual Key ForwardKey
        {
            get; set;
        }

        public virtual Key BackwardKey
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

        public virtual bool ForwardBackEnabled
        {
            get; set;
        }

        public virtual bool LeftRightEnabled
        {
            get; set;
        }

        public virtual bool UpDownEnabled
        {
            get; set;
        }

        #endregion

    }
}
