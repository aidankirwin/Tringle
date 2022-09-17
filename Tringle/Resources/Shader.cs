using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Tringle
{
    public class Shader
    {
        public readonly int Handle;
        private readonly Dictionary<string, int> m_uniformLocations;

        public Shader(string vertPath, string fragPath)
        {
            // Load vertex shader
            var source = File.ReadAllText(vertPath);

            var vShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vShader, source);
            // Compile vertex shader
            CompileShader(vShader);

            // Load fragment shader
            source = File.ReadAllText(fragPath);

            var fShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fShader, source);
            // Compile fragment shader
            CompileShader(fShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vShader);
            GL.AttachShader(Handle, fShader);

            LinkProgram(Handle);

            GL.DetachShader(Handle, vShader);
            GL.DetachShader(Handle, fShader);
            GL.DeleteShader(vShader);
            GL.DeleteShader(fShader);

            // Cache shader uniform locations
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            m_uniformLocations = new Dictionary<string, int>();
            for(var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(Handle, i, out _, out _);
                var location = GL.GetUniformLocation(Handle, key);
                m_uniformLocations.Add(key, location);
            }

        }

        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);

            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occured when compiling Shader({shader}). \n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);

            if(code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(program);
                throw new Exception($"Error occured when linking Program({program}). \n{infoLog}");
            }
        }

        public Shader Use()
        {
            GL.UseProgram(Handle);
            return this;
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(m_uniformLocations[name], data);
        }
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(m_uniformLocations[name], data);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(m_uniformLocations[name], true, ref data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(m_uniformLocations[name], data);
        }

        public void DeleteShader()
        {
            GL.DeleteProgram(Handle);
        }
    }
}
