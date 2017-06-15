using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class ModelPackage
    {

        #region FIELDS

        private Model model;
        private ModelConfig config;

        #endregion

        #region CONSTRUCTORS

        public ModelPackage(Model model, ModelConfig config)
        {
            this.model = model;
            this.config = config;
        }

        #endregion

        #region PROPERTIES

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public ModelConfig Config
        {
            get { return config; }
            set { config = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
