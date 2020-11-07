using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace Demo 
{
    public class PoolCounter : MonoBehaviour
    {
        public PoolCountersEvent OnCounted;

        private void Start()
        { 
            CountPoolItems();
        }

        public void CountPoolItems()
        {
            var result = new Dictionary<PoolObjectType, int>();

            var poolableObjects = Resources.FindObjectsOfTypeAll<PoolableObject>();
            foreach(var poolableObject in poolableObjects)
            {
                var type = poolableObject.Type;
                if(!result.ContainsKey(type))
                {
                    result.Add(type, 0);
                }
                
                if(!poolableObject.gameObject.activeSelf)
                {
                    result[type]++;
                }
            }

            OnCounted?.Invoke(result);
        }
    }
}

