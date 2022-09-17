using OpenTK.Mathematics;

namespace Tringle
{
    /// <summary>
    /// Transform component.
    /// </summary>
    public class Transforms : Component
    {
        public Vector3 Position;
        public Vector2 Size;
        public Rotation Rotation;

        public Transforms(Vector3 Position, Vector2 Size, Rotation Rotation)
        {
            this.Position = Position;
            this.Size = Size;
            this.Rotation = Rotation;
        }

        public Transforms(Vector3 Position, Vector2 Size)
        {
            this.Position = Position;
            this.Size = Size;
            this.Rotation = new Rotation(0.0f);
        }
    }
}
