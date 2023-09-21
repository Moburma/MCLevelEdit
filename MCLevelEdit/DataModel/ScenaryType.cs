using System;
using System.Linq;

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
        private static EntityChildType[] _childTypes;

        public ScenaryType(Scenary scenary) : base(TypeId.Scenary, ((int)scenary), scenary.ToString()) { }

        public override EntityChildType[] ChildTypes
        {
            get
            {
                if (_childTypes is null)
                {
                    _childTypes = Enum.GetValues(typeof(Scenary))
                        .Cast<int>()
                        .Select(x => new EntityChildType() { Id = x, Name = Enum.GetName(typeof(Scenary), x) })
                        .ToArray();
                }

                return _childTypes;
            }
        }
    }
}
