using System.Collections.Generic;
using BattleSystem.Const;
using BattleSystem.Tools;

namespace BattleSystem.Config
{
    public class BulletConfig
    {
        public object ConfigId => Id;

        public int Id { get; set; }

        public int Speed { get; set; }

        public BulletType AtkLogic { get; set; }

        public int GenerateHeight { get; set; }

        public int DealBuff { get; set; }

        public int PeriodicSkill { get; set; }

        public int PeriodicTime { get; set; }

        public int EndSkill { get; set; }

        public ResourceTag BulletPrefab { get; set; }
        
        public ResourceTag HitEffectPrefab { get; set; }
    }
}