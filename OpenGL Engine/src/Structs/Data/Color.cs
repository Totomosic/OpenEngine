using System;
using System.Xml.Serialization;
using System.Linq;

namespace OpenEngine
{
    /// <summary>
    /// Struct that represents an RGBA Color
    /// </summary>
    public struct Color
    {

        #region FIELDS

        private static Random random = new Random();
        private Vector4 rgba;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Create color from RGBA components
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha</param>
        public Color(byte r, byte g, byte b, byte a)
        {
            rgba = new Vector4(r, g, b, a);
        }

        /// <summary>
        /// Creates color from RGB components (a = 255)
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        public Color(byte r, byte g, byte b) : this(r, g, b, 255)
        {

        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets a random color
        /// </summary>
        public static Color Random
        {
            get { return FromFloats((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1); }
        }

        /// <summary>
        /// Gets RGBA components as a 4D vector
        /// </summary>
        public Vector4 RGBA
        {
            get { return rgba; }
            set { rgba = value; }
        }

        /// <summary>
        /// Red Component
        /// </summary>
        [XmlIgnore]
        public byte R
        {
            get { return (byte)rgba.X; }
            set { rgba.X = value; }
        }

        /// <summary>
        /// Green Component
        /// </summary>
        [XmlIgnore]
        public byte G
        {
            get { return (byte)rgba.Y; }
            set { rgba.Y = value; }
        }

        /// <summary>
        /// Blue Component
        /// </summary>
        [XmlIgnore]
        public byte B
        {
            get { return (byte)rgba.Z; }
            set { rgba.Z = value; }
        }

        /// <summary>
        /// Alpha Component
        /// </summary>
        [XmlIgnore]
        public byte A
        {
            get { return (byte)rgba.W; }
            set { rgba.W = value; }
        }

        /// <summary>
        /// Normalised Red Component
        /// </summary>
        public float NR
        {
            get { return R / 255f; }
        }

        /// <summary>
        /// Normalised Green Component
        /// </summary>
        public float NG
        {
            get { return G / 255f; }
        }

        /// <summary>
        /// Normalise Blue Component
        /// </summary>
        public float NB
        {
            get { return B / 255f; }
        }

        /// <summary>
        /// Normalised Alpha Component
        /// </summary>
        public float NA
        {
            get { return A / 255f; }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns a 4D vector of normalised color values (0 - 1)
        /// </summary>
        /// <returns></returns>
        public Vector4 GetNormalised()
        {
            return rgba / 255f;
        }

        /// <summary>
        /// Gets the brightness of this color (0 - 1)
        /// </summary>
        /// <returns></returns>
        public float Brightness()
        {
            return (R + G + B + A) / 4f / 255f;
        }

        #region STATIC CONSTRUCTORS

        /// <summary>
        /// Constructs a new color from normalised color values (0 - 1)
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha</param>
        /// <returns></returns>
        public static Color FromFloats(float r, float g, float b, float a = 1)
        {
            return new Color((byte)(r * 255), (byte)(g * 255), (byte)(b * 255), (byte)(a * 255));
        }

        public static Color Red
        {
            get { return new Color(255, 0, 0, 255); }
        }

        public static Color Green
        {
            get { return new Color(0, 255, 0, 255); }
        }

        public static Color Blue
        {
            get { return new Color(0, 0, 255, 255); }
        }

        public static Color White
        {
            get { return new Color(255, 255, 255, 255); }
        }

        public static Color Black
        {
            get { return new Color(0, 0, 0, 255); }
        }

        public static Color Orange
        {
            get { return new Color(255, 204, 51, 255); }
        }

        public static Color CornflowerBlue
        {
            get { return new Color(100, 200, 255, 255); }
        }

        public static Color Purple
        {
            get { return new Color(204, 0, 255, 255); }
        }

        public static Color Yellow
        {
            get { return new Color(255, 255, 0, 255); }
        }

        public static Color LightGrey
        {
            get { return new Color(200, 200, 200, 255); }
        }

        public static Color DarkGrey
        {
            get { return new Color(70, 70, 70, 255); }
        }

        public static Color Cyan
        {
            get { return new Color(0, 255, 255, 255); }
        }

        public static Color Brown
        {
            get { return new Color(139, 69, 19, 255); }
        }

        /// <summary>
        /// Gets the float array of normalised color values
        /// </summary>
        /// <returns></returns>
        public float[] ToFloat()
        {
            return new float[] { NR, NG, NB, NA };
        }

        /// <summary>
        /// Gets byte array of color values
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            return new byte[] { R, G, B, A };
        }

        /// <summary>
        /// Constructs an array of this color for specified vertices
        /// </summary>
        /// <param name="vertexCount">Number of vertices</param>
        /// <returns></returns>
        public float[] ToVertexData(int vertexCount)
        {
            return Enumerable.Repeat(ToFloat(), vertexCount).SelectMany(arr => arr).ToArray();
        }

        #endregion

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
