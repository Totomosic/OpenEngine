using System;

namespace OpenEngine
{
    public class Angles
    {
        public static float ToRadians(float degrees)
        {
            return (float)(Math.PI * degrees / 180.0);
        }

        public static float ToDegrees(float radians)
        {
            return (float)(radians * (180.0 / Math.PI));
        }
    }
}
