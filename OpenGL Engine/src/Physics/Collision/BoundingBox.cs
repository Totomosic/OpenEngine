using System;

namespace OpenEngine
{
    /// <summary>
    /// Class representing a bounding box region
    /// </summary>
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

        /// <summary>
        /// Creates a bounding box from 2 corners of the box
        /// </summary>
        /// <param name="min">Min corner</param>
        /// <param name="max">Max corner</param>
        /// <returns></returns>
        public static BoundingBox CreateFromCorner(Vector3 min, Vector3 max)
        {
            return new BoundingBox(min, max);
        }

        public static BoundingBox CreateFromCorner(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            return CreateFromCorner(new Vector3(minX, minY, minZ), new Vector3(maxX, maxY, maxZ));
        }

        /// <summary>
        /// Creates a bounding box from a center position and total size
        /// </summary>
        /// <param name="centerPosition">Position of center</param>
        /// <param name="size">Total size</param>
        /// <returns></returns>
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

        /// <summary>
        /// Minimum corner
        /// </summary>
        public Vector3 Min
        {
            get { return min; }
            set { min = value; }
        }

        /// <summary>
        /// Maximum corner
        /// </summary>
        public Vector3 Max
        {
            get { return max; }
            set { max = value; }
        }

        /// <summary>
        /// Position of box center
        /// </summary>
        public Vector3 Position
        {
            get { return Min + (Max - Min) / 2f; }
            set { Min += value - Position; Max += value - Position; }
        }

        /// <summary>
        /// Total size of box
        /// </summary>
        public Vector3 Size
        {
            get { return Max - Min; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Test whether this box intersects another
        /// </summary>
        /// <param name="bbox">Other bounding box</param>
        /// <returns></returns>
        public CollisionResolution Intersects(BoundingBox bbox)
        {
            return Physics.Intersects(Position, Size, bbox.Position, bbox.Size);
        }

        /// <summary>
        /// Test whether a point is contained within this box
        /// </summary>
        /// <param name="point">Point</param>
        /// <returns></returns>
        public CollisionResolution Contains(Vector3 point)
        {
            return Physics.Intersects(Position, Size, point, new Vector3());
        }

        /// <summary>
        /// Test whether another bounding box is completely contained within this box
        /// </summary>
        /// <param name="bbox">Other bounding box</param>
        /// <returns></returns>
        public CollisionResolution Contains(BoundingBox bbox)
        {
            return Physics.Contains(Position, Size, bbox.Position, bbox.Size);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
