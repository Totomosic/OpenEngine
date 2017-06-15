using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class Ellipse
    {

        public static Model CreateModel(float w, float h, Color color, SubImage image = null, float lod = 60)
        {
            return CreateModel(new Vector2(w, h), color, image, lod);
        }

        public static Model CreateModel(Vector2 Size, Color color, SubImage subImage = null, float lod = 60)
        {
            float w = Size.X;
            float h = Size.Y;
            List<float> vertices = new List<float>();
            List<float> normals = new List<float>();
            List<float> tex = new List<float>();
            List<uint> indices = new List<uint>();
            float[] texArray;
            if (subImage != null)
            {
                texArray = subImage.GetTexCoordArray();
            }
            else
            {
                texArray = new float[] { 0, 1, 0, 0, 1, 0, 1, 1 };
            }
            float minX = texArray[0];
            float maxX = texArray[6];
            float minY = texArray[3];
            float maxY = texArray[7];

            vertices.Add(0.0f);
            vertices.Add(0.0f);
            vertices.Add(0.0f);

            normals.Add(0.0f);
            normals.Add(0.0f);
            normals.Add(1.0f);

            tex.Add(Utilities.Map(0.5f, 0, 1, minX, maxX));
            tex.Add(Utilities.Map(0.5f, 0, 1, minY, maxY));

            for (float i = 0; i <= 360.0f; i += 360.0f / lod)
            {
                float r = (((w / 2) * (h / 2)) / (float)Math.Sqrt(Math.Pow(h / 2 * Math.Cos(Angles.ToRadians(i)), 2) + Math.Pow(w / 2 * Math.Sin(Angles.ToRadians(i)), 2)));                

                vertices.Add((float)Math.Cos((double)Angles.ToRadians(i)) * r);
                vertices.Add((float)Math.Sin((double)Angles.ToRadians(i)) * r);
                vertices.Add(0.0f);                

                normals.Add(0.0f);
                normals.Add(0.0f);
                normals.Add(1.0f);              

                tex.Add(Utilities.Map((float)Math.Cos(Angles.ToRadians(i)) * 0.5f + 0.5f, 0, 1, minX, maxX));
                tex.Add(Utilities.Map((float)Math.Sin(Angles.ToRadians(i)) * 0.5f + 0.5f, 0, 1, minY, maxY));
            }

            for (int i = 1; i <= lod; i++)
            {
                indices.Add(0);
                indices.Add((uint)i);
                indices.Add((uint)i + 1);
            }

            Model model = ContentManager.LoadModel(vertices.ToArray(), normals.ToArray(), tex.ToArray(), color, indices.ToArray(), 3);
            model.CalculateSize();
            return model;
        }


    }
}
