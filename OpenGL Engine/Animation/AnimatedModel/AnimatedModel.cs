using System;

namespace OpenEngine
{
    public class AnimatedModel : Model
    {

        #region FIELDS

        private Joint rootJoint;
        private int jointCount;

        private Animator animator;

        #endregion

        #region CONSTRUCTORS

        public AnimatedModel(VAO vao, Joint rootJoint, int jointCount) : base(vao)
        {
            this.rootJoint = rootJoint;
            this.jointCount = jointCount;

            this.animator = new Animator(this);
            rootJoint.CalculateInverseBindTransform(Matrix4.Identity);
        }

        #endregion

        #region PROPERTIES

        public Joint RootJoint
        {
            get { return rootJoint; }
            set { rootJoint = value; }
        }

        public int JointCount
        {
            get { return jointCount; }
            set { jointCount = value; }
        }

        public Animator Animator
        {
            get { return animator; }
            set { animator = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public override void Update(GameTime time)
        {
            Animator.Update(time);
        }

        public void PerformAnimation(Animation animation)
        {
            Animator.PerformAnimation(animation);
        }

        public Matrix4[] GetJointTransforms()
        {
            Matrix4[] jointMatrices = new Matrix4[JointCount];
            AddJointsToArray(RootJoint, jointMatrices);
            return jointMatrices;
        }

        #endregion

        #region PRIVATE METHODS

        private void AddJointsToArray(Joint headJoint, Matrix4[] jointMatrices)
        {
            jointMatrices[headJoint.Index] = headJoint.AnimatedTransform;
            foreach (Joint child in headJoint.Children)
            {
                AddJointsToArray(child, jointMatrices);
            }
        }

        #endregion

    }
}
