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

        public GameObject(TransformSetting transformSetting = TransformSetting.UseGlobalSetting, ComponentSetting setting = ComponentSetting.None)
        {
            id = GenerateNextID();
            gameObjects[id] = this;
            components = new ComponentSet(this);

            if ((transformSetting == TransformSetting.UseGlobalSetting && autoAddTransform) || transformSetting == TransformSetting.AlwaysAdd)
            {
                Transform = new CTransform();
            }
            if (setting == ComponentSetting.None || setting == ComponentSetting.IsCamera)
            {
                Identifier = new CIdentifier(Tags.None);
                ShaderComponent = new CShader(Engine.Shader);
                RenderTargetComponent = new CRenderTarget(Context.Window.Framebuffer);
                Color = new CColor(OpenEngine.Color.White);
                ModelComponent = new CModel(Cuboid.CreateModel(1, 1, 1, OpenEngine.Color.White));

                if (setting != ComponentSetting.IsCamera)
                {
                    GameObject camera = ObjectPool.GetObjectByTag(Tags.MainCamera);
                    if (camera == null)
                    {
                        throw new EngineException("Either no camera entity was created or not tagged with Identifier: Main Camera.");
                    }
                    CameraReference = new CCameraReference(camera);
                }
            }
        }

        public GameObject(Vector3 position, ComponentSetting setting = ComponentSetting.None) : this(TransformSetting.NeverAdd, setting)
        {
            Transform = new CTransform(position);
        }

        public GameObject(Vector3 position, Matrix4 rotationMatrix, Vector3 scale = default(Vector3), ComponentSetting setting = ComponentSetting.None) : this(TransformSetting.NeverAdd, setting)
        {
            Transform = new CTransform(position, (scale == default(Vector3)) ? new Vector3(1, 1, 1) : scale, rotationMatrix);
        }

        public GameObject(Vector3 position, Model model, ComponentSetting setting = ComponentSetting.None) : this(position, setting)
        {
            Model = model;
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
            get { return new GameObject(TransformSetting.NeverAdd, ComponentSetting.LeaveEmpty); }
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

        public CModel ModelComponent
        {
            get { return Components.GetComponent<CModel>(); }
            set { Components.AddComponent(value); }
        }

        public Model Model
        {
            get { return ModelComponent.Model; }
            set { ModelComponent.Model = value; }
        }

        public CShader ShaderComponent
        {
            get { return Components.GetComponent<CShader>(); }
            set { Components.AddComponent(value); }
        }

        public ShaderProgram Shader
        {
            get { return ShaderComponent.Program; }
            set { ShaderComponent.Program = value; }
        }

        public CRenderTarget RenderTargetComponent
        {
            get { return Components.GetComponent<CRenderTarget>(); }
            set { Components.AddComponent(value); }
        }

        public FBO RenderTarget
        {
            get { return RenderTargetComponent.FBO; }
            set { RenderTargetComponent.FBO = value; }
        }

        public CIdentifier Identifier
        {
            get { return Components.GetComponent<CIdentifier>(); }
            set { Components.AddComponent(value); }
        }

        public string Tag
        {
            get { return Identifier.ID; }
            set { Identifier.ID = value; }
        }

        public CColor Color
        {
            get { return Components.GetComponent<CColor>(); }
            set { Components.AddComponent(value); }
        }

        public CCameraReference CameraReference
        {
            get { return Components.GetComponent<CCameraReference>(); }
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
