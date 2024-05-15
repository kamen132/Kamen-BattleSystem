using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Component;
using BattleSystem.Main.Bullet.Component;
using KamenMessage.RunTime.Singleton;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleSystem.Tools
{
    /// <summary>
    /// fortest
    /// </summary>
    public class ResourceMapContainer : MonoSingleton<ResourceMapContainer>
    {
        public BulletComponent BulletComponent;
        public Transform PoolRoot { get; set; }
        
        public ResourceConfigList ResourceConfigList { get; set; }

        private readonly Dictionary<string, Queue<GameObject>> mObjectPool = new Dictionary<string, Queue<GameObject>>();
        private readonly Dictionary<int, string> mIDMapPath = new Dictionary<int, string>();

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PoolRoot = new GameObject("[PoolRoot]").transform;
            PoolRoot.SetParent(Instance.transform);
            PoolRoot.gameObject.SetActive(value: false);
        }

        public void Release(GameObject @object, bool isMove = true)
        {
            int instanceId = @object.GetInstanceID();
            if (!mIDMapPath.TryGetValue(instanceId, out string pathKey))
            {
                UnityEngine.Object.Destroy(@object);
                return;
            }

            if (isMove)
            {
                @object.transform.SetParent(PoolRoot);
            }

            if (!mObjectPool.TryGetValue(pathKey, out Queue<GameObject> objects))
            {
                objects = new Queue<GameObject>();
                mObjectPool.Add(pathKey, objects);
            }

            objects.Enqueue(@object);
        }

        public GameObject Get(ResourceTag resourceTag, Transform parent, bool isReset = true, int hashCode = 0)
        {
            string pathKey = resourceTag.ToString();
            GameObject o = ResourceConfigList.GetObjectByResourceTag(resourceTag);
            if (mObjectPool.TryGetValue(pathKey, out Queue<GameObject> objects) && objects.Any())
            {
                o = objects.Dequeue();
                if (o == null)
                {
                    o = UnityEngine.Object.Instantiate(o, parent);
                }
                else
                {
                    Transform oTransform = o.transform;
                    if (oTransform.parent != parent)
                    {
                        oTransform.SetParent(parent);
                    }
                }
            }
            else
            {
                o = UnityEngine.Object.Instantiate(o, parent);
            }
            if (isReset)
            {
                Transform transform = o.transform;
                transform.localScale = Vector3.one;
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
            }
            mIDMapPath.Add(o.GetInstanceID(), pathKey);
            return o;
        }
        
        
        public T Get<T>(GameObject go, Transform parent, bool isReset = true) where T : UnityEngine.Component
        {
            GameObject o;
            string pathKey = typeof(T).FullName;
            if (mObjectPool.TryGetValue(pathKey, out var objects) && objects.Any())
            {
                o = objects.Dequeue();
                if (o == null)
                {
                    o = Instantiate(go, parent);
                }
                else
                {
                    Transform oTransform = o.transform;
                    if (oTransform.parent != parent)
                    {
                        oTransform.SetParent(parent);
                    }
                }
            }
            else
            {
                o = Instantiate(go, parent);
            }

            if (isReset)
            {
                Transform transform = o.transform;
                transform.localScale = Vector3.one;
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
            }

            mIDMapPath.Add(o.GetInstanceID(), pathKey);
            return o.GetOrAddComponent<T>();
        }
    }
}