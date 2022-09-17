using System.Collections.Generic;
using OpenTK.Mathematics;

namespace Tringle
{
    public class CameraManager
    {
        public static Dictionary<string, Camera> Cameras = new();
        private static string m_activeCamera;

        public static Camera AddCamera(string name, Vector3 position, float aspectRatio)
        {
            Camera camera = new(position, aspectRatio);
            Cameras.Add(name, camera);
            return camera;
        }

        public static Camera GetCamera(string name)
        {
            return Cameras[name];
        }

        public static void SetActiveCamera(string name)
        {
            m_activeCamera = name;
        }

        public static Camera GetActiveCamera()
        {
            return Cameras[m_activeCamera];
        }
    }
}
