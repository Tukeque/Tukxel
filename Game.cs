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
            try
            {
                KeyboardState keyboard = Keyboard.GetState();

                Tukxel.keyboard = keyboard;

                Debugger.Update();
                Tukxel.Update();

                base.OnUpdateFrame(e);
            }
            catch (Exception error)
            {
                Debugger.Error(error.ToString(), "at Game.OnUpdateFrame(), updating the frame.");
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            try
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);

                Renderer.Update();

                Context.SwapBuffers();
                base.OnRenderFrame(e);

                // FPS Tracking
                FPSTracker.UpdateFPS();
            }
            catch (Exception error)
            {
                Debugger.Error(error.ToString(), "at Game.OnRenderFrame(), rendering the frame.");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

                Debugger.Setup();
                Tukxel.Setup();
                Renderer.Setup();

                base.OnLoad(e);
            }
            catch (Exception error)
            {
                Debugger.Error(error.ToString(), "at Game.OnLoad(), loading the game.");
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            try
            {
                Debugger.End = true;

                Renderer.Unload();

                base.OnUnload(e);
            }
            catch (Exception error)
            {
                Debugger.Error(error.ToString(), "at Game.OnUnload, unloading the game.");
            }
        }

        protected override void OnResize(EventArgs e)
        {
            try
            {
                Width = Width == 0 ? 1 : Width;
                Height = Height == 0 ? 1 : Height;

                GL.Viewport(0, 0, Width, Height);

                Tukxel.Width = Width;
                Tukxel.Height = Height;

                base.OnResize(e);
            }
            catch (Exception error)
            {
                Debugger.Error(error.ToString(), "at Game.OnResize(), resizing the screen.");
            }
        }
    }
}
