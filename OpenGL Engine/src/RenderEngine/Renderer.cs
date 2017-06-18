using OpenEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenEngine
{
    public static class Renderer
    {

        #region FIELDS

        private static int drawCallsPerFrame = 0;

        private static ShaderProgram fontProgram = ShaderProgram.Font;

        #endregion

        #region INIT

        #endregion

        #region PROPERTIES

        public static int DrawCallsPerFrame
        {
            get { return drawCallsPerFrame; }
        }

        public static ShaderProgram FontShader
        {
            get { return fontProgram; }
            set { fontProgram = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public static void RenderEntities()
        {
            drawCallsPerFrame = 0;
            GameObject[] entities = GameObjects.GetAllObjectsWith(new Type[] { typeof(Transform), typeof(Mesh), typeof(CameraReference), typeof(Shader), typeof(RenderTarget), typeof(MeshMaterial) });
            foreach (GameObject entity in entities)
            {
                Transform transform = entity.Transform;
                Mesh model = entity.MeshComponent;
                CameraReference camera = entity.CameraReference;
                Shader shader = entity.ShaderComponent;
                RenderTarget renderTarget = entity.RenderTargetComponent;
                MeshMaterial material = entity.MeshMaterial;
                MeshPackage package = new MeshPackage(model.Model, new MeshConfig(renderTarget.FBO, 0, shader.Program, camera.ID, transform.GetModelMatrix()), material.Material);
                RenderModelPackage(package);

            }
        }

        public static void RenderText(GameObject camera, Vector3 position, string text, Font font, float textSize, Color color, bool italics = false, FBO renderTarget = null)
        {
            if (FontShader == null) throw new RendererException("No font shader specified.");
            Model textModel = Text.CreateModel(text, font, textSize, color, italics);
            Material mat = new Material(font.FontImage);
            MeshPackage package = new MeshPackage(textModel, new MeshConfig((renderTarget == null) ? Context.Window.Framebuffer : renderTarget, 0, FontShader, camera, Matrix4.CreateTranslation(position)), mat);
            RenderModelPackage(package);
            ResourceManager.ReleaseReference(textModel);
        }

        public static void RenderText(Vector3 position, string text, Font font, float textSize, Color color, bool italics = false)
        {
            Camera camera = new Camera(Context.Window.Viewport, new Vector3(0, 0, 10), CameraMode.FirstPerson, ProjectionType.Orthographic);
            RenderText(camera, position, text, font, textSize, color, italics);
            camera.Destroy();
        }

        public static void RenderModel(GameObject camera, Vector3 position, Model model, Material material, FBO renderTarget = null)
        {
            MeshPackage package = new MeshPackage(model, new MeshConfig((renderTarget == null) ? Context.Window.Framebuffer : renderTarget, 0, Engine.Shader, camera, Matrix4.CreateTranslation(position)), material);
            RenderModelPackage(package);
        }

        #endregion

        #region PRIVATE METHODS

        private static void RenderModelPackage(MeshPackage model)
        {

            if (FBOManager.CurrentlyBoundFBO != model.Config.RenderTarget)
            {
                model.Config.RenderTarget.Bind();
                model.Config.RenderTarget.Clear();
            }
            if (ShaderManager.CurrentlyActiveShader != model.Config.ShaderProgram)
            {
                model.Config.ShaderProgram.Start();
            }

            model.Config.ShaderProgram.SetUniformValue(model.Config.ShaderProgram.ModelMatrix, model.Config.ModelMatrix);
            model.Config.ShaderProgram.SetUniformValue(model.Config.ShaderProgram.ViewMatrix, model.Config.Camera.Components.GetComponent<CameraComponent>().ViewMatrix);
            model.Config.ShaderProgram.SetUniformValue(model.Config.ShaderProgram.ProjectionMatrix, model.Config.Camera.Components.GetComponent<CameraComponent>().ProjectionMatrix);

            BindTextures(model.Material.Textures);

            model.Model.Render();

            drawCallsPerFrame++;
        }

        private static void BindTextures(List<Texture> textures)
        {
            if (textures != null && textures.Count > 0)
            {
                if (textures.Count == 1)
                {
                    textures[0].Bind();
                    return;
                }
                for (int i = 0; i < textures.Count; i++)
                {
                    textures[i].Bind(i);
                }
            }
        }

        #endregion

    }
}
