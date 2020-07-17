using System;
using System.Text;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Tukxel
{
    class Shader : IDisposable
    {
        public int Handle;

        public void Use()
        {
            try
            {
                
                int location = 0;
                location = GL.GetUniformLocation(Handle, "model");
                GL.UniformMatrix4(location, true, ref Renderer.model);

                location = GL.GetUniformLocation(Handle, "view");
                GL.UniformMatrix4(location, true, ref Renderer.view);

                location = GL.GetUniformLocation(Handle, "projection");
                GL.UniformMatrix4(location, true, ref Renderer.projection);

                GL.UseProgram(Handle);
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Shader.Use(), using the shader.");
            }
        }

        public int GetAttribLocation(string attribName)
        {
            try
            {
                return GL.GetAttribLocation(Handle, attribName);
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Shader.GetAttribLocatin(), getting attribute location.");
            }
            return 0;
        }

        public Shader(string vertexPath, string fragmentPath)
        {
            try
            {
                int VertexShader;
                int FragmentShader;

                // Getting source into shaders

                string VertexShaderSource;

                StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8);

                VertexShaderSource = reader.ReadToEnd();

                string FragmentShaderSource;

                reader = new StreamReader(fragmentPath, Encoding.UTF8);
                FragmentShaderSource = reader.ReadToEnd();

                VertexShader = GL.CreateShader(ShaderType.VertexShader);
                GL.ShaderSource(VertexShader, VertexShaderSource);

                FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
                GL.ShaderSource(FragmentShader, FragmentShaderSource);

                // Compiling shaders

                GL.CompileShader(VertexShader);

                string infoLogVert = GL.GetShaderInfoLog(VertexShader);
                if (infoLogVert != String.Empty) Debugger.Error(infoLogVert, "compiling vertex shader(in shader class)");

                GL.CompileShader(FragmentShader);

                string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);
                if (infoLogFrag != String.Empty) Debugger.Error(infoLogFrag, "compiling fragment shader(in shader class)");

                Handle = GL.CreateProgram();

                GL.AttachShader(Handle, VertexShader);
                GL.AttachShader(Handle, FragmentShader);

                GL.LinkProgram(Handle);

                // Cleaning up

                GL.DetachShader(Handle, VertexShader);
                GL.DetachShader(Handle, FragmentShader);
                GL.DeleteShader(FragmentShader);
                GL.DeleteShader(VertexShader);
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Shader.Shader(), constructing the shader.");
            }
        }

        // Deleting when application closed

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!disposedValue)
                {
                    GL.DeleteProgram(Handle);

                    disposedValue = true;
                }
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Shader.Dispose(), disposing the shader.");
            }
        }

        ~Shader()
        {
            try
            {
                GL.DeleteProgram(Handle);
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Shader.~Shader()(??wot)");
            }
        }

        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Shader.Dispose(), disposing the shader.");
            }
        }
    }
}
