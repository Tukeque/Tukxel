using OpenTK;
using OpenTK.Input;
using System;

namespace Tukxel
{
    class Tukxel
    {
        public static KeyboardState keyboard;
        public enum GameState { TUKXELSP, MENU }
        public static GameState gameState;
        public static int X;
        public static int Y;

        public static int Width;
        public static int Height;
        public static bool Focused;
        public static bool CursorLockAndInvisible;
        public static int EscTimer = 0;

        public static float speed;
        public static float sensitivity;

        public static MouseState mouse = new MouseState();

        public static void Main()
        {
            try
            {
                Console.Title = "Tukxel Console";

                using Game game = new Game(800, 600, "Tukxel");
                {
                    Width = game.Width;
                    Height = game.Height;

                    game.Run(60, 60);
                }
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "intializing the game in Tukxel.Main()");
            }
        }

        public static void Update()
        {
            try
            {
                EscTimer = EscTimer == 0 ? 0 : EscTimer - 1;

                KeyboardState keyboard = Keyboard.GetState();

                if (keyboard.IsKeyDown(Key.Escape))
                {
                    if (CursorLockAndInvisible & EscTimer == 0)
                    {
                        CursorLockAndInvisible = false;
                        EscTimer += 30;
                    }
                    else if (EscTimer == 0)
                        Environment.Exit(0x00);
                }

                if ((keyboard.IsKeyDown(Key.AltLeft) | keyboard.IsKeyDown(Key.AltRight)) & keyboard.IsKeyDown(Key.F4))
                {
                    Environment.Exit(0x00);
                }

                if (Focused)
                {
                    if (keyboard.IsKeyDown(Key.W))
                    {
                        Camera.position += Camera.front * speed * (float)Game.DeltaTime;
                    }
                    if (keyboard.IsKeyDown(Key.A))
                    {
                        Camera.position -= Vector3.Normalize(Vector3.Cross(Camera.front, Camera.cameraUp)) * speed * (float)Game.DeltaTime;
                    }
                    if (keyboard.IsKeyDown(Key.S))
                    {
                        Camera.position -= Camera.front * speed * (float)Game.DeltaTime;
                    }
                    if (keyboard.IsKeyDown(Key.D))
                    {
                        Camera.position += Vector3.Normalize(Vector3.Cross(Camera.front, Camera.cameraUp)) * speed * (float)Game.DeltaTime;
                    }
                    if (keyboard.IsKeyDown(Key.Q))
                    {
                        Camera.position -= Camera.up * speed * (float)Game.DeltaTime;
                    }
                    if (keyboard.IsKeyDown(Key.E))
                    {
                        Camera.position += Camera.up * speed * (float)Game.DeltaTime;
                    }
                }

                mouse = Mouse.GetCursorState();
                float deltaX = mouse.X - (X + Width  / 2f);
                float deltaY = mouse.Y - (Y + Height / 2f);

                Camera.Yaw -= deltaX * sensitivity * (float)Game.DeltaTime;
                if (Camera.Pitch > 89.0f)
                {
                    Camera.Pitch = 89.0f;
                }
                else if (Camera.Pitch < -89.0f)
                {
                    Camera.Pitch = -89.0f;
                }
                else
                {
                    Camera.Pitch += deltaY * sensitivity * (float)Game.DeltaTime;
                }

                Camera.front.X = (float)Math.Cos(MathHelper.DegreesToRadians(Camera.Pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(Camera.Yaw));
                Camera.front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Camera.Pitch));
                Camera.front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Camera.Pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(Camera.Yaw));
                Camera.front = Vector3.Normalize(Camera.front);

                Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Tukxel.Update() called by Game.OnUpdateFrame() i think may not be right lol");
            }
        }

        public static void Setup()
        {
            try
            {
                CursorLockAndInvisible = true;

                speed = 5f;
                sensitivity = 10;

                gameState = GameState.TUKXELSP;
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Tukxel.Setup() called by Game.OnLoad() i think may not be right lel");
            }
        }
    }
}
