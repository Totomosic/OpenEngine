using System;
using System.Collections.Generic;

namespace OpenEngine.Components
{
    public abstract class Component
    {

        #region FIELDS

        private GameObject owner;

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
            get { return owner; }
            set { SetOwner(value); }
        }

        public virtual bool IsActive
        {
            get; set;
        }

        #endregion

        #region PUBLIC METHODS

        public virtual void SetOwner(GameObject owner)
        {
            this.owner = owner;
            if (this.owner != null)
            {
                Initialise();
            }
        }

        public virtual void Initialise()
        {

        }

        public abstract Component Clone();

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
