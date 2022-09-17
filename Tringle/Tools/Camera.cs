using OpenTK.Mathematics;

namespace Tringle
{
    public class Camera
    {
        Vector3 _front = -Vector3.UnitZ;
        Vector3 _right = Vector3.UnitX;
        Vector3 _up = Vector3.UnitY;

        float _fov = MathHelper.PiOver2;
        float _yaw = -90.0f;
        float _pitch = 0.0f;

        public float lastX = Global.SCREEN_WIDTH / 2.0f;
        public float lastY = Global.SCREEN_HEIGHT / 2.0f;

        public bool firstMouse = true;

        public Projection View;

        public Vector3 Position;
        public float AspectRatio;
        public Vector3 Front => _front;
        public Vector3 Right => _right;
        public Vector3 Up => _up;
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        public enum Projection
        {
            ORTHOGRAPHIC,
            PERSPECTIVE
        }
        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

        Matrix4 GetOrthoProjMatrix()
        {
            return Matrix4.CreateOrthographic(Global.SCREEN_WIDTH, Global.SCREEN_HEIGHT, -4.0f, 4.0f);
        }

        Matrix4 GetPerspProjMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 1000f);
        }

        public Matrix4 GetProjMatrix()
        {
            if (this.View == Projection.ORTHOGRAPHIC)
            {
                return GetOrthoProjMatrix();
            }
            else
            {
                return GetPerspProjMatrix();
            }
        }

        private void UpdateVectors()
        {
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }
    }
}
