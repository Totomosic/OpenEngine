using System;

namespace OpenEngine.Components
{
    public class CCameraReference : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CCameraReference(uint camera)
        {
            ID = camera;
        }

        public CCameraReference() : this(0)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual uint ID
        {
            get; set;
        }

        #endregion

    }
}
