using System;
using System.Collections.Generic;

namespace OpenEngine
{
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

        public static Timer CreateNew(string name, float timeInSeconds, RepeatType repeat = RepeatType.Repeat, GameTime time = null)
        {
            Timer timer = new Timer((time == null) ? Context.Window.Time : time, timeInSeconds, repeat, false);
            timers.Add(name, timer);
            return timer;
        }

        public static void Add(Timer timer)
        {
            timers.Add(currentID.ToString(), timer);
            currentID++;
        }

        public static void Update()
        {
            foreach (Timer timer in timers.Values)
            {
                timer.Update();
            }
        }

        public static void RemoveTimer(string name)
        {
            timers.Remove(name);
        }

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
