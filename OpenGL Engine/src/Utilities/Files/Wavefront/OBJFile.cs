using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class OBJFile : DataFile
    {

        #region FIELDS

        private List<float> vertices;
        private List<float> normals;
        private List<float> texCoords;

        #endregion

        #region CONSTRUCTORS

        public OBJFile(string path, float[] vertices, float[] normals, float[] texCoords) : base(path)
        {
            this.vertices = new List<float>(vertices);
            this.normals = new List<float>(normals);
            this.texCoords = new List<float>(texCoords);
        }

        #endregion

        #region PROPERTIES

        public float[] Vertices
        {
            get { return vertices.ToArray(); }
            set { vertices = new List<float>(value); }
        }

        public float[] Normals
        {
            get { return normals.ToArray(); }
            set { normals = new List<float>(value); }
        }

        public float[] TextureCoords
        {
            get { return texCoords.ToArray(); }
            set { texCoords = new List<float>(value); }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
