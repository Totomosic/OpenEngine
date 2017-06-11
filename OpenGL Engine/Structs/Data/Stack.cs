using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Stack<T>
    {

        #region FIELDS

        private List<T> data;

        #endregion

        #region CONSTRUCTORS

        public Stack()
        {
            // List is opposite to stack ie. last index is top of stack
            data = new List<T>();
        }

        #endregion

        #region PROPERTIES

        public int Length
        {
            get { return data.Count; }
        }

        public T TopItem
        {
            get { return LookAtTop(); }
        }

        #endregion

        #region PUBLIC METHODS

        public virtual void Push(T item)
        {
            data.Add(item);
        }

        public virtual void Push()
        {
            Push(LookAtTop());
        }

        public virtual T Pop()
        {
            T item = data[Length - 1];
            data.RemoveAt(Length - 1);
            return item;
        }

        public virtual T LookAtTop()
        {
            return data[Length - 1];
        }

        public virtual void Clear()
        {
            data.Clear();
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
