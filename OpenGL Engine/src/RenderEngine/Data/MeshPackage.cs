using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class MeshPackage
    {

        #region FIELDS

        private Model model;
        private MeshConfig config;
        private Material material;

        #endregion

        #region CONSTRUCTORS

        public MeshPackage(Model model, MeshConfig config, Material material)
        {
            this.model = model;
            this.config = config;
            this.material = material;
        }

        #endregion

        #region PROPERTIES

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public MeshConfig Config
        {
            get { return config; }
            set { config = value; }
        }

        public Material Material
        {
            get { return material; }
            set { material = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
