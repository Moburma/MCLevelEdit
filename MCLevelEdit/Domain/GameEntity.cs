using System;

namespace MCLevelEdit.Domain
{
    public class GameEntity
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }

        public GameEntity(int id, int typeId, string name)
        {
            this.Id = id;
            this.TypeId = typeId;
            this.Name = name;
        }

        public GameEntity(int id, string name) : this(id, GetTypeIdFromName(name), name) { }

        public GameEntity(int id, int typeId) : this(id, typeId, GetNameFromTypeId(typeId)) { }

        public static string GetNameFromTypeId(int entityTypeId)
        {
            switch (entityTypeId)
            {
                case 0:
                    return "blank";
                case 2:
                    return "Scenery";
                case 3:
                    return "Player Spawn";
                case 5:
                    return "Creature";
                case 7:
                    return "Weather";
                case 10:
                    return "blank";
                case 11:
                    return "Switch";
                case 12:
                    return "Spell";
                default:
                    return "N/A";
            }
        }

        public static int GetTypeIdFromName(string entityName)
        {
            switch (entityName)
            {
                case "Blank":
                    return 0;
                case "N/A":
                    return 1;
                case "Scenery":
                    return 2;
                case "Player Spawn":
                    return 3;
                case "Creature":
                    return 5;
                case "Weather":
                    return 7;
                case "Effect":
                    return 10;
                case "Switch":
                    return 11;
                case "Spell":
                    return 12;
                default:
                    return 1;
            }
        }
    }

}
