using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Hexagon
    {

        public static Model CreateModel(float radius, Color color, bool pointAtTop = false)
        {
            List<float> vertices = new List<float>();
            List<float> normals = new List<float>();
            List<float> texCoords = new List<float>();
            List<uint> indices = new List<uint>();

            vertices.Add(0);
            vertices.Add(0);
            vertices.Add(0);

            normals.Add(0);
            normals.Add(1);
            normals.Add(0);

            texCoords.Add(0.5f);
            texCoords.Add(0.5f);

            if (!pointAtTop)
            {
                for (float angle = 0; angle < 360; angle += 60)
                {
                    vertices.Add(radius * (float)Math.Cos(Angles.ToRadians(angle)));
                    vertices.Add(0);
                    vertices.Add(radius * (float)Math.Sin(Angles.ToRadians(angle)));

                    normals.Add(0);
                    normals.Add(1);
                    normals.Add(0);

                    texCoords.Add((float)Math.Cos(Angles.ToRadians(angle)));
                    texCoords.Add((float)Math.Sin(Angles.ToRadians(angle)));
                }
            }
            else
            {
                for (float angle = 30; angle < 390; angle += 60)
                {
                    vertices.Add(radius * (float)Math.Cos(Angles.ToRadians(angle)));
                    vertices.Add(radius * (float)Math.Sin(Angles.ToRadians(angle)));
                    vertices.Add(0);

                    normals.Add(0);
                    normals.Add(1);
                    normals.Add(0);

                    texCoords.Add((float)Math.Cos(Angles.ToRadians(angle)));
                    texCoords.Add((float)Math.Sin(Angles.ToRadians(angle)));
                }
            }

            indices.AddRange(new uint[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 1 });
            Model model = ContentManager.LoadModel(vertices.ToArray(), normals.ToArray(), texCoords.ToArray(), color, indices.ToArray());
            return model;
        }

    }
}
