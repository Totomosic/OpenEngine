using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    /// <summary>
    /// Class that manages external resources for your project
    /// </summary>
    public static class ContentManager
    {

        #region FIELDS

        #endregion

        #region PROPERTIES

        #endregion

        #region GETTERS

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Load an external resource
        /// </summary>
        /// <typeparam name="T">Type of resource</typeparam>
        /// <param name="filename">Filename of resource</param>
        /// <param name="info">DataObject that provides extra information about how to load this resource</param>
        /// <param name="usePathExtensions">Determine whether to use path extensions found in Paths</param>
        /// <returns></returns>
        public static T Load<T>(string filename, DataObject info = null, bool usePathExtensions = true)
            where T : class
        {
            if (typeof(T) == typeof(Model) && filename.Contains(".obj"))
            {
                Model model = LoadOBJModel((usePathExtensions) ? Paths.ModelPath + filename : filename, (info == null) ? new ModelData() : info as ModelData);
                return model as T;
            }
            else if (typeof(T) == typeof(Model) && (filename.Contains(".png") || filename.Contains(".jpg")))
            {
                Model model = LoadHeightmap((usePathExtensions) ? Paths.HeightmapPath + filename : filename, (info == null) ? new ModelData() : info as ModelData);
                return model as T;
            }
            else if (typeof(T) == typeof(Texture2D))
            {
                Texture2D tex = LoadTexture2D((usePathExtensions) ? Paths.TexturePath + filename : filename, (info == null) ? new TextureData() : info as TextureData);
                return tex as T;
            }
            else if (typeof(T) == typeof(TextureAtlas))
            {
                TextureAtlas tex = LoadTextureAtlas((usePathExtensions) ? Paths.TexturePath + filename : filename, (info == null) ? new TextureData() : info as TextureData);
                return tex as T;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Loads a resource from an array of files
        /// </summary>
        /// <typeparam name="T">Type of resource</typeparam>
        /// <param name="filenames">Array of filenames from which the resource will be loaded from</param>
        /// <param name="info">DataObject that describes how this resource will be loaded</param>
        /// <returns></returns>
        public static T Load<T>(string[] filenames, DataObject info = null)
            where T : class
        {
            if (typeof(T) == typeof(TextureCubeMap))
            {
                for (int i = 0; i < filenames.Length; i++)
                {
                    filenames[i] = Paths.TexturePath + filenames[i];
                }
                TextureCubeMap tex = LoadCubeMap(filenames, (info == null) ? new TextureData() : info as TextureData);
                return tex as T;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Load a model from raw floating point data
        /// </summary>
        /// <param name="vertices">Array of vertex positions</param>
        /// <param name="normals">Array of vertex normals</param>
        /// <param name="texCoords">Array of vertex Texture Coordinates</param>
        /// <param name="colors">Array of vertex colors</param>
        /// <param name="vertexDimension">How many dimensions the vertex positions are specified in</param>
        /// <returns></returns>
        public static Model LoadModel(float[] vertices, float[] normals, float[] texCoords, float[] colors, int vertexDimension = 3)
        {
            VAO vao = new VAO(RenderMode.Arrays, vertices.Length);
            vao.CreateAttribute((int)BufferLayout.Vertices, vertices, vertexDimension);
            vao.CreateAttribute((int)BufferLayout.Normals, normals, 3);
            vao.CreateAttribute((int)BufferLayout.TextureCoordinates, texCoords, 2);
            vao.CreateAttribute((int)BufferLayout.Color, colors, 4);
            Model model = new Model(vao);
            return model;
        }

        /// <summary>
        /// Load a model from raw floating point data
        /// </summary>
        /// <param name="vertices">Array of vertex positions</param>
        /// <param name="normals">Array of vertex normals</param>
        /// <param name="texCoords">Array of vertex Texture Coordinates</param>
        /// <param name="color">Specify a color that all vertices will share</param>
        /// <param name="vertexDimension">How many dimensions the vertex positions are specified in</param>
        /// <returns></returns>
        public static Model LoadModel(float[] vertices, float[] normals, float[] texCoords, Color color, int vertexDimension = 3)
        {
            return LoadModel(vertices, normals, texCoords, color.ToVertexData(vertices.Length / vertexDimension), vertexDimension);
        }

        /// <summary>
        /// Load a model from raw floating point data
        /// </summary>
        /// <param name="vertices">Array of vertex positions</param>
        /// <param name="normals">Array of vertex normals</param>
        /// <param name="texCoords">Array of vertex Texture Coordinates</param>
        /// <param name="colors">Array of vertex colors</param>
        /// <param name="indices">Array of indices</param>
        /// <param name="vertexDimension">How many dimensions the vertex positions are specified in</param>
        /// <returns></returns>
        public static Model LoadModel(float[] vertices, float[] normals, float[] texCoords, float[] colors, uint[] indices, int vertexDimension = 3)
        {
            Model model = LoadModel(vertices, normals, texCoords, colors, vertexDimension);
            IndexBuffer index = new IndexBuffer(indices.Length * sizeof(uint), indices);
            model.VAO.AttachIndexBuffer(index);
            model.Mode = RenderMode.Elements;
            return model;
        }

        /// <summary>
        /// Load a model from raw floating point data
        /// </summary>
        /// <param name="vertices">Array of vertex positions</param>
        /// <param name="normals">Array of vertex normals</param>
        /// <param name="texCoords">Array of vertex Texture Coordinates</param>
        /// <param name="color">Color that all vertices will share</param>
        /// <param name="indices">Array of indices</param>
        /// <param name="vertexDimension">How many dimensions the vertex positions are specified in</param>
        /// <returns></returns>
        public static Model LoadModel(float[] vertices, float[] normals, float[] texCoords, Color color, uint[] indices, int vertexDimension = 3)
        {
            Model model = LoadModel(vertices, normals, texCoords, color.ToVertexData(vertices.Length / vertexDimension), indices, vertexDimension);
            return model;
        }

        #endregion

        #region PRIVATE METHODS


        private static Model LoadOBJModel(string filename, ModelData info)
        {
            Model model = OBJReader.CreateModel(filename);
            return model;
        }

        private static Model LoadHeightmap(string filename, ModelData info)
        {
            float max = info.MaxHeight;
            float min = info.MinHeight;

            return LoadMesh(filename, max, min);

        }

        private static TextureCubeMap LoadCubeMap(string[] filenames, TextureData info)
        {
            GL.Enable(EnableCap.TextureCubeMap);
            int texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.TextureCubeMap, texture);
            for (int i = 0; i < filenames.Length; i++)
            {
                SetTextureData(filenames[i], TextureTarget.TextureCubeMapPositiveX + i, info);
            }
            Bitmap image = new Bitmap(filenames[0]);
            SetTextureParameters(TextureTarget.TextureCubeMap, info);
            GL.GenerateMipmap(GenerateMipmapTarget.TextureCubeMap);
            GL.BindTexture(TextureTarget.TextureCubeMap, 0);
            return new TextureCubeMap(texture, new Vector2(image.Width, image.Height), IsTransparent(image));
        }

        private static TextureAtlas LoadTextureAtlas(string filename, TextureData info)
        {
            Bitmap image = new Bitmap(filename);
            Size size = image.Size;
            return new TextureAtlas(LoadTexture(filename, info, TextureTarget.Texture2D), new Vector2(size.Width, size.Height), info.Columns, info.Rows, IsTransparent(image));
        }

        private static Texture2D LoadTexture2D(string filename, TextureData info)
        {
            Bitmap image = new Bitmap(filename);
            Size size = image.Size;
            return new Texture2D(LoadTexture(filename, info, TextureTarget.Texture2D), new Vector2(size.Width, size.Height), IsTransparent(image));
        }

        private static int LoadTexture(string filename, TextureData info, TextureTarget target)
        {
            int texture = GL.GenTexture();
            GL.BindTexture(target, texture);
            SetTextureData(filename, target, info);
            GL.GenerateMipmap((GenerateMipmapTarget)target);
            GL.BindTexture(target, 0);
            return texture;
        }

        private static float GetPixelHeight(Bitmap image, int x, int y, float max, float min)
        {
            float b = image.GetPixel(x, y).GetBrightness();
            return Utilities.Map(b, 0, 1, min, max);
        }

        private static Model LoadMesh(string filename, float maxH, float minH)
        {
            List<float> verts = new List<float>();
            List<float> norms = new List<float>();
            List<float> tex = new List<float>();
            List<uint> indices = new List<uint>();
            Bitmap image = new Bitmap(filename);
            Table<float> heights = Table<float>.FromImageBrightness(image, minH, maxH, OutOfRange.ReturnDefault);

            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    float height = GetPixelHeight(image, i, j, maxH, minH);

                    verts.Add(i - image.Width / 2f);
                    verts.Add(heights.GetValue(i, j));
                    verts.Add(j - image.Height / 2f);

                    tex.Add((float)i / image.Width);
                    tex.Add(1 - (float)j / image.Height);

                    float rSide = heights.GetValue(i + 1, j);
                    float lSide = heights.GetValue(i - 1, j);
                    float tSide = heights.GetValue(i, j - 1);
                    float bSide = heights.GetValue(i, j + 1);
                    Vector3 normal = new Vector3(lSide - rSide, 2f, tSide - bSide).Normalize();

                    norms.Add(normal.X);
                    norms.Add(normal.Y);
                    norms.Add(normal.Z);

                    if (i < image.Width - 1 && j < image.Height - 1)
                    {
                        indices.Add((uint)((i) + (j) * image.Width));
                        indices.Add((uint)((i) + (j + 1) * image.Width));
                        indices.Add((uint)((i + 1) + (j + 1) * image.Width));

                        indices.Add((uint)((i) + (j) * image.Width));
                        indices.Add((uint)((i + 1) + (j + 1) * image.Width));
                        indices.Add((uint)((i + 1) + (j) * image.Width));
                    }

                }
            }

            VAO vao = new VAO(RenderMode.Elements, verts.Count);
            vao.CreateAttribute((int)BufferLayout.Vertices, verts.ToArray(), 3);
            vao.CreateAttribute((int)BufferLayout.Normals, norms.ToArray(), 3);
            vao.CreateAttribute((int)BufferLayout.TextureCoordinates, tex.ToArray(), 2);
            vao.CreateIndexBuffer(indices.ToArray());
            vao.CreateColorBuffer(Color.White);
            Model model = new Model(vao);

            image.Dispose();

            return model;
        }

        #region BASE FUNCTIONS

        private static void SetTextureData(string filename, TextureTarget target, TextureData info)
        {
            Bitmap image = new Bitmap(filename);
            if (info.InvertY)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            BitmapData imageData = GetData(image);
            Size size = image.Size;
            SetTextureParameters(target, info);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            GL.TexImage2D(target, 0, PixelInternalFormat.Rgba16, image.Width, image.Height, 0, Pencil.Gaming.Graphics.PixelFormat.Bgra, PixelType.UnsignedByte, imageData.Scan0);
            image.UnlockBits(imageData);
            image.Dispose();
        }

        private static BitmapData GetData(Bitmap image)
        {
            return image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        private static void SetTextureParameters(TextureTarget target, TextureData info)
        {
            GL.TexParameter(target, TextureParameterName.TextureWrapS, (int)info.WrapS);
            GL.TexParameter(target, TextureParameterName.TextureWrapT, (int)info.WrapT);
            GL.TexParameter(target, TextureParameterName.TextureWrapR, (int)info.WrapR);
            GL.TexParameter(target, TextureParameterName.TextureMagFilter, (int)info.MagFilter);
            GL.TexParameter(target, TextureParameterName.TextureMinFilter, (int)info.MinFilter);
        }

        private static bool IsTransparent(Bitmap image)
        {
            bool hasTransparency = false;
            for (int i = 0; i < image.Width; i++)
            {
                if (!hasTransparency)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        if (image.GetPixel(i, j).A != 255)
                        {
                            hasTransparency = true;
                            break;
                        }
                    }
                }
            }
            return hasTransparency;
        }

        #endregion

        #endregion

    }
}
