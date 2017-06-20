using System;

namespace OpenEngine.Components
{
    /// <summary>
    /// Component that represents a lifetime of an object
    /// </summary>
    public class Lifespan : Component
    {

        #region FIELDS

        private float time;
        private float currentTime;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructs a new lifespan component
        /// </summary>
        /// <param name="timeInSeconds">Lifetime in seconds</param>
        public Lifespan(float timeInSeconds)
        {
            time = timeInSeconds;
            currentTime = 0;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Lifespan() : this(0)
        {

        }

        #endregion

        #region PROPERTIES
        
        /// <summary>
        /// Total lifetime
        /// </summary>
        public float Lifetime
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// Current time alive
        /// </summary>
        public float CurrentLife
        {
            get { return currentTime; }
            set { currentTime = value; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns clone of this component
        /// </summary>
        /// <returns></returns>
        public override Component Clone()
        {
            Lifespan l = new Lifespan();
            l.Lifetime = Lifetime;
            return l;
        }

        #endregion

    }
}
