using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class Text
    {
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
    }
}
