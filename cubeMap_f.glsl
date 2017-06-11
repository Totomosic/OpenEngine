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
in vec3 f_TexCoords;

uniform samplerCube Tex0;

out vec4 finalColor;

void main()
{
	finalColor = texture(Tex0, normalize(f_TexCoords));
}