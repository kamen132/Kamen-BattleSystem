using System;
using System.Collections.Generic;
using BattleSystem.BattleMsg;
using BattleSystem.Const;
using KamenMessage;
using KamenMessage.RunTime.Basic.Model;
using KamenMessage.RunTime.Interface.Message;

namespace BattleSystem.Main.Base.Behavior
{
    public abstract class BattleBehaviorModel : ManualModel
    {
        private BehaviorContainer BehaviorContainerModel { get; set; }

        public abstract BattleBehaviorType Type { get; }

        public bool IsDispose { get; private set; }

        public bool IsReleasing { get; private set; }

        private List<IDisposable> _disposables { get; set; }

        public BattleBehaviorModel(IMessageService messageService)
        {
            _disposables = new List<IDisposable>
            {
                messageService.Register<BattleRoundStartDto>(OnBattleRoundStart),
                messageService.Register<BattleRoundEndDto>(OnBattleRoundEnd),
                messageService.Register<BattleEndDto>(OnBattleEnd)
            };
        }

        protected virtual void OnBattleEnd(BattleEndDto obj)
        {
        }

        protected virtual void OnBattleRoundStart(BattleRoundStartDto obj)
        {
        }

        protected virtual void OnBattleRoundEnd(BattleRoundEndDto dto)
        {
        }

        public void Passed(float deltaTime)
        {
            if (!IsDispose && !IsReleasing)
            {
                OnPassed(deltaTime);
            }
        }

        protected abstract void OnPassed(float deltaTime);

        public bool PassedDispose(float deltaTime)
        {
            if (IsDispose)
            {
                if (IsReleasing)
                {
                    return false;
                }
                Release();
                return true;
            }
            return OnPassedDispose(deltaTime);
        }

        protected virtual bool OnPassedDispose(float deltaTime)
        {
            return true;
        }

        protected void Dispose()
        {
            IsDispose = true;
        }

        public void Release()
        {
            IsDispose = true;
            IsReleasing = true;
            foreach (IDisposable disposable in _disposables)
            {
                disposable.Dispose();
            }
            OnRelease();
            BehaviorContainerModel.RemoveBattleBehavior(this);
        }

        protected virtual void OnRelease()
        {
        }
    }
}