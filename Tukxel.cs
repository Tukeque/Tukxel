using OpenTK.Input;
using System;

namespace Tukxel
{
    class Tukxel
    {
        public static KeyboardState keyboard;

        public static void Main ()
        {
            Console.Title = "Tukxel Console";

            using Game game = new Game(800, 600, "Tukxel");
            {
                game.VSync = OpenTK.VSyncMode.On;
                game.Run(0);
            }
        }

        public static void Update()
        {
            if (keyboard.IsKeyDown(Key.Escape))
            {
                Environment.Exit(0x00);
            }
        }

        public static void Setup()
        {

        }
    }
}
