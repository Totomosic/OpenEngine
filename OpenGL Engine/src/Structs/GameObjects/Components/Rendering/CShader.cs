using System;

namespace OpenEngine.Components
{
    public class CShader : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CShader(ShaderProgram program)
        {
            Program = program;
        }

        public CShader(string shaderName) : this(ShaderManager.GetShader(shaderName))
        {

        }

        public CShader() : this(Engine.Shader)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual ShaderProgram Program
        {
            get; set;
        }

        #endregion

    }
}
