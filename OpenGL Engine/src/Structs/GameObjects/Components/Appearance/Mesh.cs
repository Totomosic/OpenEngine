using System;

namespace OpenEngine.Components
{
    public class Mesh : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public Mesh(Model model, Vector3 modelRotation = new Vector3(), AngleType angleType = AngleType.Degrees)
        {
            Model = model;
            ModelRotation = modelRotation;
            AngleType = angleType;
        }

        public Mesh() : this(null)
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

        public override Component Clone()
        {
            Mesh model = new Mesh();
            model.Model = Model;
            model.ModelRotation = ModelRotation;
            model.AngleType = AngleType;
            ResourceManager.FetchReference(model.Model);
            return model;
        }

    }
}
