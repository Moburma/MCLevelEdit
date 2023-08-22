using System;

namespace MCLevelEdit.Domain
{
    public record LevelEntity(int Id, EntityType EntityType, Position Position);
}
