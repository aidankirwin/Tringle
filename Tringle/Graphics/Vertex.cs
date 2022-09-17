using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Tringle
{
    public class VTX
    {
        /// <summary>
        /// Struct containing position, UV, and normal values for a given vertex.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Vertex
        {
            // Position
            public float X;
            public float Y;
            public float Z;
            // Texcoord
            public float TX;
            public float TY;
            // Normals
            public float NX;
            public float NY;
            public float NZ;
        }
        /// <summary>
        /// Create a new <see cref="Vertex"/>.
        /// </summary>
        public static Vertex NewVertex(Vector3 position, Vector2 texCoord, Vector3 normal)
        {
            Vertex vertex = new Vertex();
            vertex.X = position.X;
            vertex.Y = position.Y;
            vertex.Z = position.Z;
            vertex.TX = texCoord.X;
            vertex.TY = texCoord.Y;
            vertex.NX = normal.X;
            vertex.NY = normal.Y;
            vertex.NZ = normal.Z;

            return vertex;
        }

        /// <summary>
        /// Get the maximum x, y, and z coordinates of a set of vertices.
        /// </summary>
        /// <param name="vertices">A <see cref="List{T}"/> of <see cref="VTX.Vertex"/> objects.</param>
        /// <returns>The maximum vertex.</returns>
        public static Vector3 GetMaxVertex(List<VTX.Vertex> vertices)
        {
            float maxX = vertices[0].X;
            float maxY = vertices[0].Y;
            float maxZ = vertices[0].Z;

            // Find max vertex
            foreach (var vertex in vertices)
            {
                float currX = vertex.X;
                float currY = vertex.Y;
                float currZ = vertex.Z;

                if (currX > maxX) maxX = currX;
                if (currY > maxY) maxY = currY;
                if (currZ > maxZ) maxZ = currZ;
            }

            Vector3 max = new Vector3(maxX, maxY, maxZ);

            return max;
        }

        /// <summary>
        /// Get the minimum x, y, and z coordinates of a set of vertices.
        /// </summary>
        /// <param name="vertices">A <see cref="List{T}"/> of <see cref="VTX.Vertex"/> objects.</param>
        /// <returns>The minimum vertex.</returns>
        public static Vector3 GetMinVertex(List<VTX.Vertex> vertices)
        {
            float minX = vertices[0].X;
            float minY = vertices[0].Y;
            float minZ = vertices[0].Z;

            // Find min vertex
            foreach (var vertex in vertices)
            {
                float currX = vertex.X;
                float currY = vertex.Y;
                float currZ = vertex.Z;

                if (currX < minX) minX = currX;
                if (currY < minY) minY = currY;
                if (currZ < minZ) minZ = currZ;
            }

            Vector3 max = new Vector3(minX, minY, minZ);

            return max;
        }
    }
}
