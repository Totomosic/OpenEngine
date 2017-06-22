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
<<<<<<< master
            GameObject[] texts = GameObjects.GetAllObjectsWith(new Type[] { typeof(Transform), typeof(CameraReference), typeof(Shader), typeof(RenderTarget), typeof(MeshMaterial), typeof(Text) });
            foreach (GameObject entity in texts)
            {
                Transform transform = entity.Transform;
                Text textC = entity.GetComponent<Text>();
=======
            GameObject[] texts = GameObjects.GetAllObjectsWith(new Type[] { typeof(Transform), typeof(Text), typeof(CameraReference), typeof(Shader), typeof(RenderTarget), typeof(MeshMaterial) });
            foreach (GameObject entity in texts)
            {
                Transform transform = entity.Transform;
                Text text = entity.GetComponent<Text>();
>>>>>>> local
                CameraReference camera = entity.CameraReference;
                Shader shader = entity.ShaderComponent;
                RenderTarget renderTarget = entity.RenderTargetComponent;
                MeshMaterial material = entity.MeshMaterial;
<<<<<<< master
=======

                float x = 0;
                foreach (char chr in text.Value)
                {
                    Model model = Text.CreateModel(chr.ToString(), text.Font, text.Color);
                    Material mat = new Material(material.Material.DiffuseColor, text.Font.Characters[chr].Texture);
                    MeshPackage package = new MeshPackage(model, new MeshConfig(renderTarget.FBO, 0, shader.Program, camera.ID, transform.GetModelMatrix() * Matrix4.CreateTranslation(new Vector3(x, 0, 0))), mat);
                    RenderModelPackage(package);
                    model.Dispose();
                    x += (text.Font.Characters[chr].Advance >> 6) * text.Size;
                }

            }
        }
>>>>>>> local

                float x = 0;
                foreach (char chr in textC.Value)
                {
                    Model model = Text.CreateModel(chr.ToString(), textC.Font, textC.Color, textC.Size);
                    Material mat = new Material(material.Material.DiffuseColor, material.Material.SpecularColor, material.Material.Reflectivity, material.Material.ShineDamper, new Texture[] { textC.Font.Characters[chr].Texture });
                    MeshPackage package = new MeshPackage(model, new MeshConfig(renderTarget.FBO, 0, shader.Program, camera.ID, transform.GetModelMatrix() * Matrix4.CreateTranslation(new Vector3(x, 0, 0))), mat);
                    RenderModelPackage(package);
                    model.Dispose();
                    x += (textC.Font.Characters[chr].Advance >> 6) * textC.Size;
                }

            }
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
