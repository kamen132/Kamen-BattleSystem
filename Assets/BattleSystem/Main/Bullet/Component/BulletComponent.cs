using BattleSystem.Component;
using BattleSystem.Tools;
using UnityEngine;

namespace BattleSystem.Main.Bullet.Component
{
    public class BulletComponent : MonoBehaviour, IBattleComponent
    {
        private GameObject mBulletPrefab;
        public void Init(BulletModel model)
        {
            mBulletPrefab = ResourceMapContainer.Instance.Get(model.Config.BulletPrefab, transform);
        }
        public void Release()
        {
            ResourceMapContainer.Instance.Release(mBulletPrefab);
            GameObject.Destroy(this);
        }
    }
}