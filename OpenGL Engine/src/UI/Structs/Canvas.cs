using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine.UI
{
    public class Canvas : Camera
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public Canvas(Viewport view) : base(view, new Vector3(0, 0, 900), CameraMode.FirstPerson, ProjectionType.Orthographic)
        {

        }

        public Canvas() : this(Context.Window.Viewport)
        {

        }

        #endregion

        #region PROPERTIES

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
