using System;
using System.Collections.Generic;

namespace OpenEngine
{
    class OBJVertex
    {

        protected float x;
        protected float y;
        protected float z;

        public OBJVertex(float x_, float y_, float z_)
        {
            x = x_;
            y = y_;
            z = z_;
        }

        public float X
        {
            get { return x; }
        }

        public float Y
        {
            get { return y; }
        }

        public float Z
        {
            get { return z; }
        }

        public virtual float[] Values
        {
            get { return new float[] { x, y, z }; }
        }

    }
}
