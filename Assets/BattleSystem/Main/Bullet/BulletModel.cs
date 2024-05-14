using System;
using System.Collections.Generic;
using BattleSystem.Config;
using BattleSystem.Const;
using BattleSystem.Main.Base.Model;
using BattleSystem.Main.Unit;
using UnityEngine;

namespace BattleSystem.Main.Bullet
{
    public class BulletModel : BattleViewModel
    {
        public Transform Parent { get; set; }
        public BattleUnitModel Atk { get; set; }
        public BattleUnitModel Hit { get; set; }
        public Vector3 BornPosition { get; set; }
        public Vector3 Destination { get; set; }
        public DamageSourceType DamageSourceType { get; set; }
        public BulletConfig Config { get; set; }
        
        public bool IsDispose { get; set; }

        public void Init(Transform parent, BattleUnitModel atk, int bulletId, BattleUnitModel hit, Vector3 bornPosition, DamageSourceType sourceType)
        {
            Parent = parent;
            Atk = atk;
            Hit = hit;
            BornPosition = bornPosition;
            Destination = Hit.Transform.position;
            DamageSourceType = sourceType;
            BulletBehaviorModel behavior = BulletContainer.Instance.GetBehaviorByBulletType(Config.AtkLogic);
            behavior.SetModel(this);
            BehaviorContainer.Instance.AddBattleBehavior(behavior);
        }
    }
}