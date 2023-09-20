using CommunityToolkit.Mvvm.ComponentModel;
using System;
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
        private int _id;
        private string _name;

        public TypeId TypeId
        {
            get { return _typeId; }
            set { SetProperty(ref _typeId, value); }
        }
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public EntityType(TypeId typeId, int id, string name)
        {
            _typeId = typeId;
            _id = id;
            _name = name;
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

        public static KeyValuePairViewModel[] GetEntityTypesFromTypeId(this TypeId typeId)
        {
            switch (typeId)
            {
                case TypeId.Scenary:
                    return Enum.GetValues(typeof(Scenary))
                        .Cast<int>()
                        .Select(x => new KeyValuePairViewModel(){ Key = x, Value = Enum.GetName(typeof(Scenary), x) })
                        .ToArray();
                case TypeId.Spawn:
                    return Enum.GetValues(typeof(Spawn))
                        .Cast<int>()
                        .Select(x => new KeyValuePairViewModel() { Key = x, Value = Enum.GetName(typeof(Spawn), x) })
                        .ToArray();
                case TypeId.Creature:
                    return Enum.GetValues(typeof(Creature))
                        .Cast<int>()
                        .Select(x => new KeyValuePairViewModel() { Key = x, Value = Enum.GetName(typeof(Creature), x) })
                        .ToArray();
                case TypeId.Effect:
                    return Enum.GetValues(typeof(Effect))
                        .Cast<int>()
                        .Select(x => new KeyValuePairViewModel() { Key = x, Value = Enum.GetName(typeof(Effect), x) })
                        .ToArray();
                default:
                    return new KeyValuePairViewModel[0];
            }
        }
    }
}
