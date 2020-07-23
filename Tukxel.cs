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

        // Game(class) stuff
        public static int Width;
        public static int Height;
        public static bool Focused;

        public static float speed;
        public static float rawSpeed;

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
                KeyboardState keyboard = Keyboard.GetState();

                if (keyboard.IsKeyDown(Key.Escape))
                {
                    Environment.Exit(0x00);
                }

                if ((keyboard.IsKeyDown(Key.AltLeft) | keyboard.IsKeyDown(Key.AltRight)) & keyboard.IsKeyDown(Key.F4))
                {
                    Environment.Exit(0x00);
                }

                #region cameraMovement
                rawSpeed = speed * (float)Game.DeltaTime;

                //Debugger.DebugWriteLine($"{rawSpeed}");

                if (true)
                {
                    if (keyboard.IsKeyDown(Key.W))
                    {
                        Camera.position += Camera.front * speed;
                    }
                    if (keyboard.IsKeyDown(Key.A))
                    {
                        Camera.position -= Vector3.Normalize(Vector3.Cross(Camera.front, Camera.up)) * speed;
                    }
                    if (keyboard.IsKeyDown(Key.S))
                    {
                        Camera.position -= Camera.front * speed;
                    }
                    if (keyboard.IsKeyDown(Key.D))
                    {
                        Camera.position += Vector3.Normalize(Vector3.Cross(Camera.front, Camera.up)) * speed;
                    }
                    if (keyboard.IsKeyDown(Key.Q))
                    {
                        Camera.position -= Camera.up * speed;
                    }
                    if (keyboard.IsKeyDown(Key.E))
                    {
                        Camera.position += Camera.up * speed;
                    }
                }
                #endregion
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
                speed = .5f;

                gameState = GameState.TUKXELSP;
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Tukxel.Setup() called by Game.OnLoad() i think may not be right lol");
            }
        }
    }
}
