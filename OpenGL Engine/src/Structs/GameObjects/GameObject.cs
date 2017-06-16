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
                Transform = new Transform();
            }
            if (setting == ComponentSetting.None || setting == ComponentSetting.IsCamera)
            {
                Identifier = new Identifier(Tags.None);
                ShaderComponent = new Shader(Engine.Shader);
                RenderTargetComponent = new RenderTarget(Context.Window.Framebuffer);
                Color = new MeshColor(OpenEngine.Color.White);
                ModelComponent = new Mesh(Cuboid.CreateModel(1, 1, 1, OpenEngine.Color.White));

                if (setting != ComponentSetting.IsCamera)
                {
                    GameObject camera = ObjectPool.GetObjectByTag(Tags.MainCamera);
                    if (camera == null)
                    {
                        throw new EngineException("Either no camera entity was created or not tagged with Identifier: Main Camera.");
                    }
                    CameraReference = new CameraReference(camera);
                }
            }
        }

        public GameObject(Vector3 position, ComponentSetting setting = ComponentSetting.None) : this(TransformSetting.NeverAdd, setting)
        {
            Transform = new Transform(position);
        }

        public GameObject(Vector3 position, Matrix4 rotationMatrix, Vector3 scale = default(Vector3), ComponentSetting setting = ComponentSetting.None) : this(TransformSetting.NeverAdd, setting)
        {
            Transform = new Transform(position, (scale == default(Vector3)) ? new Vector3(1, 1, 1) : scale, rotationMatrix);
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
                    Components.AddComponent(c.Clone());
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

        public Transform Transform
        {
            get { return Components.GetComponent<Transform>(); }
            set { Components.AddComponent(value); }
        }

        public Mesh ModelComponent
        {
            get { return Components.GetComponent<Mesh>(); }
            set { Components.AddComponent(value); }
        }

        public Model Model
        {
            get { return ModelComponent.Model; }
            set { ModelComponent.Model = value; }
        }

        public Shader ShaderComponent
        {
            get { return Components.GetComponent<Shader>(); }
            set { Components.AddComponent(value); }
        }

        public ShaderProgram Shader
        {
            get { return ShaderComponent.Program; }
            set { ShaderComponent.Program = value; }
        }

        public RenderTarget RenderTargetComponent
        {
            get { return Components.GetComponent<RenderTarget>(); }
            set { Components.AddComponent(value); }
        }

        public FBO RenderTarget
        {
            get { return RenderTargetComponent.FBO; }
            set { RenderTargetComponent.FBO = value; }
        }

        public Identifier Identifier
        {
            get { return Components.GetComponent<Identifier>(); }
            set { Components.AddComponent(value); }
        }

        public string Tag
        {
            get { return Identifier.ID; }
            set { Identifier.ID = value; }
        }

        public MeshColor Color
        {
            get { return Components.GetComponent<MeshColor>(); }
            set { Components.AddComponent(value); }
        }

        public CameraReference CameraReference
        {
            get { return Components.GetComponent<CameraReference>(); }
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
