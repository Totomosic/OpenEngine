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

        /// <summary>
        /// Window object that represents the Display and OpenGL Context
        /// </summary>
        /// <param name="w">Width in pixels of the window</param>
        /// <param name="h">Height in pixels of the window</param>
        /// <param name="title">Title displayed in top left of window</param>
        /// <param name="cColor">Clear color of the window</param>
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

        /// <summary>
        /// Clears the window to specified ClearColor
        /// </summary>
        /// <param name="mask">Specify which buffers to clear</param>
        public void Clear(ClearBufferMask mask = ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit)
        {
            framebuffer.Bind();
            if (Engine.UseDefaultShader)
            {
                ShaderProgram.Default.Start();
            }
            framebuffer.Clear(mask);
        }

        /// <summary>
        /// Swap the back and front framebuffer to display the next frame
        /// </summary>
        /// <param name="update">Specify whether the window should update at the same time</param>
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

        /// <summary>
        /// Respond to the Windows OS and propulate event Queue
        /// </summary>
        public void PollEvents()
        {
            Glfw.PollEvents();
        }

        /// <summary>
        /// Returns a Queue of all events received since last Update() call
        /// </summary>
        /// <returns></returns>
        public EventQueue GetEvents()
        {
            return eventQueue;
        }

        /// <summary>
        /// Sets window size
        /// </summary>
        /// <param name="w">New width in pixels</param>
        /// <param name="h">New height in pixels</param>
        public void SetSize(int w, int h)
        {
            Info.SetSize(w, h);
            framebuffer.SetSize(w, h);
        }

        /// <summary>
        /// Sets the location that the window appears on the screen
        /// </summary>
        /// <param name="x">X coordinate of the top left of the window</param>
        /// <param name="y">Y coordinate of the top left of the window</param>
        public void SetPosition(int x, int y)
        {
            Info.SetPosition(x, y);
        }

        /// <summary>
        /// Sets the window title
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(string title)
        {
            Info.SetTitle(title);
        }

        /// <summary>
        /// Test if the given key is currently held down
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns></returns>
        public bool TestKey(Key key)
        {
            return Glfw.GetKey(displayPtr, key);
        }

        /// <summary>
        /// Indicate that the window should be closed, does not close the window
        /// </summary>
        public void Close()
        {
            Glfw.SetWindowShouldClose(displayPtr, true);
        }

        /// <summary>
        /// Set this window as the current OpenGL Context
        /// </summary>
        public void SetAsContext()
        {
            Glfw.MakeContextCurrent(displayPtr);
            Context.Destroy();
            Context.Initialise(this);
        }

        /// <summary>
        /// Update the mouse and active timers, clears event queue
        /// </summary>
        public void Update()
        {
            mouse.Update();
            eventQueue.Clear();
            Timers.Update();
        }

        /// <summary>
        /// Set window hints before the creation of a window to influence it
        /// </summary>
        /// <param name="hint">GLFW hint</param>
        /// <param name="value">Hint value</param>
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
