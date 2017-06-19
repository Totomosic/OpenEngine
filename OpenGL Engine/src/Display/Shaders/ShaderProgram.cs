using System;
using System.IO;
using System.Collections.Generic;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class ShaderProgram
    {

        #region FIELDS

        private static ShaderProgram baseProgram = new ShaderProgram("Default", @"Shaders\base_v", @"Shaders\base_f", new ShaderData("Matrices.Model", "Matrices.View", "Matrices.Projection", new string[] { }, new string[] { "UseTexture", "UseLighting", "UsedLights", "Lights", "Color" }), false);
        private static ShaderProgram fontProgram = new ShaderProgram("Font", @"Shaders\font_v", @"Shaders\font_f", new ShaderData("Matrices.Model", "Matrices.View", "Matrices.Projection", new string[] { "Tex0" }, new string[] { "Color" }), false);
        private static ShaderProgram uiProgram = new ShaderProgram("UI", @"Shaders\ui_v", @"Shaders\ui_f", new ShaderData("Matrices.Model", "Matrices.View", "Matrices.Projection", new string[] { }, new string[] { "Color" }), false);
        private static ShaderProgram uiTextureProgram = new ShaderProgram("UITexture", @"Shaders\textureUI_v", @"Shaders\textureUI_f", new ShaderData("Matrices.Model", "Matrices.View", "Matrices.Projection", new string[] { "Tex0" }, new string[] { "Color" }), false);

        private string name;
        private string vFile;
        private string fFile;
        private uint programID;
        private Dictionary<string, int> uniformDict;
        private ShaderData info;

        private bool useShaderPath;
        private List<Request> uniformRequests;

        #endregion

        #region CONSTRUCTORS

        public ShaderProgram(string shaderName, string vShader, string fShader, ShaderData information, bool addShaderPath = true)
        {
            name = shaderName;
            vFile = vShader;
            fFile = fShader;
            useShaderPath = addShaderPath;
            uniformDict = new Dictionary<string, int>();
            info = information;
            uniformRequests = new List<Request>();

            programID = CreateProgram();
            Initialise(programID);

            ShaderManager.AddShader(this);

        }

        #endregion

        #region PROPERTIES

        public static ShaderProgram Default
        {
            get { return baseProgram; }
        }

        public static ShaderProgram Font
        {
            get { return fontProgram; }
        }

        public static ShaderProgram UI
        {
            get { return uiProgram; }
        }

        public static ShaderProgram UITexture
        {
            get { return uiTextureProgram; }
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual int ID
        {
            get { return (int)programID; }
        }

        public virtual ShaderData Information
        {
            get { return info; }
            set { info = value; }
        }

        public virtual string ModelMatrix
        {
            get { return Information.ModelMatrix; }
        }

        public virtual string ViewMatrix
        {
            get { return Information.ViewMatrix; }
        }

        public virtual string ProjectionMatrix
        {
            get { return Information.ProjectionMatrix; }
        }

        #endregion

        #region PUBLIC METHODS

        public virtual void Start()
        {
            ShaderManager.SetAsActive(this);
            GL.UseProgram(programID);
            ProcessRequests();
        }

        public virtual void Stop()
        {
            ShaderManager.SetAsInactive(this);
            GL.UseProgram(0);
        }

        public virtual void AddRequest(Request request)
        {
            if (ShaderManager.CurrentlyActiveShader == this)
            {
                // Immediately upload value if shader is already running
                request.UploadValue(this);
            }
            else
            {
                uniformRequests.Add(request);
            }
        }

        public virtual void ClearRequests()
        {
            uniformRequests.Clear();
        }

        #region UNIFORM UPLOADS

        protected void UploadValue(string varname, int value)
        {
            GL.Uniform1(GetUniformLocation(varname), value);
        }

        protected void UploadValue(string varname, bool value)
        {
            GL.Uniform1(GetUniformLocation(varname), (value) ? 1 : 0);
        }

        protected void UploadValue(string varname, float value)
        {
            GL.Uniform1(GetUniformLocation(varname), value);
        }

        protected void UploadValue(string varname, double value)
        {
            GL.Uniform1(GetUniformLocation(varname), value);
        }

        protected void UploadValue(string varname, byte value)
        {
            GL.Uniform1(GetUniformLocation(varname), value);
        }

        protected void UploadValue(string varname, short value)
        {
            GL.Uniform1(GetUniformLocation(varname), value);
        }

        protected void UploadValue(string varname, long value)
        {
            GL.Uniform1(GetUniformLocation(varname), value);
        }

        protected void UploadValue(string varname, Vector2 value)
        {
            GL.Uniform2(GetUniformLocation(varname), value.X, value.Y);
        }

        public void SetUniformValue(string varname, int x, int y)
        {
            GL.Uniform2(GetUniformLocation(varname), x, y);
        }

        public void SetUniformValue(string varname, float x, float y)
        {
            GL.Uniform2(GetUniformLocation(varname), x, y);
        }

        public void SetUniformValue(string varname, double x, double y)
        {
            GL.Uniform2(GetUniformLocation(varname), x, y);
        }

        protected void UploadValue(string varname, Vector3 value)
        {
            GL.Uniform3(GetUniformLocation(varname), value.X, value.Y, value.Z);
        }

        public void SetUniformValue(string varname, int x, int y, int z)
        {
            GL.Uniform3(GetUniformLocation(varname), x, y, z);
        }

        public void SetUniformValue(string varname, float x, float y, float z)
        {
            GL.Uniform3(GetUniformLocation(varname), x, y, z);
        }

        public void SetUniformValuei(string varname, int x, int y, int z)
        {
            GL.Uniform3(GetUniformLocation(varname), x, y, z);
        }

        public void SetUniformValue(string varname, double x, double y, double z)
        {
            GL.Uniform3(GetUniformLocation(varname), x, y, z);
        }

        protected void UploadValue(string varname, Vector4 value)
        {
            GL.Uniform4(GetUniformLocation(varname), value.X, value.Y, value.Z, value.W);
        }

        public void SetUniformValue(string varname, int x, int y, int z, int w)
        {
            GL.Uniform4(GetUniformLocation(varname), x, y, z, w);
        }

        public void SetUniformValue(string varname, float x, float y, float z, float w)
        {
            GL.Uniform4(GetUniformLocation(varname), x, y, z, w);
        }

        public void SetUniformValue(string varname, double x, double y, double z, double w)
        {
            GL.Uniform4(GetUniformLocation(varname), x, y, z, w);
        }

        protected void UploadValue(string varname, Matrix4 value)
        {
            GL.UniformMatrix4(GetUniformLocation(varname), 1, false, value.ToFloat());
        }

        protected void UploadValue(string varname, Matrix3 value)
        {
            GL.UniformMatrix3(GetUniformLocation(varname), 1, false, value.ToFloat());
        }

        protected void UploadValue(string varname, Color value)
        {
            GL.Uniform4(GetUniformLocation(varname), value.NR, value.NG, value.NB, value.NA);
        }

        public void SetUniformValue<T>(string varname, T[] values)
            where T : struct
        {
            if (varname.Contains(Engine.ArrayDelimeter))
            {
                string[] halfName = varname.Split(new string[] { Engine.ArrayDelimeter }, StringSplitOptions.None);
                for (int i = 0; i < values.Length; i++)
                {
                    if (halfName.Length == 2)
                    {
                        SetUniformValue(halfName[0] + "[" + i.ToString() + "]" + halfName[1], values[i]);
                    }
                    else
                    {
                        SetUniformValue(halfName[0] + "[" + i.ToString() + "]", values[i]);
                    }
                }
            }
            else
            {
                throw new ShaderManagementException("Specifying an array requires an array delimeter present in the variable name. Use Engine.ArrayDelimeter.");
            }
        }

        public void SetUniformValue<T>(string varname, T value)
            where T : struct
        {
            if (typeof(T) == typeof(int))
            {
                UploadValue(varname, (int)(object)value);
            }
            else if (typeof(T) == typeof(bool))
            {
                UploadValue(varname, (bool)(object)value);
            }
            else if (typeof(T) == typeof(short))
            {
                UploadValue(varname, (short)(object)value);
            }
            else if (typeof(T) == typeof(long))
            {
                UploadValue(varname, (long)(object)value);
            }
            else if (typeof(T) == typeof(double))
            {
                UploadValue(varname, (double)(object)value);
            }
            else if (typeof(T) == typeof(float))
            {
                UploadValue(varname, (float)(object)value);
            }
            else if (typeof(T) == typeof(byte))
            {
                UploadValue(varname, (byte)(object)value);
            }
            else if (typeof(T) == typeof(Vector2))
            {
                UploadValue(varname, (Vector2)(object)value);
            }
            else if (typeof(T) == typeof(Vector3))
            {
                UploadValue(varname, (Vector3)(object)value);
            }
            else if (typeof(T) == typeof(Vector4))
            {
                UploadValue(varname, (Vector4)(object)value);
            }
            else if (typeof(T) == typeof(Matrix3))
            {
                UploadValue(varname, (Matrix3)(object)value);
            }
            else if (typeof(T) == typeof(Matrix4))
            {
                UploadValue(varname, (Matrix4)(object)value);
            }
            else if (typeof(T) == typeof(Color))
            {
                UploadValue(varname, (Color)(object)value);
            }
            else
            {
                throw new ShaderManagementException("Type not supported!");
            }
        }

        #endregion

            #endregion

        #region PRIVATE METHODS

        private int GetUniformLocation(string varname)
        {
            if (uniformDict.ContainsKey(varname))
            {
                return uniformDict[varname];
            }
            else
            {
                int location = GL.GetUniformLocation(programID, varname);
                uniformDict.Add(varname, location);
                if (location == -1)
                {
                    Console.WriteLine("Could not find variable: " + varname + " in shader: " + Name);
                }
                return location;
            }
        }

        private void Initialise(uint id)
        {
            uint vShader = CreateShader(ShaderType.VertexShader);
            uint fShader = CreateShader(ShaderType.FragmentShader);
            if (useShaderPath)
            {
                GL.ShaderSource(vShader, ParseShaderSource(Paths.ShaderPath + vFile + Paths.ShaderExtension));
                GL.ShaderSource(fShader, ParseShaderSource(Paths.ShaderPath + fFile + Paths.ShaderExtension));
            }
            else
            {
                GL.ShaderSource(vShader, ParseShaderSource(vFile + Paths.ShaderExtension));
                GL.ShaderSource(fShader, ParseShaderSource(fFile + Paths.ShaderExtension));
            }
            GL.CompileShader(vShader);
            GL.CompileShader(fShader);
            CheckShader(vShader);
            CheckShader(fShader);
            AttachShader(vShader, id);
            AttachShader(fShader, id);

            LinkProgram(id);

            DeleteShader(vShader, id);
            DeleteShader(fShader, id);

        }

        private uint CreateProgram()
        {
            return GL.CreateProgram();
        }

        private void LinkProgram(uint programID)
        {
            GL.LinkProgram(programID);
        }

        private void AttachShader(uint shader, uint program)
        {
            GL.AttachShader(program, shader);
        }

        private void DeleteShader(uint shader, uint program)
        {
            GL.DetachShader(program, shader);
            GL.DeleteShader(shader);
        }

        private uint CreateShader(ShaderType type)
        {
            return GL.CreateShader(type);
        }

        private string ParseShaderSource(string filename)
        {
            StreamReader file = new StreamReader(filename);
            return file.ReadToEnd();
        }

        private void CheckShader(uint shader)
        {
            int passed = 0;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out passed);
            if (passed == 0)
            {
                throw new ShaderCompileException(GL.GetShaderInfoLog((int)shader));
            }
        }

        private void ProcessRequests()
        {
            foreach (Request r in uniformRequests)
            {
                r.UploadValue(this);
            }
            uniformRequests.Clear();
        }

        #endregion

    }
}
