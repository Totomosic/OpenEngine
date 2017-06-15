using System;
using System.Collections.Generic;

namespace OpenEngine.Components
{
    public class Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public Component(GameObject owner)
        {
            Owner = owner;
            IsActive = true;
        }

        public Component() : this(null)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual GameObject Owner
        {
            get; set;
        }

        public virtual bool IsActive
        {
            get; set;
        }

        #endregion

        #region PUBLIC METHODS

        public T Clone<T>()
            where T : Component, new()
        {
            return this.MemberwiseClone() as T;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
