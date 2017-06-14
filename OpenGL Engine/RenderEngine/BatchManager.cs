using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenEngine
{
    public static class BatchManager
    {

        #region FIELDS

        private static Dictionary<FBO, List<VertexBatch>> batches;

        #endregion

        #region INIT

        static BatchManager()
        {
            batches = new Dictionary<FBO, List<VertexBatch>>();
        }

        #endregion

        #region PROPERTIES

        public static Dictionary<FBO, List<VertexBatch>> Batches
        {
            get { return batches; }
        }

        #endregion

        #region PUBLIC METHODS

        public static VertexBatch[] GetAllVertexBatches(FBO renderTarget)
        {
            List<VertexBatch> buffers = new List<VertexBatch>();
            if (Batches.ContainsKey(renderTarget))
            {
                buffers.AddRange(Batches[renderTarget]);
            }
            buffers.Sort();
            return buffers.ToArray();
        }

        public static void SubmitVertexBatch(VertexBatch buffer)
        {
            if (Batches.ContainsKey(buffer.Config.RenderTarget))
            {
                Batches[buffer.Config.RenderTarget].Add(buffer);
            }
            else
            {
                Batches[buffer.Config.RenderTarget] = new List<VertexBatch>();
                Batches[buffer.Config.RenderTarget].Add(buffer);
            }
        }

        public static void SubmitVertices(Vertex[] vertices, BatchConfig config, uint[] indices = null)
        {
            AddNewBatch(vertices, config, indices);
        }

        public static void SubmitModel(Model model, BatchConfig config, Matrix4 transformation = default(Matrix4))
        {
            Matrix4 matrix = (transformation == default(Matrix4)) ? Matrix4.Identity : transformation;
            SubmitVertices(Vertex.ApplyMatrix4(model.ToVertexArray(), matrix), config, (model.VAO.HasIndexBuffer) ? model.VAO.IndexBuffer.DownloadData() : null);
        }

        public static void Clear(ClearMode mode = ClearMode.DynamicOnly)
        {
            if (mode == ClearMode.All)
            {
                Batches.Clear();
            }
        }

        #endregion

        #region PRIVATE METHODS

        private static void AddNewBatch(Vertex[] vertices, BatchConfig config, uint[] indices = null)
        {
            VertexBatch batch = new VertexBatch(vertices.Length, config);
            batch.AddVertices(vertices, indices);
            SubmitVertexBatch(batch);
        }

        #endregion

    }
}
