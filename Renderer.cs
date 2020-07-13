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

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public Shader(string vertexPath, string fragmentPath)
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

        // Deleting when application closed

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

    public struct Mesh
    {
        public float[] verts;
        public uint[]  indices;
    }

    class Renderer
    {
        public static Shader shader;

        public static float[] vertices = {
            -0.5f, -0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
             0.0f,  0.5f, 0.0f
        };

        public static Mesh mesh;

        public static int ElementBufferObject;
        public static int VertexBufferObject;
        public static int VertexArrayObject;

        public static void Update()
        {
            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.verts.Length * sizeof(float), mesh.verts, BufferUsageHint.DynamicDraw);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            //^ old draw arrays

            GL.DrawElements(PrimitiveType.Triangles, mesh.indices.Length, DrawElementsType.UnsignedInt, 0);

            //for (int i = 0; i < vertices.Length; i++)
            //{
            //    vertices[i] = vertices[i] + (0.05f * (float)Game.DeltaTime);
            //}

            for (int i = 0; i < mesh.verts.Length; i++)
            {
                mesh.verts[i] = mesh.verts[i] + (0.05f * (float)Game.DeltaTime);
            }
        }

        public static void Setup()
        {
            mesh.verts = new float[]{
                 0.5f,  0.5f,  0.0f, // top right
                 0.5f, -0.5f,  0.0f, // bottom right
                -0.5f, -0.5f,  0.0f, // bottom left
                -0.5f,  0.5f,  0.0f  // top left
            };
            mesh.indices = new uint[]{
                0, 1, 3, // first triangle
                1, 2, 3  // second triangle
            };

            #region OpenGL stuff
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);



            // Vertex Buffer Object
            VertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.verts.Length * sizeof(float), mesh.verts, BufferUsageHint.StaticDraw);

            // Element Buffer Object
            ElementBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.indices.Length * sizeof(uint), mesh.indices, BufferUsageHint.StaticDraw);

            // Vertex Attribute Pointer
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            #endregion
        }

        public static void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);

            shader.Dispose();
        }

    }
}
