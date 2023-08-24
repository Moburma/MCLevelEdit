using System.Collections.Generic;

namespace MCLevelEdit.DataModel
{
    public class EntityTypes
    {
        private static EntityTypes instance;

        public readonly Dictionary<int, EntityType> Sceneries;
        public readonly Dictionary<int, EntityType> Creatures;
        public readonly Dictionary<int, EntityType> Effects;

        public static EntityTypes I
        {
            get
            {
                if (instance == null)
                    instance = new EntityTypes();

                return instance;
            }
        }

        public EntityTypes()
        {
            Creatures = new Dictionary<int, EntityType>
            {
                { 0, new EntityType(0, "Dragon") },
                { 1, new EntityType(1, "Vulture") },
                { 2, new EntityType(2, "Bee") },
                { 3, new EntityType(3, "Worm") },
                { 4, new EntityType(4, "Archer") },
                { 5, new EntityType(5, "Crab") },
                { 6, new EntityType(6, "Kraken") },
                { 7, new EntityType(7, "Troll/Ape") },
                { 8, new EntityType(8, "Griffin") },
                { 9, new EntityType(9, "Skeleton") },
                { 10, new EntityType(10, "Emu") }
            };

            Effects = new Dictionary<int, EntityType>
            {
                { 0, new EntityType(0, "Unknown")},
                { 1, new EntityType(1, "Big explosion")},
                { 2, new EntityType(2, "Unknown")},
                { 3, new EntityType(3, "Unknown")},
                { 4, new EntityType(4, "Unknown")},
                { 5, new EntityType(5, "Splash")},
                { 6, new EntityType(6, "Fire")},
                { 7, new EntityType(7, "Unknown")},
                { 8, new EntityType(8, "Mini Volcano")},
                { 9, new EntityType(9, "Volcano")},
                { 10, new EntityType(10, "Mini crater")},
                { 11, new EntityType(11, "Crater")},
                { 12, new EntityType(12, "Unknown")},
                { 13, new EntityType(13, "White smoke")},
                { 14, new EntityType(14, "Black smoke")},
                { 15, new EntityType(15, "Earthquake")},
                { 16, new EntityType(16, "Unknown")},
                { 17, new EntityType(17, "Meteor")},
                { 18, new EntityType(18, "Unknown")},
                { 19, new EntityType(19, "Unknown")},
                { 20, new EntityType(20, "Unknown")},
                { 21, new EntityType(21, "Steal Mana")},
                { 22, new EntityType(22, "Unknown")},
                { 23, new EntityType(23, "Lightning")},
                { 24, new EntityType(24, "Rain of Fire")},
                { 25, new EntityType(25, "Unknown")},
                { 26, new EntityType(26, "Unknown")},
                { 27, new EntityType(27, "Unknown")},
                { 28, new EntityType(28, "Wall")},
                { 29, new EntityType(29, "Path")},
                { 30, new EntityType(30, "Unknown")},
                { 31, new EntityType(31, "Canyon")},
                { 32, new EntityType(32, "Unknown")},
                { 33, new EntityType(33, "Unknown")},
                { 34, new EntityType(34, "Teleport")},
                { 35, new EntityType(35, "Unknown")},
                { 36, new EntityType(36, "Unknown")},
                { 37, new EntityType(37, "Unknown")},
                { 38, new EntityType(38, "Unknown")},
                { 39, new EntityType(39, "Mana Ball")},
                { 40, new EntityType(40, "Unknown")},
                { 41, new EntityType(41, "Unknown")},
                { 42, new EntityType(42, "Unknown")},
                { 43, new EntityType(43, "Unknown")},
                { 44, new EntityType(44, "Unknown")},
                { 45, new EntityType(45, "Villager Building")},
                { 46, new EntityType(46, "Unknown")},
                { 47, new EntityType(47, "Unknown")},
                { 48, new EntityType(48, "Unknown")},
                { 49, new EntityType(49, "Unknown")},
                { 50, new EntityType(50, "Ridge Node")},
                { 51, new EntityType(51, "Unknown")},
                { 52, new EntityType(52, "Crab Egg") }
            };

            Sceneries = new Dictionary<int, EntityType>
            {
                { 0, new EntityType(0, "Tree")},
                { 1, new EntityType(1, "Standing Stone")},
                { 2, new EntityType(2, "Dolmen")},
                { 3, new EntityType(3, "Bad Stone")},
                { 4, new EntityType(4, "2D Dome")},
                { 5, new EntityType(5, "2D Dome")}
            };
        }
    }

}
