using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Animation
    {

        #region FIELDS

        private float length; // in seconds
        private KeyFrame[] keyFrames;

        #endregion

        #region CONSTRUCTORS

        public Animation(float lengthInSeconds, KeyFrame[] frames)
        {
            length = lengthInSeconds;
            keyFrames = frames;
        }

        #endregion

        #region PROPERTIES

        public float Length
        {
            get { return length; }
            set { length = value; }
        }

        public float TotalTime
        {
            get { return Length; }
            set { Length = value; }
        }

        public KeyFrame[] KeyFrames
        {
            get { return keyFrames; }
            set { keyFrames = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
