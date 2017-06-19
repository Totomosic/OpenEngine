using System;
using System.IO;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class OBJWriter
    {

        public static void Write(Model model, string filename, bool useModelPath = true)
        {
            if (useModelPath)
            {
                filename = Paths.ModelPath + filename;
            }
            FloatVBO ve = model.VAO.GetBuffer((int)BufferLayout.Vertices);
            FloatVBO no = model.VAO.GetBuffer((int)BufferLayout.Normals);
            FloatVBO te = model.VAO.GetBuffer((int)BufferLayout.TextureCoordinates);
            float[] vertices = ve.DownloadData();
            float[] normals = no.DownloadData();
            float[] tex = te.DownloadData();
            int vertexDimension = model.GetVertexDimension();

            using (StreamWriter writer = new StreamWriter(filename, false))
            {

                for (int i = 0; i < vertices.Length; i += vertexDimension)
                {
                    if (vertexDimension == 2)
                    {
                        writer.Write("v " + vertices[i] + " " + vertices[i + 1] + " 0");
                        writer.WriteLine();
                    }
                    else if (vertexDimension == 3)
                    {
                        writer.Write("v " + vertices[i] + " " + vertices[i + 1] + " " + vertices[i + 2]);
                        writer.WriteLine();
                    }
                }

                writer.WriteLine();

                for (int i = 0; i < tex.Length; i += 2)
                {
                    writer.Write("vt " + tex[i] + " " + tex[i + 1]);
                    writer.WriteLine();
                }

                writer.WriteLine();

                for (int i = 0; i < normals.Length; i += 3)
                {
                    writer.Write("vn " + normals[i] + " " + normals[i + 1] + " " + normals[i + 2]);
                    writer.WriteLine();
                }

                writer.WriteLine();

                for (int i = 1; i <= vertices.Length / vertexDimension; i += 3)
                {
                    writer.Write("f " + i + "/" + i + "/" + i + " " + (i + 1) + "/" + (i + 1) + "/" + (i + 1) + " " + (i + 2) + "/" + (i + 2) + "/" + (i + 2) + " ");
                    writer.WriteLine();
                }

            }
        }

    }
}
