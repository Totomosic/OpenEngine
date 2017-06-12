using OpenEngine.Components;
using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class Renderer
    {

        #region FIELDS

        private static int drawCallsPerFrame = 0;

        private static ShaderProgram fontProgram;

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

        public static void Render()
        {
            drawCallsPerFrame = 0;
            foreach (FBO renderTarget in BatchManager.Batches.Keys)
            {
                renderTarget.Bind();
                renderTarget.Clear();

                foreach (VertexBatch batch in BatchManager.Batches[renderTarget])
                {
                    if (batch.Config.ShaderProgram != ShaderManager.CurrentlyActiveShader)
                    {
                        batch.Config.ShaderProgram.Start();
                    }
                    RenderVertices(batch);
                }
            }
        }

        public static void RenderVertexBatch(VertexBatch batch)
        {
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
            VertexBatch batch = new VertexBatch(textModel, camera, Matrix4.CreateTranslation(position));
            batch.Config = new BatchConfig(BatchType.Dynamic, (renderTarget == null) ? Context.Window.Framebuffer : renderTarget, 0, FontShader, camera, new Texture[] { font.FontImage }, Matrix4.CreateTranslation(position));
            RenderVertexBatch(batch);
            batch.Delete();
            textModel.VAO.Delete();
        }

        public static void RenderText(Vector3 position, string text, Font font, float textSize, Color color, bool italics = false)
        {
            Camera camera = new Camera(Context.Window.View, new Vector3(0, 0, 10), CameraMode.FirstPerson, ProjectionType.Orthographic);
            RenderText(camera, position, text, font, textSize, color, italics);
            camera.Destroy();
        }

        public static void RenderModel(GameObject camera, Vector3 position, Model model, FBO renderTarget = null)
        {
            VertexBatch v = new VertexBatch(model, camera, Matrix4.CreateTranslation(position));
            v.Config.RenderTarget = (renderTarget == null) ? Context.Window.Framebuffer : renderTarget;
            RenderVertexBatch(v);
            v.Delete();
        }

        #endregion

        #region PRIVATE METHODS

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
