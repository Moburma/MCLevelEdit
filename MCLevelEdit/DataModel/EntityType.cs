using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCLevelEdit.DataModel
{
    public enum TypeId
    {
        None = 0,
        Scenary = 2,
        Spawn = 3,
        Creature = 5,
        Effect = 10
    }

    public class EntityType : ObservableObject
    {
        private TypeId _typeId;
        private EntityChildType _child;

        public EntityChildType[] ChildTypes
        {
            get
            {
                var T = EntityTypeExtensions.GetEnumTypeFromTypeId(_typeId);
                if (T is not null)
                {
                    return Enum.GetValues(T)?
                            .Cast<int>()
                            .Select(x => new EntityChildType() { Id = x, Name = Enum.GetName(T, x) })
                            .ToArray();
                }
                return new EntityChildType[] { };
            }
        }

        public TypeId TypeId
        {
            get { return _typeId; }
            set { SetProperty(ref _typeId, value); }
        }
        public EntityChildType Child
        {
            get { return _child; }
            set { 
                SetProperty(ref _child, value); 
            }
        }

        public EntityType(TypeId typeId, int id, string name)
        {
            _typeId = typeId;
            _child = new EntityChildType()
            {
                Id = id,
                Name = name
            };
        }

    };

    public static class EntityTypeExtensions
    {
        public static EntityType GetEntityFromTypeId(this TypeId typeId)
        {
            switch (typeId)
            {
                case TypeId.Scenary:
                    return new ScenaryType(Scenary.Tree);
                case TypeId.Spawn:
                    return new SpawnType(Spawn.Flyer1);
                case TypeId.Creature:
                    return new CreatureType(Creature.Vulture);
                case TypeId.Effect:
                    return new EffectType(Effect.Unknown0);
                default:
                    return new EntityType(typeId, 0, "");
            }
        }

        public static Type GetEnumTypeFromTypeId(this TypeId typeId)
        {
            switch (typeId)
            {
                case TypeId.Scenary:
                    return typeof(Scenary);
                case TypeId.Spawn:
                    return typeof(Spawn);
                case TypeId.Creature:
                    return typeof(Creature);
                case TypeId.Effect:
                    return typeof(Effect);
                default:
                    return null;
            }
        }
    }
}
