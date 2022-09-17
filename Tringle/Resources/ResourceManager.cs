namespace Tringle
{
    public class ResourceManager
    {
        public static Dictionary<string, Shader> Shaders = new();
        public static Dictionary<string, Texture> Textures = new();

        public static Shader LoadShader(string vFile, string fFile, string name)
        {
            var shader = new Shader(vFile, fFile);
            Shaders.Add(name, shader);

            return Shaders[name];
        }
        public static Shader GetShader(string name)
        {
            return Shaders[name];
        }

        public static Texture LoadTexture(string file, bool alpha, string name)
        {
            var texture = Texture.LoadFromFile(file);
            Textures.Add(name, texture);

            return Textures[name];
        }
        public static Texture GetTexture(string name)
        {
            return Textures[name];
        }
    }
}