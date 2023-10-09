﻿using Avalonia.Media;
using System;
using System.Linq;

namespace MCLevelEdit.DataModel
{
    public enum Spell
    {
        Fireball = 0,
        Heal = 1,
        SpeedUp = 2,
        Possession = 3,
        Shield = 4,
        BeyondSight = 5,
        Earthquake = 6,
        Meteor = 7,
        Volcano = 8,
        Crater = 9,
        Teleport = 10,
        Duel = 11,
        Invisible = 12,
        StealMana = 13,
        Rebound = 14,
        Lightning = 15,
        Castle = 16,
        Skeleton = 17,
        Thunderbolt = 18,
        ManaMagnet = 19,
        FireWall = 20,
        ReverseSpeed = 21,
        GlobalDeath = 22,
        RapidFireball = 23
    }

    public class SpellType : EntityType
    {
        private static EntityChildType[] _childTypes;

        public SpellType(Spell spell) : base(TypeId.Spell, Color.FromRgb(128, 0, 128), ((int)spell), spell.ToString()) { }

        public override EntityChildType[] ChildTypes
        {
            get
            {
                if (_childTypes is null)
                {
                    _childTypes = Enum.GetValues(typeof(Spell))
                        .Cast<int>()
                        .Select(x => new EntityChildType() { Id = x, Name = Enum.GetName(typeof(Spell), x) })
                        .ToArray();
                }

                return _childTypes;
            }
        }
    }
}
