using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public static class ResourceManager
    {

        #region FIELDS

        private static Dictionary<Model, int> referenceCount;

        #endregion

        #region INIT

        static ResourceManager()
        {
            referenceCount = new Dictionary<Model, int>();
        }

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public static int ModelCount()
        {
            return referenceCount.Keys.Count;
        }

        public static int GetReferenceCount(Model model)
        {
            if (referenceCount.ContainsKey(model))
            {
                return referenceCount[model];
            }
            return 0;
        }

        public static Model FetchReference(Model model)
        {
            if (referenceCount.ContainsKey(model))
            {
                referenceCount[model]++;
            }
            else
            {
                referenceCount[model] = 1;
            }
            return model;
        }

        public static void ReleaseReference(Model model)
        {
            if (referenceCount.ContainsKey(model))
            {
                referenceCount[model]--;
                if (GetReferenceCount(model) == 0)
                {
                    referenceCount.Remove(model);
                    model.Dispose();
                }
            }
            else
            {
                throw new EngineException("Model did not have an existing reference to release.");
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
