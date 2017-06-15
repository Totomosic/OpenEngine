using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Sphere : Shape
    {

        #region FIELDS

        private float r;
        private float space;

        #endregion

        #region CONSTRUCTORS

        public Sphere(Vector3 position, float radius, int vertexSpacing = 10, Matrix4 rotationMatrix = default(Matrix4)) : base(position, new Vector3(radius * 2), rotationMatrix)
        {
            r = radius;
            space = vertexSpacing;
            CreateModel();
        }

        public Sphere(float x, float y, float z, float radius, int vertexSpacing = 10, Matrix4 rotationMatrix = default(Matrix4)) : this(new Vector3(x, y, z), radius, vertexSpacing, rotationMatrix)
        {

        }

        #endregion

        #region PROPERTIES

        public float Radius
        {
            get { return r; }
        }

        public float VertexSpace
        {
            get { return space; }
        }

        #endregion

        #region PRIVATE METHODS

        protected override void CreateModel()
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
                    Vector3 vertex = new Vector3((float)(r * Math.Sin(Angles.ToRadians(a)) * Math.Sin(Angles.ToRadians(b))), (float)(r * Math.Cos(Angles.ToRadians(a)) * Math.Sin(Angles.ToRadians(b))), (float)(r * Math.Cos(Angles.ToRadians(b))));
                    verts.Add(vertex.X);
                    verts.Add(vertex.Y);
                    verts.Add(vertex.Z);

                    Vector3 normal = vertex.Normalize();

                    norms.Add(normal.X);
                    norms.Add(normal.Y);
                    norms.Add(normal.Z);

                    tex.Add((b) / 360f);
                    tex.Add((a) / 360f);

                    vertex = new Vector3((float)(r * Math.Sin(Angles.ToRadians(a)) * Math.Sin(Angles.ToRadians(b - space))), (float)(r * Math.Cos(Angles.ToRadians(a)) * Math.Sin(Angles.ToRadians(b - space))), (float)(r * Math.Cos(Angles.ToRadians(b - space))));
                    verts.Add(vertex.X);
                    verts.Add(vertex.Y);
                    verts.Add(vertex.Z);

                    normal = vertex.Normalize();

                    norms.Add(normal.X);
                    norms.Add(normal.Y);
                    norms.Add(normal.Z);

                    tex.Add((b - space) / 360f);
                    tex.Add((a) / 360f);

                    vertex = new Vector3((float)(r * Math.Sin(Angles.ToRadians(a - space)) * Math.Sin(Angles.ToRadians(b))), (float)(r * Math.Cos(Angles.ToRadians(a - space)) * Math.Sin(Angles.ToRadians(b))), (float)(r * Math.Cos(Angles.ToRadians(b))));
                    verts.Add(vertex.X);
                    verts.Add(vertex.Y);
                    verts.Add(vertex.Z);

                    normal = vertex.Normalize();

                    norms.Add(normal.X);
                    norms.Add(normal.Y);
                    norms.Add(normal.Z);

                    tex.Add((b) / 360f);
                    tex.Add((a - space) / 360f);

                    vertex = new Vector3((float)(r * Math.Sin(Angles.ToRadians(a - space)) * Math.Sin(Angles.ToRadians(b - space))), (float)(r * Math.Cos(Angles.ToRadians(a - space)) * Math.Sin(Angles.ToRadians(b - space))), (float)(r * Math.Cos(Angles.ToRadians(b - space))));
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

            Model = ContentManager.LoadModel(verts.ToArray(), norms.ToArray(), tex.ToArray(), 3);
            Model.VAO.PrimitiveType = Pencil.Gaming.Graphics.BeginMode.TriangleStrip;
            Model.VAO.RenderCount = verts.Count / Utilities.GetVertexCount(Model.VAO.PrimitiveType);
        }

        #endregion

    }
}
