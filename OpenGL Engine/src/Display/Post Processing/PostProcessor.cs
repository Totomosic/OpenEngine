using System;

namespace OpenEngine
{
    public static class PostProcessor
    {

        private static FBO fbo;

        static PostProcessor()
        { 
            fbo = new FBO("PostProcessing", Context.Window.Width, Context.Window.Height, false);
            fbo.CreateColorTextureAttachment();
            fbo.CreateDepthTextureAttachment();
        }

        public static FBO Apply(Texture2D image, PostProcessingEffect effect)
        {
            fbo.SetSize(image.Width, image.Height);

            effect.ShaderProgram.Start();

            effect.ShaderProgram.SetUniformValue(effect.ShaderProgram.ModelMatrix, Matrix4.Identity);
            effect.ShaderProgram.SetUniformValue(effect.ShaderProgram.ViewMatrix, Matrix4.Identity);
            effect.ShaderProgram.SetUniformValue(effect.ShaderProgram.ProjectionMatrix, Matrix4.Identity);

            image.Bind();
            fbo.Bind();
            fbo.Clear();
            Model rect = Rectangle.CreateModel(new Vector2(2, 2), Color.White);
            rect.Render();
            rect.VAO.Delete();
            return fbo;
        }

    }
}
