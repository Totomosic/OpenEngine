using System;
using System.Collections.Generic;
using Pencil.Gaming.Graphics;
using System.Xml.Serialization;

namespace OpenEngine
{
    public class Texture : GLObject
    {

        #region FIELDS

        private TextureTarget textureType;
        private Vector2 textureSize;
        private bool hasTransparency;

        #endregion

        #region CONSTRUCTORS

        public Texture(int texId, TextureTarget type, Vector2 size, bool transparent) : base(0, GLType.Texture)
        {
            ID = texId;
            textureType = type;
            textureSize = size;
            hasTransparency = transparent;
        }

        public Texture() : this(0, TextureTarget.Texture2D, new Vector2(), false)
        {

        }

        public override void Delete()
        {
            GL.DeleteTexture(ID);
            base.Delete();
        }

        #endregion

        #region PROPERTIES

        public bool HasTransparency
        {
            get { return hasTransparency; }
            set { hasTransparency = value; }
        }

        public TextureTarget Target
        {
            get { return textureType; }
            set { textureType = value; }
        }

        public Vector2 Size
        {
            get { return textureSize; }
            set { textureSize = value; }
        }

        public int Width
        {
            get { return (int)Size.X; }
        }

        public int Height
        {
            get { return (int)Size.Y; }
        }

        #endregion

        #region PUBLIC METHODS

        public override void Bind()
        {
            Bind(0);
        }

        public override void Unbind()
        {
            Unbind(0);
        }

        public void Bind(int textureBank = 0)
        {
            State = BindState.Bound;
            GL.ActiveTexture(TextureUnit.Texture0 + textureBank);
            GL.BindTexture(Target, ID);
        }

        public void Unbind(int textureBank = 0)
        {
            State = BindState.Unbound;
            GL.ActiveTexture(TextureUnit.Texture0 + textureBank);
            GL.BindTexture(Target, 0);
        }

        public void SetTextureParameter(TextureParameterName paramName, int value)
        {
            Bind();
            GL.TexParameter(Target, paramName, value);
            Unbind();
        }

        public virtual void SaveToFile(string filename, bool useTexturePath = true)
        {
            if (useTexturePath)
            {
                filename = Paths.TexturePath + filename;
            }
            TextureWriter.WriteTexture(this, filename);
        }

        public virtual byte[] GetPixels(PixelFormat format = PixelFormat.Bgra)
        {
            byte[] bytes = new byte[(int)(Size.X * Size.Y * 4)];
            Bind();
            GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
            GL.GetTexImage(Target, 0, format, PixelType.UnsignedByte, bytes);
            Unbind();
            return bytes;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
