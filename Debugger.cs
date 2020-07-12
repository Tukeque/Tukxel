using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        }

        public static void Setup()
        {
            ThreadName = "[Main Thread]";
            DebugWriteLine("Thread initialized");

            Thread fpsTracker = new Thread(new ThreadStart(FPSTracker.main));
            fpsTracker.Start();
        }
    }

    class FPSTracker
    {
        public static long ms;
        public static float fps;
        public static string ThreadName;

        public static string StartMessage()
        {
            return Debugger.CurrentTime() + ThreadName;
        }

        public static void DebugWriteLine(string text)
        {
            Console.WriteLine(StartMessage() + text);
        }

        public static void main()
        {
            while (!Debugger.End)
            {
                Thread.Sleep(1000);
                Update();
            }
        }

        public static void Update()
        {
            if (ms != 0) fps = 1000 / ms;
            else fps = 500;

            DebugWriteLine("FPS = " + fps);
        }

        public static void Setup()
        {
            ThreadName = "[FPS Tracker]";

            ms = 0;
            fps = 500;
            DebugWriteLine("Thread Initialized.");
        }

        public static void GetFPS()
        {
            if (ms != 0) fps = 1000 / ms;
            ms = 0;
        }
    }
}
