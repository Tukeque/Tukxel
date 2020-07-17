using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using System;

namespace Tukxel
{
    public struct Mesh
    {
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

        public void Draw()
        {
            texture.Use();
            shader.Use();
            GL.BindVertexArray(VertexArrayObject);

            if (UseElementBufferObject)
                GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            else
                GL.DrawArrays(PrimitiveType.Triangles, 0, verts.Length);
        }

        public void Setup()
        {
            // Vertex Array Object
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            // Vertex Buffer Object
            VertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticDraw);

            //Shader
            shader = new Shader(ShaderVertexPath, ShaderFragmentPath);
            shader.Use();

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
        public static Matrix4 CreateModel()
        {
            Matrix4 matrix;

            matrix =  Matrix4.CreateRotationX(MathHelper.DegreesToRadians(theta * 2));
            matrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(theta));
            matrix *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(0.0f));

            return matrix;
        }

        static float theta;

        // Matrices
        public static Matrix4 model = CreateModel();
        public static Matrix4 view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        public static Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Tukxel.Width / Tukxel.Height, 0.1f, 100.0f);

        public static Mesh rectangol;
        public static Mesh coob;

        public static void Update()
        {
            try
            {
                theta++;
                model = CreateModel();

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

                #region rectangol
                rectangol.verts = new float[]
                {
                // [Position      ] [Texture coords]
                     0.5f,  0.5f,  0.0f, 1.0f, 0.0f, // top right
                     0.5f, -0.5f,  0.0f, 1.0f, 1.0f, // bottom right
                    -0.5f, -0.5f,  0.0f, 0.0f, 1.0f, // bottom left
                    -0.5f,  0.5f,  0.0f, 0.0f, 0.0f  // top left
                };
                rectangol.indices = new uint[]
                {
                    0, 1, 3, // first triangle
                    1, 2, 3  // second triangle
                };
                #endregion

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
