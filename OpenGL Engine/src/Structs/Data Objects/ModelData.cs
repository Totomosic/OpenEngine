using System;

namespace OpenEngine
{
    public class ModelData : DataObject
    {

        #region FIELDS

        private float maxHeight;
        private float minHeight;

        #endregion

        #region CONSTRUCTORS

        public ModelData(float maxH, float minH)
        {
            maxHeight = maxH;
            minHeight = minH;
        }

        public ModelData(float maxH) : this(maxH, maxH * -1)
        {

        }

        public ModelData() : this(50, -50)
        {

        }

        #endregion

        #region PROPERTIES

        public float MaxHeight
        {
            get { return maxHeight; }
        }

        public float MinHeight
        {
            get { return minHeight; }
        }

        #endregion

    }
}
