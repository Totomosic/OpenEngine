using System;
using System.Collections.Generic;

namespace OpenEngine
{
    /// <summary>
    /// Interface that defines something as renderable
    /// </summary>
    public interface IRenderable
    {

        /// <summary>
        /// Matrix that defines position and orientation of model in worldspace
        /// </summary>
        Matrix4 ModelMatrix
        {
            get;
        }

        /// <summary>
        /// Model to be rendered
        /// </summary>
        Model Model
        {
            get;
        }

        /// <summary>
        /// Material applied to model
        /// </summary>
        Material Material
        {
            get;
        }

        /// <summary>
        /// ShaderProgram to render model with
        /// </summary>
        ShaderProgram ShaderProgram
        {
            get;
        }



    }
}
