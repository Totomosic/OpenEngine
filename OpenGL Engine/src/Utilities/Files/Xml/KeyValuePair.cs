using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class KeyValuePair<TKey, TValue>
    {

        #region FIELDS

        private TKey key;
        private TValue value;

        #endregion

        #region CONSTRUCTORS

        public KeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public KeyValuePair()
        {

        }

        #endregion

        #region PROPERTIES

        public TKey Key
        {
            get { return key; }
            set { key = value; }
        }

        public TValue Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #endregion

    }
}
