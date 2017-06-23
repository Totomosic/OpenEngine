using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine
{
    /// <summary>
    /// Class that manages all physics
    /// </summary>
    public class PhysicsEngine
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public PhysicsEngine()
        {

        }

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Update(GameTime time)
        {
            GameObject[] objects = GameObjects.GetAllObjectsWith(new Type[] { typeof(Transform), typeof(RigidBody) });
            foreach (GameObject firstObject in objects)
            {
                if (!firstObject.HasComponent<BoxCollider>())
                {
                    // Did not have a suitable collider for physics
                    continue;
                }                
                foreach (GameObject secondObject in objects)
                {
                    if (firstObject == secondObject)
                    {
                        continue;
                    }
                    ComputePhysics(firstObject, secondObject);
                }
            }
        }

        #endregion

        #region PRIVATE METHODS

        private static void ComputePhysics(GameObject firstObject, GameObject secondObject)
        {
            Transform transform1 = firstObject.Transform;
            RigidBody rb1 = firstObject.GetComponent<RigidBody>();
            BoxCollider collider1 = firstObject.GetComponent<BoxCollider>();
            Transform transform2 = secondObject.Transform;
            RigidBody rb2 = secondObject.GetComponent<RigidBody>();
            BoxCollider collider2 = secondObject.GetComponent<BoxCollider>();
        }

        #endregion

    }
}
