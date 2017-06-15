using System;
using System.Collections.Generic;

namespace OpenEngine
{
    class OBJFace
    {

        protected List<int> vertexIndices;
        protected List<int> normalIndices;
        protected List<int> texIndices;

        public OBJFace(int[] verts, int[] normals, int[] tex)
        {
            vertexIndices = new List<int>(verts);
            normalIndices = new List<int>(normals);
            texIndices = new List<int>(tex);
        }

        public int[] Vertices
        {
            get { return vertexIndices.ToArray(); }
        }

        public int[] Normals
        {
            get { return normalIndices.ToArray(); }
        }

        public int[] TextureCoordinates
        {
            get { return texIndices.ToArray(); }
        }

        public void Triangulate()
        {
            if (vertexIndices.Count == 4)
            {
                int[] newVerts = { vertexIndices[0], vertexIndices[1], vertexIndices[2], vertexIndices[0], vertexIndices[2], vertexIndices[3] };
                vertexIndices = new List<int>(newVerts);
            }

            if (normalIndices.Count == 4)
            {
                int[] newNorms = { normalIndices[0], normalIndices[1], normalIndices[2], normalIndices[0], normalIndices[2], normalIndices[3] };
                normalIndices = new List<int>(newNorms);
            }

            if (texIndices.Count == 4)
            {
                int[] newTex = { texIndices[0], texIndices[1], texIndices[2], texIndices[0], texIndices[2], texIndices[3] };
                texIndices = new List<int>(newTex);
            }
        }

    }
}
