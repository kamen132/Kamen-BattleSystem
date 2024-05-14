using BattleSystem.Const;
using BattleSystem.Main.Base.Model;
using BattleSystem.Main.Buff;

namespace BattleSystem.Main.Unit
{
    public class BattleUnitModel : BattleViewModel
    {
        public event UnitActionEvent OnHitSelfAfterEvent;

        public event UnitActionEvent OnHitSelfBeforeEvent;

        public event UnitActionEvent OnHitAfterEvent;

        public event UnitActionEvent OnHitBeforeEvent;

        public event UnitActionEvent OnKillEvent;

        public event UnitActionEvent OnBlockEvent;

        public event UnitOnAddBuffEvent OnAddBuffEvent;

        public event UnitActionEvent OnCritEvent;
    }

    public delegate void UnitOnAddBuffEvent(BuffModel buff, BattleUnitModel target);
    public delegate void UnitActionEvent(BattleUnitModel target, BattleUnitModel attacker, DamageSourceType sourceType, int dmg = 0, bool stack = true);
}