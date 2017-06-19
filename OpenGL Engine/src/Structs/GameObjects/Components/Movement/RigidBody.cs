using System;

namespace OpenEngine.Components
{
    public class RigidBody : Component
    {

        #region FIELDS

        private float mass;
        private Vector3 velocity;
        private Vector3 acceleration;

        #endregion

        #region CONSTRUCTORS

        public RigidBody(float mass, Vector3 velocity, Vector3 acceleration)
        {
            this.mass = mass;
            this.velocity = velocity;
            this.acceleration = acceleration;
        }

        public RigidBody(float mass) : this(mass, new Vector3(), new Vector3())
        {

        }

        public RigidBody() : this(1)
        {

        }

        #endregion

        #region PROPERTIES

        public float Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector3 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public override Component Clone()
        {
            RigidBody rb = new RigidBody();
            rb.Mass = Mass;
            rb.Velocity = Velocity;
            rb.Acceleration = Acceleration;
            return rb;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
