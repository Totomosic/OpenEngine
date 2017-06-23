using OpenEngine.Components;
using OpenEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenEngine
{
    /// <summary>
    /// Class that manages scene rendering
    /// </summary>
    public static class Renderer
    {

        #region FIELDS

        private static int drawCallsPerFrame = 0;

        private static ShaderProgram fontProgram = ShaderProgram.Font;

        #endregion

        #region INIT

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the number of draw calls per frame
        /// </summary>
        public static int DrawCallsPerFrame
        {
            get { return drawCallsPerFrame; }
        }

        /// <summary>
        /// Get and set the font shader
        /// </summary>
        public static ShaderProgram FontShader
        {
            get { return fontProgram; }
            set { fontProgram = value; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Render all GameObjects in the scene
        /// </summary>
        public static void RenderEntities()
        {
            drawCallsPerFrame = 0;
            GameObject[] entities = GameObjects.GetAllObjectsWith(new Type[] { typeof(Transform), typeof(Mesh), typeof(CameraReference), typeof(Shader), typeof(RenderTarget), typeof(MeshMaterial) });
            foreach (GameObject entity in entities)
            {
                if (!entity.HasComponent<Text>())
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
            GameObject[] texts = GameObjects.GetAllObjectsWith(new Type[] { typeof(Transform), typeof(Text), typeof(CameraReference), typeof(Shader), typeof(RenderTarget), typeof(MeshMaterial) });
            foreach (GameObject entity in texts)
            {
                Transform transform = entity.Transform;
                Text text = entity.GetComponent<Text>();
                CameraReference camera = entity.CameraReference;
                Shader shader = entity.ShaderComponent;
                RenderTarget renderTarget = entity.RenderTargetComponent;
                MeshMaterial material = entity.MeshMaterial;
                RenderString(text.Value, transform.GetModelMatrix(), text.Font, text.Color, text.Size, renderTarget.FBO, camera.ID);
            }
        }

        /// <summary>
        /// Render a mode in the world
        /// </summary>
        /// <param name="camera">Camera to render via</param>
        /// <param name="position">World space position</param>
        /// <param name="model">Model to render</param>
        /// <param name="material">Material to attach to model</param>
        /// <param name="renderTarget">Render target to render too, defaults to framebuffer</param>
        public static void RenderModel(GameObject camera, Vector3 position, Model model, Material material, FBO renderTarget = null)
        {
            MeshPackage package = new MeshPackage(model, new MeshConfig((renderTarget == null) ? Context.Window.Framebuffer : renderTarget, 0, Engine.Shader, camera, Matrix4.CreateTranslation(position)), material);
            RenderModelPackage(package);
        }

        /// <summary>
        /// Renders text to the screen
        /// </summary>
        /// <param name="text">String of characters</param>
        /// <param name="transform">Position to render at</param>
        /// <param name="font">FreeTypeFont</param>
        /// <param name="color">Text color</param>
        /// <param name="scale">Text scale</param>
        /// <param name="renderTarget">Render target, defaults to framebuffer</param>
        /// <param name="camera">Camera to render via, defaults to main canvas</param>
        public static void RenderString(string text, Matrix4 transform, FreeTypeFont font, Color color, float scale = 1, FBO renderTarget = null, GameObject camera = null)
        {
            bool depthSetting = OpenStates.DepthTest;
            OpenStates.DisableDepth();
            float x = 0;
            foreach (char chr in text)
            {
                Model model = Text.CreateModel(chr.ToString(), font, color, scale);
                Material mat = new Material(Color.White, font.Characters[chr].Texture);
                MeshPackage package = new MeshPackage(model, new MeshConfig((renderTarget == null) ? Context.Window.Framebuffer : renderTarget, 0, FontShader, (camera == null) ? Canvas.Main : camera, transform * Matrix4.CreateTranslation(new Vector3(x, 0, 0))), mat);
                RenderModelPackage(package);
                model.Dispose();
                x += (font.Characters[chr].Advance >> 6) * scale;
            }
            if (depthSetting)
            {
                OpenStates.EnableDepth();
            }
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

            BindMaterial(model.Material, model.Config.ShaderProgram);
            BindTextures(model.Material.Textures, model.Config.ShaderProgram);

            model.Model.Render();

            drawCallsPerFrame++;
        }

        private static void BindTextures(List<Texture> textures, ShaderProgram shader)
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
                    shader.SetUniformValue("Tex" + i.ToString(), i);
                }
            }
        }

        private static void BindMaterial(Material material, ShaderProgram shader)
        {
            shader.SetUniformValue("Material.Diffuse", material.DiffuseColor);
            shader.SetUniformValue("Material.Specular", material.SpecularColor);
            shader.SetUniformValue("Material.Reflectivity", material.Reflectivity);
            shader.SetUniformValue("Material.ShineDamper", material.ShineDamper);
        }

        #endregion

    }
}
