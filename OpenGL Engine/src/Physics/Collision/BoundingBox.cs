using System;

namespace OpenEngine
{
    public class BoundingBox
    {

        #region FIELDS

        private Vector3 min;
        private Vector3 max;

        #endregion

        #region CONSTRUCTORS

        private BoundingBox(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public static BoundingBox CreateFromCorner(Vector3 min, Vector3 max)
        {
            return new BoundingBox(min, max);
        }

        public static BoundingBox CreateFromCorner(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            return CreateFromCorner(new Vector3(minX, minY, minZ), new Vector3(maxX, maxY, maxZ));
        }

        public static BoundingBox CreateFromCenter(Vector3 centerPosition, Vector3 size)
        {
            return new BoundingBox(centerPosition - size / 2f, centerPosition + size / 2f);
        }

        public static BoundingBox CreateFromCenter(float x, float y, float z, float w, float h, float d)
        {
            return CreateFromCenter(new Vector3(x, y, z), new Vector3(w, h, d));
        }

        #endregion

        #region PROPERTIES

        public Vector3 Min
        {
            get { return min; }
            set { min = value; }
        }

        public Vector3 Max
        {
            get { return max; }
            set { max = value; }
        }

        public Vector3 Position
        {
            get { return Min + (Max - Min) / 2f; }
            set { Min += value - Position; Max += value - Position; }
        }

        public Vector3 Size
        {
            get { return Max - Min; }
        }

        #endregion

        #region PUBLIC METHODS

        public CollisionResolution Intersects(BoundingBox bbox)
        {
            return Physics.Intersects(Position, Size, bbox.Position, bbox.Size);
        }

        public CollisionResolution Contains(Vector3 point)
        {
            return Physics.Intersects(Position, Size, point, new Vector3());
        }

        public CollisionResolution Contains(BoundingBox bbox)
        {
            return Physics.Contains(Position, Size, bbox.Position, bbox.Size);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
