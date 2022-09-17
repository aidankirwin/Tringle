using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Tringle
{
    /// <summary>
    /// Handles the rendering processes for <see cref="Mesh"/> or <see cref="Sprite"/> objects.
    /// </summary>
    public class MeshRenderer
    {
        /// <summary>
        /// Draws the passed <paramref name="mesh"/>.
        /// </summary>
        public void Draw(Mesh mesh, Texture texture, Transforms transform)
        {
            mesh.Shader.Use();

            var model = Matrix4.Identity;

            // Get transforms
            var position = transform.Position;
            var size = transform.Size;
            var rotate = transform.Rotation;

            // Get transform matrices
            var translate = Model.Matrix4Translate(position);
            var rotation = Model.Matrix4Rotate(rotate, mesh);
            var scale = Model.Matrix4Scale(size);

            // Apply transformations
            var transformations = rotation * scale * translate;
            model *= transformations;

            mesh.Shader.SetMatrix4("model", model);

            GL.ActiveTexture(TextureUnit.Texture0);
            texture.Use();

            // Draw call
            GL.BindVertexArray(mesh.VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, mesh.Vertices.Count);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Initialize the rendering data for a <paramref name="mesh"/>.
        /// </summary>
        public void Initialize(Mesh mesh)
        {
            mesh.VAO = GL.GenVertexArray();
            VTX.Vertex[] vertices = mesh.Vertices.ToArray();

            // Add new VBO to mesh object
            int newVBO = GL.GenBuffer();
            mesh.VBO.Add(newVBO);

            // Bind VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VBO[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Marshal.SizeOf(typeof(VTX.Vertex)), vertices, BufferUsageHint.StaticDraw);

            // Bind VAO
            GL.BindVertexArray(mesh.VAO);

            // Position coordinates
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), 0);
            // Texture coordinates
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), sizeof(float) * 3);
            // Normals
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), sizeof(float) * 5);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Initialize the rendering data for a <paramref name="mesh"/>.
        /// </summary>
        public void Initialize(Mesh mesh, TextureAtlas atlas)
        {
            mesh.VAO = GL.GenVertexArray();

            // Get width of each texture, may be 1x1
            float textureWidth = 1.0f / (float)atlas.Cols;
            float textureHeight = 1.0f / (float)atlas.Rows;

            int vboCount = 0;

            // Set VBOs for all textures
            for (int i = 0; i < atlas.Rows; i++)
            {
                for (int j = 0; j < atlas.Cols; j++)
                {
                    VTX.Vertex[] vertices = mesh.Vertices.ToArray();

                    // Positive offset to read the atlas from left to right
                    float xOffset = textureWidth * (float)j;

                    // Negative offset to read the atlas from top to bottom
                    float yOffset = -textureHeight * (float)i;

                    for(int k = 0; k < vertices.Length; k++)
                    {
                        vertices[k].TX += xOffset;
                        vertices[k].TY += yOffset;
                    }

                    // Add new VBO to mesh object
                    int newVBO = GL.GenBuffer();
                    mesh.VBO.Add(newVBO);

                    GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VBO[vboCount]);
                    GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Marshal.SizeOf(typeof(VTX.Vertex)), vertices, BufferUsageHint.StaticDraw);  

                    vboCount++;
                }
            }

            // Set VBO to the index parameter
            if (atlas.Index >= mesh.VBO.Count) GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VBO[0]);
            else GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VBO[atlas.Index]);

            GL.BindVertexArray(mesh.VAO);

            // Position coordinates
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), 0);
            // Texture coordinates
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), sizeof(float) * 3);
            // Normals
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), sizeof(float) * 5);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Index an atlas of textures by changing the VBO.
        /// </summary>
        public void ChangeTextureToIndex(Mesh mesh, int n)
        {
            // Check if index parameter is valid
            if (mesh.VBO.Count == 0) return;
            if (n >= mesh.VBO.Count) return;

            GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VBO[n]);

            GL.BindVertexArray(mesh.VAO);

            // Position coordinates
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), 0);
            // Texture coordinates
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), sizeof(float) * 3);
            // Normals
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(VTX.Vertex)), sizeof(float) * 5);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }
    }
}