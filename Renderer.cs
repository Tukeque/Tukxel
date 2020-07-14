using OpenTK.Graphics.OpenGL;

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
        public static Shader shader;
        public static Texture texture;

        public static Mesh rectangol;
        public static Mesh triangol;

        public static int ElementBufferObject;
        public static int VertexBufferObject;
        public static int VertexArrayObject;

        public static void Update()
        {
            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.BufferData(BufferTarget.ArrayBuffer, rectangol.verts.Length * sizeof(float), rectangol.verts, BufferUsageHint.DynamicDraw);

            GL.DrawElements(PrimitiveType.Triangles, rectangol.indices.Length, DrawElementsType.UnsignedInt, 0);

            float change = 0.05f;
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

        public static void Setup()
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

            //Shader
            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            shader.Use();
            texture = new Texture("Images/summer.jpg");
            texture.Use();


            // Element Buffer Object
            ElementBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, rectangol.indices.Length * sizeof(uint), rectangol.indices, BufferUsageHint.StaticDraw);

            // Vertex Attribute Pointer
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            //^ before textures
            //v wen textures
            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // TExtures stuff??
            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));


            #endregion
        }

        public static void Unload()
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

    }
}
