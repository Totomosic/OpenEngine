using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class KeyFrame
    {

        #region FIELDS

        private float timeStamp;
        private Dictionary<string, JointTransform> pose;

        #endregion

        #region CONSTRUCTORS

        public KeyFrame(float timeStamp, Dictionary<string, JointTransform> jointKeyFrames)
        {
            this.timeStamp = timeStamp;
            this.pose = jointKeyFrames;
        }

        #endregion

        #region PROPERTIES

        public float TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public Dictionary<string, JointTransform> JointKeyFrames
        {
            get { return pose; }
            set { pose = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
