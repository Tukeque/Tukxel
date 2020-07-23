using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using System;

namespace Tukxel
{
    public struct Mesh
    {
        public static Matrix4 CreateRotation(float x, float y, float z)
        {
            Matrix4 matrix;

            matrix =  Matrix4.CreateRotationX(MathHelper.DegreesToRadians(x));
            matrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(y));
            matrix *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(z));

            return matrix;
        }

        public float[] verts;
        public uint[]  indices;
        public int ElementBufferObject;
        public int VertexBufferObject;
        public int VertexArrayObject;
        public Shader shader;
        public Texture texture;
        public bool UseElementBufferObject;
        public string ShaderVertexPath;
        public string ShaderFragmentPath;
        public string TexturePath;
        public Matrix4 translate;
        public Matrix4 rotate;

        public void Draw()
        {
            texture.Use();
            shader.Use(Camera.projection, rotate, translate * Camera.view);
            GL.BindVertexArray(VertexArrayObject);

            if (UseElementBufferObject)
                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            else
                GL.DrawArrays(PrimitiveType.Triangles, 0, verts.Length);
        }

        public void Setup()
        {
            translate = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            rotate    = CreateRotation(0.0f, 0.0f, 0.0f);

            // Vertex Array Object
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            // Vertex Buffer Object
            VertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticDraw);

            //Shader
            shader = new Shader(ShaderVertexPath, ShaderFragmentPath);
            shader.Use(Camera.projection, rotate, translate * Camera.view);

            // Element Buffer Object
            if (UseElementBufferObject)
            {
                ElementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            }

            // Vertex Attribute Pointer
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Textures
            texture = new Texture(TexturePath);
            texture.Use();

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
        }

        public void End()
        {
            GL.DeleteBuffer(VertexBufferObject);

            GL.DeleteBuffer(ElementBufferObject);
            GL.DeleteBuffer(VertexArrayObject);

            GL.DeleteProgram(shader.Handle);
            GL.DeleteProgram(texture.Handle);

            shader.Dispose();
        }
    }

    class Renderer
    {
        static float theta;

        public static Mesh coob;

        public static void Update()
        {
            try
            {
                Camera.Update();

                theta++;

                coob.rotate = Mesh.CreateRotation(theta * 2, theta, 0.0f);

                coob.Draw();
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Renderer.Update(), rendering.");
            }
        }

        public static void Setup()
        {
            try
            {
                GL.Enable(EnableCap.DepthTest);

                Camera.Setup();

                #region coob
                coob.verts = new float[]
                {
		-0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
		 0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
		 0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
		 0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
		-0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
		-0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

		-0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
		 0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
		 0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
		 0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
		-0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
		-0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

		-0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
		-0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
		-0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
		-0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
		-0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
		-0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

		 0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
		 0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
		 0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
		 0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
		 0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
		 0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

		-0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
		 0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
		 0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
		 0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
		-0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
		-0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

		-0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
		 0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
		 0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
		 0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
		-0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
		-0.5f,  0.5f, -0.5f,  0.0f, 1.0f
                };
                coob.UseElementBufferObject = false;
                coob.ShaderFragmentPath = "Shaders/shader.frag";
                coob.ShaderVertexPath   = "Shaders/shader.vert";
                coob.TexturePath        = "Images/pog.png";
                #endregion

                coob.Setup();
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Renderer.Setup(), good luck with this one because it has the most code Lmao!");
            }
        }

        public static void Unload()
        {
            try
            {
                coob.End();

                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                GL.UseProgram(0);
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Renderer.Unload(), cleaning up opengl stuff.");
            }
        }

    }
}
