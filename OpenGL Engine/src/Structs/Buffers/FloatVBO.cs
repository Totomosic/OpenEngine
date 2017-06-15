using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class FloatVBO : VBO<float>
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public FloatVBO(int bufferSize, int attribIndex, int dDimension, float[] data = null, BufferUsageHint usageHint = BufferUsageHint.StaticDraw) : base(bufferSize, attribIndex, dDimension, sizeof(float), data, usageHint)
        {

        }

        public FloatVBO() : this(0, 0, 0)
        {

        }

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public virtual float[] GetFloatArray()
        {
            return DownloadData();
        }

        public virtual Vector3[] GetVector3Array()
        {
            float[] floats = GetFloatArray();
            Vector3[] vectors = new Vector3[BufferSize / DataTypeSize / DataDimension];
            if (DataDimension == 3)
            {
                for (int i = 0; i < floats.Length; i += 3)
                {
                    vectors[i / 3] = new Vector3(floats[i], floats[i + 1], floats[i + 2]);
                }
            }
            else if (DataDimension == 2)
            {
                for (int i = 0; i < floats.Length; i += 2)
                {
                    vectors[i / 2] = new Vector3(floats[i], floats[i + 1], 0);
                }
            }
            return vectors;
        }

        #endregion

    }
}
