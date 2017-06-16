using System;
using System.Collections.Generic;

namespace OpenEngine.Components
{
    public class Textures : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public Textures(Dictionary<int, Texture> textures)
        {
            TextureMap = textures;
        }

        public Textures(Texture[] textures) : this(new Dictionary<int, Texture>())
        {
            for (int i = 0; i < textures.Length; i++)
            {
                TextureMap.Add(i, textures[i]);
            }
        }

        public Textures(Texture texture) : this(new Dictionary<int, Texture>())
        {
            TextureMap.Add(0, texture);
        }

        public Textures() : this(new Dictionary<int, Texture>())
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Dictionary<int, Texture> TextureMap
        {
            get; set;
        }

        #endregion

        public override Component Clone()
        {
            Textures textures = new Textures();
            textures.TextureMap = TextureMap;
            return textures;
        }

    }
}
