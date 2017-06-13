using System;

namespace OpenEngine.Components
{
    public class CCameraReference : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CCameraReference(GameObject camera)
        {
            ID = camera;
        }

        public CCameraReference() : this(null)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual GameObject ID
        {
            get; set;
        }

        #endregion

    }
}
