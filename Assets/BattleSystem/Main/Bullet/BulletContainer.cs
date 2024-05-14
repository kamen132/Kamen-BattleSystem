using System.Collections.Generic;
using System.Linq;
using BattleSystem.Const;
using BattleSystem.Main.Unit;
using KamenMessage;
using KamenMessage.RunTime.Basic.Message;
using KamenMessage.RunTime.Singleton;
using UniRx;
using UnityEngine;

namespace BattleSystem.Main.Bullet
{
    public class BulletContainer : MonoSingleton<BulletContainer>
    {
        public readonly Dictionary<BulletType, BulletBehaviorModel> mBulletBehaviorMap = new Dictionary<BulletType, BulletBehaviorModel>();
        public ReactiveCollection<BulletModel> Bullets { get; set; }
        public Transform Parent { get; private set; }

        protected override void OnInitialize()
        {
            Bullets = new ReactiveCollection<BulletModel>();
            MessageService.Instance.Register<BattleRoundEndDto>(OnBattleRoundEnd);
        }

        private void OnBattleRoundEnd(BattleRoundEndDto dto)
        {
            List<BulletModel> tempBullets = Bullets.ToList();
            foreach (BulletModel bullet in tempBullets)
            {
                bullet.IsDispose = true;
                RemoveBullet(bullet);
            }
        }
        
        public BulletModel CreateBullet(BattleUnitModel atk, BattleUnitModel hit, int bulletId, Vector3 bornPosition = default(Vector3), DamageSourceType sourceType = DamageSourceType.Ally)
        {
            BulletModel model = new BulletModel();
            model.Init(Parent, atk, bulletId, hit, bornPosition, sourceType);
            Bullets.Add(model);
            return model;
        }
        public BulletBehaviorModel GetBehaviorByBulletType(BulletType bulletType)
        {
            mBulletBehaviorMap.TryGetValue(bulletType, out var result);
            return result;
        }
        
        public void RemoveBullet(BulletModel model)
        {
            Bullets.Remove(model);
        }
    }
}