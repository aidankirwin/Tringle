using OpenTK.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace Tringle
{
    public class Input
    {
        private static bool[] m_keys = new bool[1024];
        private static Vector2 m_mousePosition = new Vector2(0, 0);

        // Key library wrapper variables
        public static Keys          A =         Keys.A;
        public static Keys          B =         Keys.B;
        public static Keys          C =         Keys.C;
        public static Keys          D =         Keys.D;
        public static Keys          E =         Keys.E;
        public static Keys          F =         Keys.F;
        public static Keys          G =         Keys.G;
        public static Keys          H =         Keys.H;
        public static Keys          I =         Keys.I;
        public static Keys          J =         Keys.J;
        public static Keys          K =         Keys.K;
        public static Keys          L =         Keys.L;
        public static Keys          M =         Keys.M;
        public static Keys          N =         Keys.N;
        public static Keys          O =         Keys.O;
        public static Keys          P =         Keys.P;
        public static Keys          Q =         Keys.Q;
        public static Keys          R =         Keys.R;
        public static Keys          S =         Keys.S;
        public static Keys          T =         Keys.T;
        public static Keys          U =         Keys.U;
        public static Keys          V =         Keys.V;
        public static Keys          W =         Keys.W;
        public static Keys          X =         Keys.X;
        public static Keys          Y =         Keys.Y;
        public static Keys          Z =         Keys.Z;
        public static Keys        Esc =    Keys.Escape;
        public static Keys      Space =     Keys.Space;
        public static Keys  LeftShift = Keys.LeftShift;
        public static Keys    UpArrow =        Keys.Up;
        public static Keys  DownArrow =      Keys.Down;
        public static Keys RightArrow =     Keys.Right;
        public static Keys  LeftArrow =      Keys.Left;

        public static void KeyDown(KeyboardKeyEventArgs key)
        {
            // Set value in Keys to true
            m_keys[(int)key.Key] = true;
        }
        public static void KeyUp(KeyboardKeyEventArgs key)
        {
            // Set value in Keys to false
            m_keys[(int)key.Key] = false;
        }

        public static bool CheckKey(Keys key)
        {
            // Return value of Keys at Key
            return m_keys[(int)key];
        }

        public static void SetMousePos(MouseMoveEventArgs mouse)
        {
            m_mousePosition = new Vector2(mouse.X, mouse.Y);
        }

        public static Vector2 GetMousePos()
        {
            return m_mousePosition;
        }
    }
}