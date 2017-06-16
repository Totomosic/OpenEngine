using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine
{
    public static class GameObjects
    {

        #region PUBLIC METHODS

        public static GameObject[] FIndObjectsByTag(string tag)
        {
            List<GameObject> objects = new List<GameObject>();
            GameObject[] entities = GetAllObjectsWith<Identifier>();
            foreach (GameObject obj in entities)
            {
                if (obj.Tag == tag)
                {
                    objects.Add(obj);
                }
            }
            return objects.ToArray();
        }

        public static GameObject FindObjectByTag(string tag)
        {
            GameObject[] objects = FIndObjectsByTag(tag);
            if (objects.Length > 0)
            {
                return objects[0];
            }
            return null;
        }

        public static GameObject[] GetAllObjects()
        {
            List<GameObject> objects = new List<GameObject>();
            for (uint i = 0; i <= GameObject.HighestID; i++)
            {
                GameObject obj = null;
                if ((obj = GameObject.GetAtID(i)) != null)
                {
                    objects.Add(obj);
                }
            }
            return objects.ToArray();
        }

        public static GameObject[] GetAllObjectsWith(Type[] types)
        {
            List<GameObject> objects = new List<GameObject>();
            for (uint i = 0; i <= GameObject.HighestID; i++)
            {
                GameObject obj = null;
                if ((obj = GameObject.GetAtID(i)) != null)
                {
                    bool passed = true;
                    foreach (Type type in types)
                    {
                        if (!obj.Components.HasComponent(type))
                        {
                            passed = false;
                            break;
                        }
                    }
                    if (passed)
                    {
                        objects.Add(obj);
                    }
                }
            }
            return objects.ToArray();
        }

        public static GameObject[] GetAllObjectsWith(Type type)
        {
            return GetAllObjectsWith(new Type[] { type });
        }

        public static GameObject[] GetAllObjectsWith<T1>()
        {
            return GetAllObjectsWith(new Type[] { typeof(T1) });
        }

        public static GameObject GetAtID(uint id)
        {
            return GameObject.GetAtID(id);
        }

        public static void DestroyObject(uint id)
        {
            if (GameObject.IsValidGameObject(id))
            {
                GetAtID(id).Destroy();
            }
        }

        #endregion

    }
}
