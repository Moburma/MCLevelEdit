using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCLevelEdit.DataModel
{
    public class Map : ObservableObject
    {
        public static Map Instance { get; set; }

        private IList<Entity> _entities;

        public IList<Entity> Entities { get { return _entities; } }

        public Map()
        {
            _entities = new List<Entity>();
        }

        public IList<Entity> GetEntitiesByPosition(Position postion)
        {
            return _entities.Where(e => e.Position == postion).ToList();
        }

        public void AddEntity(Entity entity)
        {
            //TODO: Validation needed, this assumes for the moment that more than 1 entity can be on one square
            this.Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            this.Entities.Remove(entity);
        }
    }
}
