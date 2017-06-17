#version 430 core

in vec3 f_WorldPosition;
in vec3 f_WorldNormal;
in vec2 f_TexCoords;
in vec4 f_Color;

out vec4 finalColor;

void main()
{
	finalColor = f_Color;
}