using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Font
    {

        #region FIELDS

        private static Font arial = ContentManager.Load<Font>(@"Fonts\Arial", null, false);

        private string filename;
        private TextureAtlas fontImage;
        private bool depthFields;

        private Dictionary<int, Character> characterSet;

        #endregion

        #region CONSTRUCTORS

        public Font(string file, TextureAtlas image, bool depthField)
        {
            filename = file;
            fontImage = image;
            depthFields = depthField;
            characterSet = new Dictionary<int, Character>();
        }

        #endregion

        #region PROPERIES

        public static Font Arial
        {
            get { return arial; }
        }

        public TextureAtlas FontImage
        {
            get { return fontImage; }
        }

        public Vector2 FontDimensions
        {
            get { return fontImage.Size; }
        }

        #endregion

        #region PUBLIC METHODS

        public void AddCharacter(Character @char)
        {
            characterSet.Add(@char.ID, @char);
        }

        public void AddCharacters(Character[] chars)
        {
            foreach (Character chr in chars)
            {
                AddCharacter(chr);
            }
        }

        public void Bind()
        {
            fontImage.Bind();
        }

        public void Unbind()
        {
            fontImage.Unbind();
        }

        public Character GetCharacter(int id)
        {
            return characterSet[id];
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
