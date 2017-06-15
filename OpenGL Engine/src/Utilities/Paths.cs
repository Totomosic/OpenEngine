using System;
using System.IO;

namespace OpenEngine
{
    public class Paths
    {

        #region FIELDS

        // Root directory is the location of .sln file
        protected static string rootDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\"));

        // Shader paths
        protected static string shaderPath = "";
        protected static string vertexRelPath = "";
        protected static string fragmentRelPath = "";
        protected static string geometryRelPath = "";
        protected static string shaderExtension = ".glsl";
        protected static string fontExtension = ".fnt";

        protected static string texturePath = "";
        protected static string fontPath = "";
        protected static string modelPath = "";
        protected static string heightmapPath = "";
        protected static string videoPath = "";

        #endregion

        #region PROPERTIES

        public static string RootDirectory
        {
            get { return rootDirectory; }
            set { rootDirectory = value; }
        }

        public static string ShaderExtension
        {
            get { return shaderExtension; }
            set { shaderExtension = value; }
        }

        public static string ShaderPath
        {
            get { return rootDirectory + shaderPath; }
            set { shaderPath = value; }
        }

        public static string RelativeVertexShaderPath
        {
            get { return vertexRelPath; }
            set { vertexRelPath = value; }
        }

        public static string RelativeFragmentShaderPath
        {
            get { return fragmentRelPath; }
            set { fragmentRelPath = value; }
        }

        public static string RelativeGeometryShaderPath
        {
            get { return geometryRelPath; }
            set { geometryRelPath = value; }
        }

        public static string TexturePath
        {
            get { return rootDirectory + texturePath; }
            set { texturePath = value; }
        }

        public static string FontPath
        {
            get { return rootDirectory + fontPath; }
            set { fontPath = value; }
        }

        public static string ModelPath
        {
            get { return rootDirectory + modelPath; }
            set { modelPath = value; }
        }

        public static string HeightmapPath
        {
            get { return rootDirectory + heightmapPath; }
            set { heightmapPath = value; }
        }

        public static string VideoPath
        {
            get { return rootDirectory + videoPath; }
            set { videoPath = value; }
        }

        public static string FontExtension
        {
            get { return fontExtension; }
            set { fontExtension = value; }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
