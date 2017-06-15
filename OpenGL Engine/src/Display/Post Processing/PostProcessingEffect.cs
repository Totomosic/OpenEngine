using System;

namespace OpenEngine
{
    [Serializable]
    public class PostProcessingEffect
    {

        #region FIELDS

        private ShaderProgram shader;
        private string name;
        private string desc;

        #endregion

        #region CONSTRUCTORS

        public PostProcessingEffect(string name, ShaderProgram program, string desc = "")
        {
            this.name = name;
            shader = program;
            this.desc = desc;
        }

        #endregion

        #region PROPERTIES

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Description
        {
            get { return desc; }
            set { desc = value; }
        }

        public virtual ShaderProgram ShaderProgram
        {
            get { return shader; }
            set { shader = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
