using System.Collections.Generic;
using BattleSystem.Const;
using BattleSystem.Tools;

namespace BattleSystem.Config
{
    public class SkillConfig
    {
        public object ConfigId => Id;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<BattleParamType, int> Params { get; set; }

        public int Range { get; set; }

        public BattleUnitTargetType TargetType { get; set; }

        public ResourceTag SkillPrefab { get; set; }
    }
}