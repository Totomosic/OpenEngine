using System;
using System.Collections.Generic;

namespace OpenEngine.Components
{
    public class CTexture : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CTexture(Dictionary<int, Texture> textures)
        {
            Textures = textures;
        }

        public CTexture(Texture[] textures) : this(new Dictionary<int, Texture>())
        {
            for (int i = 0; i < textures.Length; i++)
            {
                Textures.Add(i, textures[i]);
            }
        }

        public CTexture(Texture texture) : this(new Dictionary<int, Texture>())
        {
            Textures.Add(0, texture);
        }

        public CTexture() : this(new Dictionary<int, Texture>())
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Dictionary<int, Texture> Textures
        {
            get; set;
        }

        #endregion

    }
}
