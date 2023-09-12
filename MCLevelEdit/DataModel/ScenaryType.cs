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

    public record ScenaryType(Scenary scenary) : EntityType(2, ((int)scenary), scenary.ToString());
}
