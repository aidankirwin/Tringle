using OpenTK.Mathematics;

namespace Tringle
{
    /// <summary>
    /// Inherits from <see cref="Mesh"/>; a simple 2D sprite component.
    /// </summary>
    public class Sprite : Mesh
    {
        public Sprite(Shader shader) : base
            (
                shader, 
                new List<VTX.Vertex>
                {
                    // Sprite is a mesh with set vertices (1x1 rectangle)
                    VTX.NewVertex(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(0.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f)),
                    VTX.NewVertex(new Vector3(1.0f, 0.0f, 0.0f), new Vector2(1.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f)),
                    VTX.NewVertex(new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f)),

                    VTX.NewVertex(new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f)),
                    VTX.NewVertex(new Vector3(1.0f, 0.0f, 0.0f), new Vector2(1.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f)),
                    VTX.NewVertex(new Vector3(1.0f, 1.0f, 0.0f), new Vector2(1.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f))
                }, 
                // Max vertex
                new Vector3(1.0f, 1.0f, 0.0f), 
                // Min vertex
                new Vector3(0.0f)
            )
        {
            // Empty constructor; uses Mesh constructor
        }
    }
}
