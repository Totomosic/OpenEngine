using System;

namespace OpenEngine.Components
{
    public class Parent : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public Parent(GameObject parent)
        {
            ParentObject = parent;
        }

        public Parent() : this(null)
        {

        }

        #endregion

        #region PROPERTIES

        public GameObject ParentObject
        {
            get; set;
        }

        #endregion

        #region PUBLIC METHODS

        public override Component Clone()
        {
            Parent p = new Parent();
            p.ParentObject = ParentObject;
            return p;
        }

        #endregion
    }
}
