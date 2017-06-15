using System;
using Pencil.Gaming.Graphics;
using System.Xml.Serialization;

namespace OpenEngine
{
    public class Texture2D : Texture
    {
        public Texture2D(int id, Vector2 imageSize, bool transparent) : base(id, TextureTarget.Texture2D, imageSize, transparent)
        {
            
        }

        public Texture2D() : this(0, new Vector2(), false)
        {

        }

    }
}
