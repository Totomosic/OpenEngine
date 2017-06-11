using System;

namespace OpenEngine
{
    public static class Cuboid
    {

        public static Model CreateModel(float w, float h, float d, Color color, SubImage image = null, bool reverseWinding = false)
        {
            return CreateModel(new Vector3(w, h, d), color, image, reverseWinding);
        }

        public static Model CreateModel(Vector3 Size, Color color, SubImage image = null, bool reverseWinding = false)
        {

            float x = Size.X / 2f;
            float y = Size.Y / 2f;
            float z = Size.Z / 2f;

            Model Model = null;

            if (!reverseWinding)
            {
                float[] vertices = { -x, y, z, -x, -y, z, x, -y, z, -x, y, z, x, -y, z, x, y, z, x, y, -z, x, -y, -z, -x, -y, -z, x, y, -z, -x, -y, -z, -x, y, -z, x, y, z, x, -y, z, x, -y, -z, x, y, z, x, -y, -z, x, y, -z, -x, y, -z, -x, -y, -z, -x, -y, z, -x, y, -z, -x, -y, z, -x, y, z, -x, y, -z, -x, y, z, x, y, z, -x, y, -z, x, y, z, x, y, -z, x, -y, -z, x, -y, z, -x, -y, z, x, -y, -z, -x, -y, z, -x, -y, -z };
                float[] normals = { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0 };
                float[] tex;
                if (image != null)
                {
                    tex = image.GetCuboidTexArray();
                }
                else
                {
                    tex = new float[] { 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1 };
                }
                Model = ContentManager.LoadModel(vertices, normals, tex, color, 3);
            }
            else
            {
                //                                     front                                             back                                                  right                                                 left                                                 top                                                                   bottom
                float[] vertices = { x, y, z, x, -y, z, -x, -y, z, x, y, z, -x, -y, z, -x, y, z, -x, y, -z, -x, -y, -z, x, -y, -z, -x, y, -z, x, -y, -z, x, y, -z, x, y, -z, x, -y, -z, x, -y, z, x, y, -z, x, -y, z, x, y, z, -x, y, z, -x, -y, z, -x, -y, -z, -x, y, z, -x, -y, -z, -x, y, -z, x, y, -z, x, y, z, -x, y, z, x, y, -z, -x, y, z, -x, y, -z, -x, -y, -z, -x, -y, z, x, -y, z, -x, -y, -z, x, -y, z, x, -y, -z };
                float[] normals = { 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0 };
                float[] tex;
                if (image != null)
                {
                    tex = image.GetCuboidTexArray();
                }
                else
                {
                    tex = new float[] { 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1 };
                }
                Model = ContentManager.LoadModel(vertices, normals, tex, color, 3);
            }
            Model.CalculateSize();
            return Model;
        }

    }
}
