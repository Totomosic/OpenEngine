using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class IntVBO : Buffer<int>
    {

        #region FIELDS

        private int attributeIndex;
        private int dataDimension;

        #endregion

        #region CONSTRUCTORS

        public IntVBO(int bufferSize, int attribIndex, int dDimension, int[] data = null, BufferUsageHint usageHint = BufferUsageHint.StaticDraw) : base(bufferSize, sizeof(float), BufferTarget.ArrayBuffer, data, usageHint)
        {
            attributeIndex = attribIndex;
            dataDimension = dDimension;
        }

        public IntVBO() : this(0, 0, 0)
        {

        }

        #endregion

        #region PROPERTIES

        public int AttributeIndex
        {
            get { return attributeIndex; }
            set { attributeIndex = value; }
        }

        public int DataDimension
        {
            get { return dataDimension; }
            set { dataDimension = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

    }
}
