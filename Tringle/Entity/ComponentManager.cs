namespace Tringle
{
    /// <summary>
    /// A class to hold a <see cref="Dictionary{TKey, TValue}"/> for each component type.
    /// </summary>
    public class ComponentManager : 
        IComponentManager<Transforms>, 
        IComponentManager<Sprite>, 
        IComponentManager<Mesh>
    {
        private Dictionary<int, Transforms>  m_transformComp = new();
        private Dictionary<int, Sprite> m_spriteComp = new();
        private Dictionary<int, Mesh> m_meshComp = new();
        private Dictionary<int, AABB> m_aabbComp = new();

        public void AddComponent(int entityID, Transforms component)
        {
            m_transformComp.Add(entityID, component);
        }

        public void AddComponent(int entityID, Sprite component)
        {
            m_spriteComp.Add(entityID, component);
        }

        public void AddComponent(int entityID, Mesh component)
        {
            m_meshComp.Add(entityID, component);
        }

        public void AddComponent(int entityID, AABB component)
        {
            m_aabbComp.Add(entityID, component);
        }

        public Transforms GetTransform(int entityID)
        {
            return m_transformComp[entityID];
        }

        public Sprite GetSprite(int entityID)
        {
            return m_spriteComp[entityID];
        }

        public Mesh GetMesh(int entityID)
        {
            return m_meshComp[entityID];
        }

        public AABB GetAABB(int entityID)
        {
            return m_aabbComp[entityID];
        }
    }

    public interface IComponentManager<TComponent>
    {
        void AddComponent(int entityID, TComponent component);
    }
}
