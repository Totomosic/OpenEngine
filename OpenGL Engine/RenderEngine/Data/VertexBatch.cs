using System;
using System.Linq;
using System.Collections.Generic;

namespace OpenEngine
{
    public class VertexBatch : IComparable<VertexBatch>, IDisposable
    {

        #region FIELDS

        private BatchConfig config;

        private int maxVerts;
        private uint currentVerts;

        private VAO vao;
        private FloatVBO vertexBuffer;
        private FloatVBO normalBuffer;
        private FloatVBO texCoordBuffer;
        private FloatVBO colorBuffer;
        private IndexBuffer indexBuffer;

        private bool disposed = false;

        #endregion

        #region CONSTRUCTORS

        public VertexBatch(int maxVerts, BatchConfig config)
        {
            this.config = config;
            this.maxVerts = maxVerts;
            currentVerts = 0;

            vao = new VAO(config.RenderMode, maxVerts, config.PrimitiveType);

            vertexBuffer = new FloatVBO(maxVerts * sizeof(float) * 3, (int)BufferLayout.Vertices, 3, null, Pencil.Gaming.Graphics.BufferUsageHint.DynamicDraw);
            normalBuffer = new FloatVBO(maxVerts * sizeof(float) * 3, (int)BufferLayout.Normals, 3, null, Pencil.Gaming.Graphics.BufferUsageHint.DynamicDraw);
            texCoordBuffer = new FloatVBO(maxVerts * sizeof(float) * 2, (int)BufferLayout.TextureCoordinates, 2, null, Pencil.Gaming.Graphics.BufferUsageHint.DynamicDraw);
            colorBuffer = new FloatVBO(maxVerts * sizeof(float) * 4, (int)BufferLayout.Color, 4, null, Pencil.Gaming.Graphics.BufferUsageHint.DynamicDraw);

            if (config.RenderMode == RenderMode.Elements)
            {
                indexBuffer = new IndexBuffer(4 * maxVerts * sizeof(uint));
                vao.AttachIndexBuffer(indexBuffer);
            }

            vao.AttachVBOs(new FloatVBO[] { vertexBuffer, normalBuffer, texCoordBuffer, colorBuffer });
        }

        public VertexBatch(Model model, GameObject camera, Matrix4 transform = default(Matrix4)) : this(model.RenderCount, new BatchConfig(Context.Window.Framebuffer, 0, Engine.Shader, camera, primitive: model.PrimitiveType, renderMode: model.Mode))
        {
            AddModel(model, (transform == default(Matrix4)) ? Matrix4.Identity : transform);
        }

        public void Delete()
        {
            vao.Delete();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                Delete();
                disposed = true;
            }
        }

        #endregion

        #region PROPERTIES

        public VAO VAO
        {
            get { return vao; }
            set { vao = value; }
        }

        public BatchConfig Config
        {
            get { return config; }
            set { config = value; }
        }

        public int MaxVertices
        {
            get { return maxVerts; }
            set { maxVerts = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public bool TryAddVertices(Vertex[] vertices, uint[] indices = null)
        {
            if (vertices.Length * sizeof(float) * 3 > vertexBuffer.BufferSize - vertexBuffer.UsedBytes)
            {
                return false;
            }
            AddVertices(vertices, indices);
            return true;
        }

        public void AddVertices(Vertex[] vertices, uint[] indices = null)
        {
            VertexData vData = new VertexData(vertices);
            vertexBuffer.AddData(vData.Positions);
            normalBuffer.AddData(vData.Normals);
            texCoordBuffer.AddData(vData.TextureCoordinates);
            colorBuffer.AddData(vData.Colors);
            if (config.RenderMode == RenderMode.Elements)
            {
                AddValueToIndices(ref indices, currentVerts);
                indexBuffer.AddData(indices);
                currentVerts = indices.Max() + 1;
            }
        }

        public void AddModel(Model model, Matrix4 matrix = default(Matrix4))
        {
            matrix = (matrix == default(Matrix4)) ? Matrix4.Identity : matrix;
            Vertex[] verts = Vertex.ApplyMatrix4(model.ToVertexArray(), matrix);
            AddVertices(verts, (model.VAO.HasIndexBuffer) ? model.VAO.IndexBuffer.DownloadData() : null);
        }

        public VAO GetVAO()
        {
            return vao;
        }

        public int CompareTo(VertexBatch other)
        {
            if (other.Config.Priority > Config.Priority)
            {
                return 1;
            }
            else if (other.Config.ShaderProgram == Config.ShaderProgram && other.Config.Priority == Config.Priority)
            {
                return 0;
            }
            return -1;
        }

        #endregion

        #region PRIVATE METHODS

        private void AddValueToIndices(ref uint[] indices, uint value)
        {
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = indices[i] + value;
            }
        }

        #endregion

    }
}
