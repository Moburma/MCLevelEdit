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

    public record EntityType(TypeId TypeId, int Id, string Name);

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
