using System;
using System.Linq;

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

    public class CreatureType : EntityType
    {
        private static EntityChildType[] _childTypes;

        public CreatureType(Creature creature) : base(TypeId.Creature, ((int)creature), creature.ToString()) { }

        public override EntityChildType[] ChildTypes
        {
            get
            {
                if (_childTypes is null)
                {
                    _childTypes = Enum.GetValues(typeof(Creature))
                        .Cast<int>()
                        .Select(x => new EntityChildType() { Id = x, Name = Enum.GetName(typeof(Creature), x) })
                        .ToArray();
                }

                return _childTypes;
            }
        }
    }
}
