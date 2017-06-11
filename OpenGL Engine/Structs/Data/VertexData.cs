using System;

namespace OpenEngine
{
    public class VertexData
    {

        #region FIELDS

        private float[] vertexPositions;
        private float[] vertexNormals;
        private float[] vertexTexCoords;
        private float[] vertexColors;

        #endregion

        #region CONSTRUCTORS

        public VertexData(Vertex[] vertices)
        {
            GetArrays(vertices);
        }

        #endregion

        #region PROPERTIES

        public float[] Positions
        {
            get { return vertexPositions; }
        }

        public float[] Normals
        {
            get { return vertexNormals; }
        }

        public float[] TextureCoordinates
        {
            get { return vertexTexCoords; }
        }

        public float[] Colors
        {
            get { return vertexColors; }
        }

        #endregion

        #region PRIVATE METHODS

        private void GetArrays(Vertex[] vertices)
        {
            vertexPositions = new float[vertices.Length * 3];
            vertexNormals = new float[vertices.Length * 3];
            vertexTexCoords = new float[vertices.Length * 2];
            vertexColors = new float[vertices.Length * 4]; 
            for (int i = 0; i < vertices.Length; i++)
            {
                vertexPositions[i * 3] = vertices[i].Position.X;
                vertexPositions[i * 3 + 1] = vertices[i].Position.Y;
                vertexPositions[i * 3 + 2] = vertices[i].Position.Z;

                vertexNormals[i * 3] = vertices[i].Normal.X;
                vertexNormals[i * 3 + 1] = vertices[i].Normal.Y;
                vertexNormals[i * 3 + 2] = vertices[i].Normal.Z;

                vertexTexCoords[i * 2] = vertices[i].TexCoord.X;
                vertexTexCoords[i * 2 + 1] = vertices[i].TexCoord.Y;

                vertexColors[i * 4] = vertices[i].Color.NR;
                vertexColors[i * 4 + 1] = vertices[i].Color.NG;
                vertexColors[i * 4 + 2] = vertices[i].Color.NB;
                vertexColors[i * 4 + 3] = vertices[i].Color.NA;
            }
        }

        #endregion

    }
}
