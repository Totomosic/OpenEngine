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

        public Material(Color dColor, Color sColor, float reflectivity = 0, float shineDamper = 10, Texture texture = null) : this(dColor, sColor, reflectivity, shineDamper, (texture != null) ? new Texture[] { texture } : null)
        {

        }

        public Material(Color dColor, float reflectivity, float shineDamper = 10, Texture texture = null) : this(dColor, Color.White, reflectivity, shineDamper, (texture != null) ? new Texture[] { texture } : null)
        {

        }

        public Material(Color dColor, Texture texture = null) : this(dColor, 0, 10, texture)
        {

        }

        public Material(Texture texture) : this(Color.White, texture)
        {

        }

        public Material() : this(null)
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

        public virtual bool HasTexture()
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

        public virtual void UnbindTextures()
        {
            foreach (Texture texture in Textures)
            {
                texture.Unbind(Textures.IndexOf(texture));
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
