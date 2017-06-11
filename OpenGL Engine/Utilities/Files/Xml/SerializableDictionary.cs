using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class SerializableDictionary<TKey, TValue>
    {

        #region FIELDS

        private List<KeyValuePair<TKey, TValue>> items;
        private List<TKey> metadata;

        #endregion

        #region CONSTRUCTORS

        public SerializableDictionary()
        {
            items = new List<KeyValuePair<TKey, TValue>>();
            metadata = new List<TKey>();
        }

        #endregion

        #region PROPERTIES

        public TValue this[TKey index]
        {
            get { return GetValue(index); }
        }

        public List<KeyValuePair<TKey, TValue>> Items
        {
            get { return items; }
            set { items = value; }
        }

        public List<TKey> Keys
        {
            get { return GetKeys(); }
        }

        #endregion

        #region PUBLIC METHODS

        public static SerializableDictionary<TKey, TValue> FromDictionary(Dictionary<TKey, TValue> dict)
        {
            SerializableDictionary<TKey, TValue> sd = new SerializableDictionary<TKey, TValue>();
            foreach (TKey key in dict.Keys)
            {
                sd.Add(key, dict[key]);
            }
            return sd;
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (TKey key in Keys)
            {
                dict.Add(key, this[key]);
            }
            return dict;
        }

        public virtual void Add(TKey key, TValue value)
        {
            Items.Add(new KeyValuePair<TKey, TValue>(key, value));
            metadata.Add(key);
        }

        public virtual List<TKey> GetKeys()
        {
            List<TKey> keys = new List<TKey>();
            foreach (KeyValuePair<TKey, TValue> kvp in Items)
            {
                keys.Add(kvp.Key);
            }
            return keys;
        }

        public virtual TValue GetValue(TKey key)
        {
            return Items[metadata.IndexOf(key)].Value;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
