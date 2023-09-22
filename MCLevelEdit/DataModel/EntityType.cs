using CommunityToolkit.Mvvm.ComponentModel;

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

        public virtual EntityChildType[] ChildTypes
        {
            get
            {
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
    }
}
