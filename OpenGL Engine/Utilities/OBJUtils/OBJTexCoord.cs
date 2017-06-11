using System;
using System.Collections.Generic;

namespace OpenEngine
{
    class OBJTexCoord : OBJVertex
    {

        public OBJTexCoord(float x_, float y_) : base(x_, y_, 0)
        {

        }

        public override float[] Values
        {
            get { return new float[] { x, y }; }
        }

    }
}
