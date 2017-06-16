using System;

namespace OpenEngine.Components
{
    public class AnimatedMesh : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public AnimatedMesh(AnimatedModel model)
        {
            Model = model;
        }

        public AnimatedMesh() : this(null)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual AnimatedModel Model
        {
            get; set;
        }

        public override Component Clone()
        {
            AnimatedMesh model = new AnimatedMesh();
            model.Model = Model;
            return model;
        }

        #endregion

    }
}
