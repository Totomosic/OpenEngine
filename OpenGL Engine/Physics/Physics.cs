using System;

namespace OpenEngine
{
    public static class Physics
    {

        public static CollisionResolution TestNextPosition(Vector3 aabbPosition1, Vector3 aabbSize1, Vector3 aabbPosition2, Vector3 aabbSize2, Vector3 object1Velocity, Vector3 object2Velocity)
        {
            bool x, y, z;
            Vector3 obj1Pos = aabbPosition1;
            Vector3 obj2Pos = aabbPosition2;

            aabbPosition1 += new Vector3(0, object1Velocity.Y, 0);
            aabbPosition2 += new Vector3(0, object2Velocity.Y, 0);
            y = Intersects(aabbPosition1, aabbSize1, aabbPosition2, aabbSize2).Collided;

            aabbPosition1 = obj1Pos;
            aabbPosition2 = obj2Pos;
            aabbPosition1 += new Vector3(object1Velocity.X, 0, 0);
            aabbPosition2 += new Vector3(object2Velocity.X, 0, 0);
            x = Intersects(aabbPosition1, aabbSize1, aabbPosition2, aabbSize2).Collided;

            aabbPosition1 = obj1Pos;
            aabbPosition2 = obj2Pos;
            aabbPosition1 += new Vector3(0, 0, object1Velocity.Z);
            aabbPosition2 += new Vector3(0, 0, object2Velocity.Z);
            z = Intersects(aabbPosition1, aabbSize1, aabbPosition2, aabbSize2).Collided;

            return new CollisionResolution(x || y || z, 0, x, y, z);
        }

        public static CollisionResolution RayCast(Ray ray, Vector3 aabbPosition, Vector3 aabbSize)
        {
            float collLength;
            Vector3 dirfrac = new Vector3();
            dirfrac.X = 1f / ray.Direction.X;
            dirfrac.Y = 1f / ray.Direction.Y;
            dirfrac.Z = 1f / ray.Direction.Z;

            Vector3 BottomLeftCorner = aabbPosition - aabbSize * 0.5f;
            Vector3 TopRightCorner = aabbPosition + aabbSize * 0.5f;

            float t1 = (BottomLeftCorner.X - ray.Position.X) * dirfrac.X;
            float t2 = (TopRightCorner.X - ray.Position.X) * dirfrac.X;
            float t3 = (BottomLeftCorner.Y - ray.Position.Y) * dirfrac.Y;
            float t4 = (TopRightCorner.Y - ray.Position.Y) * dirfrac.Y;
            float t5 = (BottomLeftCorner.Z - ray.Position.Z) * dirfrac.Z;
            float t6 = (TopRightCorner.Z - ray.Position.Z) * dirfrac.Z;

            float tmin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));
            float tmax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

            // if tmax < 0, ray is intersecting but aabb is behind
            if (tmax < 0)
            {
                collLength = tmax;
                return new CollisionResolution(false, collLength);
            }

            // if tmin > tmax ray doesnt intersect
            if (tmin > tmax)
            {
                collLength = tmax;
                return new CollisionResolution(false, collLength);
            }

            collLength = tmin;
            return new CollisionResolution(collLength <= ray.Length, collLength, true, true, true);

        }

        public static CollisionResolution Contains(Vector3 containerPosition, Vector3 containerSize, Vector3 objectPosition, Vector3 objectSize)
        {
            bool x = false, y = false, z = false;
            if ((objectPosition - objectSize * 0.5f).X >= (containerPosition - containerSize * 0.5f).X && (objectPosition + objectSize * 0.5f).X <= (containerPosition + containerSize * 0.5f).X)
            {
                x = true;
            }
            if ((objectPosition - objectSize * 0.5f).Y >= (containerPosition - containerSize * 0.5f).Y && (objectPosition + objectSize * 0.5f).Y <= (containerPosition + containerSize * 0.5f).Y)
            {
                y = true;
            }
            if ((objectPosition - objectSize * 0.5f).Z >= (containerPosition - containerSize * 0.5f).Z && (objectPosition + objectSize * 0.5f).Z <= (containerPosition + containerSize * 0.5f).Z)
            {
                z = true;
            }
            return new CollisionResolution(x && y && z, 0, x, y, z);
        }

        public static CollisionResolution Contains(Vector2 containerPosition, Vector2 containerSize, Vector2 objectPosition, Vector2 objectSize)
        {
            return Contains(new Vector3(containerPosition, 0), new Vector3(containerSize, 10), new Vector3(objectPosition, 0), new Vector3(objectSize, 1));
        }

        public static CollisionResolution Intersects(Vector3 position1, Vector3 size1, Vector3 position2, Vector3 size2)
        {
            bool x = false, y = false, z = false;
            if ((position1 + size1 * 0.5f).X >= (position2 - size2 * 0.5f).X && (position1 - size1 * 0.5f).X <= (position2 + size2 * 0.5f).X)
            {
                x = true;
            }
            if ((position1 + size1 * 0.5f).Y >= (position2 - size2 * 0.5f).Y && (position1 - size1 * 0.5f).Y <= (position2 + size2 * 0.5f).Y)
            {
                y = true;
            }
            if ((position1 + size1 * 0.5f).Z >= (position2 - size2 * 0.5f).Z && (position1 - size1 * 0.5f).Z <= (position2 + size2 * 0.5f).Z)
            {
                z = true;
            }
            return new CollisionResolution(x && y && z, 0, x, y, z);
        }

    }
}
