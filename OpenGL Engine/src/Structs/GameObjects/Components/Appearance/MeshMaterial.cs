using System;

namespace OpenEngine.Components
{
    public class MeshMaterial : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public MeshMaterial(Material material)
        {
            Material = material;
        }

        public MeshMaterial() : this(new Material())
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Material Material
        {
            get; set;
        }

        #endregion

        public override Component Clone()
        {
            MeshMaterial material = new MeshMaterial();
            material.Material = Material;
            return material;
        }

    }
}
