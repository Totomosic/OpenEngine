using System;

namespace OpenEngine.Components
{
    public class CAnimatedTexture : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CAnimatedTexture(AnimatedTexture texture, int bindUnit = 0)
        {
            Texture = texture;
            BindUnit = bindUnit;
        }

        public CAnimatedTexture() : this(null)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual int BindUnit
        {
            get; set;
        }

        public virtual AnimatedTexture Texture
        {
            get; set;
        }

        #endregion

    }
}
