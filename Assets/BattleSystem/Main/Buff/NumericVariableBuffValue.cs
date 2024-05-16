using BattleSystem.Const;

namespace BattleSystem.Main.Buff
{
    public class NumericVariableBuffValue : NumericVariableValue
    {
        public string BuffGuid { get; set; }

        public void Init(NumericVariableType type, int value, string buffGuid)
        {
            base.NumericVariableType = type;
            base.NumericSourceType = NumericSourceType.Buff;
            base.Value = value;
            BuffGuid = buffGuid;
        }
    }
}