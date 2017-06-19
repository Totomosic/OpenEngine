using System;
using System.Collections.Generic;

namespace OpenEngine
{
    /// <summary>
    /// Class that represents a timer
    /// </summary>
    public class Timer
    {

        #region FIELDS

        private GameTime time;
        private float currentTime;
        private float goal;

        private bool running;
        private RepeatType repeat;
        private bool hasChecked;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Construct a new timer
        /// </summary>
        /// <param name="timer">GameTime object to use</param>
        /// <param name="seconds">Time in seconds</param>
        /// <param name="repeatType">Repeat model</param>
        /// <param name="add">Add to Timer Manager?</param>
        public Timer(GameTime timer, float seconds, RepeatType repeatType = RepeatType.Repeat, bool add = true)
        {
            time = timer;
            currentTime = 0;
            goal = seconds;
            repeat = repeatType;
            running = true;
            hasChecked = false;
            if (add)
            {
                Timers.Add(this);
            }
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Current time of timer
        /// </summary>
        public float CurrentTime
        {
            get { return currentTime; }
        }

        /// <summary>
        /// Specified end time of timer
        /// </summary>
        public float GoalTime
        {
            get { return goal; }
            set { SetGoal(value); }
        }

        /// <summary>
        /// Is timer running
        /// </summary>
        public bool Running
        {
            get { return running; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Check whether timer has reached goal
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            bool value = currentTime >= goal;
            hasChecked = value;
            return value;
        }

        /// <summary>
        /// Update timer by adding delta time to its current time
        /// </summary>
        public void Update()
        {
            currentTime += time.ElapsedSeconds;
            if (currentTime >= goal && hasChecked && repeat == RepeatType.Repeat)
            {
                currentTime = 0;
            }
        }

        /// <summary>
        /// Set timer's goal time
        /// </summary>
        /// <param name="seconds">Time in seconds</param>
        public void SetGoal(float seconds)
        {
            goal = seconds;
        }

        /// <summary>
        /// Pause timer
        /// </summary>
        public void Pause()
        {
            running = false;
        }

        /// <summary>
        /// Start / resume timer
        /// </summary>
        public void Start()
        {
            running = true;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
