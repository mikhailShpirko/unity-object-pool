/*
Source: https://github.com/mikhailShpirko/unity-object-pool
Component: ObjectPool/Pool.cs

The object fills the pool from settings on startup and returns the object from pool upon request.

MIT License
Copyright (c) 2020 Mikhail Shpirko
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPool
{   
    public class Pool : MonoBehaviour
    {
        [SerializeField]
        private PoolSetupItem[] _poolObjectsSetting;
        
        private Dictionary<PoolObjectType, Queue<PoolableObject>> _objectPool;

        private void Awake()
        {
            FillPool();
        }

        private void FillPool()
        {
            _objectPool = new Dictionary<PoolObjectType, Queue<PoolableObject>>();

            foreach(var poolObjectSetup in _poolObjectsSetting)
            {     
                //safety check        
                if(poolObjectSetup.Prefab == null)
                {
                    continue;
                }

                //create new key in dictionary if not added yet
                if(!_objectPool.ContainsKey(poolObjectSetup.Type))
                {
                    _objectPool.Add(poolObjectSetup.Type, new Queue<PoolableObject>());
                }

                for(var i = 0; i < poolObjectSetup.NumberOfInstancesInPool; i++)
                {
                    var poolableObject = InitializePoolableObject(poolObjectSetup.Prefab);
                    poolableObject.Disable(); //will automatically put the object to pool
                }
            }
        }

        private PoolableObject InitializePoolableObject(PoolableObject prefab)
        {
            var poolableObject = Instantiate(prefab);
            poolableObject.ConfigureReturnToPool(ReturnToPool);
            return poolableObject;
        }

        private void ReturnToPool(PoolableObject poolableObject)
        {
            //safety check
            if(!_objectPool.ContainsKey(poolableObject.Type))
            {
                return;
            }
            poolableObject.ConfigurePosition(Vector3.zero);
            _objectPool[poolableObject.Type].Enqueue(poolableObject);
        }

        /// <summary>
        /// Retrieves object from pool. Object will set as active on scene and positioned on specified location. Pool will expand if there are no free objects
        /// </summary>
        /// <param name="type">Pool Object Type</param>
        /// <param name="position">Position to be set to the object</param>
        /// <typeparam name="T">Component to return</typeparam>
        /// <returns>Component of the object from pool</returns>
        public T GetFromPool<T>(PoolObjectType type, Vector3 position) where T : MonoBehaviour
        {
            //safety check
            if(!_objectPool.ContainsKey(type))
            {
                return null;
            }

            PoolableObject poolableObject;
            if(_objectPool[type].Count == 0)
            {
                var poolObjectSetep = _poolObjectsSetting.FirstOrDefault(p=> p.Type == type);
                //safety check
                if(poolObjectSetep?.Prefab == null)
                {
                    return null;
                }
                poolableObject = InitializePoolableObject(poolObjectSetep.Prefab);
            }
            else
            {
                poolableObject = _objectPool[type].Dequeue();
            }
            poolableObject.Enable();
            poolableObject.ConfigurePosition(position);
            return poolableObject.GetComponent<T>();
        }
    }
}

