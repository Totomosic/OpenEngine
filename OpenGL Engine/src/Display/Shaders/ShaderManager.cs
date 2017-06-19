using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenEngine
{
    /// <summary>
    /// Class that manages all shader programs
    /// </summary>
    public static class ShaderManager
    {

        #region FIELDS

        private static Dictionary<string, ShaderProgram> shaderPrograms = new Dictionary<string, ShaderProgram>();
        private static ShaderProgram currentlyActiveProgram = null;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Get a dictionary containing all shaders and their associated names
        /// </summary>
        public static Dictionary<string, ShaderProgram> Shaders
        {
            get { return shaderPrograms; }
        }

        /// <summary>
        /// Get the currently bound shader program
        /// </summary>
        public static ShaderProgram CurrentlyActiveShader
        {
            get { return currentlyActiveProgram; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Request shader from name
        /// </summary>
        /// <param name="shaderName">Name of shader</param>
        /// <returns></returns>
        public static ShaderProgram GetShader(string shaderName)
        {
            return shaderPrograms[shaderName];
        }

        /// <summary>
        /// Get all shaders
        /// </summary>
        /// <returns></returns>
        public static ShaderProgram[] GetAllShaders()
        {
            return shaderPrograms.Values.ToArray();
        }

        /// <summary>
        /// Add shader program, should not be called from client code
        /// </summary>
        /// <param name="shader">Shader to be added</param>
        public static void AddShader(ShaderProgram shader)
        {
            if (!shaderPrograms.ContainsKey(shader.Name))
            {
                shaderPrograms[shader.Name] = shader;
            }
            else
            {
                throw new EngineException("Shader with name: " + shader.Name + " already exists");
            }
        }

        /// <summary>
        /// Sets the given shader as currently bound, should not be called from client code
        /// </summary>
        /// <param name="shader">Shader to set as active</param>
        public static void SetAsActive(ShaderProgram shader)
        {
            currentlyActiveProgram = shader;
        }

        /// <summary>
        /// Deactivate shader
        /// </summary>
        /// <param name="shader">Shader to deactivate</param>
        public static void SetAsInactive(ShaderProgram shader)
        {
            if (currentlyActiveProgram == shader)
            {
                currentlyActiveProgram = null;
            }
            else
            {
                throw new ShaderManagementException("Either no shader was active or this shader (" + shader.Name + ") was not active.");
            }
        }

        /// <summary>
        /// Clear all shaders
        /// </summary>
        public static void Clear()
        {
            currentlyActiveProgram = null;
            shaderPrograms.Clear();
        }

        /// <summary>
        /// Clear all pending requests to modify shader uniform variables
        /// </summary>
        public static void ClearAllRequests()
        {
            foreach (ShaderProgram shader in GetAllShaders())
            {
                shader.ClearRequests();
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
