using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Tukxel
{
    class Camera
    {
        public static float FoV;
        public static float Aspect;
        public static float zNear;
        public static float zFar;

        public static Matrix4 projection;

        public static Vector3 position;
        public static Vector3 cameraTarget;
        public static Vector3 cameraDirection;
        public static Vector3 up;
        public static Vector3 cameraRight;
        public static Vector3 cameraUp;
        public static Vector3 front;

        //rotation
        public static float Pitch;
        public static float Yaw;

        // walk around

        public static Matrix4 view;

        public static void UpdateProjection()
        {
            FoV = 85.0f;
            Aspect = (float)Tukxel.Height / Tukxel.Width;
            zNear = 0.001f;
            zFar = 100.0f;

            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FoV), Aspect, zNear, zFar);
        }

        public static void Setup()
        {
            UpdateProjection();

            position = Vector3.Zero;

            // walk around
            position = new Vector3(0.0f, 0.0f, 3.0f);
            front = new Vector3(0.0f, 0.0f, -1.0f);

            cameraTarget = Vector3.Zero;
            cameraDirection = Vector3.Normalize(position - cameraTarget);

            up = Vector3.UnitY;
            cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));
            cameraUp = Vector3.Cross(cameraDirection, cameraRight);

            view = Matrix4.LookAt(position, position + front, up); // up != cameraup tho ??
        }

        public static void Update()
        {
            UpdateProjection();

            cameraTarget = Vector3.Zero;
            cameraDirection = Vector3.Normalize(position - cameraTarget);

            up = Vector3.UnitY;
            cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));
            cameraUp = Vector3.Cross(cameraDirection, cameraRight);

            //Debugger.DebugWriteLine($"pitch: {Pitch}, yaw: {Yaw}, roll: {Roll}");

            view = Matrix4.LookAt(position, position + front, up); // up != cameraup tho ??
        }
    }
}
