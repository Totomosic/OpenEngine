using System;
using Pencil.Gaming;

namespace OpenEngine
{
    public class GameTime
    {

        #region FIELDS

        private double totalSeconds;
        private double elapsedSeconds;

        private double prevSeconds;

        #endregion

        #region CONSTRUCTORS

        public GameTime()
        {
            totalSeconds = 0;
            elapsedSeconds = 0;
            prevSeconds = 0;
        }

        #endregion

        #region PROPERTIES

        public float TotalSeconds
        {
            get { return (float)totalSeconds; }
        }

        public int TotalMilliseconds
        {
            get { return (int)(totalSeconds * 1000); }
        }

        public float ElapsedSeconds
        {
            get { return (float)elapsedSeconds; }
        }

        public int ElapsedMilliseconds
        {
            get { return (int)(elapsedSeconds * 1000); }
        }

        #endregion

        #region PUBLIC METHODS

        public void Tick()
        {
            totalSeconds = Glfw.GetTime();
            elapsedSeconds = totalSeconds - prevSeconds;
            prevSeconds = totalSeconds;
        }

        public void Reset()
        {
            totalSeconds = Glfw.GetTime();
            elapsedSeconds = 0;
            prevSeconds = totalSeconds;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
