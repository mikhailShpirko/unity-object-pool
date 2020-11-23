/*
Source: https://github.com/mikhailShpirko/unity-object-pool
Component: ObjectPool/PoolSetupItem.cs

Object for pool configuration in Editor.

MIT License
Copyright (c) 2020 Mikhail Shpirko
*/

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

