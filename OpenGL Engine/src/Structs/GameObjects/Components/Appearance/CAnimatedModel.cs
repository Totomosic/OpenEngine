using System;

namespace OpenEngine.Components
{
    public class CAnimatedModel : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CAnimatedModel(AnimatedModel model)
        {
            Model = model;
        }

        public CAnimatedModel() : this(null)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual AnimatedModel Model
        {
            get; set;
        }

        #endregion

    }
}
