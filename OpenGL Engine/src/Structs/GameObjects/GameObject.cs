using System;
using System.Reflection;
using OpenEngine.Components;

namespace OpenEngine
{
    /// <summary>
    /// Class that represents all game objects, attach components to edit behaviour
    /// </summary>
    public class GameObject
    {

        #region FIELDS

        private static FBO defaultRenderTarget = null;
        private static GameObject[] gameObjects = new GameObject[Engine.MaxEntities];
        private static uint highestID = 0;

        private uint id;
        private ComponentSet components;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructs a new GameObject with default components
        /// </summary>
        public GameObject()
        {
            id = GenerateNextID();
            gameObjects[id] = this;
            components = new ComponentSet(this);

            // Add Default Components
            Transform = new Transform();
            MeshComponent = new Mesh(Cuboid.CreateModel(1, 1, 1, Color.White));
            ShaderComponent = new Shader(Engine.Shader);
            RenderTargetComponent = new RenderTarget((DefaultRenderTarget == null) ? Context.Window.Framebuffer : DefaultRenderTarget);
            Identifier = new Identifier(Tags.None);
            MeshColor = new MeshColor(Color.White);
            MeshMaterial = new MeshMaterial(new Material());
            if (OpenEngine.Camera.Main != null)
            {
                CameraReference = new CameraReference(OpenEngine.Camera.Main);
            }
        }

        /// <summary>
        /// Constructs a new GameObject
        /// </summary>
        /// <param name="position">World space position</param>
        /// <param name="rotation">Rotation matrix</param>
        /// <param name="scale">Scale in x, y, z axis</param>
        public GameObject(Vector3 position, Matrix4 rotation = default(Matrix4), Vector3 scale = default(Vector3)) : this()
        {
            Transform transform = Transform;
            transform.Position = position;
            transform.Rotation = (rotation == default(Matrix4)) ? Matrix4.Identity : rotation;
            transform.Scale = (scale == default(Vector3)) ? new Vector3(1, 1, 1) : scale;
        }

