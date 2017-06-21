using System;
using System.Collections.Generic;

namespace OpenEngine.Components
{
    /// <summary>
    /// Component that represents the object's position and orientation in the world
    /// </summary>
    public class Transform : Component
    {

        #region FIELDS

        private Vector3 position;
        private Vector3 scale;
        private Matrix4 rotation;

        private Vector3 prevPosition;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructs a new transform
        /// </summary>
        /// <param name="position">World space position</param>
        /// <param name="scale">Scale of object</param>
        /// <param name="rotation">Local rotation matrix</param>
        public Transform(Vector3 position, Vector3 scale, Matrix4 rotation = default(Matrix4))
        {
            this.position = position;
            this.prevPosition = this.position;
            this.scale = scale;
            this.rotation = (rotation == default(Matrix4)) ? Matrix4.Identity : rotation;
        }

        /// <summary>
        /// Constructs a new transform
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z position</param>
        /// <param name="rotation">Local rotation matrix</param>
        public Transform(float x, float y, float z, Matrix4 rotation = default(Matrix4)) : this(new Vector3(x, y, z), rotation)
        {

        }

        /// <summary>
        /// Constructs a new transform
        /// </summary>
        /// <param name="position">World space position</param>
        /// <param name="rotation">Local rotation matrix</param>
        public Transform(Vector3 position, Matrix4 rotation = default(Matrix4)) : this(position, new Vector3(1, 1, 1), rotation)
        {

        }

        /// <summary>
        /// Constructs a default transform at position (0, 0, 0)
        /// </summary>
        public Transform() : this(new Vector3())
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Get and set the world space position
        /// </summary>
        public virtual Vector3 Position
        {
            get { return GetPosition(); }
            set { position = value; }
        }

        /// <summary>
        /// X component of position
        /// </summary>
        public virtual float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        /// <summary>
        /// Y component of position
        /// </summary>
        public virtual float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        /// <summary>
        /// Z component of position
        /// </summary>
        public virtual float Z
        {
            get { return position.Z; }
            set { position.Z = value; }
        }

        /// <summary>
        /// Object scaling
        /// </summary>
        public virtual Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Object's local rotation
        /// </summary>
        public virtual Matrix4 Rotation
        {
            get { return GetRotation(); }
            set { rotation = value; }
        }

        /// <summary>
        /// Gets the vector in the forward direction relative to this object
        /// </summary>
        public virtual Vector3 Forward
        {
            get { return -Rotation[2].XYZ; }
        }

        /// <summary>
        /// Gets the vector in the right direction relative to this object
        /// </summary>
        public virtual Vector3 Right
        {
            get { return Rotation[0].XYZ; }
        }

        /// <summary>
        /// Gets the vector in the up direction relative to this object
        /// </summary>
        public virtual Vector3 Up
        {
            get { return Rotation[1].XYZ; }
        }

        /// <summary>
        /// Get the full model matrix that includes the rotation and position
        /// </summary>
        public virtual Matrix4 ModelMatrix
        {
            get { return GetModelMatrix(); }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Sets the scaling of this transform
        /// </summary>
        /// <param name="scale">Scaling in all cardinal directions</param>
        public virtual void SetScaling(float scale)
        {
            Scale = new Vector3(scale);
        }

        /// <summary>
        /// Rotate this transform
        /// </summary>
        /// <param name="angle">Angle to rotate by</param>
        /// <param name="axis">Axis to rotate around</param>
        /// <param name="space">Rotation space</param>
        /// <param name="type">Angle specification</param>
        public virtual void Rotate(float angle, Vector4 axis, Space space = Space.Local, AngleType type = AngleType.Radians)
        {
            Rotation *= CalculateRotation(angle, axis, space, type);
        }

        /// <summary>
        /// Gets the model matrix
        /// </summary>
        /// <returns></returns>
        public virtual Matrix4 GetModelMatrix()
        {
            Matrix4 modelMatrix = Rotation;
            modelMatrix *= Matrix4.CreateTranslation(Position);
            modelMatrix *= Matrix4.CreateScaling(Scale);
            return modelMatrix;
        }

        /// <summary>
        /// Gets a clone of this transform
        /// </summary>
        /// <returns></returns>
        public override Component Clone()
        {
            Transform transform = new Transform();
            transform.Position = position;
            transform.Rotation = rotation;
            transform.Scale = scale;
            return transform;
        }

        #endregion

        #region PRIVATE METHODS

        private Vector3 GetPosition()
        {
            // If has a parent, the position is relative to its parents position
            if (Owner.HasComponent<Parent>())
            {
                Transform t = Owner.GetComponent<Parent>().ParentObject.Transform;
                return position + t.Position;
            }
            return position;
        }

        private Matrix4 GetRotation()
        {
            if (Owner.HasComponent<Parent>())
            {
                Transform t = Owner.GetComponent<Parent>().ParentObject.Transform;
                return t.Rotation * rotation;
            }
            return rotation;
        }

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