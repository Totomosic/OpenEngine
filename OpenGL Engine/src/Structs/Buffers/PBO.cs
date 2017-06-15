using System;
using System.Collections.Generic;
using Pencil.Gaming.Graphics;
using System.Runtime.InteropServices;

namespace OpenEngine
{
    public class PBO<T> : Buffer<T>
        where T : struct
    {

        #region FIELDS

        private PixelOperation currentOperation;

        #endregion

        #region CONSTRUCTORS

        public PBO(int bufferSize, int dataTypeSize, T[] data = null) : base(bufferSize, dataTypeSize, BufferTarget.PixelUnpackBuffer, data, BufferUsageHint.DynamicCopy)
        {
            currentOperation = PixelOperation.Upload;
        }

        #endregion

        #region PROPERTIES

        public PixelOperation Operation
        {
            get { return currentOperation; }
            set { currentOperation = value; }
        }

        public override BufferTarget Target
        {
            get { return (BufferTarget)Operation; }
        }

        #endregion

        #region PULIC METHODS

        public void SwapOperation()
        {
            if (Operation == PixelOperation.Download)
            {
                currentOperation = PixelOperation.Upload;
            }
            else
            {
                currentOperation = PixelOperation.Download;
            }
        }

        public void SetOperation(PixelOperation operation)
        {
            currentOperation = operation;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}