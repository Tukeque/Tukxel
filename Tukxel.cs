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

        public static int Width;
        public static int Height;

        public static int speed;

        public static void Main ()
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
                if (keyboard.IsKeyDown(Key.Escape))
                {
                    Environment.Exit(0x00);
                }

                if (keyboard.IsKeyDown(Key.AltLeft) | keyboard.IsKeyDown(Key.AltRight) && keyboard.IsKeyDown(Key.F4))
                {
                    Environment.Exit(0x00);
                }

                if (keyboard.IsKeyDown(Key.W))
                {
                    Vector4 movement = -Vector4.UnitZ * Camera.rotate * speed * (float)Game.DeltaTime;

                    Camera.x += movement.X;
                    Camera.y += movement.Y;
                    Camera.z += movement.Z;
                }
                if (keyboard.IsKeyDown(Key.A))
                {
                    Vector4 movement = -Vector4.UnitX * Camera.rotate * speed * (float)Game.DeltaTime;

                    Camera.x += movement.X;
                    Camera.y += movement.Y;
                    Camera.z += movement.Z;
                }
                if (keyboard.IsKeyDown(Key.S))
                {
                    Vector4 movement = Vector4.UnitZ * Camera.rotate * speed * (float)Game.DeltaTime;

                    Camera.x += movement.X;
                    Camera.y += movement.Y;
                    Camera.z += movement.Z;
                }
                if (keyboard.IsKeyDown(Key.D))
                {
                    Vector4 movement = Vector4.UnitX * Camera.rotate * speed * (float)Game.DeltaTime;

                    Camera.x += movement.X;
                    Camera.y += movement.Y;
                    Camera.z += movement.Z;
                }
                if (keyboard.IsKeyDown(Key.Q))
                {
                    Vector4 movement = -Vector4.UnitY * Camera.rotate * speed * (float)Game.DeltaTime;

                    Camera.x += movement.X;
                    Camera.y += movement.Y;
                    Camera.z += movement.Z;
                }
                if (keyboard.IsKeyDown(Key.E))
                {
                    Vector4 movement = Vector4.UnitY * Camera.rotate * speed * (float)Game.DeltaTime;

                    Camera.x += movement.X;
                    Camera.y += movement.Y;
                    Camera.z += movement.Z;
                }
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
                speed = 5;

                gameState = GameState.TUKXELSP;
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Tukxel.Setup() called by Game.OnLoad() i think may not be right lol");
            }
        }
    }
}
