using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class VBO<T> : Buffer<T>
        where T : struct
    {

        #region FIELDS

        protected int attributeIndex;
        protected int dataDimension;

        #endregion

        #region CONSTRUCTORS

        public VBO(int bufferSize, int attribIndex, int dDimension, int dataTypeSize, T[] data = null, BufferUsageHint usageHint = BufferUsageHint.StaticDraw) : base(bufferSize, dataTypeSize, BufferTarget.ArrayBuffer, data, usageHint)
        {
            attributeIndex = attribIndex;
            dataDimension = dDimension;
        }

        public VBO(int bufferSize, BufferLayout attribIndex, int dataDimension, int dataTypeSize, T[] data = null, BufferUsageHint usageHint = BufferUsageHint.StaticDraw) : this(bufferSize, (int)attribIndex, dataDimension, dataTypeSize, data, usageHint)
        {

        }

        #endregion

        #region PROPERTIES

        public int AttributeIndex
        {
            get { return attributeIndex; }
        }

        public int DataDimension
        {
            get { return dataDimension; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

    }
}
