using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class AnimatedTexture
    {

        #region FIELDS

        private int framesPerSecond;
        private int currentFrame;
        private TextureAtlas texture;
        private float realFrame;
        private bool running;
        private RepeatType repeatType;
        private int repeatCount;

        #endregion

        #region CONSTRUCTORS

        public AnimatedTexture(TextureAtlas tex, int fps, RepeatType repeat = RepeatType.Repeat, int startIndex = 0, bool run = true)
        {
            texture = tex;
            framesPerSecond = fps;
            currentFrame = startIndex;
            realFrame = 0;
            repeatType = repeat;
            running = run;
            repeatCount = 0;
        }

        #endregion

        #region PROPERTIES

        public TextureAtlas Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public int FramesPerSecond
        {
            get { return framesPerSecond; }
            set { framesPerSecond = value; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }

        public bool Running
        {
            get { return running; }
            set { running = value; }
        }

        public RepeatType RepeatType
        {
            get { return repeatType; }
            set { repeatType = value; }
        }

        public int RepeatCount
        {
            get { return repeatCount; }
        }

        public SubImage CurrentImage
        {
            get { return texture.GetSubImage(CurrentFrame); }
        }

        #endregion

        #region PUBLIC METHODS

        public void Update(GameTime time)
        {
            float neededTime = 1f / framesPerSecond;
            float elapsedTime = time.ElapsedSeconds;
            float advance = elapsedTime / neededTime;
            if (running)
            {
                realFrame += advance;
                if (realFrame >= texture.ImageCount)
                {
                    repeatCount++;
                }
                realFrame %= texture.ImageCount;
                currentFrame = (int)realFrame;
                if (currentFrame == texture.ImageCount && repeatType == RepeatType.None)
                {
                    Pause();
                }
            }
        }

        public void Bind(int unit = 0)
        {
            texture.Bind(unit);
        }

        public void Unbind(int unit = 0)
        {
            texture.Unbind(unit);
        }

        public void Start()
        {
            running = true;
        }

        public void Pause()
        {
            running = false;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
