using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Character
    {

        #region FIELDS

        private int id;
        private int x;
        private int y;
        private int width;
        private int height;
        private int xOffset;
        private int yOffset;
        private int xAdvance;

        #endregion

        #region CONSTRUCTORS

        public Character(int chrId, int x_, int y_, int w, int h, int xOff, int yOff, int xAdv)
        {
            id = chrId;
            x = x_;
            y = y_;
            width = w;
            height = h;
            xOffset = xOff;
            yOffset = yOff;
            xAdvance = xAdv;
        }

        #endregion

        #region PROPERTIES

        public int ID
        {
            get { return id; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int XOffset
        {
            get { return xOffset; }
        }

        public int YOffset
        {
            get { return yOffset; }
        }

        public int XAdvance
        {
            get { return xAdvance; }
        }

        #endregion

    }
}
