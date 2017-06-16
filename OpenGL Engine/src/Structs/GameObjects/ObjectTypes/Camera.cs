﻿using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine
{
    public class Camera : GameObject
    {

        #region FIELDS

        private static bool firstCamera = true;

        #endregion

        #region CONSTRUCTORS

        public Camera(Viewport view, Vector3 position, CameraMode mode = CameraMode.FirstPerson, ProjectionType projection = ProjectionType.Perspective, Matrix4 rotationMatrix = default(Matrix4), float fov = 60, float zNear = 1, float zFar = 1000) : base(position, ComponentSetting.IsCamera)
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

        public CameraComponent CameraComponent
        {
            get { return Components.GetComponent<CameraComponent>(); }
            set { Components.AddComponent(value); }
        }

        public Matrix4 ViewMatrix
        {
            get { return CameraComponent.ViewMatrix; }
        }

        public Matrix4 ProjectionMatrix
        {
            get { return CameraComponent.ProjectionMatrix; }
            set { CameraComponent.ProjectionMatrix = value; }
        }

        public static Camera Main
        {
            get { return GameObjects.FindObjectByTag(Tags.MainCamera) as Camera; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}