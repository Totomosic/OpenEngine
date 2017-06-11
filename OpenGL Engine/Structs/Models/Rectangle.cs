using System;

namespace OpenEngine
{
    public static class Rectangle
    {

        public static Model CreateModel(float w, float h, Color color, SubImage image = null)
        {
            return CreateModel(new Vector2(w, h), color, image);
        }

        public static Model CreateModel(Vector2 Size, Color color, SubImage subImage = null)
        {
            float x = Size.X / 2f;
            float y = Size.Y / 2f;
            float[] vertices = { -x, y, -x, -y, x, -y, -x, y, x, -y, x, y };
            float[] normals = { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1 };
            float[] texCoords;
            if (subImage != null)
            {
                texCoords = subImage.GetRectangleTexArray();
            }
            else
            {
                texCoords = new float[] { 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1 };
            }
            Model model = ContentManager.LoadModel(vertices, normals, texCoords, color, 2);
            model.CalculateSize();
            return model;
        }


    }
}
