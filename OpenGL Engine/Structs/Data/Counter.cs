using System;

namespace OpenEngine
{
    public class Counter
    {

        #region FIELDS

        private float count;

        private static float globalCount = 0;

        #endregion

        #region CONSTRCTORS

        public Counter(float start = 0)
        {
            count = start;
        }

        #endregion

        #region PROPERTIES

        public float Value
        {
            get { return count; }
            set { count = value; }
        }

        public static float GlobalValue
        {
            get { return globalCount; }
            set { globalCount = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public void Increment(float val = 1)
        {
            count += val;
        }

        public void Decrement(float val = 1)
        {
            count -= val;
        }

        public static void GlobalIncrement(float val = 1)
        {
            globalCount += val;
        }

        public static void GlobalDecrement(float val = 1)
        {
            globalCount -= val;
        }

        #endregion

    }
}
