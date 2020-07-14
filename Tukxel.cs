using OpenTK.Input;
using System;

namespace Tukxel
{
    class Tukxel
    {
        public static KeyboardState keyboard;
        public enum GameState { TUKXELSP, MENU }
        public static GameState gameState;

        public static void Main ()
        {
            try
            {
                Console.Title = "Tukxel Console";

                using Game game = new Game(800, 600, "Tukxel");
                {
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
                gameState = GameState.TUKXELSP;
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at Tukxel.Setup() called by Game.OnLoad() i think may not be right lol");
            }
        }
    }
}
