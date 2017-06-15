using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Ray
    {

        #region FIELDS

        private Vector3 origin;
        private Vector3 unitDirection;
        private float length;

        #endregion

        #region CONSTRUCTORS

        public Ray(Vector3 position, Vector3 direction, float rayLength)
        {
            origin = position;
            unitDirection = direction.Normalize();
            length = rayLength;
        }

        #endregion

        #region PROPERTIES

        public Vector3 Position
        {
            get { return origin; }
            protected set { origin = value; }
        }

        public Vector3 Direction
        {
            get { return unitDirection; }
            protected set { unitDirection = value; }
        }

        public float Length
        {
            get { return length; }
            set { length = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
