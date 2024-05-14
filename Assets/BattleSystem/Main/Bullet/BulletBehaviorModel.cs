using BattleSystem.Const;
using BattleSystem.Main.Base.Behavior;
using KamenMessage.RunTime.Interface.Message;

namespace BattleSystem.Main.Bullet
{
    public class BulletBehaviorModel : BattleBehaviorModel
    {
        public override BattleBehaviorType Type => BattleBehaviorType.Bullet;
        protected BulletModel Model { get; set; }

        public virtual void SetModel(BulletModel model)
        {
            Model = model;
            Model.SetOnViewReadyAction(OnViewReady);
        }

        protected virtual void OnViewReady()
        {
            Model.Transform.gameObject.SetActive(value: true);
        }

        protected override void OnPassed(float deltaTime)
        {
            OnUpdate(deltaTime);
        }

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        protected virtual void OnHit()
        {

        }

        public BulletBehaviorModel(IMessageService messageService) : base(messageService)
        {
        }
    }
}