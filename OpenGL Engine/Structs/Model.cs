using System;
using Pencil.Gaming.Graphics;
using System.Linq;

namespace OpenEngine
{
    public class Model
    {

        #region FIELDS

        private VAO vao;
        private Vector3 size;

        #endregion

        #region CONSTRUCTORS

        public Model(VAO vertexArray)
        {
            vao = vertexArray;
            size = new Vector3();
        }

        public Model() : this(new VAO(RenderMode.Arrays, 0))
        {

        }

        #endregion

        #region PROPERTIES

        public VAO VAO
        {
            get { return vao; }
            set { vao = value; }
        }

        public Vector3 Size
        {
            get { return size; }
            set { size = value; }
        }

        public int RenderCount
        {
            get { return vao.RenderCount; }
            set { vao.RenderCount = value; }
        }

        public RenderMode Mode
        {
            get { return vao.Mode; }
            set { vao.Mode = value; }
        }

        public BeginMode PrimitiveType
        {
            get { return vao.PrimitiveType; }
            set { vao.PrimitiveType = value; }
        }

        public int VertexCount
        {
            get { return vao.VertexCount; }
            set { vao.VertexCount = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public virtual void Update(GameTime time)
        {

        }

        public virtual Vertex[] ToVertexArray()
        {
            float[] verts = GetVertexBuffer().DownloadData();
            float[] norms = GetNormalBuffer().DownloadData();
            float[] tex = GetTexCoordBuffer().DownloadData();
            float[] colors = GetColorBuffer().DownloadData();
            Vertex[] vertices = new Vertex[verts.Length / GetVertexBuffer().DataDimension];
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 position = new Vector3();
                if (GetVertexBuffer().DataDimension == 3)
                {
                    position = new Vector3(verts[i * 3], verts[i * 3 + 1], verts[i * 3 + 2]);
                }
                else if (GetVertexBuffer().DataDimension == 2)
                {
                    position = new Vector3(verts[i * 2], verts[i * 2 + 1], 0);
                }
                Vector3 normal = new Vector3(norms[i * 3], norms[i * 3 + 1], norms[i * 3 + 2]);
                Vector2 texCoord = new Vector2(tex[i * 2], tex[i * 2 + 1]);
                Color color = Color.FromFloats(colors[i * 4], colors[i * 4 + 1], colors[i * 4 + 2], colors[i * 4 + 3]);
                vertices[i] = new Vertex(position, normal, texCoord, color);
            }
            return vertices;
        }

        public virtual FloatVBO GetVertexBuffer()
        {
            return vao.GetBuffer((int)BufferLayout.Vertices);
        }

        public virtual FloatVBO GetNormalBuffer()
        {
            return vao.GetBuffer((int)BufferLayout.Normals);
        }

        public virtual FloatVBO GetTexCoordBuffer()
        {
            return vao.GetBuffer((int)BufferLayout.TextureCoordinates);
        }

        public virtual FloatVBO GetColorBuffer()
        {
            return vao.GetBuffer((int)BufferLayout.Color);
        }

        public int GetVertexDimension()
        {
            return vao.GetBuffer((int)BufferLayout.Vertices).DataDimension;
        }

        public void Bind()
        {
            vao.Bind();
        }

        public void Unbind()
        {
            vao.Unbind();
        }

        public void EnableVertexAttribArrays()
        {
            vao.EnableVertexAttribArrays();
        }

        public void DisableVertexAttribArrays()
        {
            vao.DisableVertexAttribArrays();
        }

        public void Render(int instances = 0)
        {
            vao.Render(instances);
        }

        public void CalculateSize()
        {
            FloatVBO vertexBuffer = vao.GetBuffer((int)BufferLayout.Vertices);
            vertexBuffer.Bind();
            int vertexDimension = vao.GetBuffer((int)BufferLayout.Vertices).DataDimension;
            float[] vertexData = vertexBuffer.DownloadData();
            vertexBuffer.Unbind();
            float minX = float.PositiveInfinity;
            float maxX = float.NegativeInfinity;
            float minY = float.PositiveInfinity;
            float maxY = float.NegativeInfinity;
            float minZ = float.PositiveInfinity;
            float maxZ = float.NegativeInfinity;

            for (int i = 0; i < vertexData.Length - 1; i += vertexDimension)
            {
                minX = (vertexData[i] < minX) ? vertexData[i] : minX;
                maxX = (vertexData[i] > maxX) ? vertexData[i] : maxX;

                minY = (vertexData[i + 1] < minY) ? vertexData[i + 1] : minY;
                maxY = (vertexData[i + 1] > maxY) ? vertexData[i + 1] : maxY;

                if (vertexDimension == 3)
                {
                    minZ = (vertexData[i + 2] < minZ) ? vertexData[i + 2] : minZ;
                    maxZ = (vertexData[i + 2] > maxZ) ? vertexData[i + 2] : maxZ;
                }
                else
                {
                    minZ = 0;
                    maxZ = 0;
                }
            }

            Size = new Vector3(Math.Max(Math.Abs(maxX - minX), 0.25f), Math.Max(Math.Abs(maxY - minY), 0.25f), Math.Max(Math.Abs(maxZ - minZ), 0.25f));

        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
