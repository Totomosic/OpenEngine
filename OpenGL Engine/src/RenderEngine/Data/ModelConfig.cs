using System;
using System.Collections.Generic;
using Pencil.Gaming.Graphics;
using System.Linq;

namespace OpenEngine
{
    public class ModelConfig 
    {

        #region FIELDS

        private FBO renderTarget;
        private int priority; // higher is rendered first
        private ShaderProgram shaderProgram;
        private Texture[] textures;

        private Matrix4 modelMatrix;
        private GameObject camera;

        #endregion

        #region CONSTRUCTORS

        public ModelConfig(FBO renderTarget, int priority, ShaderProgram shader, GameObject cameraEntity, Texture[] textures = null, Matrix4 modelMatrix = default(Matrix4))
        {
            this.renderTarget = renderTarget;
            this.priority = priority;
            shaderProgram = shader;
            this.textures = (textures == null) ? new Texture[0] : textures;
            this.modelMatrix = (modelMatrix == default(Matrix4)) ? Matrix4.Identity : modelMatrix;
            camera = cameraEntity;
        }

        #endregion

        #region PROPERTIES

        public FBO RenderTarget
        {
            get { return renderTarget; }
            set { renderTarget = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public ShaderProgram ShaderProgram
        {
            get { return shaderProgram; }
            set { shaderProgram = value; }
        }

        public Texture[] Textures
        {
            get { return textures; }
            set { textures = value; }
        }

        public Matrix4 ModelMatrix
        {
            get { return modelMatrix; }
            set { modelMatrix = value; }
        }

        public GameObject Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public override bool Equals(object obj)
        {
            return (this == (ModelConfig)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ModelConfig c1, ModelConfig c2)
        {
            return !(c1.RenderTarget != c2.RenderTarget || c1.ShaderProgram != c2.ShaderProgram  
                || !Enumerable.SequenceEqual(c1.Textures, c2.Textures) || c1.Priority != c2.Priority
                || c1.Camera != c2.Camera || c1.ModelMatrix != c2.ModelMatrix);
        }

        public static bool operator !=(ModelConfig c1, ModelConfig c2)
        {
            return !(c1 == c2);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
