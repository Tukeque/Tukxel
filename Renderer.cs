using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Tukxel
{
    public struct Mesh
    {
        public float[] verts;
        public uint[]  indices;
        public float[] texCoords;
    }

    class Renderer
    {
        // Matrices
        public static Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(45.0f));
        public static Matrix4 scale = Matrix4.CreateScale(1.5f, 0.5f, 1.0f);
        public static Matrix4 trans = rotation * scale;

        public static Shader shader;
        public static Texture texture;

        public static Mesh rectangol;
        public static Mesh triangol;

        public static int ElementBufferObject;
        public static int VertexBufferObject;
        public static int VertexArrayObject;

        public static void Update()
        {
            try
            {
                shader.Use();
                GL.BindVertexArray(VertexArrayObject);
                GL.BufferData(BufferTarget.ArrayBuffer, rectangol.verts.Length * sizeof(float), rectangol.verts, BufferUsageHint.DynamicDraw);

                GL.DrawElements(PrimitiveType.Triangles, rectangol.indices.Length, DrawElementsType.UnsignedInt, 0);

                float change = 0.00f;
                int s = 0;
                for (int i = 0; i < rectangol.verts.Length; i++)
                {
                    switch (s)
                    {
                        case 0:
                            rectangol.verts[i] += (change * (float)Game.DeltaTime);
                            break;

                        case 1:
                            rectangol.verts[i] += (change * (float)Game.DeltaTime);
                            break;

                        case 2:
                            rectangol.verts[i] += (change * (float)Game.DeltaTime);
                            break;
                    }
                    s = (s + 1) % 5;
                }
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
                 1.0f,  1.0f,  0.0f, 2.0f, 2.0f, // top right
                 1.0f, -1.0f,  0.0f, 2.0f, 0.0f, // bottom right
                -1.0f, -1.0f,  0.0f, 0.0f, 0.0f, // bottom left
                -1.0f,  1.0f,  0.0f, 0.0f, 2.0f  // top left
                };
                rectangol.indices = new uint[]
                {
                0, 1, 3, // first triangle
                1, 2, 3  // second triangle
                };
                #endregion

                int s = 0;
                for (int i = 0; i < rectangol.verts.Length; i++)
                {
                    switch (s)
                    {
                        case 0:
                            rectangol.verts[i] = rectangol.verts[i] / 2.0f;
                            break;

                        case 1:
                            rectangol.verts[i] = rectangol.verts[i] / 2.0f;
                            break;

                        case 2:
                            rectangol.verts[i] = rectangol.verts[i] / 2.0f;
                            break;

                        case 3:
                            rectangol.verts[i] = rectangol.verts[i] / 2.0f;
                            break;

                        case 4:
                            rectangol.verts[i] = rectangol.verts[i] / 2.0f;
                            break;
                    }
                    s = (s + 1) % 5;
                }

                #region triangol
                triangol.verts = new float[]
                {
                 0.5f, -0.5f,  0.0f, // bottom left
                -0.5f, -0.5f,  0.0f, // bottom right
                 0.0f,  0.5f,  0.0f  // middle top
                };
                triangol.indices = new uint[]
                {
                0, 1, 2, // triangol
                };
                triangol.texCoords = new float[]
                {
                0.0f, 0.0f, // bottom left corner
                1.0f, 0.0f, // bottom right corner
                0.5f, 1.0f  // top center corner
                };
                #endregion

                #region OpenGL stuff
                // Vertex Array Object
                VertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(VertexArrayObject);

                // Vertex Buffer Object
                VertexBufferObject = GL.GenBuffer();

                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, rectangol.verts.Length * sizeof(float), rectangol.verts, BufferUsageHint.StaticDraw);

                // Creating texture and shader
                shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
                shader.Use();
                texture = new Texture("Images/summer.jpg");
                texture.Use();


                // Element Buffer Object
                ElementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, rectangol.indices.Length * sizeof(uint), rectangol.indices, BufferUsageHint.StaticDraw);

                // Vertex Attribute Pointer
                int vertexLocation = shader.GetAttribLocation("aPosition");
                GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                // Textures
                int texCoordLocation = shader.GetAttribLocation("aTexCoord");
                GL.EnableVertexAttribArray(texCoordLocation);
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));


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
