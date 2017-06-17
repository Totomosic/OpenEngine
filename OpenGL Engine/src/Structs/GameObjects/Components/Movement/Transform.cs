using System;
using System.Collections.Generic;

namespace OpenEngine.Components
{
    public class Transform : Component
    {

        #region FIELDS

        private Vector3 position;
        private Vector3 scale;
        private Matrix4 rotation;

        private Vector3 prevPosition;

        #endregion

        #region CONSTRUCTORS

        public Transform(Vector3 position, Vector3 scale, Matrix4 rotation = default(Matrix4))
        {
            this.position = position;
            this.prevPosition = this.position;
            this.scale = scale;
            this.rotation = (rotation == default(Matrix4)) ? Matrix4.Identity : rotation;
        }

        public Transform(float x, float y, float z, Matrix4 rotation = default(Matrix4)) : this(new Vector3(x, y, z), rotation)
        {

        }

        public Transform(Vector3 position, Matrix4 rotation = default(Matrix4)) : this(position, new Vector3(1, 1, 1), rotation)
        {

        }

        public Transform() : this(new Vector3())
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public virtual float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public virtual float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public virtual float Z
        {
            get { return position.Z; }
            set { position.Z = value; }
        }

        public virtual Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public virtual Matrix4 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public virtual Vector3 Forward
        {
            get { return -rotation[2].XYZ; }
        }

        public virtual Vector3 Right
        {
            get { return rotation[0].XYZ; }
        }

        public virtual Vector3 Up
        {
            get { return rotation[1].XYZ; }
        }

        public virtual Matrix4 ModelMatrix
        {
            get { return GetModelMatrix(); }
        }

        #endregion

        #region PUBLIC METHODS

        public virtual bool HasMoved()
        {
            bool moved = position != prevPosition;
            prevPosition = position;
            return moved;
        }

        public virtual void Rotate(float angle, Vector4 axis, Space space = Space.Local, AngleType type = AngleType.Radians)
        {
            Rotation *= CalculateRotation(angle, axis, space, type);
        }

        public virtual Matrix4 GetModelMatrix()
        {
            Matrix4 modelMatrix = Rotation;
            modelMatrix *= Matrix4.CreateTranslation(Position);
            modelMatrix *= Matrix4.CreateScaling(Scale);
            return modelMatrix;
        }

        public override Component Clone()
        {
            Transform transform = new Transform();
            transform.Position = Position;
            transform.Rotation = Rotation;
            transform.Scale = Scale;
            return transform;
        }

        #endregion

        #region PRIVATE METHODS

        private Matrix4 CalculateRotation(float angle, Vector4 axis, Space space = Space.Local, AngleType type = AngleType.Radians)
        {
            if (space == Space.Local)
            {
                axis *= Rotation;
            }
            if (type == AngleType.Degrees)
            {
                angle = Angles.ToRadians(angle);
            }
            return Matrix4.CreateRotation(axis.XYZ, angle);
        }

        #endregion

    }
}