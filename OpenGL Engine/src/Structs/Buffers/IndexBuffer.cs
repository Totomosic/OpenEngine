using System;

namespace OpenEngine
{
    public class IndexBuffer : Buffer<uint>
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public IndexBuffer(int bufferSize, uint[] data = null) : base(bufferSize, sizeof(uint), Pencil.Gaming.Graphics.BufferTarget.ElementArrayBuffer, data)
        {

        }

        public IndexBuffer() : this(0)
        {

        }

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void UploadData(uint[] data)
        {
            Bind();
            base.UploadData(data);
        }

        #endregion

    }
}
