using System;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class TextureCubeMap : Texture
    {

        public TextureCubeMap(int id, Vector2 imageSize, bool transparent) : base(id, TextureTarget.TextureCubeMap, imageSize, transparent)
        {

        }

        public TextureCubeMap() : this(0, new Vector2(), false)
        {

        }

    }
}
