using System;

namespace OpenEngine.Components
{

    public class CCamera : Component
    {

        #region FIELDS

        private Matrix4 projectionMatrix;
        private Viewport view;

        private ProjectionType projection;
        private CameraMode mode;
        private float fov;
        private float zNear;
        private float zFar;

        #endregion

        #region CONSTRUCTORS

        public CCamera(Viewport view, CameraMode mode = CameraMode.FirstPerson, ProjectionType projection = ProjectionType.Perspective, float fov = (float)Math.PI / 3, float zNear = 1, float zFar = 1000)
        {
            this.view = view;
            this.mode = mode;
            this.projection = projection;
            this.fov = fov;
            this.zNear = zNear;
            this.zFar = zFar;

            ProjectionMatrix = CreateProjectionMatrix();
        }

        #endregion

        #region PROPERTIES

        public Matrix4 ViewMatrix
        {
            get { return GetViewMatrix(); }
        }

        public Matrix4 ProjectionMatrix
        {
            get { return projectionMatrix; }
            set { projectionMatrix = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public Matrix4 GetViewMatrix()
        {
            Matrix4 viewMatrix = Matrix4.Identity;
            CTransform transform = Owner.Transform;
            if (mode == CameraMode.FirstPerson)
            {
                viewMatrix = Matrix4.CreateTranslation(-transform.Position) * transform.Rotation.Inverse();
                return viewMatrix;
            }
            else
            {
                viewMatrix = transform.Rotation * Matrix4.CreateTranslation(-transform.Position);
                return viewMatrix;
            }
        }

        public Vector3 ScreenToWorld(Vector3 screenPosition)
        {
            Vector4 position = new Vector4(screenPosition, 1);
            Vector4 worldSpace = ViewMatrix.Inverse() * position;
            return worldSpace.XYZ;
        }

        public Ray ScreenToWorldRay(Vector3 screenPosition, float length = 1000)
        {
            Vector4 position = new Vector4(screenPosition, 1);
            position.Z = -1;
            Vector4 worldSpace = ViewMatrix.Inverse() * position;
            return new Ray(Owner.Transform.Position, worldSpace.XYZ.Normalize(), length);
        }

        #endregion

        #region PRIVATE METHODS

        private Matrix4 CreateProjectionMatrix()
        {
            if (projection == ProjectionType.Perspective)
            {
                return Matrix4.CreatePerspectiveFieldOfView(fov, view.Aspect, zNear, zFar);
            }
            else
            {
                return Matrix4.CreateOrthographic(view.Width, view.Height, zNear, zFar);
            }
        }

        #endregion

    }

}