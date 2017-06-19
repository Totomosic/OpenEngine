using System;
using System.IO;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class OBJReader
    {

        #region FIELDS

        #endregion

        #region PUBLIC METHODS

        public static Model CreateModel(string filename, bool useModelPath = true)
        {
            string path = filename;
            if (useModelPath)
            {
                path = Paths.ModelPath + filename;
            }
            OBJFile file = LoadOBJFile(filename);
            VAO vao = new VAO(RenderMode.Arrays, file.Vertices.Length);
            vao.CreateAttribute((int)BufferLayout.Vertices, file.Vertices, 3);
            vao.CreateAttribute((int)BufferLayout.Normals, file.Normals, 3);
            vao.CreateAttribute((int)BufferLayout.TextureCoordinates, file.TextureCoords, 2);
            vao.CreateAttribute((int)BufferLayout.Color, Color.White.ToVertexData(file.Vertices.Length / 3), 4);

            return new Model(vao);
        }

        #endregion

        #region PRIVATE METHODS

        private static OBJFile LoadOBJFile(string filename)
        {
            StreamReader filestream = new StreamReader(filename);
            string line;

            List<OBJVertex> vertices = new List<OBJVertex>();
            List<OBJNormal> normals = new List<OBJNormal>();
            List<OBJTexCoord> texCoords = new List<OBJTexCoord>();
            List<OBJFace> faces = new List<OBJFace>();
            while ((line = filestream.ReadLine()) != null)
            {
                // Read over each line of the obj file

                List<string> lineData = new List<string>(line.Split(new string[] { " " }, StringSplitOptions.None));
                List<string> data = new List<string>();

                foreach (string value in lineData)
                {
                    float floatValue;
                    if (float.TryParse(value, out floatValue))
                    {
                        data.Add(value);
                    }
                }
                if (line.StartsWith("v "))
                {
                    // Line corresponds to a vertex
                    vertices.Add(new OBJVertex(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2])));
                }
                else if (line.StartsWith("vn "))
                {
                    // Line corresponds to a normal
                    normals.Add(new OBJNormal(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2])));
                }
                else if (line.StartsWith("vt "))
                {
                    // Line corresponds to a texture coordinate
                    texCoords.Add(new OBJTexCoord(float.Parse(data[0]), float.Parse(data[1])));
                }
                else if (line.StartsWith("f "))
                {
                    List<int> v = new List<int>();
                    List<int> n = new List<int>();
                    List<int> t = new List<int>();
                    foreach (string value in lineData)
                    {

                        if (value.Contains("//"))
                        {
                            // No Tex coords
                            string[] sections = value.Split(new string[] { "//" }, StringSplitOptions.None);
                            v.Add(int.Parse(sections[0]));
                            n.Add(int.Parse(sections[1]));
                        }
                        else if (value.Contains("/") && value.Split(new string[] { "/" }, StringSplitOptions.None).Length == 2)
                        {
                            // No Normals
                            string[] sections = value.Split(new string[] { "/" }, StringSplitOptions.None);
                            v.Add(int.Parse(sections[0]));
                            t.Add(int.Parse(sections[1]));
                        }
                        else if (value.Contains("/"))
                        {
                            // Everything
                            string[] sections = value.Split(new string[] { "/" }, StringSplitOptions.None);
                            v.Add(int.Parse(sections[0]));
                            t.Add(int.Parse(sections[1]));
                            n.Add(int.Parse(sections[2]));
                        }                        
                    }
                    faces.Add(new OBJFace(v.ToArray(), n.ToArray(), t.ToArray()));

                }

            }

            List<float> verts = new List<float>();
            List<float> norms = new List<float>();
            List<float> tex = new List<float>();

            foreach (OBJFace face in faces)
            {
                face.Triangulate();
                foreach (int i in face.Vertices)
                {
                    verts.AddRange(vertices[i - 1].Values);
                }
                foreach (int i in face.Normals)
                {
                    norms.AddRange(normals[i - 1].Values);
                }
                foreach (int i in face.TextureCoordinates)
                {
                    tex.AddRange(texCoords[i - 1].Values);
                }
            }
            return new OBJFile(filename, verts.ToArray(), norms.ToArray(), tex.ToArray());
        }

    }

        #endregion
}
