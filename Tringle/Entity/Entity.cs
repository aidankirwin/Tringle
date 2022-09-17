using OpenTK.Mathematics;

namespace Tringle
{
    /// <summary>
    /// An entity; carries an integer ID.
    /// </summary>
    public class Entity
    {
        int m_id;

        public Entity(int id)
        {
            this.m_id = id;
        }

        public int GetID()
        {
            return m_id;
        }
    }
}