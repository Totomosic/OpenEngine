using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class Octree
    {

        #region FIELDS

        public static bool Debug = false;

        private BoundingBox region;
        private OctreeNode rootNode;

        #endregion

        #region CONSTRUCTORS

        public Octree(BoundingBox region)
        {
            this.region = region;
            rootNode = new OctreeNode(null, region, 0);
        }

        #endregion

        #region PROPERTIES

        public OctreeNode Root
        {
            get { return rootNode; }
        }

        #endregion

        #region PUBLIC METHODS

        public void CreateEntities(uint camera, ShaderProgram shader)
        {
            OctreeNode.camera = camera;
            OctreeNode.shader = shader;
            //rootNode.CreateEntity();
        }

        public int GetNodeCount()
        {
            return rootNode.GetNodeCount();
        }

        public void Update(GameTime time)
        {
            rootNode.Update(time);
        }

        public void AddItem(uint entity)
        {
            //rootNode.AddItem(entity);
        }

        public void RemoveItem(uint entity)
        {
            rootNode.RemoveItem(entity);
        }

        public void AddItems(uint[] entities)
        {
            foreach (uint ent in entities)
            {
                AddItem(ent);
            }
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
