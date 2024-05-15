using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.Tools
{
    [CreateAssetMenu(menuName = "ScriptObject/Res/ResourceConfigList")]
    public class ResourceConfigList: ScriptableObject
    {
        public List<ResourceConfig> ResourceList = new List<ResourceConfig>();

        public GameObject GetObjectByResourceTag(ResourceTag tag)
        {
            var config = ResourceList.Find(a => a.ResourceTag == tag);
            return config?.Prefab;
        }
    }
    
    [Serializable]
    public class ResourceConfig
    {
        public ResourceTag ResourceTag;

        public GameObject Prefab;
    }
}