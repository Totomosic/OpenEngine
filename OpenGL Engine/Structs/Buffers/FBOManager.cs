using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenEngine
{
    public static class FBOManager
    {

        #region FIELDS

        private static Dictionary<string, FBO> fbos;

        #endregion

        #region INIT

        static FBOManager()
        {
            fbos = new Dictionary<string, FBO>();
        }

        #endregion

        #region PROPERTIES

        public static Dictionary<string, FBO> FBOs
        {
            get { return fbos; }
        }

        #endregion

        #region PUBLIC METHODS

        public static FBO GetFBO(string name)
        {
            return fbos[name];
        }

        public static string[] GetAllNames()
        {
            return fbos.Keys.ToArray();
        }

        public static FBO[] GetAllFBOs()
        {
            return fbos.Values.ToArray();
        }

        public static void AddFBO(FBO fbo)
        {
            if (!FBOs.ContainsKey(fbo.Name))
            {
                fbos[fbo.Name] = fbo;
                return;
            }
            throw new EngineException("Fbo with name: " + fbo.Name + ", already exists.");
        }

        public static void Clear()
        {
            fbos.Clear();
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
