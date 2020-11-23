/*
Source: https://github.com/mikhailShpirko/unity-object-pool
Component: ObjectPool/PoolableObject.cs

The object is to be used by Pool. The object will return itself to pool when disabled.

MIT License
Copyright (c) 2020 Mikhail Shpirko
*/

using System;
using UnityEngine;

namespace ObjectPool
{    
    public class PoolableObject : MonoBehaviour
    {
        //better performance if assigned from Editor
        //as each call to transoform is same as GetComponent<Transform>
        [SerializeField]
        private Transform _transform;

        [SerializeField]    
        private PoolObjectType _type;

        private Action<PoolableObject> _returnToPool;
        public PoolObjectType Type => _type;

        private void OnDisable() 
        {
            _returnToPool?.Invoke(this);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void ConfigureReturnToPool(Action<PoolableObject> returnToPool)
        {
            _returnToPool = returnToPool;
        }

        public void ConfigurePosition(Vector3 position)
        {
            _transform.position = position;
        }

        public T GetComponent<T>() where T: MonoBehaviour
        {
            return gameObject.GetComponent<T>();
        }
    }
}
