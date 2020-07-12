﻿using System;
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

        }

        public static void Setup()
        {
            ThreadName = "[Main Thread]";
            DebugWriteLine("Thread initialized");

            Thread fpsTracker = new Thread(new ThreadStart(FPSTracker.TheSetup));
            fpsTracker.Start();
        }



        public static void Error(string error, string location)
        {
            DebugWriteLine("haha error occured rib, error is: " + error + "\n and it come from: " + location);
        }
    }

    class FPSTracker
    {
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
            Setup();

            while (!Debugger.End)
            {
                Thread.Sleep(1000);
                Update();
            }
        }

        public static void Update()
        {
            DebugWriteLine("FPS = " + fps);
            fps = 0;
        }

        public static void Setup()
        {
            ThreadName = "[FPS Tracker]";

            fps = 0;
            DebugWriteLine("Thread Initialized");
        }

        public static void UpdateFPS()
        {
            fps++;
        }
    }
}
