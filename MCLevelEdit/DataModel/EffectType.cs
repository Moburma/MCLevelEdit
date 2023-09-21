﻿using Avalonia.Media;
using System;
using System.Linq;

namespace MCLevelEdit.DataModel
{
    public enum Effect
    {
        Unknown0 = 0,  
        BigExplosion = 1, 
        Unknown1 = 2,
        Unknown2 = 3,
        Unknown4 = 4,
        Splash = 5,
        Fire = 6,
        Unknown7 = 7, 
        MiniVolcano = 8,
        Volcano = 9, 
        MiniCrater = 10,
        Crater = 11,
        Unknown12 = 12,
        WhiteSmoke = 13,
        BlackSmoke = 14,
        Earthquake = 15,
        Unknown = 16,
        Meteor = 17,
        Unknown18 = 18,
        Unknown19 = 19,
        Unknown20 = 20,
        StealMana = 21,
        Unknown22 = 22,
        Lightning = 23,
        RainOfFire = 24,
        Unknown25 = 25,
        Unknown26 = 26,
        Unknown27 = 27,
        Wall = 28,
        Path = 29,
        Unknown13 = 30,
        Canyon = 31,
        Unknown32 = 32,
        Unknown33 = 33,
        Teleport = 34,
        Unknown16 = 35,
        Unknown36 = 36,
        Unknown37 = 37,
        Unknown38 = 38,
        ManaBall = 39,
        Unknown40 = 40,
        Unknown41 = 41,
        Unknown42 = 42,
        Unknown43 = 43,
        Unknown44 = 44,
        VillagerBuilding = 45,
        Unknown46 = 46,
        Unknown47 = 47,
        Unknown48 = 48,
        Unknown49 = 49,
        RidgeNode = 50,
        Unknown51 = 51,
        CrabEgg = 52
    }

    public class EffectType : EntityType
    {
        private static EntityChildType[] _childTypes;

        public EffectType(Effect effect) : base(TypeId.Effect, ((int)effect), effect.ToString()) { }

        public override EntityChildType[] ChildTypes
        {
            get
            {
                if (_childTypes is null)
                {
                    _childTypes = Enum.GetValues(typeof(Effect))
                        .Cast<int>()
                        .Select(x => new EntityChildType() { Id = x, Name = Enum.GetName(typeof(Effect), x) })
                        .ToArray();
                }

                return _childTypes;
            }
        }
    }
}
