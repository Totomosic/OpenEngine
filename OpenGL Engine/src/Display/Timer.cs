using System;
using System.Collections.Generic;

namespace OpenEngine
{
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

        public float CurrentTime
        {
            get { return currentTime; }
        }

        public float GoalTime
        {
            get { return goal; }
            set { SetGoal(value); }
        }

        public bool Running
        {
            get { return running; }
        }

        #endregion

        #region PUBLIC METHODS

        public bool Check(bool addTime = true)
        {
            bool value = currentTime >= goal;
            hasChecked = value;
            return value;
        }

        public void Update()
        {
            currentTime += time.ElapsedSeconds;
            if (currentTime >= goal && hasChecked && repeat == RepeatType.Repeat)
            {
                currentTime = 0;
            }
        }

        public void SetGoal(float seconds)
        {
            goal = seconds;
        }

        public void Pause()
        {
            running = false;
        }

        public void Start()
        {
            running = true;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
