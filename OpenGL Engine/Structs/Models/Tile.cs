using System;

namespace OpenEngine
{
    public static class Tile
    {

        public static Model CreateModel(float w, float h, Color color, SubImage image = null)
        {
            return CreateModel(new Vector2(w, h), color, image);
        }

        public static Model CreateModel(Vector2 Size, Color color, SubImage subImage = null)
        {
            float x = Size.X / 2f;
            float z = Size.Y / 2f;
            float[] verts = { -x, 0, -z, -x, 0, z, x, 0, z, -x, 0, -z, x, 0, z, x, 0, -z };
            float[] norms = { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, };
            float[] tex;
            if (subImage != null)
            {
                tex = subImage.GetRectangleTexArray();
            }
            else
            {
                tex = new float[] { 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1 };
            }
            Model model = ContentManager.LoadModel(verts, norms, tex, color, 3);
            model.CalculateSize();
            return model;
        }

    }
}
