#version 430 core

in vec3 f_WorldPosition;
in vec3 f_WorldNormal;
in vec2 f_TexCoords;
in vec4 f_Color;

uniform vec4 Color;
uniform sampler2D Tex0;

out vec4 finalColor;

const float width = 0.2;
const float edge = 0.45;

void main()
{
	float distance = 1.0 - texture(Tex0, f_TexCoords).a;
	float alpha = 1.0 - smoothstep(width, width + edge, distance);
	
	finalColor = vec4(f_Color.xyz, alpha);	
}