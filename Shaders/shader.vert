﻿#version 330 core

layout(location = 0) in vec3 aPosition;

layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

uniform mat4 transform;

void main(void)
{
	texCoord = aTexCoord;

	gl_Position = transform * vec4(aPosition, 1.0);
}