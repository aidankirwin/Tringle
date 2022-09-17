using OpenTK.Mathematics;

namespace Tringle
{
    /// <summary>
    /// Container for texture atlas data.
    /// </summary>
    public class TextureAtlas
    {
        public int Rows;
        public int Cols;
        public int Index;
        private Vector2[] m_vertices;

        /// <summary>
        /// Initializes a <see cref="TextureAtlas"/> object.
        /// </summary>
        /// <param name="rows">Number of rows in the <see cref="TextureAtlas"/>.</param>
        /// <param name="cols">Number of columns in the <see cref="TextureAtlas"/>.</param>
        /// <param name="index">The index of the initially rendered texture.</param>
        public TextureAtlas(int rows, int cols, int index) 
        {
            this.Rows = rows; 
            this.Cols = cols; 
            this.Index = index;
            this.SetUVs();
        }

        private void SetUVs()
        {
            float width = 1.0f / (float)Cols;
            float height = 1.0f;
            float minWidth = 0.0f;
            float minHeight = height - 1.0f / (float)Rows;

            m_vertices = new[]
            {
                new Vector2(minWidth, height),        // Top left
                new Vector2(width, minHeight),        // Bottom right
                new Vector2(minWidth, minHeight),     // Bottom left
                new Vector2(width, height)            // Top right
            };
        }

        /// <summary>
        /// Get the UV coordinates of the first texture in the texture atlas.
        /// </summary>
        /// <returns>A <see cref="Vector2"/> of UV coordinates.</returns>
        public Vector2[] GetUVs()
        {
            return m_vertices;
        }
    }
}
