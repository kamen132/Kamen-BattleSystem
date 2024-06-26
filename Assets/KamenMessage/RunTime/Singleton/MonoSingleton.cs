﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace KamenMessage.RunTime.Singleton
{
    public class MonoSingleton<T> : MonoSingletonBase where T : MonoBehaviour
    {
        private static readonly string mMonoSingletonRoot = "KamenApp/Kamen";
        
        private readonly List<IDisposable> _entrustDisposables = new List<IDisposable>();

        protected void EntrustDisposable(IDisposable disposable)
        {
            _entrustDisposables.Add(disposable);
        }
        
        public void EntrustDisposablesClear()
        {
            foreach (IDisposable entrustDisposable in _entrustDisposables)
            {
                entrustDisposable.Dispose();
            }
            _entrustDisposables.Clear();
        }
        
        private static T mInstance = null;
        public static T Instance
        {
            get
            {
                if (null == mInstance)
                {
                    var go = GameObject.Find(mMonoSingletonRoot);
                    if (go)
                    {
                        mInstance = go.GetComponent<T>();
                        if (mInstance == null)
                        {
                            mInstance = go.AddComponent<T>();
                            var singleton = mInstance as MonoSingletonInterface;
                            if (singleton != null)
                            {
                                /// OnInitialize晚于AwakeEx执行;
                                singleton.MonoSingletonInterfaceOnInitialize();
                            }
                        }
                    }
                }
                return mInstance;
            }
        }

        public static bool ApplicationIsPlaying => mInstance != null;

        protected MonoSingleton()
        {
            if (null == mInstance)
            {
                Debug.Log($"[MonoSingleton]{typeof(T)} singleton instance created.");
            }
        }

        void Awake()
        {
            AwakeEx();
        }

        void Start()
        {
            StartEx();
        }

        void OnEnable()
        {
            OnEnableEx();
        }

        void FixedUpdate()
        {
            FixedUpdateEx(Time.deltaTime);
        }

        void Update()
        {
            UpdateEx(Time.deltaTime);
        }

        void LateUpdate()
        {
            LateUpdateEx(Time.deltaTime);
        }

        void OnDisable()
        {
            OnDisableEx();
        }

        /// MonoSingleton只有在ApplicationQuit时才会Destroy;
        void OnApplicationQuit()
        {
            var singleton = mInstance as MonoSingletonInterface;
            if (singleton != null)
            {
                singleton.MonoSingletonInterfaceOnUnInitialize();
            }

            EntrustDisposablesClear();
            OnDestroyEx();
            mInstance = null;
        }

        protected virtual void AwakeEx() { }
        protected virtual void StartEx() { }
        protected virtual void OnEnableEx() { }
        protected virtual void FixedUpdateEx(float interval) { }
        protected virtual void UpdateEx(float interval) { }
        protected virtual void LateUpdateEx(float interval) { }
        protected virtual void OnDisableEx() { }
        protected virtual void OnDestroyEx() { }
    }
}