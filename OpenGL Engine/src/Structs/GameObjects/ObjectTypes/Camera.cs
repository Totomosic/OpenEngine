using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine
{
    /// <summary>
    /// Class that represents a camera in the world
    /// </summary>
    public class Camera : GameObject
    {

        #region FIELDS

        /// <summary>
        /// Is this the first camera
        /// </summary>
        protected static bool firstCamera = true;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructs a new GameObject with components suitable for a camera
        /// </summary>
        /// <param name="view">Viewport to use</param>
        /// <param name="position">World space position</param>
        /// <param name="mode">Camera mode</param>
        /// <param name="projection">Projection Type</param>
        /// <param name="rotationMatrix">Initial rotation</param>
        /// <param name="fov">Field of view specified in degrees</param>
        /// <param name="zNear">Distance from camera to near clipping plane</param>
        /// <param name="zFar">Distnace from camera to far clipping plane</param>
        public Camera(Viewport view, Vector3 position, CameraMode mode = CameraMode.FirstPerson, ProjectionType projection = ProjectionType.Perspective, Matrix4 rotationMatrix = default(Matrix4), float fov = 60, float zNear = 1, float zFar = 1000) : base(position)
        {
            Components.AddComponent(new CameraComponent(view, mode, projection, Angles.ToRadians(fov), zNear, zFar));
            Transform.Rotation = (rotationMatrix == default(Matrix4)) ? Matrix4.Identity : rotationMatrix;

            if (firstCamera)
            {
                Tag = Tags.MainCamera;
                firstCamera = false;
            }
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the CameraComponent of this Camera
        /// </summary>
        public CameraComponent CameraComponent
        {
            get { return Components.GetComponent<CameraComponent>(); }
            set { Components.AddComponent(value); }
        }

        /// <summary>
        /// Gets the view matrix of this Camera
        /// </summary>
        public Matrix4 ViewMatrix
        {
            get { return CameraComponent.ViewMatrix; }
        }

        /// <summary>
        /// Gets and sets the projection matrix of this camera
        /// </summary>
        public Matrix4 ProjectionMatrix
        {
            get { return CameraComponent.ProjectionMatrix; }
            set { CameraComponent.ProjectionMatrix = value; }
        }

        /// <summary>
        /// Gets the main camera - camera tagged with Tags.MainCamera
        /// </summary>
        public static Camera Main
        {
            get { return GameObjects.FindObjectByTag(Tags.MainCamera) as Camera; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Convert a screenpoint into a worldpoint
        /// </summary>
        /// <param name="screenPoint">Screen point</param>
        /// <returns></returns>
        public Vector3 ScreenToWorldPoint(Vector3 screenPoint)
        {
            return CameraComponent.ScreenToWorldPoint(screenPoint);
        }

        /// <summary>
        /// Convert a screenpoint into a world space ray
        /// </summary>
        /// <param name="screenPoint">Screen point</param>
        /// <param name="length">Ray length</param>
        /// <returns></returns>
        public Ray ScreenToWorldRay(Vector3 screenPoint, float length = 1000)
        {
            return CameraComponent.ScreenToWorldRay(screenPoint, length);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
