using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class CollisionResolution
    {

        #region FIELDS

        private bool collided;
        private float collisionDistance;

        private bool x;
        private bool y;
        private bool z;

        private object state;

        #endregion

        #region CONSTRUCTORS

        public CollisionResolution(bool didCollide, float distance = 0, bool xCollided = false, bool yCollided = false, bool zCollided = false)
        {
            collided = didCollide;
            collisionDistance = distance;

            x = xCollided;
            y = yCollided;
            z = zCollided;

            state = null;
        }

        #endregion

        #region PROPERTIES

        public virtual object State
        {
            get { return state; }
            set { state = value; }
        }

        public virtual bool Collided
        {
            get { return collided; }
            set { collided = value; }
        }

        public virtual float CollideDistance
        {
            get { return collisionDistance; }
            set { collisionDistance = value; }
        }

        public virtual bool XCollided
        {
            get { return x; }
            set { x = value; }
        }

        public virtual bool YCollided
        {
            get { return y; }
            set { y = value; }
        }

        public virtual bool ZCollided
        {
            get { return z; }
            set { z = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
