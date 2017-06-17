using System;
using System.Collections.Generic;

namespace OpenEngine.Components
{
    public class Text : Component
    {

        #region FIELDS

        private string text;
        private Font font;
        private float size;
        private Color color;

        #endregion

        #region CONSTRUCTORS

        public Text(string text, Font font, float size, Color color)
        {
            this.text = text;
            this.font = font;
            this.size = size;
            this.color = color;
        }

        public Text() : this("", null, 200, Color.White)
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

        public Font Font
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

        private void SetText(string text, Font font, float size, Color color)
        {
            this.text = text;
            this.font = font;
            this.size = size;
            this.color = color;
            if (Owner.MeshComponent != null)
            {
                Owner.Model.Dispose();
                Owner.MeshComponent = new Mesh(CreateModel(text, font, size, color));
            }
            else
            {
                Owner.Model = CreateModel(text, font, size, color);
            }
            Owner.Components.AddComponent(new Textures(font.FontImage));
        }

        public static Model CreateModel(string text, Font font, float textSize, Color color, bool italics = false, float desiredOffset = 0)
        {
            List<float> vertices = new List<float>();
            List<float> normals = new List<float>();
            List<float> tex = new List<float>();
            Vector2 cursorPos = new Vector2(0);

            float minX = 0;
            float maxX = 0;
            float minY = 1000000000;
            float maxY = 0;
            float offset = (italics) ? textSize * 0.05f + desiredOffset : desiredOffset;

            foreach (char chr in text)
            {
                Character character = font.GetCharacter(chr);
                float x = character.X / font.FontDimensions.X;
                float y = 1 - (character.Y / font.FontDimensions.Y);
                float w = character.Width / font.FontDimensions.X;
                float h = character.Height / font.FontDimensions.Y;
                float cx = cursorPos.X + character.XOffset / font.FontDimensions.X * textSize;
                float cy = cursorPos.Y - character.YOffset / font.FontDimensions.Y * textSize;
                float[] verts = { cx + offset, cy, 0, cx, cy - h * textSize, 0, cx + w * textSize, cy - h * textSize, 0, cx + offset, cy, 0, cx + w * textSize, cy - h * textSize, 0, cx + offset + w * textSize, cy, 0 };
                float[] norms = { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 };
                float[] texCoords = { x, y, x, y - h, x + w, y - h, x, y, x + w, y - h, x + w, y };
                vertices.AddRange(verts);
                normals.AddRange(norms);
                tex.AddRange(texCoords);

                if (cx + w * textSize > maxX)
                {
                    maxX = cx + w * textSize;
                }

                if (cy - h * textSize < minY)
                {
                    minY = cy - h * textSize;
                }

                cursorPos.X += character.XAdvance / font.FontDimensions.X * textSize;

            }

            Vector3 tmpSize = (new Vector3(Math.Abs(maxX - minX), Math.Abs(maxY - minY), 0));

            for (int i = 0; i < vertices.Count; i += 3)
            {
                vertices[i] -= tmpSize.X / 2f;
                vertices[i + 1] += tmpSize.Y / 2f;
            }

            tmpSize.Z = 1;
            Model model = ContentManager.LoadModel(vertices.ToArray(), normals.ToArray(), tex.ToArray(), color, 3);
            model.Size = tmpSize;
            return model;
        }

        #endregion
    }
}
