using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine
{
    public class GameObject
    {

        #region FIELDS

        private static GameObject[] gameObjects = new GameObject[Engine.MaxEntities];
        private static uint highestID = 0;
        private static bool autoAddTransform = true;

        private uint id;
        private ComponentSet components;

        #endregion

        #region CONSTRUCTORS

        public GameObject(TransformSetting transformSetting = TransformSetting.UseGlobalSetting)
        {
            id = GenerateNextID();
            gameObjects[id] = this;
            components = new ComponentSet(this);

            if ((transformSetting == TransformSetting.UseGlobalSetting && autoAddTransform) || transformSetting == TransformSetting.AlwaysAdd)
            {
                Components.AddComponent(new CTransform());
            }
        }

        public GameObject(Vector3 position) : this(TransformSetting.NeverAdd)
        {
            Transform = new CTransform(position);
        }

        public GameObject(Vector3 position, Matrix4 rotationMatrix) : this(TransformSetting.NeverAdd)
        {
            Transform = new CTransform(position, rotationMatrix);
        }

        public GameObject(GameObject other, bool clone = true) : this(TransformSetting.NeverAdd)
        {
            if (!clone)
            {
                Components.AddComponents(other.Components.GetAllComponents());
            }
            else
            {
                foreach (Component c in other.Components.GetAllComponents())
                {
                    Components.AddComponent(c.DeepClone());
                }
            }
        }

        #endregion

        #region PROPERTIES

        public static bool AutoAddTransform
        {
            get { return autoAddTransform; }
            set { autoAddTransform = value; }
        }

        public static uint HighestID
        {
            get { return highestID; }
        }

        public static GameObject Empty
        {
            get { return new GameObject(TransformSetting.NeverAdd); }
        }

        public uint ID
        {
            get { return id; }
        }

        public ComponentSet Components
        {
            get { return components; }
            set { components = value; }
        }

        #region BASIC COMPONENTS

        public CTransform Transform
        {
            get { return Components.GetComponent<CTransform>(); }
            set { Components.AddComponent(value); }
        }

        public CMotion Motion
        {
            get { return Components.GetComponent<CMotion>(); }
            set { Components.AddComponent(value); }
        }

        public CModel Model
        {
            get { return Components.GetComponent<CModel>(); }
            set { Components.AddComponent(value); }
        }

        public CShader Shader
        {
            get { return Components.GetComponent<CShader>(); }
            set { Components.AddComponent(value); }
        }

        public CRenderTarget RenderTarget
        {
            get { return Components.GetComponent<CRenderTarget>(); }
            set { Components.AddComponent(value); }
        }

        public CIdentifier Identifier
        {
            get { return Components.GetComponent<CIdentifier>(); }
            set { Components.AddComponent(value); }
        }

        public CColor Color
        {
            get { return Components.GetComponent<CColor>(); }
            set { Components.AddComponent(value); }
        }

        #endregion

        #endregion

        #region PUBLIC METHODS

        public static bool IsValidGameObject(uint id)
        {
            return gameObjects[id] != null;
        }

        public static GameObject GetAtID(uint id)
        {
            if (IsValidGameObject(id))
            {
                return gameObjects[id];
            }
            return null;
        }

        public void Destroy()
        {
            gameObjects[id] = null;
        }

        #endregion

        #region PRIVATE METHODS

        private static uint GenerateNextID()
        {
            for (uint i = 0; i <= highestID + 1 && i < Engine.MaxEntities; i++)
            {
                if (gameObjects[i] == null)
                {
                    if (i > highestID)
                    {
                        highestID = i;
                    }
                    return i;
                }
            }
            throw new EngineException("Unable to create new GameObject");
        }

        #endregion

    }
}
