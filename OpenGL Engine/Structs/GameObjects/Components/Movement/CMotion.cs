using System;

namespace OpenEngine.Components
{
    public class CMotion : Component
    {

        #region FIELDS

        private Vector3 velocity;
        private Vector3 acceleration;

        #endregion

        #region CONSTRUCTORS

        public CMotion(Vector3 velocity = new Vector3(), Vector3 acceleration = new Vector3())
        {
            this.velocity = velocity;
            this.acceleration = acceleration;
        }

        public CMotion() : this(new Vector3(), new Vector3())
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public virtual Vector3 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        #endregion

    }
}
