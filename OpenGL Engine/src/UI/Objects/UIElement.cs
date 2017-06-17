using System;
using OpenEngine.Components;

namespace OpenEngine.UI
{
    public class UIElement : GameObject
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public UIElement(Vector3 position) : base(position, Rectangle.CreateModel(100, 100, Color.White))
        {
            Shader = ShaderProgram.UI;
            CanvasObject = Canvas.Main;
        }

        #endregion

        #region PROPERTIES

        public GameObject CanvasObject
        {
            get { return CameraObject; }
            set { CameraObject = value; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
