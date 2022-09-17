using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Tringle;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(Global.SCREEN_WIDTH, Global.SCREEN_HEIGHT),
                Title = Global.TITLE,

                // This is needed to run on Mac
                Flags = ContextFlags.ForwardCompatible
            };

            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}