using OpenTK.Mathematics;

namespace Tringle
{
    /// <summary>
    /// Global variables.
    /// </summary>
    public class Global
    {
        public static int SCREEN_WIDTH = 300;
        public static int SCREEN_HEIGHT = 600;
        public static bool MAC = false;
        public static string PATH = "../../../";
        public static string TITLE = "TETRIS";

        public static bool CULL_FACE = true;
        public static bool CULL_CCW = true;
        public static bool DEPTH_TEST = true;
        public static bool CURSOR_GRABBED = true;
    }
}