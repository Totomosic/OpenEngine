using System;
using System.Collections.Generic;
using SharpFont;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class FreeTypeFont
    {

        #region FIELDS

        private Dictionary<uint, FreeTypeCharacter> characters;

        #endregion

        #region CONSTRUCTORS

        public FreeTypeFont(string file, uint size)
        {
            characters = new Dictionary<uint, FreeTypeCharacter>();

            Library lib = new Library();
            Face face = new Face(lib, file);
            face.SetPixelSizes(size, size);
            LoadCharacters(face);
        }

        #endregion

        #region PROPERTIES

        public Dictionary<uint, FreeTypeCharacter> Characters
        {
            get { return characters; }
        }
        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        private void LoadCharacters(Face face)
        {
            for (uint i = 0; i < 128; i++)
            {
                face.LoadChar(i, LoadFlags.Render, LoadTarget.Normal);
                int id = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, id);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.CompressedRed, face.Glyph.Metrics.Width.Value, face.Glyph.Metrics.Height.Value, 0, PixelFormat.Red, PixelType.UnsignedByte, face.Glyph.Bitmap.Buffer);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.ClampToEdge });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.ClampToEdge });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Linear });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Linear });
                FreeTypeCharacter chr = new FreeTypeCharacter(new Texture2D(id, new Vector2(face.Glyph.Metrics.Width.Value, face.Glyph.Metrics.Height.Value), true), new Vector2(face.Glyph.Metrics.Width.Value, face.Glyph.Metrics.Height.Value), new Vector2(face.Glyph.BitmapLeft, face.Glyph.BitmapTop), face.Glyph.Advance.X.Value);
                characters.Add(i, chr);
            }
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            face.Dispose();
        }

        #endregion

    }
}
