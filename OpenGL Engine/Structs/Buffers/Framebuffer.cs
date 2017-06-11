using System;

namespace OpenEngine
{
    public class Framebuffer : FBO
    {

        public Framebuffer(int width, int height) : base("Framebuffer", width, height)
        {
            ID = 0;
        }

    }
}
