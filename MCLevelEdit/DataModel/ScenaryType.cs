namespace MCLevelEdit.DataModel
{
    public enum Scenary
    {
        Tree = 0,
        StandingStone = 1,
        Dolmen = 2,
        BadStone = 3,
        Dome1 = 4,
        Dome2 = 5
    }

    public class ScenaryType : EntityType
    {
        public ScenaryType(Scenary scenary) : base(TypeId.Scenary, ((int)scenary), scenary.ToString()) { }
    }
}
