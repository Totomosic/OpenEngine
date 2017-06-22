using System;
using System.Collections.Generic;

namespace OpenEngine.Components
{
    public class Text : Component
    {

        #region FIELDS

        private string text;
        private FreeTypeFont font;
        private float size;
        private Color color;

        #endregion

        #region CONSTRUCTORS

<<<<<<< master
        public Text(string text, FreeTypeFont font, float size, Color color)
=======
        public Text(string text, FreeTypeFont font, Color color, float size = 1)
>>>>>>> local
        {
            this.text = text;
            this.font = font;
            this.size = size;
            this.color = color;
        }

        public Text() : this("", null, Color.White)
        {

        }

        public override void Initialise()
        {
            SetText(text, font, size, color);
            Owner.Shader = ShaderProgram.Font;
            base.Initialise();
        }

        #endregion

        #region PROPERTIES

        public string Value
        {
            get { return text; }
            set { SetText(value, font, size, color); }
        }

        public FreeTypeFont Font
        {
            get { return font; }
            set { SetText(text, value, size, color); }
        }

        public float Size
        {
            get { return size; }
            set { SetText(text, font, value, color); }
        }

        public Color Color
        {
            get { return color; }
            set { SetText(text, font, size, value); }
        }

        #endregion

        #region PUBLIC METHODS

        public override Component Clone()
        {
            Text text = new Text();
            text.Value = Value;
            text.Font = Font;
            text.Size = Size;
            text.Color = Color;
            return text;
        }

        private void SetText(string text, FreeTypeFont font, float size, Color color)
        {
            this.text = text;
            this.font = font;
            this.size = size;
            this.color = color;
<<<<<<< master
=======
            Owner.Model = CreateModel(text, font, color, size);
            Owner.Transform.SetScaling(size);
>>>>>>> local
        }

        public static Model CreateModel(string text, FreeTypeFont font, Color color, float scale = 1)
        {
            List<float> vertices = new List<float>();
            List<float> normals = new List<float>();
            List<float> texCoords = new List<float>();

            float x = 0;
            float y = 0;

            foreach (char chr in text)
            {
                FreeTypeCharacter character = font.Characters[chr];

                float xpos = x + character.Bearing.X * scale;
                float ypos = y - (character.Size.Y - character.Bearing.Y) * scale;

                float w = character.Size.X * scale;
                float h = character.Size.Y * scale;

                vertices.AddRange(new float[] { xpos, ypos + h, 0, xpos, ypos, 0, xpos + w, ypos, 0, xpos, ypos + h, 0, xpos + w, ypos, 0, xpos + w, ypos + h, 0 });
                normals.AddRange(new float[] { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 });
                texCoords.AddRange(new float[] { 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0 });
<<<<<<< master

                x += (character.Advance >> 6) * scale;
=======
>>>>>>> local
            }

            VAO vao = new VAO(RenderMode.Arrays, vertices.Count / 3);
            vao.CreateAttribute(BufferLayout.Vertices, vertices.ToArray(), 3);
            vao.CreateAttribute(BufferLayout.Normals, normals.ToArray(), 3);
            vao.CreateAttribute(BufferLayout.TextureCoordinates, texCoords.ToArray(), 2);
            vao.CreateAttribute(BufferLayout.Color, color.ToVertexData(vao.VertexCount), 4);

            return new Model(vao);
        }

        #endregion
    }
}
