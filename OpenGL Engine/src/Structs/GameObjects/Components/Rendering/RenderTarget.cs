using System;

namespace OpenEngine.Components
{
    public class RenderTarget : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public RenderTarget(FBO renderTarget)
        {
            FBO = renderTarget;
        }

        public RenderTarget(string renderTargetName) : this(FBOManager.GetFBO(renderTargetName))
        {

        }

        public RenderTarget() : this("Framebuffer")
        {

        }

        #endregion

        #region PROPERTIES

        public FBO FBO
        {
            get; set;
        }

        #endregion

        public override Component Clone()
        {
            RenderTarget renderTarget = new RenderTarget();
            renderTarget.FBO = FBO;
            return renderTarget;
        }

    }
}
