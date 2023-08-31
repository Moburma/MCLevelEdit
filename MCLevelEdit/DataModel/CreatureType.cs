namespace MCLevelEdit.DataModel
{
    public enum Creature
    {
        Dragon = 0,
        Vulture = 1,
        Bee = 2,
        Worm = 3,
        Archer = 4,
        Crab = 5,
        Kraken = 6,
        TrollOrApe = 7,
        Griffin = 8,
        Skeleton = 9,
        Emu = 10
    }

    public record CreatureType(Creature creature) : EntityType(((int)creature), creature.ToString());
}
