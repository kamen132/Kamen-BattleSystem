using BattleSystem.Const;
using KamenMessage.RunTime.Basic.Model;

namespace BattleSystem.Main.Buff
{
    public class NumericVariableValue : ManualModel
    {
        public NumericVariableType NumericVariableType { get; protected set; }

        public NumericSourceType NumericSourceType { get; protected set; }

        public int Value { get; protected set; }

        public void Init(NumericVariableType type, NumericSourceType sourceType, int value)
        {
            NumericVariableType = type;
            NumericSourceType = sourceType;
            Value = value;
        }
    }

}