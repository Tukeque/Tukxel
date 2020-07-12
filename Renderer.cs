using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System;
using System.Text;
using System.IO;

namespace Tukxel
{
    class Shader : IDisposable
    {
        int Handle;

        void Use()
        {
            GL.UseProgram(Handle);
        }

        public Shader(string vertexPath, string fragmentPath)
        {
            int VertexShader;
            int FragmentShader;

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

            GL.CompileShader(VertexShader);

            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != String.Empty)
                Debugger.Error(infoLogVert, "compiling vertex shader");

            GL.CompileShader(FragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);
            if (infoLogFrag != String.Empty)
                Debugger.Error(infoLogFrag, "compiling fragment shader");

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            // cleaning up

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    class Renderer
    {
        public static float[] vertices = {
            -0.5f, -0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
             0.0f,  0.5f, 0.0f
        };

        public static int VertexBufferObject;

        public static void Update()
        {

        }

        public static void Setup()
        {
            VertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            Game.shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        }

        public static void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);

            Game.shader.Dispose();
        }

    }
}
