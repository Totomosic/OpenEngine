using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine
{
    public class OctreeNode
    {

        #region FIELDS
        public static uint camera;
        public static ShaderProgram shader;

        private const int SUBDIVISIONS = 8;

        private BoundingBox region;
        private OctreeNode parent;

        private OctreeNode[] childrenNodes;
        private List<uint> entities;

        private int depth;
        private bool isLeaf;
        private uint entity;

        #endregion

        #region CONSTRUCTORS

        public OctreeNode(OctreeNode parent, BoundingBox region, int depth)
        {
            this.parent = parent;
            this.region = region;
            this.depth = depth;
            childrenNodes = new OctreeNode[8];
            entities = new List<uint>();
            isLeaf = true;
        }

        #endregion

        #region PROPERTIES

        public BoundingBox Region
        {
            get { return region; }
            set { region = value; }
        }

        public OctreeNode[] Children
        {
            get { return childrenNodes; }
        }

        public List<uint> Objects
        {
            get { return entities; } 
        }

        public int ObjectCount
        {
            get { return Objects.Count; }
        }

        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public bool IsLeaf
        {
            get { return isLeaf; }
            private set { isLeaf = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public OctreeNode GetNode(int index = 0)
        {
            if (IsLeaf || index > 7)
            {
                return null;
            }
            return childrenNodes[index];
        }

        public OctreeNode GetNode(NodeIndex index)
        {
            return GetNode((int)index);
        }

        public uint[] GetAllEntities()
        {
            List<uint> ents = entities.Copy();
            if (!IsLeaf)
            {
                foreach (OctreeNode node in childrenNodes)
                {
                    ents.AddRange(node.GetAllEntities());
                }
            }
            return ents.ToArray();
        }

        public int TotalEntityCount()
        {
            int count = ObjectCount;
            if (!IsLeaf)
            {
                foreach (OctreeNode node in childrenNodes)
                {
                    count += node.TotalEntityCount();
                }
            }
            return count;
        }

        public int GetNodeCount()
        {
            if (!IsLeaf)
            {
                int count = 9;
                foreach (OctreeNode node in childrenNodes)
                {
                    count += node.GetNodeCount();
                }
                return count;
            }
            return 0;
        }

        public void Update(GameTime time)
        {
            UpdateObjects();
            Subdivide();
            if (!IsLeaf)
            {
                for (int i = 0; i < SUBDIVISIONS; i++)
                {
                    childrenNodes[i].Update(time);
                }
            }
        }

        /* public void AddItem(uint entity)
        {
            if (Entities.HasComponent<CTransform>(entity))
            {
                if (IsLeaf)
                {
                    entities.Add(entity);
                }
                else
                {
                    bool added = false;
                    CTransform transform = Entities.GetComponent<CTransform>(entity);
                    for (int i = 0; i < SUBDIVISIONS; i++)
                    {
                        if (Entities.HasComponent<CAABB>(entity))
                        {
                            CAABB aabb = Entities.GetComponent<CAABB>(entity);
                            if (childrenNodes[i].Region.Contains(BoundingBox.CreateFromCenter(transform.Position, aabb.Size)).Collided)
                            {
                                childrenNodes[i].AddItem(entity);
                                added = true;
                                break;
                            }
                        }
                        else if (childrenNodes[i].Region.Contains(transform.Position).Collided)
                        {
                            childrenNodes[i].AddItem(entity);
                            added = true;
                            break;
                        }
                    }
                    if (!added)
                    {
                        entities.Add(entity);
                    }
                }
            }
        }*/

        public void RemoveItem(uint entity)
        {
            if (IsLeaf)
            {
                if (entities.Contains(entity))
                {
                    entities.Remove(entity);
                }
            }
            else
            {
                for (int i = 0; i < SUBDIVISIONS; i++)
                {
                    childrenNodes[i].RemoveItem(entity);
                }
            }
        }

        public void Subdivide()
        {
            if (childrenNodes[0] == null && ObjectCount > 1)
            {
                IsLeaf = false;
                Vector3 center = Region.Position;
                BoundingBox[] bounds = new BoundingBox[8];
                bounds[0] = BoundingBox.CreateFromCorner(Region.Min, center); // bottom left front
                bounds[1] = BoundingBox.CreateFromCorner(new Vector3(center.X, Region.Min.Y, region.Min.Z), new Vector3(region.Max.X, center.Y, center.Z)); // bottom right front
                bounds[2] = BoundingBox.CreateFromCorner(new Vector3(Region.Min.X, Region.Min.Y, center.Z), new Vector3(center.X, center.Y, region.Max.Z)); // bottom left back
                bounds[3] = BoundingBox.CreateFromCorner(new Vector3(center.X, region.Min.Y, center.Z), new Vector3(Region.Max.X, center.Y, region.Max.Z)); // bottom right back
                bounds[4] = BoundingBox.CreateFromCorner(new Vector3(region.Min.X, center.Y, region.Min.Z), new Vector3(center.X, region.Max.Y, center.Z)); // top left front
                bounds[5] = BoundingBox.CreateFromCorner(new Vector3(center.X, center.Y, region.Min.Z), new Vector3(region.Max.X, region.Max.Y, center.Z)); // top right front
                bounds[6] = BoundingBox.CreateFromCorner(new Vector3(Region.Min.X, center.Y, center.Z), new Vector3(center.X, region.Max.Y, region.Max.Z)); // top left back
                bounds[7] = BoundingBox.CreateFromCorner(center, Region.Max); // top right back

                for (int i = 0; i < SUBDIVISIONS; i++)
                {
                    childrenNodes[i] = new OctreeNode(this, bounds[i], Depth + 1);
                }
                List<uint> removeList = new List<uint>();
                foreach (uint entity in entities)
                {
                    foreach (OctreeNode node in childrenNodes)
                    {
                    }                    
                }
                foreach (OctreeNode node in childrenNodes)
                {
                    node.Subdivide();
                }
                foreach (uint entity in removeList)
                {
                    entities.Remove(entity);
                }            
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void UpdateObjects()
        {
            List<uint> movedObjects = new List<uint>();
            foreach (uint entity in entities)
            {
            }
            OctreeNode current = this;
            while (current.parent != null)
            {
                current = current.parent;
            }
            foreach (uint entity in movedObjects)
            {
                entities.Remove(entity);
            }
        }

        #endregion

    }
}
