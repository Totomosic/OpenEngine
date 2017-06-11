using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class StaticVertexBatching
    {

        #region FIELDS

        private static Dictionary<FBO, List<VertexBatch>> buffers;
        private static int bufferSize;

        #endregion

        #region INIT

        static StaticVertexBatching()
        {
            buffers = new Dictionary<FBO, List<VertexBatch>>();
            bufferSize = 10000;
        }

        #endregion

        #region PROPERTIES

        public static Dictionary<FBO, List<VertexBatch>> Buffers
        {
            get { return buffers; }
        }

        public static int BatchSize
        {
            get { return bufferSize; }
            set { bufferSize = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public static void SubmitStaticVertices(Vertex[] vertices, BatchConfig config, uint[] indices = null)
        {
            if (Buffers.ContainsKey(config.RenderTarget))
            {
                foreach (VertexBatch buffer in Buffers[config.RenderTarget])
                {
                    if (buffer.Config == config && buffer.TryAddVertices(vertices, indices))
                    {
                        return;
                    }
                }
                VertexBatch newBuffer = new VertexBatch(bufferSize, config);
                if (!newBuffer.TryAddVertices(vertices, indices))
                {
                    throw new BufferException("Vertex data is too large to be batched. Size: " + vertices.Length);
                }
                AddNewBuffer(newBuffer);
            }
            else
            {
                Buffers[config.RenderTarget] = new List<VertexBatch>();
                VertexBatch newBuffer = new VertexBatch(bufferSize, config);
                if (!newBuffer.TryAddVertices(vertices, indices))
                {
                    throw new BufferException("Vertex data is too large to be batched. Size: " + vertices.Length);
                }
                AddNewBuffer(newBuffer);
            }
        }

        public static void SubmitStaticModel(Model model, BatchConfig config, Matrix4 transformation = default(Matrix4))
        {
            Matrix4 matrix = (transformation == default(Matrix4)) ? Matrix4.Identity : transformation;
            SubmitStaticVertices(Vertex.ApplyMatrix4(model.ToVertexArray(), matrix), config);
        }

        #endregion

        #region PRIVATE METHODS

        private static void AddNewBuffer(VertexBatch batch)
        {
            Buffers[batch.Config.RenderTarget].Add(batch);
            BatchManager.SubmitVertexBatch(batch);
        }

        #endregion

    }
}
