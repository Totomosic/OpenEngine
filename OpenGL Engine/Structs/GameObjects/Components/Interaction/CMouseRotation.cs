using System;

namespace OpenEngine.Components
{
    public class CMouseRotation : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CMouseRotation(float xSense, float ySense, Space xSpace = Space.Local, Space ySpace = Space.World)
        {
            XSensitivity = xSense;
            YSensitivity = ySense;
            XSpace = xSpace;
            YSpace = ySpace;
        }

        public CMouseRotation() : this(0.1f, 0.1f)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual float XSensitivity
        {
            get; set;
        }

        public virtual float YSensitivity
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

        #endregion

    }
}
