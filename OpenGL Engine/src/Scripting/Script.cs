using OpenEngine.Components;
using System;

namespace OpenEngine
{
    public abstract class Script : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public Script()
        {

        }

        #endregion

        #region PROPERTIES

        public GameObject GameObject
        {
            get { return Owner; }
        }

        public GameTime GameTime
        {
            get { return Context.Window.Time; }
        }

        #endregion

        #region PUBLIC METHODS

        public abstract void Initialise();

        public abstract void Update();

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
