using System;

namespace OpenEngine.Components
{
    public class CMaterial : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CMaterial(Material material)
        {
            Material = material;
        }

        public CMaterial() : this(new Material())
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Material Material
        {
            get; set;
        }

        #endregion

    }
}
