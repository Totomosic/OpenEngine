using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEngine
{
    public class SystemBehaviour
    {

        #region FIELDS

        private Random random;
        private float speed;

        #endregion

        #region CONSTRUCTORS

        public SystemBehaviour(float speed)
        {
            random = new Random();
            this.speed = speed;
        }

        #endregion

        #region PROPERTIES

        public Vector3 NextVelocity
        {
            get { return new Vector3((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f).Normalize() * speed; }
        }

        #endregion

        #region PUBLIC METHODS

        #endregion

    }
}
