using System;
using System.Collections.Generic;
using BattleSystem.Const;
using BattleSystem.Main.Base.Model;
using BattleSystem.Main.Buff;
using BattleSystem.Main.Skill;
using UnityEngine;

namespace BattleSystem.Main.Unit
{
    public abstract class BattleUnitModel : BattleViewModel
    {
        private bool IsCrit { get; set; }
        public abstract int GetDmgBase { get; }
        public abstract int GetDmg { get; }
        public abstract float GetDmgPct { get; }
        public abstract int GetCrit { get; }
        public abstract float GetCritDmg { get; }
        public abstract int GetDef { get; }
        public abstract float GetRange { get; }
        public abstract int GetAtkSpd { get; }
        public abstract float GetFinalDmg { get; }
        public abstract int GetHp { get; }
        public abstract int GetHpMax { get; }
        public abstract int ConfigId { get; }
        public abstract int GetEnergy { get; }
        public abstract int GetEnergyMax { get; }
        public event UnitActionEvent OnHitSelfAfterEvent;
        public event UnitActionEvent OnHitSelfBeforeEvent;
        public event UnitActionEvent OnHitAfterEvent;
        public event UnitActionEvent OnHitBeforeEvent;
        public event UnitActionEvent OnKillEvent;
        public event UnitOnAddBuffEvent OnAddBuffEvent;
        public event UnitActionEvent OnCritEvent;

        public void UseSkill(int skillId, List<BattleUnitModel> targets = null, Vector3 position = default(Vector3))
        {
            string typeName = $"Model.Battle.Behavior.Quark.Other.Impl.SkillImpl.Skill{skillId}";
            Type type = System.Type.GetType(typeName);
            if (type == null)
            {
                return;
            }

            SkillBehaviorModel behavior = (SkillBehaviorModel) Activator.CreateInstance(type);
            behavior.Handle(this, skillId, position, targets);
        }

        public bool TryAddBuff(int buffId, out BuffModel buffModel, BattleUnitModel spellcaster = null)
        {
            buffModel = null;
            //todo 
            OnAddBuffEvent?.Invoke(buffModel, this);
            spellcaster?.OnAddBuffEvent?.Invoke(buffModel, this);
            return true;
        }
        
        public void Hit(BattleUnitModel attacker, DamageSourceType sourceType = DamageSourceType.None)
        {
            if (attacker == null)
            {
                return;
            }
            attacker.OnHitBeforeEvent?.Invoke(this, attacker, sourceType);
            this.OnHitSelfBeforeEvent?.Invoke(this, attacker, sourceType);
            int crit = attacker.GetCrit;
            int critRate = UnityEngine.Random.Range(0, 101);
            bool isCrit = crit >= critRate;
            float def = ((GetDef <= 0) ? 0f : ((float)GetDef / (float)(GetDef + 15)));
            float logicPct = attacker.GetCritDmg;
            float dmg = (isCrit ? ((float)(attacker.GetDmgBase + attacker.GetDmg) * logicPct * (1f + attacker.GetDmgPct - def)) : 
                ((float)(attacker.GetDmgBase + attacker.GetDmg) * (1f + attacker.GetDmgPct - def)));
            int amendDmg = (int)Math.Max(dmg, 1f);
            if (isCrit)
            {
                attacker.OnCritEvent?.Invoke(this, attacker, sourceType, amendDmg);
            }
            amendDmg = Mathf.CeilToInt(attacker.GetFinalDmg * (float)amendDmg);
            IsCrit = isCrit;
            OnHit(attacker, amendDmg, sourceType);
        }

        protected virtual void OnHit(BattleUnitModel attacker, int dmg, DamageSourceType sourceType)
        {
            attacker.OnHitAfterEvent?.Invoke(this, attacker, sourceType, dmg);
            OnHitSelfAfterEvent?.Invoke(this, attacker, sourceType, dmg);
            if (IsDie())
            {
                attacker.OnKillEvent?.Invoke(this, attacker, sourceType);
            }
        }
        
        public abstract bool IsDie();

        public virtual Vector3 GetBulletGeneration()
        {
            return Vector3.zero;
        }
    }

    public delegate void UnitOnAddBuffEvent(BuffModel buff, BattleUnitModel target);
    public delegate void UnitActionEvent(BattleUnitModel target, BattleUnitModel attacker, DamageSourceType sourceType, int dmg = 0);
}