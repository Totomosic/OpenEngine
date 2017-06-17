using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine.UI
{
    public class Canvas : Camera
    {

        #region FIELDS

        private static bool firstCanvas = true;

        #endregion

        #region CONSTRUCTORS

        public Canvas(Viewport view) : base(view, new Vector3(0, 0, 900), CameraMode.FirstPerson, ProjectionType.Orthographic, camera: false)
        {
            if (firstCanvas)
            {
                Tag = Tags.MainCanvas;
            }
        }

        public Canvas() : this(Context.Window.Viewport)
        {

        }

        #endregion

        #region PROPERTIES

        public new static Canvas Main
        {
            get { return GameObjects.FindObjectByTag(Tags.MainCanvas) as Canvas; }
        }

        public CameraComponent CanvasCamera
        {
            get { return CameraComponent; }
            set { CameraComponent = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
