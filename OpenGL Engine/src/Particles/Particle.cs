using System;
using OpenEngine.Components;

namespace OpenEngine
{
    public class Particle : GameObject
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public Particle(Vector3 position, Vector3 velocity, float scale = 1) : base(position)
        {
            Model = Rectangle.CreateModel(scale, scale, Color.White);
            AddComponent(new RigidBody(1, velocity, new Vector3()));
        }

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
