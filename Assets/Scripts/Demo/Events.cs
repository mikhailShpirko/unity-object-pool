using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ObjectPool;

namespace Demo 
{
    [Serializable]
    public class VoidEvent : UnityEvent
    {
    }

    [Serializable]
    public class PoolCountersEvent : UnityEvent<Dictionary<PoolObjectType, int>>
    {
    }
}
