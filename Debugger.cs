using System;
using System.Diagnostics;
using System.Threading;

namespace Tukxel
{
    class Debugger
    {
        public static bool End;
        public static string ThreadName;

        public static string CurrentTime()
        {
            DateTime time = DateTime.Now;

            return "[" + time + "]";
        }

        public static string StartMessage()
        {
            return CurrentTime() + ThreadName;
        }

        public static void DebugWriteLine(string text)
        {
            Console.WriteLine(StartMessage() + text);
        }

        public static void Update()
        {
            try
            {

            }
            catch (Exception e)
            {
                Error(e.ToString(), "at Debugger.Update(), updating the debugger.");
            }
        }

        public static void Setup()
        {
            try
            {
                ThreadName = "[Main Thread]";
                DebugWriteLine("Thread initialized");

                Thread fpsTracker = new Thread(new ThreadStart(FPSTracker.TheSetup));
                fpsTracker.Start();
            }
            catch (Exception e)
            {
                Error(e.ToString(), "at Debugger.Setup(), Setting up the debugger.");
            }
        }

        public static void Error(string error, string location)
        {
            try
            {
                DebugWriteLine(String.Format("haha error occured rib, error is: {0}\nand it come from: {1}", error, location));
                Console.ReadKey();
                Environment.Exit(0x00);
            }
            catch (Exception e)
            {
                Error(e.ToString(), "at Debugger.Error()");
            }
        }
    }

    class FPSTracker
    {
        public static Stopwatch stopwatch;

        public static double es;

        public static long fps;
        public static string ThreadName;

        public static string StartMessage()
        {
            return Debugger.CurrentTime() + ThreadName;
        }

        public static void DebugWriteLine(string text)
        {
            Console.WriteLine(StartMessage() + text);
        }

        public static void TheSetup()
        {
            try
            {
                Setup();

                while (!Debugger.End)
                {
                    Thread.Sleep(1000);
                    Update();
                }
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at FPSTracker.TheSetup(), initialiozing FPSTracker thread");
            }
        }

        public static void Update()
        {
            try
            {
                es = 1.0d / fps;
                Game.DeltaTime = es;
                DebugWriteLine(string.Format("FPS = {0}; ES = {1}", fps, es));
                // DeltaTime should be elapssed seconds
                fps = 0;
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at FPSTracker.Update(), updating frames.");
            }
        }

        public static void Setup()
        {
            try
            {
                ThreadName = "[FPS Tracker]";

                stopwatch = new Stopwatch();

                fps = 0;
                DebugWriteLine("Thread Initialized");
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at FPSTracker.Setup(), setting FPSTracker thread up.");
            }
        }

        public static void UpdateFPS()
        {
            try
            {
                fps++;
            }
            catch (Exception e)
            {
                Debugger.Error(e.ToString(), "at FPSTracker.UpdateFps(), incrementing FPS.");
            }
        }
    }
}
