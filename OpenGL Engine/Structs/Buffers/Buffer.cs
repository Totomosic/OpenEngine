using System;
using System.Collections.Generic;
using System.Linq;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class Buffer<T> : GLObject
        where T : struct
    {

        #region FIELDS

        private int bufferSize;
        private int dataTypeSize;
        private BufferUsageHint usageHint;
        private BufferTarget bufferTarget;

        private int usedBytes;

        #endregion

        #region CONSTRUCTORS

        public Buffer(int _bufferSize, int _dataTypeSize, BufferTarget target, T[] data = null, BufferUsageHint hint = BufferUsageHint.StaticDraw) : base(CreateObject(), GLType.Buffer)
        {
            bufferSize = _bufferSize;
            dataTypeSize = _dataTypeSize;
            bufferTarget = target;
            usageHint = hint;
            usedBytes = 0;

            CreateBuffer();

            if (data != null && data.Length * DataTypeSize == BufferSize)
            {
                SetData(data);
            }

        }

        public override void Delete()
        {
            GL.DeleteBuffer(ID);
            base.Delete();
        }

        #endregion

        #region PROPERTIES

        public virtual int BufferSize
        {
            get { return bufferSize; }
        }

        public virtual int UsedBytes
        {
            get { return usedBytes; }
        }

        public virtual int DataTypeSize
        {
            get { return dataTypeSize; }
        }

        public virtual BufferUsageHint UsageHint
        {
            get { return usageHint; }
        }

        public virtual BufferTarget Target
        {
            get { return bufferTarget; }
        }

        #endregion

        #region PUBLIC METHODS

        public override void Bind()
        {
            GL.BindBuffer(Target, ID);
            State = BindState.Bound;
        }

        public override void Unbind()
        {
            GL.BindBuffer(Target, 0);
            State = BindState.Unbound;
        }

        public virtual IntPtr GetSize()
        {
            return (IntPtr)BufferSize;
        }

        public virtual void Resize(int newSize)
        {
            bufferSize = newSize;
            CreateBuffer();
        }

        public virtual void SetData(T[] data)
        {
            Bind();
            UploadData(data);
        }

        public virtual void AddData(T[] data)
        {
            if (data.Length * DataTypeSize + usedBytes > BufferSize)
            {
                throw new BufferException("Unable to add data to buffer as there is not enough allocated space for data of: " + (data.Length * DataTypeSize).ToString() + " bytes.");
            }
            UploadData(data, usedBytes);
        }

        public virtual void ClearData()
        {
            CreateBuffer();
        }

        public virtual void SetUsageHint(BufferUsageHint hint)
        {
            usageHint = hint;
            CreateBuffer();
        }

        public virtual void UploadData(T[] data, int offset = 0)
        {
            if (data.Length * DataTypeSize + offset > BufferSize)
            {
                throw new BufferException("Data is larger than size of buffer. Use Buffer.Resize() to set new size.");
            }
            Bind();
            GL.BufferSubData(Target, (IntPtr)offset, (IntPtr)(data.Length * DataTypeSize), data);
            usedBytes += data.Length * DataTypeSize;
        }

        public virtual void UploadData(IntPtr data, int offset = 0)
        {
            Bind();
            GL.BufferSubData(Target, (IntPtr)offset, GetSize(), data);
        }

        public virtual T[] DownloadData(int offset = 0, int dataLength = -1)
        {
            if (dataLength == -1)
            {
                dataLength = BufferSize;
            }
            Bind();
            T[] data = new T[dataLength / DataTypeSize];
            GL.GetBufferSubData(Target, (IntPtr)offset, (IntPtr)dataLength, data);
            return data;
        }

        public IntPtr Map(BufferAccess access = BufferAccess.ReadOnly)
        {
            return GL.MapBuffer(Target, access);
        }

        public bool Unmap()
        {
            return GL.UnmapBuffer(Target);
        }

        #endregion

        #region PRIVATE METHODS

        private static int CreateObject()
        {
            return GL.GenBuffer();
        }

        private void CreateBuffer()
        {
            Bind();
            GL.BufferData(Target, GetSize(), default(IntPtr), UsageHint);
            usedBytes = 0;
        }

        #endregion

    }
}
