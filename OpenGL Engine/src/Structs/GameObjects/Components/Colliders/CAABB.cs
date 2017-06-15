using System;

namespace OpenEngine.Components
{
    public class CAABB : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public CAABB(Vector3 size, Vector3 positionOffset)
        {
            Size = size;
            Offset = positionOffset;
        }

        public CAABB(Vector3 size) : this(size, new Vector3())
        {

        }

        public CAABB(float x, float y, float z, float offsetX = 0, float offsetY = 0, float offsetZ = 0) : this(new Vector3(x, y, z), new Vector3(offsetX, offsetY, offsetZ))
        {
            
        }

        public CAABB() : this(new Vector3())
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

    }
}
