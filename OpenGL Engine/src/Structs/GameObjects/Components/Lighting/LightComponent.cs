using System;

namespace OpenEngine.Components
{
    public class LightComponent : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public LightComponent(LightType type, float ambient, Vector3 attenuation, float angle)
        {
            Type = type;    
            Ambient = ambient;
            Attenuation = attenuation;
            Angle = angle;
        }

        public LightComponent(LightType type, float ambient, float angle) : this(type, ambient, new Vector3(1, 0, 0), angle)
        {

        }

        public LightComponent(LightType type, float ambient = 0.2f) : this(type, ambient, 15f)
        {

        }

        public LightComponent() : this(LightType.Point)
        {

        }

        #endregion

        #region PROPERTIES

        public virtual LightType Type
        {
            get; set;
        }

        public virtual float Ambient
        {
            get; set;
        }

        public virtual Vector3 Attenuation
        {
            get; set;
        }

        public virtual float Angle
        {
            get; set;
        }

        #endregion

        public override Component Clone()
        {
            LightComponent light = new LightComponent();
            light.Type = Type;
            light.Ambient = Ambient;
            light.Attenuation = Attenuation;
            light.Angle = Angle;
            return light;
        }

    }
}
