using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class JointTransform
    {

        #region FIELDS

        // Position and rotation - relative to parent joint
        private Vector3 position;
        private Quaternion rotation;

        #endregion

        #region CONSTRUCTORS

        public JointTransform(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        #endregion

        #region PROPERTIES

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Quaternion Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public Matrix4 GetLocalTransform()
        {
            Matrix4 matrix = Matrix4.CreateTranslation(Position);
            matrix *= Rotation.Matrix4;
            return matrix;
        }

        public static JointTransform Interpolate(JointTransform frameA, JointTransform frameB, float progression)
        {
            Vector3 pos = Interpolate(frameA.Position, frameB.Position, progression);
            Quaternion rot = Quaternion.Slerp(frameA.Rotation, frameB.Rotation, progression);
            return new JointTransform(pos, rot);
        }

        #endregion

        #region PRIVATE METHODS

        private static Vector3 Interpolate(Vector3 start, Vector3 end, float progression)
        {
            float x = start.X + (end.X - start.X) * progression;
            float y = start.Y + (end.Y - start.Y) * progression;
            float z = start.Z + (end.Z - start.Z) * progression;
            return new Vector3(x, y, z);
        }

        #endregion

    }
}
