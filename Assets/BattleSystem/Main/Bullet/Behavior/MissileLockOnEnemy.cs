using KamenMessage.RunTime.Interface.Message;

namespace BattleSystem.Main.Bullet
{
    public class MissileLockOnEnemy : BulletBehaviorModel
    {
        public MissileLockOnEnemy(IMessageService messageService) : base(messageService)
        {
        }
    }
}