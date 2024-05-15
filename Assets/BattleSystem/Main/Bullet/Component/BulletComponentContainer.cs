using BattleSystem.Component;
using BattleSystem.Tools;
using KamenMessage.RunTime.Singleton;
using UniRx;
using UnityEngine;

namespace BattleSystem.Main.Bullet.Component
{
    public class BulletComponentContainer : MonoSingleton<BulletComponentContainer>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            EntrustDisposable(BulletContainer.Instance.Bullets.ObserveAdd().Subscribe(delegate(CollectionAddEvent<BulletModel> o)
            {
                OnAdd(o.Value);
            }));
            EntrustDisposable(BulletContainer.Instance.Bullets.ObserveRemove().Subscribe(delegate(CollectionRemoveEvent<BulletModel> o)
            {
                OnRemove(o.Value);
            }));
        }

        private void OnAdd(BulletModel model)
        {
            BulletComponent bulletComponent = ResourceMapContainer.Instance.Get<BulletComponent>(
                ResourceMapContainer.Instance.BulletComponent.gameObject, model.Parent); 
            Vector3 bulletPos = ((model.BornPosition == default(Vector3)) ? model.Atk.Transform.position : model.BornPosition);
            bulletPos += model.Atk.GetBulletGeneration();
            var transform1 = bulletComponent.transform;
            transform1.localScale = Vector3.one;
            transform1.position = bulletPos;
            bulletComponent.Init(model);
            model.ViewReady(transform1);
        }

        private void OnRemove(BulletModel model)
        {
            IBattleComponent component = model.Transform.GetComponent<IBattleComponent>();
            component.Release();
        }
    }
}