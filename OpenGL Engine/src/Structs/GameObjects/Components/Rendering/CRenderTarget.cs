using System;

namespace OpenEngine.Components
{
    public class CRenderTarget : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CRenderTarget(FBO renderTarget)
        {
            FBO = renderTarget;
        }

        public CRenderTarget(string renderTargetName) : this(FBOManager.GetFBO(renderTargetName))
        {

        }

        public CRenderTarget() : this("Framebuffer")
        {

        }

        #endregion

        #region PROPERTIES

        public FBO FBO
        {
            get; set;
        }

        #endregion

    }
}
