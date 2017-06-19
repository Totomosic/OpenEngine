using System;
using System.Collections.Generic;

namespace OpenEngine
{
    /// <summary>
    /// Class that manages all timers
    /// </summary>
    public static class Timers
    {

        #region FIELDS

        private static Dictionary<string, Timer> timers;
        private static int currentID;

        #endregion

        #region CONSTRUCTORS

        static Timers()
        {
            timers = new Dictionary<string, Timer>();
            currentID = 0;
        }

        #endregion

        #region PROPERTIES

        public static Dictionary<string, Timer> CurrentTimers
        {
            get { return timers; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Creates a new timer
        /// </summary>
        /// <param name="name">Timer name</param>
        /// <param name="timeInSeconds">Time in Seconds</param>
        /// <param name="repeat">Repeat mode</param>
        /// <param name="time">Specify a differnt GameTime Object</param>
        /// <returns></returns>
        public static Timer CreateNew(string name, float timeInSeconds, RepeatType repeat = RepeatType.Repeat, GameTime time = null)
        {
            Timer timer = new Timer((time == null) ? Context.Window.Time : time, timeInSeconds, repeat, false);
            timers.Add(name, timer);
            return timer;
        }

        /// <summary>
        /// Add a pre-existing timer
        /// </summary>
        /// <param name="timer">Timer to add</param>
        public static void Add(Timer timer)
        {
            timers.Add(currentID.ToString(), timer);
            currentID++;
        }

        /// <summary>
        /// Update all timers, called automatically by Window.Update()
        /// </summary>
        public static void Update()
        {
            foreach (Timer timer in timers.Values)
            {
                timer.Update();
            }
        }

        /// <summary>
        /// Remove a timer
        /// </summary>
        /// <param name="name">Name of timer to remove</param>
        public static void RemoveTimer(string name)
        {
            timers.Remove(name);
        }

        /// <summary>
        /// Remove a timer
        /// </summary>
        /// <param name="timer">Timer object to remove</param>
        public static void RemoveTimer(Timer timer)
        {
            foreach (string name in timers.Keys)
            {
                if (timers[name] == timer)
                {
                    timers.Remove(name);
                    break;
                }
            }
        }

        /// <summary>
        /// Check if a specified timer has reached its goal time
        /// </summary>
        /// <param name="name">Name of timer</param>
        /// <returns></returns>
        public static bool Check(string name)
        {
            if (!timers.ContainsKey(name))
            {
                return false;
            }
            return timers[name].Check();
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
