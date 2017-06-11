using System;

namespace OpenEngine.Components
{
    public class CModel : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CModel(Model model, Vector3 modelRotation = new Vector3(), AngleType angleType = AngleType.Degrees)
        {
            Model = model;
            ModelRotation = modelRotation;
            AngleType = angleType;
        }

        public CModel() : this(null)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Model Model
        {
            get; set;
        }

        public virtual Vector3 ModelRotation
        {
            get; set;
        }

        public virtual AngleType AngleType
        {
            get; set;
        }

        #endregion

    }
}
