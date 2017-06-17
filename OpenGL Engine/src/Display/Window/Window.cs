using System;
using System.Collections.Generic;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;

namespace OpenEngine
{
    public class Window
    {

        #region FIELDS

        private GlfwWindowPtr displayPtr;
        private WindowInfo windowInfo;

        private bool isFullscreen;
        private Mouse mouse;

        private GameTime time;
        private Framebuffer framebuffer;
        private int updateCount;

        // Callbacks / Events
        private Action<int, int> reshapeFunc;
        private EventQueue eventQueue;

        private List<float> framerates;
        private int smoothFramerate;
        private Timer smoothTimer;

        #endregion

        #region CONSTRUCTORS

        public Window(int w, int h, string title, Color cColor = default(Color))
        {

            FBOManager.Clear();
            ShaderManager.Clear();

            if (!Glfw.Init())
            {
                Console.WriteLine("GLFW Failed to initialise");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            windowInfo = new WindowInfo(displayPtr, new Vector2(w, h), title);
            CreateWindow();
            windowInfo.WindowPtr = displayPtr;

            isFullscreen = false;
            updateCount = 0;         

            framebuffer = new Framebuffer(Width, Height);
            framebuffer.ClearColor = cColor;
            SetSize(w, h);
            mouse = new Mouse(this);
            time = new GameTime();

            reshapeFunc = Reshape;
            Glfw.SetWindowSizeCallback(WindowPtr, IntermediateReshape);
            eventQueue = new EventQueue();
            eventQueue.Setup(WindowPtr);

            framerates = new List<float>();
            smoothFramerate = 60;
            smoothTimer = new Timer(time, 0.3f);
        }

        public Window(int x, int y, int w, int h, string name, Color clearColor = default(Color)) : this(w, h, name, clearColor)
        {
            SetPosition(x, y);
        }

        public Window(Vector2 size, string title, Color clearColor = default(Color)) : this((int)size.X, (int)size.Y, title, clearColor)
        {

        }

        public Window(Vector2 pos, Vector2 size, string title, Color clearColor = default(Color)) : this((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y, title, clearColor)
        {

        }

        #endregion

        #region PROPERTIES

        public Viewport Viewport
        {
            get { return framebuffer.Viewport; }
        }

        public float Aspect
        {
            get { return Width / (float)Height; }
        }

        public int Width
        {
            get { return (int)windowInfo.Size.X; }
        }

        public int Height
        {
            get { return (int)windowInfo.Size.Y; }
        }

        public Vector2 Resolution
        {
            get { return windowInfo.Size; }
        }

        public Vector2 OriginalResolution
        {
            get { return windowInfo.OriginalSize; }
        }

        public int OrigX
        {
            get { return (int)OriginalResolution.X; }
        }

        public int OrigY
        {
            get { return (int)OriginalResolution.Y; }
        }

        public int X
        {
            get { return (int)windowInfo.ScreenPosition.X; }
        }

        public int Y
        {
            get { return (int)windowInfo.ScreenPosition.Y; }
        }

        public Vector2 Position
        {
            get { return windowInfo.ScreenPosition; }
        }

        public string Title
        {
            get { return windowInfo.Title; }
        }

        public bool IsFullscreen
        {
            get { return isFullscreen; }
        }

        public Color ClearColor
        {
            get { return framebuffer.ClearColor; }
            set { framebuffer.ClearColor = value; }
        }

        public GlfwWindowPtr WindowPtr
        {
            get { return displayPtr; }
        }

        public bool ShouldClose
        {
            get { return Glfw.WindowShouldClose(displayPtr); }
        }

        public Mouse Mouse
        {
            get { return mouse; }
        }

        public Action<int, int> ReshapeFunction
        {
            get { return reshapeFunc; }
            set { reshapeFunc = value; }
        }

        public GameTime Time
        {
            get { return time; }
        }

        public WindowInfo Info
        {
            get { return windowInfo; }
        }

        public int Framerate
        {
            get { return (int)(1 / time.ElapsedSeconds); }
        }

        public Framebuffer Framebuffer
        {
            get { return framebuffer; }
        }

        public int UpdateCount
        {
            get { return updateCount; }
        }

        public int SmoothFramerate
        {
            get { return smoothFramerate; }
        }

        #endregion

        #region PUBLIC METHODS

        public void Clear(ClearBufferMask mask = ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit)
        {
            framebuffer.Bind();
            if (Engine.UseDefaultShader)
            {
                ShaderProgram.Default.Start();
            }
            framebuffer.Clear(mask);
        }

        public void SwapBuffers(bool update = false)
        {
            if (update)
            {
                Update();
            }
            Glfw.SwapBuffers(displayPtr);
            time.Tick();
            framerates.Add(Framerate);
            updateCount += 1;
            if (smoothTimer.Check())
            {
                float total = 0;
                foreach (float val in framerates)
                {
                    total += val;
                }
                smoothFramerate = (int)(total / framerates.Count);
                framerates.Clear();
            }
        }

        public void PollEvents()
        {
            Glfw.PollEvents();
        }

        public EventQueue GetEvents()
        {
            return eventQueue;
        }

        public void SetSize(int w, int h)
        {
            Info.SetSize(w, h);
            framebuffer.SetSize(w, h);
        }

        public void SetPosition(int x, int y)
        {
            Info.SetPosition(x, y);
        }

        public void SetTitle(string title)
        {
            Info.SetTitle(title);
        }

        public bool TestKey(Key key)
        {
            return Glfw.GetKey(displayPtr, key);
        }

        public void Close()
        {
            Glfw.SetWindowShouldClose(displayPtr, true);
        }

        public void SetAsContext()
        {
            Glfw.MakeContextCurrent(displayPtr);
            Context.Destroy();
            Context.Initialise(this);
        }

        public void Update()
        {
            mouse.Update();
            eventQueue.Clear();
            Timers.Update();
        }

        public static void SetWindowHint(WindowHint hint, int value)
        {
            Glfw.WindowHint(hint, value);
        }

        #endregion

        #region PRIVATE METHODS

        private void IntermediateReshape(GlfwWindowPtr window, int w, int h)
        {
            reshapeFunc(w, h);
        }

        private void Reshape(int w, int h)
        {
            SetSize(w, h);
            GL.Viewport(0, 0, w, h);
        }

        private void CreateWindow()
        {
            GlfwWindowPtr context = Glfw.CreateWindow(Width, Height, Title, GlfwMonitorPtr.Null, GlfwWindowPtr.Null);
            displayPtr = context;
            SetAsContext();
        }

        #endregion

    }
}
