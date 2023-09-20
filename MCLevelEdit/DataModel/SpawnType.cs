namespace MCLevelEdit.DataModel
{
    public enum Spawn
    {
        Flyer1 = 4,
        Flyer2 = 5,
        Flyer3 = 6,
        Flyer4 = 7,
        Flyer5 = 8,
        Flyer6 = 9,
        Flyer7 = 10,
        Flyer8 = 11
    }

    public class SpawnType : EntityType
    {
        public SpawnType(Spawn spawn) : base(TypeId.Spawn, ((int)spawn), spawn.ToString()) { }
    }
}
