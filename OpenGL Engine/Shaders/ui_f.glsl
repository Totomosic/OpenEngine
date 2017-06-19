#version 430 core

struct MaterialStruct
{
	vec4 Diffuse;
	vec4 Specular;
	float Reflectivity;
	float ShineDamper;
};

in vec3 f_WorldPosition;
in vec3 f_WorldNormal;
in vec2 f_TexCoords;
in vec4 f_Color;

uniform MaterialStruct Material;

out vec4 finalColor;

void main()
{
	finalColor = f_Color * Material.Diffuse;
}