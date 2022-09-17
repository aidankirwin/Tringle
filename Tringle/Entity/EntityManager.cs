using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tringle
{
    /// <summary>
    /// A class to hold a list of entities.
    /// </summary>
    public class EntityManager
    {
        List<Entity> m_entities;
        int m_count;

        public EntityManager()
        {
            this.m_entities = new List<Entity>();
            this.m_count = 0;
        }

        /// <summary>
        /// Creates a new <see cref="Entity"/> with a unique id and adds it to a list.
        /// </summary>
        /// <returns>A new <see cref="Entity"/> object.</returns>
        public Entity AddEntity()
        {
            Entity entity = new Entity(this.m_count);
            this.m_entities.Add(entity);
            this.m_count++;
            return entity;
        }

        public Entity GetEntity(int id)
        {
            return m_entities[id];
        }

        public void RemoveEntity(int id)
        {
            m_entities.Remove(this.GetEntity(id));
        }
    }
}
