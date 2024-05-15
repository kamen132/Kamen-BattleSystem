using System;
using UnityEngine;

namespace BattleSystem.Main.Base.Model
{
    public abstract class BattleViewModel : BattleQuarkModel
    {
        public bool ViewIsReady { get; private set; }
        public Transform Transform { get; private set; }
        private Action OnViewReady { get; set; }

        public void ViewReady(Transform transform)
        {
            Transform = transform;
            ViewIsReady = true;
            OnViewReady?.Invoke();
        }

        public void SetOnViewReadyAction(Action onViewReady)
        {
            OnViewReady = onViewReady;
        }
    }

}