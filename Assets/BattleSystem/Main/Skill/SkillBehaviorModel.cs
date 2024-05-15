using System;
using System.Collections.Generic;
using BattleSystem.BattleMsg;
using BattleSystem.Config;
using BattleSystem.Const;
using BattleSystem.Main.Unit;
using KamenMessage.RunTime.Basic.Model;
using UnityEngine;

namespace BattleSystem.Main.Skill
{
    public abstract class SkillBehaviorModel: ManualModel
    {
        protected BattleUnitModel Spellcaster { get; set; }
        protected List<BattleUnitModel> Targets { get; set; }
        protected Vector3 Position { get; set; }
        protected SkillConfig Config { get; set; }
        
        public void Handle(BattleUnitModel spellcaster, int skillId, Vector3 position = default(Vector3), List<BattleUnitModel> targets = null)
        {
            Spellcaster = spellcaster;
            Targets = targets;
            Position = position;
            OnHandle();
        }
        
        protected abstract void OnHandle();

        protected void PlayCommonEffect(Vector3 position, SkillEffectType skillEffectType)
        {
            MessageService.Dispatch(new CreateSkillEffectDto
            {
                SkillEffectType = skillEffectType,
                ResourceTag = Config.SkillPrefab,
                EffectPos = position,
                Radius = GetParam(BattleParamType.Radius)
            });
        }
        
        protected int GetParam(BattleParamType battleParamType)
        {
            try
            {
                return Config.Params[battleParamType];
            }
            catch (Exception)
            {
                throw new Exception($"not found id {Config.Id} param{battleParamType}");
            }
        }
    }
}