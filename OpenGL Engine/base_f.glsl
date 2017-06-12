#version 430 core

struct Light
{
	//Type: 0 = Point, 1 = Directional, 2 = Spotlight
	int Type;
	vec3 Position;
	vec4 Color;
	float Ambient;
	vec3 Attenuation;
	vec3 Direction;
	float Angle;
};

const int PointLight = 0;
const int DirectionalLight = 1;
const int SpotLight = 2;

in vec3 f_WorldPosition;
in vec3 f_WorldNormal;
in vec2 f_TexCoords;
in vec4 f_Color;

uniform vec4 Color;

//Lighting
const int MAX_LIGHTS = 10;
uniform bool UseLighting;
uniform int UsedLights;
uniform Light[MAX_LIGHTS] Lights;

out vec4 finalColor;

void main()
{
	vec4 finalDiffuse = vec4(0);
	vec4 finalSpecular = vec4(0);
	for (int i = 0; i < UsedLights; i++)
	{
		float brightness = Lights[i].Ambient;
		float attenuationFactor;
		if (Lights[i].Type == PointLight)
		{
			vec3 toLightVector = Lights[i].Position - f_WorldPosition;
			vec3 unitToLightVector = normalize(toLightVector);
			float distanceToLight = length(toLightVector);
			attenuationFactor = 1.0 / (Lights[i].Attenuation.x + Lights[i].Attenuation.y * distanceToLight + Lights[i].Attenuation.z * pow(distanceToLight, 2));

			brightness = max(dot(f_WorldNormal, unitToLightVector), Lights[i].Ambient);
		}
		else if (Lights[i].Type == DirectionalLight)
		{
			brightness = max(dot(-normalize(Lights[i].Direction), f_WorldNormal), Lights[i].Ambient);
			attenuationFactor = 1;
		}
		else if (Lights[i].Type == SpotLight)
		{
			vec3 toLightVector = Lights[i].Position - f_WorldPosition;
			vec3 unitToLightVector = normalize(toLightVector);
			float distanceToLight = length(toLightVector);
			float lightToSurfaceAngle = degrees(acos(dot(-unitToLightVector, normalize(Lights[i].Direction))));
			attenuationFactor = 1.0 / (Lights[i].Attenuation.x + Lights[i].Attenuation.y * distanceToLight + Lights[i].Attenuation.z * pow(distanceToLight, 2));

			if (lightToSurfaceAngle < Lights[i].Angle)
			{
				brightness = max(dot(f_WorldNormal, unitToLightVector) * clamp(Lights[i].Angle - lightToSurfaceAngle, 0, 1), Lights[i].Ambient);
			}
		}
		finalDiffuse += Color * f_Color * Lights[i].Color * brightness * attenuationFactor;
		finalDiffuse.a = Color.a;
	}
	finalColor = finalDiffuse + finalSpecular;
}