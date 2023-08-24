using System;

namespace MCLevelEdit.Domain
{
    public record Entity(int Id, EntityType EntityType, Position Position);
}
