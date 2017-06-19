using System;
using System.Collections.Generic;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public static class Engine
    {

        #region FIELDS

        private static bool useDefaultShader = true;
        private static string arrayDelimeter = "[INDEX]";

        private static bool useLighting = false;
        private static int maxLights = 10;

        private static uint maxEntities = 10000000;

        private static bool frustrumCulling = false;

        #endregion

        #region PROPERTIES

        public static bool UseDefaultShader
        {
            get { return useDefaultShader; }
            set { useDefaultShader = value; }
        }

        public static bool UseLighting
        {
            get { return useLighting; }
            set { useLighting = value; }
        }

        public static int MaxLights
        {
            get { return maxLights; }
            set { maxLights = value; }
        }

        public static string ArrayDelimeter
        {
            get { return arrayDelimeter; }
            set { arrayDelimeter = value; }
        }

        public static uint MaxEntities
        {
            get { return maxEntities; }
            set { maxEntities = value; }
        }

        public static ShaderProgram Shader
        {
            get { return ShaderProgram.Default; }
        }

        public static ShaderProgram FontShader
        {
            get { return ShaderProgram.Font; }
        }

        public static bool FrustrumCulling
        {
            get { return frustrumCulling; }
            set { frustrumCulling = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public static void EnableLighting()
        {
            useLighting = true;
        }

        public static void DisableLighting()
        {
            useLighting = false;
        }

        public static void EnableFrustrumCulling()
        {
            frustrumCulling = true;
        }

        public static void DisableFrustrumCulling()
        {
            frustrumCulling = false;
        }

        public static void RunSystems()
        {
            LightSystem.Update(Context.Window.Time);
            UpdateSystem.Update(Context.Window.Time);
            ScriptingSystem.Update(Context.Window.Time);
        }

        public static void Cleanup()
        {
        }

        #endregion

    }

    public class Utilities
    {

        public static float Clamp(float value, float min, float max)
        {
            if (value > max)
            {
                return max;
            }
            else if (value < min)
            {
                return min;
            }
            return value;
        }

        public static float Map(float value, float min, float max, float nmin, float nmax)
        {
            return ((value - min) / (max - min)) * (nmax - nmin) + nmin; 
        }

        public static int GetVertexCount(BeginMode mode)
        {
            switch (mode)
            {
                case BeginMode.Triangles:
                    return 3;

                case BeginMode.TriangleStrip:
                    return 3;
            }
            return 3;
        }

        public static float EuclideanDistance(Vector3 a, Vector3 b)
        {
            return (float)Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y) + (b.Z - a.Z) * (b.Z - a.Z));
        }

        public static bool IsOpposite(float value1, float value2)
        {
            if (value1 > 0)
            {
                return value2 <= 0;
            }
            else
            {
                return value2 > 0;
            }
        }

    }

    public static class World
    {
        public static Vector4 XAxis = new Vector4(1, 0, 0, 0);
        public static Vector4 YAxis = new Vector4(0, 1, 0, 0);
        public static Vector4 ZAxis = new Vector4(0, 0, -1, 0);
    }

    public static class ShaderController
    {

        // Interfaces with the Engine.Default shader
        public static void EnableTextures()
        {            
            Engine.Shader.AddRequest(new Request<bool>("UseTexture", true));
        }

        public static void DisableTextures()
        {            
            Engine.Shader.AddRequest(new Request<bool>("UseTexture", false));
        }

        public static void EnableLighting()
        {            
            Engine.Shader.AddRequest(new Request<bool>("UseLighting", true));
        }

        public static void DisableLighting()
        {            
            Engine.Shader.AddRequest(new Request<bool>("UseLighting", false));
        }

        public static void EnableInvertColors()
        {            
            Engine.Shader.AddRequest(new Request<bool>("InvertColors", true));
        }

        public static void DisableInvertColors()
        {            
            Engine.Shader.AddRequest(new Request<bool>("InvertColors", false));
        }

        public static void SetNumberOfLights(int num)
        {            
            Engine.Shader.AddRequest(new Request<int>("UsedLights", num));
        }

        public static void SetColor(Color color)
        {            
            Engine.Shader.AddRequest(new Request<Color>("Color", color));
        }


    }

    public static class Bitmask
    {
        public static bool HasFlag<T>(T flags, T flag) where T : struct
        {
            uint flagsValue = (uint)(object)flags;
            uint flagValue = (uint)(object)flag;

            return (flagsValue & flagValue) != 0;
        }

        public static void AddFlag<T>(ref T flags, T flag) where T : struct
        {
            uint flagsValue = (uint)(object)flags;
            uint flagValue = (uint)(object)flag;

            flags = (T)(object)(flagsValue | flagValue);
        }

        public static void RemoveFlag<T>(ref T flags, T flag) where T : struct
        {
            uint flagsValue = (uint)(object)flags;
            uint flagValue = (uint)(object)flag;

            flags = (T)(object)(flagsValue & (~flagValue));
        }
    }

}
