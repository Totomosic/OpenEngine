using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Animator
    {

        #region FIELDS

        private AnimatedModel entity;

        private Animation currentAnimation;
        private float animationTime;

        #endregion

        #region CONSTRUCTORS

        public Animator(AnimatedModel model)
        {
            entity = model;
            animationTime = 0;
        }

        #endregion

        #region PROPERTIES

        public AnimatedModel Model
        {
            get { return entity; }
            set { entity = value; }
        }

        public Animation CurrentAnimation
        {
            get { return currentAnimation; }
            protected set { currentAnimation = value; }
        }

        public float AnimationTime
        {
            get { return animationTime; }
            protected set { animationTime = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public void PerformAnimation(Animation animation)
        {
            AnimationTime = 0;
            CurrentAnimation = animation;
        }

        public void Update(GameTime time)
        {
            if (CurrentAnimation == null)
            {
                return;
            }
            IncreaseAnimationTime(time);
            Dictionary<string, Matrix4> currentPose = CalculateCurrentAnimationPose();
            ApplyPoseToJoint(currentPose, entity.RootJoint, Matrix4.Identity);
        }

        #endregion

        #region PRIVATE METHODS

        private void IncreaseAnimationTime(GameTime time)
        {
            AnimationTime += time.ElapsedSeconds;
            if (AnimationTime > CurrentAnimation.Length)
            {
                AnimationTime %= CurrentAnimation.Length;
            }
        }

        private Dictionary<string, Matrix4> CalculateCurrentAnimationPose()
        {
            KeyFrame[] keyFrames = GetPreviousAndNextFrames();
            float progression = CalculateProgression(keyFrames[0], keyFrames[1]);
            return InterpolatePoses(keyFrames[0], keyFrames[1], progression);
        }

        private KeyFrame[] GetPreviousAndNextFrames()
        {
            KeyFrame[] allFrames = CurrentAnimation.KeyFrames;
            KeyFrame previousFrame = allFrames[0];
            KeyFrame nextFrame = allFrames[0];
            for (int i = 0; i < allFrames.Length; i++)
            {
                nextFrame = allFrames[i];
                if (nextFrame.TimeStamp > animationTime)
                {
                    break;
                }
                previousFrame = allFrames[i];
            }
            return new KeyFrame[] { previousFrame, nextFrame };
        }

        private float CalculateProgression(KeyFrame previous, KeyFrame next)
        {
            float totalTime = next.TimeStamp - previous.TimeStamp;
            float currentTime = AnimationTime - previous.TimeStamp;
            return currentTime / totalTime;
        }

        private Dictionary<string, Matrix4> InterpolatePoses(KeyFrame previous, KeyFrame next, float progression)
        {
            Dictionary<string, Matrix4> currentPose = new Dictionary<string, Matrix4>();
            foreach (string jointName in previous.JointKeyFrames.Keys)
            {
                JointTransform previousTransform = previous.JointKeyFrames[jointName];
                JointTransform nextTransform = next.JointKeyFrames[jointName];
                JointTransform currentTransform = JointTransform.Interpolate(previousTransform, nextTransform, progression);
                currentPose.Add(jointName, currentTransform.GetLocalTransform());
            }
            return currentPose;
        }

        private void ApplyPoseToJoint(Dictionary<string, Matrix4> currentPose, Joint joint, Matrix4 parentTransform)
        {
            Matrix4 currentLocalTransform = currentPose[joint.Name];
            Matrix4 currentTransform = parentTransform * currentLocalTransform;
            foreach (Joint childJoint in joint.Children)
            {
                ApplyPoseToJoint(currentPose, childJoint, currentTransform);
            }
            currentTransform *= joint.InverseBindTransform;
            joint.AnimatedTransform = currentTransform;
        }

        #endregion

    }
}
