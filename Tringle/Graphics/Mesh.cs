using OpenTK.Mathematics;

namespace Tringle
{
    /// <summary>
    /// Mesh component.
    /// </summary>
    public class Mesh : Component
    {
        public List<VTX.Vertex> Vertices;
        public int VAO;
        public List<int> VBO;
        public Shader Shader;
        public Vector3 MaxVertex;
        public Vector3 MinVertex;

        public Mesh(Shader shader, List<VTX.Vertex> vertices)
        {
            this.Shader = shader;
            this.Vertices = vertices;
            this.VBO = new();
            this.MaxVertex = VTX.GetMaxVertex(vertices);
            this.MinVertex = VTX.GetMinVertex(vertices);
        }

        public Mesh(Shader shader, List<VTX.Vertex> vertices, Vector3 max, Vector3 min)
        {
            this.Shader = shader;
            this.Vertices = vertices;
            this.VBO = new();
            this.MaxVertex = max;
            this.MinVertex = min;
        }
    }
}
