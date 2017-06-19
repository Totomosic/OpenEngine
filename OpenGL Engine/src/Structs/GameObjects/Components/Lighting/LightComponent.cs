using System;

namespace OpenEngine.Components
{
    public class LightComponent : Component
    {

        #region FIELDS

        #endregion

        #region CONSTRUCTORS

        public LightComponent(LightType type, Color color, float ambient, Vector3 attenuation, float angle)
        {
            Type = type;    
            Ambient = ambient;
            Attenuation = attenuation;
            Angle = angle;
            Color = color;
        }

        public LightComponent(LightType type, Color color, float ambient, float angle) : this(type, color, ambient, new Vector3(1, 0, 0), angle)
        {

        }

        public LightComponent(LightType type, Color color, float ambient = 0.2f) : this(type, color, ambient, 15f)
        {

        }

        public LightComponent() : this(LightType.Point, Color.White)
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

        public virtual Color Color
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
            light.Color = Color;
            return light;
        }

    }
}
