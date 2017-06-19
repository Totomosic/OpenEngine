using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenEngine
{
    public class Material
    {

        #region FIELDS

        private Color diffuseColor;
        private Color specularColor;
        private float reflectivity;
        private float shineDamper;

        private List<Texture> textures;

        #endregion

        #region CONSTRUCTORS

        public Material(Color dColor, Color sColor, float reflect = 0, float shinedamper = 10, Texture[] textureArray = null)
        {
            diffuseColor = dColor;
            specularColor = sColor;
            reflectivity = reflect;
            shineDamper = shinedamper;

            textures = new List<Texture>((textureArray == null) ? new Texture[0] : textureArray);
        }

        public Material(Color dColor, float reflect = 0, float shinedamper = 10, Texture[] textureArray = null) : this(dColor, Color.White, reflect, shinedamper, textureArray)
        {

        }

        public Material(Color dColor, Texture[] textureArray) : this(dColor, 0, 10, textureArray)
        {

        }

        public Material(Color dColor, Color sColor, float reflect, float shineDamper, Texture texture) : this(dColor, sColor, reflect, shineDamper, new Texture[] { texture })
        {

        }

        public Material(Color dColor, Color sColor, float reflect, Texture texture) : this(dColor, sColor, reflect, 10, texture)
        {

        }

        public Material(Color dColor, Color sColor, Texture texture) : this(dColor, sColor, 0, texture)
        {

        }

        public Material(Color dColor, Texture texture) : this(dColor, Color.White, texture)
        {

        }

        public Material(Texture texture) : this(Color.White, texture)
        {

        }

        public Material(Color dColor) : this(dColor, Color.White)
        {

        }

        public Material() : this(Color.White)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual Color DiffuseColor
        {
            get { return diffuseColor; }
            set { diffuseColor = value; }
        }

        public virtual Color SpecularColor
        {
            get { return specularColor; }
            set { specularColor = value; }
        }

        public virtual float Reflectivity
        {
            get { return reflectivity; }
            set { reflectivity = value; }
        }

        public virtual float ShineDamper
        {
            get { return shineDamper; }
            set { shineDamper = value; }
        }

        public virtual List<Texture> Textures
        {
            get { return textures; }
            set { textures = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public virtual bool HasTextures()
        {
            return Textures.Count > 0;
        }

        public virtual Texture GetTexture(int index = 0)
        {
            return Textures[index];
        }

        public virtual void AddTexture(Texture texture)
        {
            Textures.Add(texture);
        }

        public virtual void BindTextures()
        {
            foreach (Texture texture in Textures)
            {
                texture.Bind(Textures.IndexOf(texture));
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
