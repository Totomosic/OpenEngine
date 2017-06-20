using System;
using System.Collections.Generic;
using OpenEngine.Components;

namespace OpenEngine
{
    /// <summary>
    /// Component that represents a particle system
    /// </summary>
    public class ParticleSystem : Script
    {

        #region FIELDS

        private Vector3 positionOffset;

        private GameObject particlePrefab;
        private float particlesPerSecond;
        private SystemBehaviour particleBehaviour;

        private float currentTime;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructs a new Particle system component
        /// </summary>
        /// <param name="particle">Particle to spawn</param>
        /// <param name="particlesPerSecond">Particles per second</param>
        /// <param name="particleBehaviour">Behaviour of particles</param>
        /// <param name="positionOffset">Position of system relative to attached object</param>
        public ParticleSystem(GameObject particle, float particlesPerSecond, SystemBehaviour particleBehaviour, Vector3 positionOffset = new Vector3())
        {
            particlePrefab = particle;
            this.particlesPerSecond = particlesPerSecond;
            this.particleBehaviour = particleBehaviour;
            this.positionOffset = positionOffset;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ParticleSystem() : this(null, 0, null)
        {

        }

        /// <summary>
        /// Init called by component system, Owner is set during this call
        /// </summary>
        public override void Initialise()
        {
            currentTime = 0;
        }

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Called each frame
        /// </summary>
        public override void Update()
        {
            currentTime += GameTime.ElapsedSeconds;
            while (currentTime >= (1 / particlesPerSecond))
            {
                GameObject particle = GameObject.Instantiate(particlePrefab, Owner.Transform.Position + positionOffset);
                if (particleBehaviour != null)
                {
                    particle.GetComponent<RigidBody>().Velocity = particleBehaviour.NextVelocity;
                }
                currentTime -= 1 / particlesPerSecond;
            }
        }

        /// <summary>
        /// Return copy of this system
        /// </summary>
        /// <returns></returns>
        public override Component Clone()
        {
            ParticleSystem s = new ParticleSystem();
            s.positionOffset = positionOffset;
            s.particleBehaviour = particleBehaviour;
            s.particlePrefab = particlePrefab;
            s.particlesPerSecond = particlesPerSecond;
            return s;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
