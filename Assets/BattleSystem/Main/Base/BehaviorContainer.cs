using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.BattleMsg;
using BattleSystem.Const;
using BattleSystem.Main.Base.Behavior;
using KamenMessage.RunTime.Basic.Message;
using KamenMessage.RunTime.Singleton;

namespace BattleSystem
{
	public class BehaviorContainer : MonoSingleton<BehaviorContainer>
	{
		private Dictionary<BattleBehaviorType, List<BattleBehaviorModel>> BattleBehaviorModels { get; set; }

		protected override void OnInitialize()
		{
			BattleBehaviorModels = new Dictionary<BattleBehaviorType, List<BattleBehaviorModel>>();
			MessageService.Instance.Register<ReadyBattleDto>(OnReadyBattle);
			MessageService.Instance.Register<BattleRoundStartDto>(OnBattleRoundStart);
			MessageService.Instance.Register<BattleEndDto>(OnBattleEnd);
			MessageService.Instance.Register<BattleClearDto>(OnBattleClear);
			base.OnInitialize();
		}

		private void OnBattleRoundStart(BattleRoundStartDto obj)
		{
			foreach (KeyValuePair<BattleBehaviorType, List<BattleBehaviorModel>> battleBehaviorModel in BattleBehaviorModels)
			{
				switch (battleBehaviorModel.Key)
				{
					case BattleBehaviorType.Bullet:
					case BattleBehaviorType.GroundEffect:
					case BattleBehaviorType.Monster:
						foreach (BattleBehaviorModel behaviorModel in battleBehaviorModel.Value.ToList())
						{
							behaviorModel.Release();
						}
						break;
					case BattleBehaviorType.Level:
					case BattleBehaviorType.Buff:
					case BattleBehaviorType.Halo:
					case BattleBehaviorType.BattleShopTower:
					case BattleBehaviorType.Core:
					case BattleBehaviorType.Hero:
					case BattleBehaviorType.Tower:
						break;
				}
			}
		}

		private void OnBattleClear(BattleClearDto obj)
		{
			List<BattleBehaviorModel> battleBehaviorModels = BattleBehaviorModels.Values.SelectMany((List<BattleBehaviorModel> b) => b).ToList();
			foreach (BattleBehaviorModel battleBehaviorModel in battleBehaviorModels)
			{
				battleBehaviorModel.Release();
			}

			BattleBehaviorModels.Clear();
		}

		private void OnReadyBattle(ReadyBattleDto obj)
		{
		}

		private void OnBattleEnd(BattleEndDto obj)
		{
		}

		public Dictionary<BattleBehaviorType, List<BattleBehaviorModel>> GetAllBattleBehaviors()
		{
			return BattleBehaviorModels.ToDictionary((KeyValuePair<BattleBehaviorType, List<BattleBehaviorModel>> k) => k.Key,
				(KeyValuePair<BattleBehaviorType, List<BattleBehaviorModel>> v) => v.Value.ToList());
		}

		public void AddBattleBehavior(BattleBehaviorModel behaviorModel)
		{
			BattleBehaviorType key = behaviorModel.Type;
			if (!BattleBehaviorModels.TryGetValue(key, out var battleBehaviorModels))
			{
				battleBehaviorModels = new List<BattleBehaviorModel>();
				BattleBehaviorModels.Add(key, battleBehaviorModels);
			}

			battleBehaviorModels.Add(behaviorModel);
		}

		public void RemoveBattleBehavior(BattleBehaviorModel behaviorModel)
		{
			BattleBehaviorType key = behaviorModel.Type;
			if (BattleBehaviorModels.TryGetValue(key, out var battleBehaviorModels))
			{
				battleBehaviorModels.Remove(behaviorModel);
			}
		}
	}
}