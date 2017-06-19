using System;
using OpenEngine.Components;

namespace OpenEngine
{
    public static class ScriptingSystem
    {

        public static void Update(GameTime time)
        {
            GameObject[] objects = GameObjects.GetAllObjectsWithDerived(typeof(Script));
            foreach (GameObject obj in objects)
            {
                foreach (Component script in obj.Components.GetComponentsDerivedFrom(typeof(Script)))
                {
                    Script s = script as Script;
                    s.Update();
                }
            }
        }

    }
}
