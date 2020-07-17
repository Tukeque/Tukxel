using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Tukxel
{
    public struct Mesh
    {
        public float[] verts;
        public uint[]  indices;
    }

    class Renderer
    {
        static int theta;

        // Matrices
        public static Matrix4 model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(theta));
        public static Matrix4 view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        public static Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Tukxel.Width / Tukxel.Height, 0.1f, 100.0f);

        public static Shader shader;
        public static Texture texture;

        public static Mesh rectangol;
        public static Mesh coob;

        public static int ElementBufferObject;
        public static int VertexBufferObject;
        public static int VertexArrayObject;

        public static void Update()
        {
            try
            {
                theta++;
                model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(theta));

                shader.Use();
                GL.BindVertexArray(VertexArrayObject);
                GL.BufferData(BufferTarget.ArrayBuffer, coob.verts.Length * sizeof(float), coob.verts, BufferUsageHint.DynamicDraw);

                //GL.DrawElements(PrimitiveType.Triangles, rectangol.indices.Length, DrawElementsType.UnsignedInt, 0); // <- used for EBO
                GL.DrawArrays(PrimitiveType.Triangles, 0, coob.verts.Length);
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
                #endregion

                #region OpenGL stuff
                GL.Enable(EnableCap.DepthTest);

                // Vertex Array Object
                VertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(VertexArrayObject);

                // Vertex Buffer Object
                VertexBufferObject = GL.GenBuffer();

                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, coob.verts.Length * sizeof(float), coob.verts, BufferUsageHint.StaticDraw);

                // Creating texture and shader
                shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
                shader.Use();
                texture = new Texture("Images/pog.png");
                texture.Use();

                // Element Buffer Object
                //ElementBufferObject = GL.GenBuffer();
                //GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
                //GL.BufferData(BufferTarget.ElementArrayBuffer, coob.indices.Length * sizeof(uint), coob.indices, BufferUsageHint.StaticDraw);

                // Vertex Attribute Pointer
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                // Textures
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);

                #endregion
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
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.DeleteBuffer(VertexBufferObject);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                GL.UseProgram(0);

                GL.DeleteBuffer(ElementBufferObject);
                GL.DeleteBuffer(VertexArrayObject);

                GL.DeleteProgram(shader.Handle);
                GL.DeleteProgram(texture.Handle);

                shader.Dispose();
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Renderer.Unload(), cleaning up opengl stuff.");
            }
        }

    }
}
