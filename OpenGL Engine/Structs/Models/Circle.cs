using System;

namespace OpenEngine
{
    public class Circle : Ellipse
    {

        public Circle(Vector3 position, float radius, int detailLevel = 60, SubImage subImage = null, Matrix4 rotationMatrix = default(Matrix4)) : base(position, new Vector2(radius, radius), detailLevel, subImage, rotationMatrix)
        {

        }

        public Circle(float x, float y, float z, float radius, int detailLevel = 60, SubImage subImage = null, Matrix4 rotationMatrix = default(Matrix4)) : this(new Vector3(x, y, z), radius, detailLevel, subImage, rotationMatrix)
        {

        }

    }
}
