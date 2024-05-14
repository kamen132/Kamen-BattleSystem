using BattleSystem.Main.Base.Behavior;
using KamenMessage.RunTime.Interface.Message;

namespace BattleSystem.Main.Buff
{
    public abstract class BuffBehaviorModel : QuarkBehaviorModel<BuffModel>
    {
        protected BuffBehaviorModel(IMessageService messageService) : base(messageService)
        {
        }
    }
}