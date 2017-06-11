using System;

namespace OpenEngine
{

    public class BufferException : Exception
    {

        public BufferException(string msg) : base(msg)
        {

        }

    }

    public class ShaderCompileException : Exception
    {

        public ShaderCompileException(string msg) : base(msg)
        {

        }

    }

    public class ShaderManagementException : Exception
    {

        public ShaderManagementException(string msg) : base(msg)
        {

        }

    }

    public class EngineException : Exception
    {

        public EngineException(string msg) : base(msg)
        {

        }

    }

    public class ComponentException : Exception
    {

        public ComponentException(string msg) : base(msg)
        {

        }

    }

    public class XMLException : Exception
    {

        public XMLException(string msg) : base(msg)
        {

        }

    }

    public class RendererException : Exception
    {

        public RendererException(string msg) : base(msg)
        {

        }

    }

    public class BatchException : Exception
    {

        public BatchException(string msg) : base(msg)
        {

        }

    }

}
