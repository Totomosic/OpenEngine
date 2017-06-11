using System;

namespace OpenEngine
{
    public class Plane
    {

        private Vector3 normal;
        private float height;

        public Plane(Vector3 direction, float distance)
        {
            normal = direction;
            height = distance;
        }

        public Vector3 Normal
        {
            get { return normal; }
        }

        public float Height
        {
            get {
                if (normal.Y > 0)
                {
                    return -height;
                }
                else
                {
                    return height;
                }
            }
        }

    }
}
