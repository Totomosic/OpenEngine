using System;
using System.Collections.Generic;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class FBO : GLObject
    {

        #region FIELDS

        private string name;

        private FramebufferTarget target;
        private Dictionary<BufferType, Texture2D> textures;
        private Dictionary<BufferType, int> renderBuffers;
        private int width;
        private int height;

        private Viewport view;
        private Color clearColor;

        #endregion

        #region CONSTUCTORS

        public FBO(string name, int w, int h, bool isPermanent = true, bool createBasicBuffers = true) : base(CreateObject(), GLType.Framebuffer)
        {
            this.name = name;
            width = w;
            height = h;
            target = FramebufferTarget.Framebuffer;
            clearColor = Color.Black;

            textures = new Dictionary<BufferType, Texture2D>();
            renderBuffers = new Dictionary<BufferType, int>();
            view = new Viewport(0, 0, w, h);

            if (isPermanent)
            {
                FBOManager.AddFBO(this);
            }
            if (createBasicBuffers)
            {
                CreateColorTextureAttachment();
                CreateDepthTextureAttachment();
            }

        }

        public override void Delete()
        {
            GL.DeleteFramebuffer(ID);
            base.Delete();
        }

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Color ClearColor
        {
            get { return clearColor; }
            set { clearColor = value; }
        }

        public Viewport View
        {
            get { return view; }
        }

        public FramebufferTarget Target
        {
            get { return target; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public void Clear(ClearBufferMask mask = ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit)
        {
            GL.ClearColor(ClearColor.NR, ClearColor.NG, ClearColor.NB, ClearColor.NA);
            if (State == BindState.Bound)
            {
                GL.Clear(mask);
            }
            else
            {
                Bind();
                Clear(mask);
            }
        }

        public void SetDrawBuffer(BufferType type)
        {
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0 + (int)type);
        }

        public void SetDrawBuffers(int num, BufferType[] buffers)
        {
            DrawBuffersEnum[] drawBuffers = new DrawBuffersEnum[buffers.Length];
            for (int i = 0; i < buffers.Length; i++)
            {
                drawBuffers[i] = DrawBuffersEnum.ColorAttachment0 + (int)buffers[i];
            }
            GL.DrawBuffers(num, drawBuffers);
        }

        public void SetSize(int w, int h)
        {
            width = w;
            height = h;
            view.Width = w;
            view.Height = h;
        }

        public override void Bind()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindFramebuffer(target, ID);
            State = BindState.Bound;
            view.Bind();
            FBOManager.SetAsBound(this);
        }

        public override void Unbind()
        {
            GL.BindFramebuffer(target, 0);
            State = BindState.Unbound;
            FBOManager.SetAsBound(null);
        }

        public Texture2D GetTexture(BufferType type = BufferType.Color0)
        {
            return textures[type];
        }

        public int GetRenderBufferID(BufferType type = BufferType.Color0)
        {
            return renderBuffers[type];
        }

        public void RecreateBuffer()
        {
            foreach (BufferType key in renderBuffers.Keys)
            {
                if ((int)key >= (int)BufferType.Depth0)
                {
                    Bind();
                    GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffers[key]);
                    GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent32, width, height);
                    GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
                }
                else if ((int)key <= (int)BufferType.Color9)
                {
                    Bind();
                    GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffers[key]);
                    GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Rgba8, width, height);
                    GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
                }
            }

            foreach (BufferType key in textures.Keys)
            {
                if ((int)key >= (int)BufferType.Depth0)
                {
                    Bind();
                    textures[key].Bind();
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent32, width, height, 0, PixelFormat.DepthComponent, PixelType.Float, default(IntPtr));
            }
                else if ((int)key <= (int)BufferType.Color9)
                {
                    Bind();
                    textures[key].Bind();
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, default(IntPtr));
                }
            }

        }

        public void CopyToFBO(FBO frameBuffer)
        {
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, frameBuffer.ID);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, ID);
            GL.BlitFramebuffer(0, 0, width, height, 0, 0, frameBuffer.Width, frameBuffer.Height, ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit, BlitFramebufferFilter.Nearest);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
        }

        public void SetAsFramebuffer()
        {
            CopyToFBO(Context.Window.Framebuffer);
        }

        public Texture2D CreateColorTextureAttachment(BufferType type = BufferType.Color0, PixelInternalFormat internalFormat = PixelInternalFormat.Rgb)
        {
            Bind();
            int texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, internalFormat, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, default(IntPtr));
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.Clamp);
            GL.FramebufferTexture(target, FramebufferAttachment.ColorAttachment0 + (int)type, texture, 0);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0 + (int)type);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            Texture2D tex2D = new Texture2D(texture, new Vector2(width, height), false);
            textures[type] = tex2D;
            return tex2D;
        }

        public Texture2D CreateDepthTextureAttachment(BufferType type = BufferType.Depth0)
        {
            Bind();
            int texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent32, width, height, 0, PixelFormat.DepthComponent, PixelType.Float, default(IntPtr));
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.Clamp);
            GL.FramebufferTexture(target, FramebufferAttachment.DepthAttachment, texture, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            Texture2D tex2D = new Texture2D(texture, new Vector2(width, height), false);
            textures[type] = tex2D;
            return tex2D;
        }

        public void CreateDepthBufferAttachment(BufferType type = BufferType.Depth0)
        {
            Bind();
            int depthBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, depthBuffer);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent32, width, height);
            GL.FramebufferRenderbuffer(target, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthBuffer);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            renderBuffers[type] = depthBuffer;
        }

        public void CreateColorBufferAttachment(BufferType type = BufferType.Color0)
        {
            Bind();
            int colorBuffer = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, colorBuffer);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Rgba8, width, height);
            GL.FramebufferRenderbuffer(target, FramebufferAttachment.ColorAttachment0 + (int)type, RenderbufferTarget.Renderbuffer, colorBuffer);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0 + (int)type);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            renderBuffers[type] = colorBuffer;
        }

        #endregion

        #region PRIVATE METHODS

        private static int CreateObject()
        {
            return GL.GenFramebuffer();
        }

        #endregion

    }
}
