using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class ShaderData : DataObject
    {

        #region FIELDS

        private string modelMatrix;
        private string viewMatrix;
        private string projectionMatrix;

        private List<string> samplers;
        private List<string> uniformNames;

        #endregion

        #region CONSTRUCTORS

        public ShaderData(string mMatrix, string vMatrix, string pMatrix, string[] textureSamplers, string[] uniforms)
        {
            modelMatrix = mMatrix;
            viewMatrix = vMatrix;
            projectionMatrix = pMatrix;

            samplers = new List<string>(textureSamplers);
            uniformNames = new List<string>(uniforms);
        }

        #endregion

        #region PROPERTIES

        public string ModelMatrix
        {
            get { return modelMatrix; }
            set { modelMatrix = value; }
        }

        public string ViewMatrix
        {
            get { return viewMatrix; }
            set { viewMatrix = value; }
        }

        public string ProjectionMatrix
        {
            get { return projectionMatrix; }
            set { projectionMatrix = value; }
        }

        public List<string> UniformNames
        {
            get { return uniformNames; }
            set { uniformNames = value; }
        }

        public List<string> TextureSamplers
        {
            get { return samplers; }
            set { samplers = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public bool ContainsUniform(string varname)
        {
            return uniformNames.Contains(varname);
        }

        public string GetSampler(int index = 0)
        {
            return samplers[index];
        }

        public int SamplerCount()
        {
            return samplers.Count;
        }

        public void AddUniform(string varname)
        {
            uniformNames.Add(varname);
        }

        #endregion

    }
}
