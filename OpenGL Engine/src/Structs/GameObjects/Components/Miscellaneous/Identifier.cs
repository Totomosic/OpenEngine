using System;

namespace OpenEngine.Components
{
    public class Identifier : Component
    {

        public Identifier(string identifier)
        {
            ID = identifier;
        }

        public Identifier() : this("Default")
        {

        }

        public virtual string ID
        {
            get; set;
        }

        public override Component Clone()
        {
            Identifier id = new Identifier();
            id.ID = ID;
            return id;
        }

    }
}
