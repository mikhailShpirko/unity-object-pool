using System;

namespace ObjectPool
{
    [Serializable]
    public class PoolSetupItem
    {
        public int NumberOfInstancesInPool;
        public PoolableObject Prefab;
        public PoolObjectType Type => Prefab.Type;
    }
}

