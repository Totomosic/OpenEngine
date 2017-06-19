using System;

namespace OpenEngine
{
    /// <summary>
    /// Class that converts between angle formats
    /// </summary>
    public class Angles
    {
        /// <summary>
        /// Converts an angle in degrees to radians
        /// </summary>
        /// <param name="degrees">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static float ToRadians(float degrees)
        {
            return (float)(Math.PI * degrees / 180.0);
        }

        /// <summary>
        /// Converts an angle in radians to degrees
        /// </summary>
        /// <param name="radians">Angle in radians</param>
        /// <returns>Angle in degrees</returns>
        public static float ToDegrees(float radians)
        {
            return (float)(radians * (180.0 / Math.PI));
        }
    }
}
