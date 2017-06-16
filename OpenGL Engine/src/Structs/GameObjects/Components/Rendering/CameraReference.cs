using System;

namespace OpenEngine.Components
{
    public class CameraReference : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CameraReference(GameObject camera)
        {
            ID = camera;
        }

        public CameraReference() : this(null)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual GameObject ID
        {
            get; set;
        }

        #endregion

        public override Component Clone()
        {
            CameraReference camera = new CameraReference();
            camera.ID = ID;
            return camera;
        }

    }
}
