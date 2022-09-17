using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;

using Program;

namespace Tringle
{
   public class Window : GameWindow
    {
        // Initialize application object
        private readonly Game m_game = new();

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        // Called once when the window loads
        protected override void OnLoad()
        {
            base.OnLoad();

            // Background colour
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            if(Global.DEPTH_TEST) GL.Enable(EnableCap.DepthTest);
            if (Global.CULL_FACE)
            {
                GL.Enable(EnableCap.CullFace);
                if(Global.CULL_CCW) GL.CullFace(CullFaceMode.Back);
                else GL.CullFace(CullFaceMode.Front);
            }

            // Remove cursor from screen
            if(Global.CURSOR_GRABBED) CursorState = CursorState.Grabbed;

            // Initialize application
            m_game.Initialize();
        }

        // Called once per frame; update
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Update application
            m_game.Update((float)args.Time);
        }

        // Called once per frame; render
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            if (Global.DEPTH_TEST) GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            else GL.Clear(ClearBufferMask.ColorBufferBit);

            // Render application
            m_game.Render((float)e.Time);

            SwapBuffers();
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            Input.SetMousePos(e);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            Input.KeyDown(e);
            if (Input.CheckKey(Input.Esc)) Close();
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            Input.KeyUp(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // Update screen dimension variables
            Global.SCREEN_WIDTH = Size.X;
            Global.SCREEN_HEIGHT = Size.Y;

            // Update viewport
            // Temp fix for Mac viewport issue
            if (Global.MAC) GL.Viewport(0, 0, Global.SCREEN_WIDTH * 2, Global.SCREEN_HEIGHT * 2);
            else GL.Viewport(0, 0, Global.SCREEN_WIDTH, Global.SCREEN_HEIGHT);

            if(CameraManager.GetActiveCamera() != null)
            {
                // Update aspect ratio
                CameraManager.GetActiveCamera().AspectRatio = Size.X / (float)Size.Y;

                // Update projection matrix for active camera object
                Matrix4 projection = CameraManager.GetActiveCamera().GetProjMatrix();

                foreach(var shader in ResourceManager.Shaders) 
                { 
                    shader.Value.SetMatrix4("projection", projection); 
                }
            }
        }
    }
}
