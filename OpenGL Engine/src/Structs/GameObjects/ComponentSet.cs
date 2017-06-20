using System;
using System.Collections.Generic;
using OpenEngine.Components;
using System.Linq;

namespace OpenEngine
{
    public class ComponentSet
    {

        #region FIELDS

        private Dictionary<Type, Component> components;
        private bool canInherit;
        private GameObject owner;

        #endregion

        #region CONSTRUCTORS

        public ComponentSet(GameObject owner)
        {
            this.owner = owner;
            components = new Dictionary<Type, Component>();
            canInherit = true;
        }

        #endregion

        #region PROPERTIES

        public bool CanInherit
        {
            get { return canInherit; }
            set { canInherit = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public void RemoveAll()
        {
            components.Clear();
        }

        public bool HasComponent<T>()
            where T : Component
        {
            return HasComponent(typeof(T));
        }

        public bool HasComponent(Type type)
        {
            return components.ContainsKey(type);
        }

        public bool HasComponent(Type type, out Component component)
        {
            if (components.ContainsKey(type))
            {
                component = components[type];
                return true;
            }
            component = null;
            return false;
        }

        public bool HasComponent<T>(out T component)
            where T : Component
        {
            Component comp;
            bool val = HasComponent(typeof(T), out comp);
            component = comp as T;
            return val;
        }

        public bool HasComponents(Type[] types)
        {
            foreach (Type t in types)
            {
                if (!HasComponent(t))
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasComponentDerivedFrom<T>()
            where T : Component, new()
        {
            return HasComponentDerivedFrom(typeof(T));
        }

        public bool HasComponentDerivedFrom(Type type)
        {
            foreach (Type t in components.Keys)
            {
                if (t.IsSubclassOf(type))
                {
                    return true;
                }
            }
            return false;
        }

        public Component[] GetComponentsDerivedFrom<T>()
        {
            return GetComponentsDerivedFrom(typeof(T));
        }

        public Component[] GetComponentsDerivedFrom(Type type)
        {
            List<Component> comps = new List<Component>();
            foreach (Type t in components.Keys)
            {
                if (t.IsSubclassOf(type))
                {
                    comps.Add(components[t]);
                }
            }
            return comps.ToArray();
        }

        public T GetComponentDerivedFrom<T>()
            where T : Component, new()
        {
            return GetComponentDerivedFrom(typeof(T)) as T;   
        }

        public Component GetComponentDerivedFrom(Type type)
        {
            Component[] c = GetComponentsDerivedFrom(type);
            if (c.Length == 0)
            {
                return null;
            }
            return c[0];
        }

        public Component GetComponent(Type type)
        {
            Component comp;
            HasComponent(type, out comp);
            return comp;
        }

        public T GetComponent<T>()
            where T : Component
        {
            return GetComponent(typeof(T)) as T;
        }

        public void AddComponent(Component component)
        {
            components[component.GetType()] = component;
            component.Owner = owner;
        }

        public void AddComponent<T>()
            where T : Component, new()
        {
            AddComponent(Activator.CreateInstance(typeof(T)) as T); 
        }

        public void AddComponents(Component[] components)
        {
            foreach (Component c in components)
            {
                AddComponent(c);
            }
        }

        public bool RemoveComponent(Component component)
        {
            Type type = component.GetType();
            if (components.ContainsKey(type))
            {
                if (components[type] == component)
                {
                    components.Remove(type);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveComponent(Type type)
        {
            if (components.ContainsKey(type))
            {
                components.Remove(type);
                return true;
            }
            return false;
        }

        public bool RemoveComponent<T>()
        {
            return RemoveComponent(typeof(T));
        }

        public Component[] GetAllComponents()
        {
            return components.Values.ToArray();
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
