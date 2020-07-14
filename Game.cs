using OpenTK;
using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Tukxel
{
    class Game : GameWindow
    {
        public static double DeltaTime;

        public Game(int width, int height, string title) : base(width, height, new GraphicsMode(new ColorFormat(8, 8, 8, 0), 24, 8, 4), title) { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState keyboard = Keyboard.GetState();

            Tukxel.keyboard = keyboard;

            Debugger.Update();
            Tukxel.Update();

            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Renderer.Update();

            Context.SwapBuffers();
            base.OnRenderFrame(e);

            // FPS Tracking
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
            Width  = Width  == 0 ? 1 : Width;
            Height = Height == 0 ? 1 : Height;

            GL.Viewport(0, 0, Width, Height);

            #region perspective?
            #endregion

            base.OnResize(e);
        }
    }
}
