#version 430 core

struct MatrixStack
{
	mat4 Model;
	mat4 View;
	mat4 Projection;
};

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 tex;
layout(location = 3) in vec4 color;

uniform MatrixStack Matrices;

out vec3 f_WorldPosition;
out vec3 f_WorldNormal;
out vec2 f_TexCoords;
out vec4 f_Color;

void main()
{
	vec4 v_WorldSpacePosition = Matrices.Model * vec4(position, 1f);
	vec4 v_CameraSpacePosition = Matrices.View * v_WorldSpacePosition;
	vec4 v_ScreenSpacePosition = Matrices.Projection * v_CameraSpacePosition;

	gl_Position = v_ScreenSpacePosition;	

	f_WorldPosition = v_WorldSpacePosition.xyz;
	f_WorldNormal = normalize(Matrices.Model * vec4(normal, 0)).xyz;
	f_TexCoords = tex;
	f_Color = color;
}