using OpenTK;
using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Diagnostics;

namespace Tukxel
{
    class Game : GameWindow
    {
        public static double DeltaTime;

        public Game(int width, int height, string title) : base(width, height, new GraphicsMode(new ColorFormat(8, 8, 8, 0), 24, 8, 4), title) { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // FPSTracker stuff
            FPSTracker.stopwatch.Restart();

            KeyboardState keyboard = Keyboard.GetState();

            Tukxel.keyboard = keyboard;

            Debugger.Update();
            Tukxel.Update();

            base.OnUpdateFrame(e);

            // FPSTracker stuff
            FPSTracker.stopwatch.Stop();
            FPSTracker.ticks += FPSTracker.stopwatch.ElapsedTicks;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // FPSTracker stuff
            FPSTracker.stopwatch.Restart();

            GL.Clear(ClearBufferMask.ColorBufferBit);

            Renderer.Update();

            Context.SwapBuffers();
            base.OnRenderFrame(e);

            // FPSTracker stuff
            FPSTracker.stopwatch.Stop();
            FPSTracker.ticks += FPSTracker.stopwatch.ElapsedTicks;
            FPSTracker.UpdateFPS();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            Debugger.Setup();
            Tukxel.Setup();
            Renderer.Setup();

            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            Debugger.End = true;

            Renderer.Unload();

            base.OnUnload(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }
    }
}
