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

const float width = 0.2;
const float edge = 0.45;

void main()
{
	float distance = 1.0 - texture(Tex0, f_TexCoords).a;
	float alpha = 1.0 - smoothstep(width, width + edge, distance);
	
	finalColor = vec4((f_Color * Material.Diffuse).xyz, alpha);	
}