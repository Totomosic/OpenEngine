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
            GameObject[] entities = ObjectPool.GetAllObjectsWith(new Type[] { typeof(CTransform), typeof(CModel), typeof(CCameraReference), typeof(CShader), typeof(CRenderTarget) });
            foreach (GameObject entity in entities)
            {
                CTransform transform = entity.Transform;
                CModel model = entity.ModelComponent;
                CCameraReference camera = entity.CameraReference;
                CShader shader = entity.ShaderComponent;
                CRenderTarget renderTarget = entity.RenderTargetComponent;
                Texture[] textures = null;
                if (entity.Components.HasComponent<CTexture>())
                {
                    textures = entity.Components.GetComponent<CTexture>().Textures.Values.ToArray();
                }

                RenderVertices(model.Model, new BatchConfig(renderTarget.FBO, 0, shader.Program, camera.ID, textures, transform.GetModelMatrix(), renderMode: model.Model.Mode));

            }
        }

        public static void RenderVertexBatch(VertexBatch batch)
        {
            if (FBOManager.CurrentlyBoundFBO != batch.Config.RenderTarget)
            {
                batch.Config.RenderTarget.Bind();
            }
            if (ShaderManager.CurrentlyActiveShader != batch.Config.ShaderProgram)
            {
                batch.Config.ShaderProgram.Start();
            }
            RenderVertices(batch);
        }

        public static void RenderText(GameObject camera, Vector3 position, string text, Font font, float textSize, Color color, bool italics = false, FBO renderTarget = null)
        {
            if (FontShader == null) throw new RendererException("No font shader specified.");
            Model textModel = Text.CreateModel(text, font, textSize, color, italics);
            RenderVertices(textModel, new BatchConfig((renderTarget == null) ? Context.Window.Framebuffer : renderTarget, 0, FontShader, camera, new Texture[] { font.FontImage }, Matrix4.CreateTranslation(position)));
            textModel.Dispose();
        }

        public static void RenderText(Vector3 position, string text, Font font, float textSize, Color color, bool italics = false)
        {
            Camera camera = new Camera(Context.Window.View, new Vector3(0, 0, 10), CameraMode.FirstPerson, ProjectionType.Orthographic);
            RenderText(camera, position, text, font, textSize, color, italics);
            camera.Destroy();
        }

        public static void RenderModel(GameObject camera, Vector3 position, Model model, FBO renderTarget = null)
        {
            RenderVertices(model, new BatchConfig((renderTarget == null) ? Context.Window.Framebuffer : renderTarget, 0, Engine.Shader, camera, null, Matrix4.CreateTranslation(position)));
        }

        #endregion

        #region PRIVATE METHODS

        private static void RenderVertices(Model model, BatchConfig config)
        {

            if (FBOManager.CurrentlyBoundFBO != config.RenderTarget)
            {
                config.RenderTarget.Bind();
                config.RenderTarget.Clear();
            }
            if (ShaderManager.CurrentlyActiveShader != config.ShaderProgram)
            {
                config.ShaderProgram.Start();
            }

            config.ShaderProgram.SetUniformValue(config.ShaderProgram.ModelMatrix, config.ModelMatrix);
            config.ShaderProgram.SetUniformValue(config.ShaderProgram.ViewMatrix, config.Camera.Components.GetComponent<CCamera>().ViewMatrix);
            config.ShaderProgram.SetUniformValue(config.ShaderProgram.ProjectionMatrix, config.Camera.Components.GetComponent<CCamera>().ProjectionMatrix);

            BindTextures(config.Textures);

            model.VAO.Render();

            drawCallsPerFrame++;
        }

        private static void RenderVertices(VertexBatch batch)
        {
            batch.Config.ShaderProgram.SetUniformValue(batch.Config.ShaderProgram.ModelMatrix, batch.Config.ModelMatrix);
            batch.Config.ShaderProgram.SetUniformValue(batch.Config.ShaderProgram.ViewMatrix, batch.Config.Camera.Components.GetComponent<CCamera>().ViewMatrix);
            batch.Config.ShaderProgram.SetUniformValue(batch.Config.ShaderProgram.ProjectionMatrix, batch.Config.Camera.Components.GetComponent<CCamera>().ProjectionMatrix);

            BindTextures(batch.Config.Textures);

            VAO vao = batch.GetVAO();
            vao.Render();

            drawCallsPerFrame++;
        }

        private static void BindTextures(Texture[] textures)
        {
            if (textures != null && textures.Length > 0)
            {
                if (textures.Length == 1)
                {
                    textures[0].Bind();
                    return;
                }
                for (int i = 0; i < textures.Length; i++)
                {
                    textures[i].Bind(i);
                }
            }
        }

        #endregion

    }
}
