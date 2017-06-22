using SharpFont;
using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public struct FreeTypeCharacter
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public FreeTypeCharacter(Texture tex, Vector2 size, Vector2 bearing, int advance)
        {
            Texture = tex;
            Size = size;
            Bearing = bearing;
            Advance = advance;
        }

        #endregion

        #region PROPERTIES

        public Texture Texture
        {
            get; set;
        }

        public Vector2 Size
        {
            get; set;
        }

        public Vector2 Bearing
        {
            get; set;
        }

        public int Advance
        {
            get; set;
        }

        #endregion

    }
}
