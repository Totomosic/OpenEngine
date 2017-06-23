using System;
using System.Collections.Generic;
using SharpFont;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    /// <summary>
    /// A class that represents a Font in FreeType
    /// </summary>
    public class FreeTypeFont
    {

        #region FIELDS

        private static FreeTypeFont arial = new FreeTypeFont(@"Fonts\arial.ttf", 32, false);
        private static FreeTypeFont robotoBlack = new FreeTypeFont(@"Fonts\Roboto-Black.ttf", 32, false);

        private Dictionary<uint, FreeTypeCharacter> characters;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructs a new font from a given TrueType font File (.ttf)
        /// </summary>
        /// <param name="file">TrueType font file</param>
        /// <param name="size">Size of text in pixels</param>
        /// <param name="useFontPath">Use path described in Paths.FontPath</param>
        public FreeTypeFont(string file, uint size, bool useFontPath = true)
        {
            characters = new Dictionary<uint, FreeTypeCharacter>();

            Library lib = new Library();
            Face face = new Face(lib, (useFontPath) ? Paths.FontPath + file : file);
            face.SetPixelSizes(size, size);
            LoadCharacters(face);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Default Arial font, 32 size
        /// </summary>
        public static FreeTypeFont Arial
        {
            get { return arial; }
        }

        /// <summary>
        /// Default RobotoBlack font, 32 size
        /// </summary>
        public static FreeTypeFont RobotoBlack
        {
            get { return robotoBlack; }
        }

        /// <summary>
        /// Gets map of all characters associated with this font
        /// </summary>
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
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            for (uint i = 0; i < 1000; i++)
            {
                face.LoadChar(i, LoadFlags.Render, LoadTarget.Normal);
                int id = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, id);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.R8, face.Glyph.Bitmap.Width, face.Glyph.Bitmap.Rows, 0, PixelFormat.Red, PixelType.UnsignedByte, face.Glyph.Bitmap.Buffer);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new int[] { (int)TextureWrapMode.ClampToEdge });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new int[] { (int)TextureWrapMode.ClampToEdge });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Linear });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Linear });
                FreeTypeCharacter chr = new FreeTypeCharacter(new Texture2D(id, new Vector2(face.Glyph.Bitmap.Width, face.Glyph.Bitmap.Rows), true), new Vector2(face.Glyph.Bitmap.Width, face.Glyph.Bitmap.Rows), new Vector2(face.Glyph.BitmapLeft, face.Glyph.BitmapTop), face.Glyph.Advance.X.Value);
                characters.Add(i, chr);
            }
            face.Dispose();
        }

        #endregion

    }
}
