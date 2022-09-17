using OpenTK.Mathematics;

using Tringle;

namespace Program
{
    public class Game
    {
        private MeshRenderer m_renderer = new();
        private Mesh? m_mesh;
        private Sprite? m_sprite;

        private const int m_gameWidth = 10;
        private const int m_gameHeight = 20;

        private BlockManager m_game = new(new int[m_gameHeight, m_gameWidth]);

        private float m_time = 0.0f;
        private float m_last = 0.0f;

        public enum GameState
        {
            PLAYING,
            PAUSED,
            GAMEOVER
        }

        // Called once when the window first loads
        public void Initialize()
        {
            // Initialize main camera
            CameraManager.AddCamera("main", new Vector3(0.0f, 0.0f, 4.0f), Global.SCREEN_WIDTH / (float)Global.SCREEN_HEIGHT);
            CameraManager.GetCamera("main").View = Camera.Projection.ORTHOGRAPHIC;
            CameraManager.SetActiveCamera("main");

            // Load shaders
            ResourceManager.LoadShader(Global.PATH + "Examples/Shaders/meshBasic.vert", Global.PATH + "Examples/Shaders/meshBasic.frag", "mesh");

            // Initialize shaders
            var projection = CameraManager.GetCamera("main").GetProjMatrix();
            var view = CameraManager.GetCamera("main").GetViewMatrix();

            ResourceManager.GetShader("mesh").Use().SetInt("sprite", 0);
            ResourceManager.GetShader("mesh").SetMatrix4("view", view);
            ResourceManager.GetShader("mesh").SetMatrix4("projection", projection);

            // Load textures
            ResourceManager.LoadTexture(Global.PATH + "Examples/Textures/textureAtlas.png", true, "gameSquare");
            ResourceManager.LoadTexture(Global.PATH + "Examples/Textures/paused.png", true, "pauseText");
            ResourceManager.LoadTexture(Global.PATH + "Examples/Textures/gameover.png", true, "gameOverText");

            // Initialize texture atlas
            var  atlas = new TextureAtlas(4, 4, 0);

            // Initialize vertices for mesh
            List<VTX.Vertex> vertices = new();
            Vector2[] UVs = atlas.GetUVs();

            vertices.Add(VTX.NewVertex(new Vector3(0.0f, 0.0f, -1.0f), UVs[2], new Vector3(0.0f, 0.0f, 1.0f)));
            vertices.Add(VTX.NewVertex(new Vector3(1.0f, 0.0f, -1.0f), UVs[1], new Vector3(0.0f, 0.0f, 1.0f)));
            vertices.Add(VTX.NewVertex(new Vector3(0.0f, 1.0f, -1.0f), UVs[0], new Vector3(0.0f, 0.0f, 1.0f)));

            vertices.Add(VTX.NewVertex(new Vector3(0.0f, 1.0f, -1.0f), UVs[0], new Vector3(0.0f, 0.0f, 1.0f)));
            vertices.Add(VTX.NewVertex(new Vector3(1.0f, 0.0f, -1.0f), UVs[1], new Vector3(0.0f, 0.0f, 1.0f)));
            vertices.Add(VTX.NewVertex(new Vector3(1.0f, 1.0f, -1.0f), UVs[3], new Vector3(0.0f, 0.0f, 1.0f)));

            // Initialize mesh
            m_mesh = new(ResourceManager.GetShader("mesh"), vertices);

            // Initialize text sprite
            m_sprite = new(ResourceManager.GetShader("mesh"));

            // Initialize renderer
            m_renderer.Initialize(m_mesh, atlas);
            m_renderer.Initialize(m_sprite);
        }

        // Called once per frame
        public void Update(float time)
        {
            m_time += time;

            m_game.GetInput();

            if (m_game.State != GameState.PLAYING) return;

            // When 1 second passes
            if (m_time - m_last > 0.2f)
            {
                m_game.UpdateBlock();

                m_last = m_time;
            }
        }

        // Called once per frame
        public void Render(float time)
        {
            for(int i = 0; i < m_gameHeight; i++)
            {
                for(int j = 0; j < m_gameWidth; j++)
                {
                    // Set transforms
                    float offsetX = 30 * (m_gameWidth / 2);
                    float offsetY = 30 * (m_gameHeight / 2);

                    float X = (30 * j) - offsetX;
                    float Y = (30 * i) - offsetY + 30;

                    Vector3 position = new(X, -Y, 2);
                    Transforms transform = new(position, new Vector2(30));

                    // Set texture
                    m_renderer.ChangeTextureToIndex(m_mesh, m_game.CurrentBoard[i,j]);

                    // Draw each square in grid
                    m_renderer.Draw(m_mesh, ResourceManager.GetTexture("gameSquare"), transform);
                }
            }

            if (m_game.State == GameState.PAUSED)
            {
                Vector3 position = new(0 - 60, 0 - 60, 2f);
                Transforms transform = new(position, new Vector2(120));

                m_renderer.Draw(m_sprite, ResourceManager.GetTexture("pauseText"), transform);
            }
            if (m_game.State == GameState.GAMEOVER)
            {
                Vector3 position = new(0 - 60, 0 - 60, 2f);
                Transforms transform = new(position, new Vector2(120));

                m_renderer.Draw(m_sprite, ResourceManager.GetTexture("gameOverText"), transform);
            }

            ResourceManager.GetShader("mesh").SetMatrix4("view", CameraManager.GetActiveCamera().GetViewMatrix());
        }
    }
}