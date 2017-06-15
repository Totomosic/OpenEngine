using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenEngine
{
    [XmlInclude(typeof(Texture)), XmlInclude(typeof(Texture2D)), XmlInclude(typeof(TextureAtlas)), XmlInclude(typeof(TextureCubeMap))]
    public abstract class GLObject
    {

        #region FIELDS

        protected int id;
        private GLType type;
        private BindState state;
        private bool isAlive;

        #endregion

        #region CONSTRUCTORS

        public GLObject(int ID, GLType target)
        {
            id = ID;
            type = target;
            state = BindState.Unbound;
            isAlive = true;
        }

        #endregion

        #region PROPERTIES

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public GLType Type
        {
            get { return type; }
            set { type = value; }
        }

        public BindState State
        {
            get { return state; }
            set { state = value; }
        }

        public bool IsActive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public abstract void Bind();

        public abstract void Unbind();

        public virtual void Delete()
        {

        }

        public static void Cleanup()
        {

        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
