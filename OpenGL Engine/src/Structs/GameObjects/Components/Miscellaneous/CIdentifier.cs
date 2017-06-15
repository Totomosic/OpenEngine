using System;

namespace OpenEngine.Components
{
    public class CIdentifier : Component
    {

        public CIdentifier(string identifier)
        {
            ID = identifier;
        }

        public CIdentifier() : this("Default")
        {

        }

        public virtual string ID
        {
            get; set;
        }

    }
}
