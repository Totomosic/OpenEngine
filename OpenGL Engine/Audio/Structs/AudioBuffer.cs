using System;
using Pencil.Gaming.Audio;

namespace OpenEngine
{
    public class AudioBuffer
    {

        #region FIELDS

        private uint id;
        private int bufferSize;

        #endregion

        #region CONSTRUCTORS

        public AudioBuffer(int bufferSize)
        {
            id = CreateObject();
        }

        #endregion

        #region PROPERTIES

        public virtual uint ID
        {
            get { return id; }
            protected set { id = value; }
        }

        public virtual int BufferSize
        {
            get { return bufferSize; }
            set { bufferSize = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        private static uint CreateObject()
        {
            uint temp = 0;
            AL.GenBuffers(1, out temp);
            return temp;
        }

        #endregion

    }
}
