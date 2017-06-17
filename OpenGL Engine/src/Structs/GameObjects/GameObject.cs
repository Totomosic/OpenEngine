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

        private uint id;
        private ComponentSet components;

        #endregion

        #region CONSTRUCTORS

        public GameObject(ComponentSetting setting = ComponentSetting.None)
        {
            id = GenerateNextID();
            gameObjects[id] = this;
            components = new ComponentSet(this);

            // Add Default Components
            Transform = new Transform();
            if (setting == ComponentSetting.None || setting == ComponentSetting.IsCamera)
            {
                MeshComponent = new Mesh(Cuboid.CreateModel(1, 1, 1, Color.White));
                ShaderComponent = new Shader(Engine.Shader);
                RenderTargetComponent = new RenderTarget(Context.Window.Framebuffer);
                Identifier = new Identifier(Tags.None);
                MeshColor = new MeshColor(Color.White);
                if (setting != ComponentSetting.IsCamera)
                {
                    CameraReference = new CameraReference(Camera.Main);
                }
            }
        }

        public GameObject(Vector3 position, Matrix4 rotation = default(Matrix4), Vector3 scale = default(Vector3), ComponentSetting setting = ComponentSetting.None) : this(setting)
        {
            OpenEngine.Components.Transform transform = Transform;
            transform.Position = position;
            transform.Rotation = (rotation == default(Matrix4)) ? Matrix4.Identity : rotation;
            transform.Scale = (scale == default(Vector3)) ? new Vector3(1, 1, 1) : scale;
        }

        public GameObject(Vector3 position, Model model, Matrix4 rotation = default(Matrix4), Vector3 scale = default(Vector3), ComponentSetting setting = ComponentSetting.None) : this(position, rotation, scale, setting)
        {
            Model = model;
        }

        #endregion

        #region PROPERTIES

        public static uint HighestID
        {
            get { return highestID; }
        }

        public static GameObject Empty
        {
            get { return new GameObject(ComponentSetting.LeaveEmpty); }
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

        public Mesh MeshComponent
        {
            get { return Components.GetComponent<Mesh>(); }
            set { Components.AddComponent(value); }
        }

        public Model Model
        {
            get { return MeshComponent.Model; }
            set { MeshComponent.Model = value; }
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

        public MeshColor MeshColor
        {
            get { return Components.GetComponent<MeshColor>(); }
            set { Components.AddComponent(value); }
        }

        public CameraReference CameraReference
        {
            get { return Components.GetComponent<CameraReference>(); }
            set { Components.AddComponent(value); }
        }

        public GameObject CameraObject
        {
            get { return CameraReference.ID; }
            set { CameraReference.ID = value; }
        }

        #endregion

        #endregion

        #region PUBLIC METHODS

        public static GameObject Instantiate(GameObject gameObject)
        {
            GameObject clone = Empty;
            foreach (Component c in gameObject.Components.GetAllComponents())
            {
                clone.Components.AddComponent(c.Clone());
            }
            return clone;
        }

        public static GameObject Instantiate(GameObject gameObject, Vector3 position)
        {
            GameObject clone = Instantiate(gameObject);
            clone.Transform.Position = position;
            return clone;
        }

        public static GameObject Instantiate(GameObject gameObject, GameObject parent)
        {
            GameObject clone = Instantiate(gameObject);
            clone.Components.AddComponent(new Parent(parent));
            return clone;
        }

        public static GameObject Instantiate(GameObject gameObject, GameObject parent, Vector3 relativePosition)
        {
            GameObject clone = Instantiate(gameObject, parent);
            clone.Transform.Position = relativePosition;
            return clone;
        }

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

        public void AddComponent(Component component)
        {
            Components.AddComponent(component);
        }

        public T GetComponent<T>()
            where T : Component, new()
        {
            return Components.GetComponent<T>();
        }

        public bool HasComponent<T>()
            where T : Component, new()
        {
            return Components.HasComponent<T>();
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
