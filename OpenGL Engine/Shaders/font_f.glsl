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

uniform sampler2D Tex0;
uniform MaterialStruct Material;

out vec4 finalColor;

void main()
{

	vec4 sampled = vec4(1.0, 1.0, 1.0, texture(Tex0, f_TexCoords).r);
	finalColor = vec4(f_Color * Material.Diffuse) * sampled;

}