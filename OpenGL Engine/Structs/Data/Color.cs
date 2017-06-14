using System;
using System.Xml.Serialization;
using System.Linq;

namespace OpenEngine
{
    public struct Color
    {

        #region FIELDS

        private static Random random = new Random();
        private Vector4 rgba;

        #endregion

        #region CONSTRUCTORS

        public Color(byte r, byte g, byte b, byte a)
        {
            rgba = new Vector4(r, g, b, a);
        }

        public Color(byte r, byte g, byte b) : this(r, g, b, 255)
        {

        }

        #endregion

        #region PROPERTIES

        public static Color Random
        {
            get { return FromFloats((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1); }
        }

        public Vector4 RGBA
        {
            get { return rgba; }
            set { rgba = value; }
        }

        [XmlIgnore]
        public byte R
        {
            get { return (byte)rgba.X; }
            set { rgba.X = value; }
        }

        [XmlIgnore]
        public byte G
        {
            get { return (byte)rgba.Y; }
            set { rgba.Y = value; }
        }

        [XmlIgnore]
        public byte B
        {
            get { return (byte)rgba.Z; }
            set { rgba.Z = value; }
        }

        [XmlIgnore]
        public byte A
        {
            get { return (byte)rgba.W; }
            set { rgba.W = value; }
        }

        public float NR
        {
            get { return R / 255f; }
        }

        public float NG
        {
            get { return G / 255f; }
        }

        public float NB
        {
            get { return B / 255f; }
        }

        public float NA
        {
            get { return A / 255f; }
        }

        #endregion

        #region PUBLIC METHODS

        public Vector4 GetNormalised()
        {
            return rgba / 255f;
        }

        public float Brightness()
        {
            return (R + G + B + A) / 4f / 255f;
        }

        #region STATIC CONSTRUCTORS

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

        public float[] ToFloat()
        {
            return new float[] { NR, NG, NB, NA };
        }

        public byte[] ToByte()
        {
            return new byte[] { R, G, B, A };
        }

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
