using OpenEngine.Components;
using System;

namespace OpenEngine.Components
{
    /// <summary>
    /// Component that represents a runnable script
    /// </summary>
    public abstract class Script : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructs a new script
        /// </summary>
        public Script()
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the GameTime object
        /// </summary>
        public GameTime GameTime
        {
            get { return Context.Window.Time; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Called once when the component is initialised. Owner property is set before this call.
        /// </summary>
        public override abstract void Initialise();

        /// <summary>
        /// Called once per frame
        /// </summary>
        public abstract void Update();

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
