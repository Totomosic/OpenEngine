using System;

namespace OpenEngine.Components
{
    public class CMotionEngine : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CMotionEngine(Vector3 velocity)
        {
            Velocity = velocity;
        }

        public CMotionEngine() : this(new Vector3())
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Vector3 Velocity
        {
            get; set;
        }

        #endregion

    }
}
