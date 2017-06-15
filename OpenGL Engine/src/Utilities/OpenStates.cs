using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class OpenStates
    {

        #region FIELDS

        private static bool depthTest = false;
        private static bool cullFace = false;
        private static bool blendEnabled = false;
        private static bool clip0 = false;
        private static float lineWidth = 1f;
        private static bool multisampling = false;

        private static BlendingFactorDest destBlend = BlendingFactorDest.OneMinusSrcAlpha;
        private static BlendingFactorSrc srcBlend = BlendingFactorSrc.SrcAlpha;
        private static DepthFunction depthFunction = DepthFunction.Less;
        private static CullFaceMode cullFaceMode = CullFaceMode.Back;

        private static PolygonMode polyMode = PolygonMode.Fill;

        #endregion

        #region PROPERTIES

        public static bool DepthTest
        {
            get { return depthTest; }
        }

        public static bool CullFace
        {
            get { return cullFace; }
        }

        public static bool AlphaBlending
        {
            get { return blendEnabled; }
        }

        public static bool ClipPlane0
        {
            get { return clip0; }
        }

        public static DepthFunction DepthFunction
        {
            get { return depthFunction; }
        }

        public static CullFaceMode CullFaceMode
        {
            get { return cullFaceMode; }
        }

        public static BlendingFactorSrc SrcBlendFunction
        {
            get { return srcBlend; }
        }

        public static BlendingFactorDest DestBlendFunction
        {
            get { return destBlend; }
        }

        public static PolygonMode PolygonMode
        {
            get { return polyMode; }
        }

        public static float LineWidth
        {
            get { return lineWidth; }
        }

        public static bool Multisampling
        {
            get { return multisampling; }
        }

        #endregion

        #region PUBLIC METHODS

        public static void EnableDepth()
        {
            depthTest = true;
            GL.Enable(EnableCap.DepthTest);
        }

        public static void DisableDepth()
        {
            depthTest = false;
            GL.Disable(EnableCap.DepthTest);
        }

        public static void EnableCullFace()
        {
            cullFace = true;
            GL.Enable(EnableCap.CullFace);
        }

        public static void DisableCullFace()
        {
            cullFace = false;
            GL.Disable(EnableCap.CullFace);
        }

        public static void EnableBlend()
        {
            blendEnabled = true;
            GL.Enable(EnableCap.Blend);
        }

        public static void DisableBlend()
        {
            blendEnabled = false;
            GL.Disable(EnableCap.Blend);
        }

        public static void EnableClip0()
        {
            GL.Enable(EnableCap.ClipPlane0);
            clip0 = true;
        }

        public static void EnableClip(int index = 0)
        {
            GL.Enable(EnableCap.ClipPlane0 + index);
        }

        public static void DisableClip(int index = 0)
        {
            GL.Disable(EnableCap.ClipPlane0 + index);
        }

        public static void DisableClip0()
        {
            GL.Disable(EnableCap.ClipPlane0);
            clip0 = false;
        }

        public static void EnableMultisampling()
        {
            GL.Enable(EnableCap.Multisample);
            multisampling = true;
        }

        public static void DisableMultisampling()
        {
            GL.Disable(EnableCap.Multisample);
            multisampling = false;
        }

        public static void SetDepthFunction(DepthFunction function)
        {
            depthFunction = function;
            GL.DepthFunc(function);
        }

        public static void SetCullFaceMode(CullFaceMode mode)
        {
            cullFaceMode = mode;
            GL.CullFace(mode);
        }

        public static void SetSrcBlendFunc(BlendingFactorSrc src)
        {
            srcBlend = src;
            GL.BlendFunc(srcBlend, destBlend);
        }

        public static void SetDestBlendFunc(BlendingFactorDest dest)
        {
            destBlend = dest;
            GL.BlendFunc(srcBlend, destBlend);
        }

        public static void SetPolygonMode(PolygonMode mode, MaterialFace face = MaterialFace.FrontAndBack)
        {
            polyMode = mode;
            GL.PolygonMode(face, mode);
        }

        public static void SetLineWidth(float width)
        {
            lineWidth = width;
            GL.LineWidth(lineWidth);
        }

        public static void UpdateStates()
        {
            GL.BlendFunc(srcBlend, destBlend);
            GL.DepthFunc(depthFunction);
            GL.CullFace(cullFaceMode);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
