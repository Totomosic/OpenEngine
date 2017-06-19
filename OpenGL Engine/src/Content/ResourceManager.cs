using System;
using System.Collections.Generic;

namespace OpenEngine
{
    /// <summary>
    /// Class that manages OpenGL Resources
    /// </summary>
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

        /// <summary>
        /// Determine whether a model has been referenced
        /// </summary>
        /// <param name="model">Model to check</param>
        /// <returns></returns>
        public static bool IsReferenced(Model model)
        {
            return referenceCount.ContainsKey(model);
        }

        /// <summary>
        /// Determine how many Models are currently referenced
        /// </summary>
        /// <returns></returns>
        public static int ModelCount()
        {
            return referenceCount.Keys.Count;
        }

        /// <summary>
        /// Determine the number of references to a specific model
        /// </summary>
        /// <param name="model">Model to check</param>
        /// <returns></returns>
        public static int GetReferenceCount(Model model)
        {
            if (referenceCount.ContainsKey(model))
            {
                return referenceCount[model];
            }
            return 0;
        }

        /// <summary>
        /// Request a reference to a model
        /// </summary>
        /// <param name="model">Model to get reference to</param>
        /// <returns></returns>
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

        /// <summary>
        /// Release reference
        /// </summary>
        /// <param name="model">Model to release reference from</param>
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
