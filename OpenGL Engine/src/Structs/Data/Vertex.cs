using System;

namespace OpenEngine
{
    public struct Vertex
    {

        #region FIELDS

        private Vector3 position;
        private Vector3 normal;
        private Vector3 texCoord;
        private Color color;

        #endregion

        #region CONSTRUCTORS

        public Vertex(Vector3 position, Vector3 normal, Vector3 texCoord, Color color)
        {
            this.position = position;
            this.normal = normal;
            this.texCoord = texCoord;
            this.color = color;
        }

        public Vertex(Vector3 position, Vector3 normal, Vector2 texCoord, Color color) : this(position, normal, new Vector3(texCoord, 0), color)
        {

        }

        #endregion

        #region PROPERTIES

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector3 Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        public Vector2 TexCoord
        {
            get { return texCoord.XY; }
            set { texCoord = new Vector3(value.X, value.Y, texCoord.Z); }
        }

        public Vector3 TexCoord3D
        {
            get { return texCoord; }
            set { texCoord = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public static Vertex[] ApplyMatrix4(Vertex[] vertices, Matrix4 matrix)
        {
            Vertex[] newVerts = new Vertex[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                newVerts[i] = vertices[i].ApplyMatrix4(matrix);
            }
            return newVerts;
        }

        public Vertex ApplyMatrix4(Matrix4 matrix)
        {
            Vector3 position = (new Vector4(Position, 1) * matrix).XYZ;
            Vector3 normal = (new Vector4(Normal, 0) * matrix).XYZ;
            return new Vertex(position, normal, TexCoord3D, Color);
        }

        public float[] GetPosition()
        {
            return position.ToFloat();
        }

        public float[] GetNormal()
        {
            return normal.ToFloat();
        }

        public float[] Get3DTexCoord()
        {
            return texCoord.ToFloat();
        }

        public float[] Get2DTexCoord()
        {
            return texCoord.XY.ToFloat();
        }

        public float[] GetColor()
        {
            return color.ToFloat();
        }

        #endregion

    }
}
