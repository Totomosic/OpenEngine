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
            if (config.Type == BatchType.Static)
            {
                StaticVertexBatching.SubmitStaticVertices(vertices, config, indices);
            }
            else if (config.Type == BatchType.Dynamic)
            {
                AddNewBatch(vertices, config, indices);
            }
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
            else if (mode == ClearMode.DynamicOnly)
            {
                RemoveBatchType(BatchType.Dynamic);
            }
            else if (mode == ClearMode.StaticOnly)
            {
                RemoveBatchType(BatchType.Static);
            }
            else if (mode == ClearMode.StaticDynamic)
            {
                RemoveBatchType(new BatchType[] { BatchType.Static, BatchType.Dynamic });
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

        private static void RemoveBatchType(BatchType type)
        {
            RemoveBatchType(new BatchType[] { type });
        }

        private static void RemoveBatchType(BatchType[] types)
        {
            foreach (FBO renderTarget in Batches.Keys)
            {
                for (int i = Batches[renderTarget].Count - 1; i >= 0; i--)
                {
                    if (types.Contains(Batches[renderTarget][i].Config.Type))
                    {
                        Batches[renderTarget].RemoveAt(i);
                    }
                }
            }
        }

        #endregion

    }
}
