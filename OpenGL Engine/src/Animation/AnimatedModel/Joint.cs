using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Joint
    {

        #region FIELDS

        private int index; // ID
        private string name;
        private List<Joint> children;

        private Matrix4 animatedTransform; // Model space transform relative to parent joint

        private Matrix4 localBindTransform; // Local - in relation to parent, Bind - in relation to model with no animation appied
        private Matrix4 inverseBindTransform;

        #endregion

        #region CONSTRUCTORS

        public Joint(int index, string name, Matrix4 localBindTransform)
        {
            this.index = index;
            this.name = name;
            this.localBindTransform = localBindTransform;

            AnimatedTransform = Matrix4.Identity;
            inverseBindTransform = Matrix4.Identity;

        }

        #endregion

        #region PROPERTIES

        public int ID
        {
            get { return index; }
            set { index = value; }
        }

        public int Index
        {
            get { return ID; }
            set { ID = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Joint> Children
        {
            get { return children; }
            set { children = value; }
        }

        public Matrix4 AnimatedTransform
        {
            get { return animatedTransform; }
            set { animatedTransform = value; }
        }

        public Matrix4 LocalBindTransform
        {
            get { return localBindTransform; }
            set { localBindTransform = value; }
        }

        public Matrix4 InverseBindTransform
        {
            get { return inverseBindTransform; }
            set { inverseBindTransform = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public void AddChild(Joint child)
        {
            Children.Add(child);
        }

        public void CalculateInverseBindTransform(Matrix4 parentBindTransform)
        {
            Matrix4 bindTransform = parentBindTransform * LocalBindTransform;
            inverseBindTransform = bindTransform.Inverse();

            foreach (Joint child in Children)
            {
                child.CalculateInverseBindTransform(bindTransform);
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
