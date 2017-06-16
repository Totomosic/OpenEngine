using System;

namespace OpenEngine.Components
{
    public class Shader : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public Shader(ShaderProgram program)
        {
            Program = program;
        }

        public Shader(string shaderName) : this(ShaderManager.GetShader(shaderName))
        {

        }

        public Shader() : this(Engine.Shader)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual ShaderProgram Program
        {
            get; set;
        }

        public override Component Clone()
        {
            Shader shader = new Shader();
            shader.Program = Program;
            return shader;
        }

        #endregion

    }
}
