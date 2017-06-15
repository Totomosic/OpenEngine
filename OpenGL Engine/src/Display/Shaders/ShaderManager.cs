using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenEngine
{
    public static class ShaderManager
    {

        #region FIELDS

        private static Dictionary<string, ShaderProgram> shaderPrograms = new Dictionary<string, ShaderProgram>();
        private static ShaderProgram currentlyActiveProgram = null;

        #endregion

        #region PROPERTIES

        public static Dictionary<string, ShaderProgram> Shaders
        {
            get { return shaderPrograms; }
        }

        public static ShaderProgram CurrentlyActiveShader
        {
            get { return currentlyActiveProgram; }
        }

        #endregion

        #region PUBLIC METHODS

        public static ShaderProgram GetShader(string shaderName)
        {
            return shaderPrograms[shaderName];
        }

        public static ShaderProgram[] GetAllShaders()
        {
            return shaderPrograms.Values.ToArray();
        }

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

        public static void SetAsActive(ShaderProgram shader)
        {
            currentlyActiveProgram = shader;
        }

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

        public static void Clear()
        {
            currentlyActiveProgram = null;
            shaderPrograms.Clear();
        }

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
