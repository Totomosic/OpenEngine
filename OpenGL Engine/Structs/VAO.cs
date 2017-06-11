using System;
using System.Collections.Generic;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class VAO : GLObject
    {

        #region FIELDS

        private int renderCount;
        private RenderMode mode;
        private BeginMode primitiveType;

        private Dictionary<int, FloatVBO> vbos;
        private Dictionary<int, IntVBO> intVbos;
        private Dictionary<int, IndexBuffer> uintBuffers;
        private List<int> attribIndices;
        private IndexBuffer indexBuffer;

        private int vertexCount;

        #endregion

        #region CONSTRUCTORS

        public VAO(RenderMode renderMode, int vertCount, BeginMode primitive = BeginMode.Triangles) : base(0, GLType.VAO)
        {
            renderCount = 0;
            mode = renderMode;

            vbos = new Dictionary<int, FloatVBO>();
            uintBuffers = new Dictionary<int, IndexBuffer>();
            intVbos = new Dictionary<int, IntVBO>();

            attribIndices = new List<int>();
            vertexCount = vertCount;
            primitiveType = primitive;
            CreateObject();
        }

        public VAO() : this(RenderMode.Arrays, 0)
        {

        }

        public override void Delete()
        {
            if (!Context.Window.ShouldClose)
            {
                GL.DeleteVertexArray(id);
                foreach (int key in vbos.Keys)
                {
                    vbos[key].Delete();
                }
                foreach (int key in intVbos.Keys)
                {
                    intVbos[key].Delete();
                }
                if (indexBuffer != null)
                {
                    indexBuffer.Delete();
                }
                base.Delete();
            }
        }

        #endregion

        #region PROPERTIES

        public int RenderCount
        {
            get { return renderCount; }
            set { renderCount = value; }
        }

        public RenderMode Mode
        {
            get { return mode; }
            set { SetRenderMode(value); }
        }

        public BeginMode PrimitiveType
        {
            get { return primitiveType; }
            set { primitiveType = value; }
        }

        public int VertexCount
        {
            get { return vertexCount; }
            set { vertexCount = value; }
        }

        public List<int> AttributeIndices
        {
            get { return attribIndices; }
            set { attribIndices = value; }
        }

        public SerializableDictionary<int, FloatVBO> FloatVBOs
        {
            get { return SerializableDictionary<int, FloatVBO>.FromDictionary(vbos); }
            set { vbos = value.ToDictionary(); }
        }

        public SerializableDictionary<int, IntVBO> IntVBOs
        {
            get { return SerializableDictionary<int, IntVBO>.FromDictionary(intVbos); }
            set { intVbos = value.ToDictionary(); }
        }

        public IndexBuffer IndexBuffer
        {
            get { return indexBuffer; }
            set { indexBuffer = value; }
        }

        public bool HasIndexBuffer
        {
            get { return indexBuffer != null; }
        }

        #endregion

        #region PUBLIC METHODS

        public void Render(int instances = 0)
        {
            Bind();
            if (instances == 0)
            {
                if (Mode == RenderMode.Arrays)
                {
                    GL.DrawArrays(PrimitiveType, 0, RenderCount);
                }
                else if (Mode == RenderMode.Elements)
                {
                    GL.DrawElements(PrimitiveType, RenderCount, DrawElementsType.UnsignedInt, 0);
                }
            }
            else
            {
                if (Mode == RenderMode.Arrays)
                {
                    GL.DrawArraysInstanced(PrimitiveType, 0, RenderCount, instances);
                }
                else if (Mode == RenderMode.Elements)
                {
                    GL.DrawElementsInstanced(PrimitiveType, RenderCount, DrawElementsType.UnsignedInt, IntPtr.Zero, instances);
                }
            }
        }

        public bool HasFloatBuffer(int attribIndex)
        {
            return vbos.ContainsKey(attribIndex);
        }

        public void SetRenderMode(RenderMode mode)
        {
            if (this.mode != mode)
            {
                if (mode == RenderMode.Arrays)
                {
                    if (HasFloatBuffer((int)BufferLayout.Vertices))
                    {
                        RenderCount = GetBuffer(BufferLayout.Vertices).DownloadData().Length / 3 / 3;
                    }
                }
                else
                {
                    if (HasIndexBuffer)
                    {
                        RenderCount = IndexBuffer.DownloadData().Length;
                    }
                }
            }
            this.mode = mode;
        }

        public override void Bind()
        {
            GL.BindVertexArray(ID);
            State = BindState.Bound;
        }

        public override void Unbind()
        {
            GL.BindVertexArray(0);
            State = BindState.Unbound;
        }

        public void EnableVertexAttribArrays()
        {
            foreach (int index in attribIndices)
            {
                GL.EnableVertexAttribArray(index);
            }
        }

        public void DisableVertexAttribArrays()
        {
            foreach (int index in attribIndices)
            {
                GL.DisableVertexAttribArray(index);
            }
        }

        public void AttachVBO(FloatVBO vbo, int attribDivisor = 0)
        {
            Bind();
            vbo.Bind();
            GL.EnableVertexAttribArray(vbo.AttributeIndex);            
            GL.VertexAttribPointer(vbo.AttributeIndex, vbo.DataDimension, VertexAttribPointerType.Float, false, 0, IntPtr.Zero);
            GL.VertexAttribDivisor(vbo.AttributeIndex, attribDivisor);
            vbos[vbo.AttributeIndex] = vbo;
            attribIndices.Add(vbo.AttributeIndex);

            if (vbo.AttributeIndex == (int)BufferLayout.Vertices)
            {
                VertexCount = vbo.DownloadData().Length / vbo.DataDimension;
                RenderCount = VertexCount;
            }

        }

        public void AttachVBOs(FloatVBO[] vbos, int attribDivisor = 0)
        {
            foreach (FloatVBO vbo in vbos)
            {
                AttachVBO(vbo, attribDivisor);
            }
        }

        public void AttachIntVBO(IntVBO vbo, int attribDivisor = 0)
        {
            Bind();
            vbo.Bind();
            GL.EnableVertexAttribArray(vbo.AttributeIndex);
            GL.VertexAttribIPointer(vbo.AttributeIndex, vbo.DataDimension, VertexAttribIPointerType.Int, 0, IntPtr.Zero);
            GL.VertexAttribDivisor(vbo.AttributeIndex, attribDivisor);
            intVbos[vbo.AttributeIndex] = vbo;
            attribIndices.Add(vbo.AttributeIndex);
        }

        public void AttachIntVBOs(IntVBO[] vbos, int attribDivisor = 0)
        {
            foreach (IntVBO vbo in vbos)
            {
                AttachIntVBO(vbo, attribDivisor);
            }
        }

        public void AttachIndexBuffer(IndexBuffer buffer)
        {
            Bind();
            buffer.Bind();            
            indexBuffer = buffer;

            RenderCount = buffer.DownloadData().Length;

        }

        public void CreateAttribute(int attribIndex, float[] data, int dataDimension = 3, int attribDivisor = 0)
        {
            FloatVBO vbo = new FloatVBO(data.Length * sizeof(float), attribIndex, dataDimension, data);
            AttachVBO(vbo, attribDivisor);
        }

        public void CreateAttribute(BufferLayout attribIndex, float[] data, int dataDimension = 3, int attribDivisor = 0)
        {
            CreateAttribute((int)attribIndex, data, dataDimension, attribDivisor);
        }

        public void CreateInstanceAttribute(int attribIndex, float[] data, int dataDimension = 3)
        {
            CreateAttribute(attribIndex, data, dataDimension, 1);
        }

        public void CreateIntAttribute(int attribIndex, int[] data, int dataDimension = 3, int attribDivisor = 0)
        {
            IntVBO vbo = new IntVBO(data.Length * sizeof(int), attribIndex, dataDimension);
            AttachIntVBO(vbo, attribDivisor);
        }

        public void CreateIntAttribute(BufferLayout attribIndex, int[] data, int dataDimension = 3)
        {
            CreateIntAttribute((int)attribIndex, data, dataDimension);
        }

        public void CreateIndexBuffer(uint[] indices)
        {
            IndexBuffer ibo = new IndexBuffer(indices.Length * sizeof(uint), indices);
            AttachIndexBuffer(ibo);
        }

        public void CreateColorBuffer(Color color)
        {
            FloatVBO vbo = new FloatVBO(VertexCount * sizeof(float) * 4, (int)BufferLayout.Color, 4, color.ToVertexData(VertexCount));
            AttachVBO(vbo);
        }

        public FloatVBO GetBuffer(int attribIndex)
        {
            return vbos[attribIndex];
        }

        public FloatVBO GetBuffer(BufferLayout layout)
        {
            return GetBuffer((int)layout);
        }

        public IntVBO GetIntBuffer(int attribIndex)
        {
            return intVbos[attribIndex];
        }

        public IndexBuffer GetIndexBuffer()
        {
            return indexBuffer;
        }

        #endregion

        #region PRIVATE METHODS

        public void CreateObject()
        {
            ID = GL.GenVertexArray();
        }

        #endregion

    }
}