        /// <summary>
        /// Constructs a new GameObject
        /// </summary>
        /// <param name="position">World space position</param>
        /// <param name="model">Model for object</param>
        /// <param name="rotation">Rotation matrix</param>
        /// <param name="scale">Scale in x, y, z, axis</param>
        public GameObject(Vector3 position, Model model, Matrix4 rotation = default(Matrix4), Vector3 scale = default(Vector3)) : this(position, rotation, scale)
        {
            Model = model;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Sets and gets the default rendertarget for GameObjects to use
        /// </summary>
        public static FBO DefaultRenderTarget
        {
            get { return defaultRenderTarget; }
            set { defaultRenderTarget = value; }
        }

        /// <summary>
        /// Current highest ID of all GameObjects currently active
        /// </summary>
        public static uint HighestID
        {
            get { return highestID; }
        }

        /// <summary>
        /// Constructs a new GameObject with no components
        /// </summary>
        public static GameObject Empty
        {
            get { GameObject o = new GameObject(); o.Components.RemoveAll(); return o; }
        }

        /// <summary>
        /// Returns the ID of this GameObject
        /// </summary>
        public uint ID
        {
            get { return id; }
        }

        /// <summary>
        /// Returns the set of components for this GameObject
        /// </summary>
        public ComponentSet Components
        {
            get { return components; }
            set { components = value; }
        }

        #region BASIC COMPONENTS

        /// <summary>
        /// Gets the transform component for this GameObject
        /// </summary>
        public Transform Transform
        {
            get { return Components.GetComponent<Transform>(); }
            set { Components.AddComponent(value); }
        }

        /// <summary>
        /// Gets the mesh component for this GameObject
        /// </summary>
        public Mesh MeshComponent
        {
            get { return Components.GetComponent<Mesh>(); }
            set { SetMeshComponent(value); }
        }

        /// <summary>
        /// Gets and Sets the Model of this GameObject
        /// </summary>
        public Model Model
        {
            get { return MeshComponent.Model; }
            set { SetModel(value); }
        }

        /// <summary>
        /// Gets the Shader component for this GameObject
        /// </summary>
        public Shader ShaderComponent
        {
            get { return Components.GetComponent<Shader>(); }
            set { Components.AddComponent(value); }
        }

        /// <summary>
        /// Gets and Sets the ShaderProgram for this GameObject
        /// </summary>
        public ShaderProgram Shader
        {
            get { return ShaderComponent.Program; }
            set { SetShader(value); }
        }

        /// <summary>
        /// Gets the RenderTarget compoenent for this GameObject
        /// </summary>
        public RenderTarget RenderTargetComponent
        {
            get { return Components.GetComponent<RenderTarget>(); }
            set { Components.AddComponent(value); }
        }

        /// <summary>
        /// Gets and Sets the Render target of this GameObject
        /// </summary>
        public FBO RenderTarget
        {
            get { return RenderTargetComponent.FBO; }
            set { RenderTargetComponent.FBO = value; }
        }

        /// <summary>
        /// Gets the Identifier component for this GameObject
        /// </summary>
        public Identifier Identifier
        {
            get { return Components.GetComponent<Identifier>(); }
            set { Components.AddComponent(value); }
        }

        /// <summary>
        /// Gets and Sets the Tag for this GameObject
        /// </summary>
        public string Tag
        {
            get { return Identifier.ID; }
            set { Identifier.ID = value; }
        }

        /// <summary>
        /// Gets the MeshColor component for this GameObject
        /// </summary>
        public MeshColor MeshColor
        {
            get { return Components.GetComponent<MeshColor>(); }
            set { Components.AddComponent(value); }
        }

        /// <summary>
        /// Gets the CameraReference component for this GameObject
        /// </summary>
        public CameraReference CameraReference
        {
            get { return Components.GetComponent<CameraReference>(); }
            set { Components.AddComponent(value); }
        }

        /// <summary>
        /// Gets and sets the camera through which this GameObject is viewed
        /// </summary>
        public GameObject Camera
        {
            get { return CameraReference.ID; }
            set { SetCamera(value); }
        }

        /// <summary>
        /// Gets the MeshMaterial component of this GameObject
        /// </summary>
        public MeshMaterial MeshMaterial
        {
            get { return Components.GetComponent<MeshMaterial>(); }
            set { Components.AddComponent(value); }
        }

        /// <summary>
        /// Gets and Sets the material of this GameObject
        /// </summary>
        public Material Material
        {
            get { return MeshMaterial.Material; }
            set { MeshMaterial.Material = value; }
        }

        #endregion

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Instantiates a new GameObject identical to provided GameObject
        /// </summary>
        /// <param name="gameObject">Prefab GameObject</param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject gameObject)
        {
            GameObject clone = Empty;
            foreach (Component c in gameObject.Components.GetAllComponents())
            {
                clone.Components.AddComponent(c.Clone());
            }
            return clone;
        }

        /// <summary>
        /// Instantiates a new GameObject identical to provided GameObject at a given world space position
        /// </summary>
        /// <param name="gameObject">Prefab GameObject</param>
        /// <param name="position">World space position</param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject gameObject, Vector3 position)
        {
            GameObject clone = Instantiate(gameObject);
            clone.Transform.Position = position;
            return clone;
        }

        /// <summary>
        /// Instantiates a new GameObject identical to provided GameObject with a given parent
        /// </summary>
        /// <param name="gameObject">Prefab GameObject</param>
        /// <param name="parent">Parent GameObject</param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject gameObject, GameObject parent)
        {
            GameObject clone = Instantiate(gameObject);
            clone.Components.AddComponent(new Parent(parent));
            return clone;
        }

        /// <summary>
        /// Instantiates a new GameObject identical to provided GameObject with a given parent at a position relevant to its parent
        /// </summary>
        /// <param name="gameObject">Prefab GameObject</param>
        /// <param name="parent">Parent GameObject</param>
        /// <param name="relativePosition">World space position relative to parent</param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject gameObject, GameObject parent, Vector3 relativePosition)
        {
            GameObject clone = Instantiate(gameObject, parent);
            clone.Transform.Position = relativePosition;
            return clone;
        }

        /// <summary>
        /// Test whether specified ID corresponds to a valid GameObject
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static bool IsValidGameObject(uint id)
        {
            return gameObjects[id] != null;
        }

        /// <summary>
        /// Get the GameObject with given ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static GameObject GetAtID(uint id)
        {
            if (IsValidGameObject(id))
            {
                return gameObjects[id];
            }
            return null;
        }

        /// <summary>
        /// Add a component to this GameObject
        /// </summary>
        /// <param name="component">Component to add</param>
        public void AddComponent(Component component)
        {
            Components.AddComponent(component);
        }

        /// <summary>
        /// Gets a component from this GameObject
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <returns></returns>
        public T GetComponent<T>()
            where T : Component, new()
        {
            return Components.GetComponent<T>();
        }
        
        /// <summary>
        /// Tests whether this GameObject has a component
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <returns></returns>
        public bool HasComponent<T>()
            where T : Component, new()
        {
            return Components.HasComponent<T>();
        }

        /// <summary>
        /// Commands all components attached to this GameObject to execute given method
        /// </summary>
        /// <param name="methodName">Method to invoke</param>
        /// <param name="parameters">Parameters to method</param>
        /// <param name="setting">Additional settings</param>
        public void BroadcastMessage(string methodName, object[] parameters = null, BroadcastSetting setting = BroadcastSetting.None)
        {
            foreach (Component c in Components.GetAllComponents())
            {
                MethodInfo method;
                if ((method = c.GetType().GetMethod(methodName)) != null)
                {
                    method.Invoke(c, (parameters == null) ? new object[0] : parameters);
                }
                else if (setting == BroadcastSetting.RequireReceive)
                {
                    throw new GameObjectException("Component of type: " + c.GetType().ToString() + " did not have method: " + methodName);
                }
            }
        }

        /// <summary>
        /// Commands all components attached to this GameObject to execute given method
        /// </summary>
        /// <param name="methodName">Method to invoke</param>
        /// <param name="parameter">Parameter to method</param>
        /// <param name="setting">Additional settings</param>
        public void BroadcastMessage(string methodName, object parameter, BroadcastSetting setting = BroadcastSetting.None)
        {
            BroadcastMessage(methodName, new object[] { parameter }, setting);
        }

        /// <summary>
        /// Destroys this GameObject
        /// </summary>
        public void Destroy()
        {
            if (HasComponent<Mesh>())
            {
                ResourceManager.ReleaseReference(GetComponent<Mesh>().Model);
            }
            gameObjects[id] = null;
        }

        #endregion

        #region PRIVATE METHODS

        private void SetModel(Model model)
        {
            if (HasComponent<Mesh>())
            {
                Mesh mesh = GetComponent<Mesh>();
                ResourceManager.ReleaseReference(mesh.Model);
                mesh.Model = model;
            }
            else
            {
                AddComponent(new Mesh(model));                
            }
            ResourceManager.FetchReference(model);
        }

        private void SetMeshComponent(Mesh mesh)
        {
            if (HasComponent<Mesh>())
            {
                ResourceManager.ReleaseReference(GetComponent<Mesh>().Model);
                AddComponent(mesh);
            }
            else
            {
                AddComponent(mesh);
            }
            ResourceManager.FetchReference(mesh.Model);
        }

        private void SetCamera(GameObject camera)
        {
            if (HasComponent<CameraReference>())
            {
                GetComponent<CameraReference>().ID = camera;
            }
            else
            {
                AddComponent(new CameraReference(camera));
            }
        }

        private void SetShader(ShaderProgram shader)
        {
            if (HasComponent<Shader>())
            {
                GetComponent<Shader>().Program = shader;
            }
            else
            {
                AddComponent(new Shader(shader));
            }
        }

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
