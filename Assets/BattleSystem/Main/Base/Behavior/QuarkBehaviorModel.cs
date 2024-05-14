using BattleSystem.Main.Base.Model;
using KamenMessage.RunTime.Interface.Message;

namespace BattleSystem.Main.Base.Behavior
{
    public abstract class QuarkBehaviorModel<T> : BattleBehaviorModel where T : BattleQuarkModel
    {
        protected T Model { get; private set; }

        protected QuarkBehaviorModel(IMessageService messageService) : base(messageService)
        {
        }

        public void Init(T model)
        {
            Model = model;
            OnInit();
        }

        protected virtual void OnInit()
        {
        }
    }

}