using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class Sphere
    {

        public static Model CreateModel(float radius, Color color, int space = 10)
        {
            List<float> verts = new List<float>();
            List<float> norms = new List<float>();
            List<float> tex = new List<float>();
            List<uint> indices = new List<uint>();
            uint currentVertex = 0;

            for (float b = 0; b <= 180; b += space)
            {
                for (float a = 0; a <= 360 - space; a += space)
                {
                    Vector3 vertex = new Vector3((float)(radius * Math.Sin(Angles.ToRadians(a)) * Math.Sin(Angles.ToRadians(b))), (float)(radius * Math.Cos(Angles.ToRadians(a)) * Math.Sin(Angles.ToRadians(b))), (float)(radius * Math.Cos(Angles.ToRadians(b))));
                    verts.Add(vertex.X);
                    verts.Add(vertex.Y);
                    verts.Add(vertex.Z);

                    Vector3 normal = vertex.Normalize();

                    norms.Add(normal.X);
                    norms.Add(normal.Y);
                    norms.Add(normal.Z);

                    tex.Add((b) / 360f);
                    tex.Add((a) / 360f);

                    vertex = new Vector3((float)(radius * Math.Sin(Angles.ToRadians(a)) * Math.Sin(Angles.ToRadians(b - space))), (float)(radius * Math.Cos(Angles.ToRadians(a)) * Math.Sin(Angles.ToRadians(b - space))), (float)(radius * Math.Cos(Angles.ToRadians(b - space))));
                    verts.Add(vertex.X);
                    verts.Add(vertex.Y);
                    verts.Add(vertex.Z);

                    normal = vertex.Normalize();

                    norms.Add(normal.X);
                    norms.Add(normal.Y);
                    norms.Add(normal.Z);

                    tex.Add((b - space) / 360f);
                    tex.Add((a) / 360f);

                    vertex = new Vector3((float)(radius * Math.Sin(Angles.ToRadians(a - space)) * Math.Sin(Angles.ToRadians(b))), (float)(radius * Math.Cos(Angles.ToRadians(a - space)) * Math.Sin(Angles.ToRadians(b))), (float)(radius * Math.Cos(Angles.ToRadians(b))));
                    verts.Add(vertex.X);
                    verts.Add(vertex.Y);
                    verts.Add(vertex.Z);

                    normal = vertex.Normalize();

                    norms.Add(normal.X);
                    norms.Add(normal.Y);
                    norms.Add(normal.Z);

                    tex.Add((b) / 360f);
                    tex.Add((a - space) / 360f);

                    vertex = new Vector3((float)(radius * Math.Sin(Angles.ToRadians(a - space)) * Math.Sin(Angles.ToRadians(b - space))), (float)(radius * Math.Cos(Angles.ToRadians(a - space)) * Math.Sin(Angles.ToRadians(b - space))), (float)(radius * Math.Cos(Angles.ToRadians(b - space))));
                    verts.Add(vertex.X);
                    verts.Add(vertex.Y);
                    verts.Add(vertex.Z);

                    normal = vertex.Normalize();

                    norms.Add(normal.X);
                    norms.Add(normal.Y);
                    norms.Add(normal.Z);

                    tex.Add((b - space) / 360f);
                    tex.Add((a - space) / 360f);

                    currentVertex += 4;

                }
            }

            Model Model = ContentManager.LoadModel(verts.ToArray(), norms.ToArray(), tex.ToArray(), color, 3);
            Model.VAO.PrimitiveType = Pencil.Gaming.Graphics.BeginMode.TriangleStrip;
            return Model;
        }

    }
}
