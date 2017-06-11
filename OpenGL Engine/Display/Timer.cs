using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Timer
    {

        #region FIELDS

        private GameTime time;
        private float startTime;
        private float goal;

        private bool running;
        private RepeatType repeat;
        private int count;

        #endregion

        #region CONSTRUCTORS

        public Timer(GameTime timer, float seconds, RepeatType repeatType = RepeatType.Repeat)
        {
            time = timer;
            startTime = time.TotalSeconds;
            goal = seconds;
            repeat = repeatType;
            running = true;
            count = 0;
        }

        #endregion

        #region PROPERTIES

        public float StartTime
        {
            get { return startTime; }
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

        public bool Check()
        {
            if (!running)
            {
                startTime = time.TotalSeconds;
            }
            if (time.TotalSeconds - startTime >= goal && running)
            {
                startTime = time.TotalSeconds;
                if (repeat == RepeatType.None)
                {
                    Pause();
                }
                count++;
                return true;
            }
            return false;
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
