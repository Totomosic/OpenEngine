using System;

namespace OpenEngine.Components
{
    public class BoxCollider : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public BoxCollider(Vector3 size, Vector3 positionOffset)
        {
            Size = size;
            Offset = positionOffset;
        }

        public BoxCollider(Vector3 size) : this(size, new Vector3())
        {

        }

        public BoxCollider(float x, float y, float z, float offsetX = 0, float offsetY = 0, float offsetZ = 0) : this(new Vector3(x, y, z), new Vector3(offsetX, offsetY, offsetZ))
        {
            
        }

        public BoxCollider() : this(new Vector3())
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Vector3 Offset
        {
            get; set;
        }

        public virtual Vector3 Size
        {
            get; set;
        }

        #endregion

        public override Component Clone()
        {
            BoxCollider aabb = new BoxCollider();
            aabb.Size = Size;
            aabb.Offset = Offset;
            return aabb;
        }

    }
}
