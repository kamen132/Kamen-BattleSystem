using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleMsg;
using BattleSystem.Const;
using BattleSystem.Main.Base.Model;
using BattleSystem.Main.Buff;
using BattleSystem.Main.Skill;
using UniRx;
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
        public ReactiveCollection<NumericVariableValue> NumericVariableValues { get; set; }

        public ReactiveDictionary<string, BuffModel> Buffs { get; set; }
        public BattleUnitModel()
        {
            NumericVariableValues = new ReactiveCollection<NumericVariableValue>();
            NumericVariableValues.ObserveAdd().Subscribe(delegate(CollectionAddEvent<NumericVariableValue> @event)
            {
                RefreshProperty(@event.Value);
            });
            NumericVariableValues.ObserveRemove().Subscribe(delegate(CollectionRemoveEvent<NumericVariableValue> @event)
            {
                RefreshProperty(@event.Value);
            });
            
            Buffs = new ReactiveDictionary<string, BuffModel>();
        }

        
        protected void OnBattleClear(BattleClearDto dto)
        {
            EntrustDisposablesClear();
            NumericVariableValues.Clear();
            Buffs.Clear();
        }
        
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
        private void RefreshProperty(NumericVariableValue numericVariableValue)
        {
            OnRefreshProperty(numericVariableValue.NumericVariableType);
        }
        protected abstract void OnRefreshProperty(NumericVariableType numericVariableType);
        
        public int GetNumericVariableValue(NumericVariableType type)
        {
            return NumericVariableValues.Where((NumericVariableValue n) => n.NumericVariableType == type).Sum((NumericVariableValue n) => n.Value);
        }
        
        public void SetNumericVariableValue(NumericVariableType type, NumericSourceType sourceType, int value)
        {
            NumericVariableValue numericVariableValue = new NumericVariableValue();
            numericVariableValue.Init(type, sourceType, value);
            NumericVariableValues.Add(numericVariableValue);
        }

        public void SetNumericVariableValueByBuff(NumericVariableType type, int value, string buffGuid)
        {
            NumericVariableBuffValue numericVariableValue = new NumericVariableBuffValue();
            numericVariableValue.Init(type, value, buffGuid);
            NumericVariableValues.Add(numericVariableValue);
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