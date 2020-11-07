using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.UserInput;
using ObjectPool;
using System.Linq;

namespace Demo
{
    public class DemoEventHandler : MonoBehaviour
    {
        [SerializeField]
        private IntInputField _numberOfObjectsInput;

        [SerializeField]
        private PoolObjectTypeDropdown _poolObjectTypeInput;

        [SerializeField]
        private Pool _pool;

        public VoidEvent OnEventHandled;

        private IEnumerator GetFromPoolCoroutine()
        {
            for(var i = 0; i < _numberOfObjectsInput.GetValue(); i++)
            {
                _pool.GetFromPool<PoolableObject>(_poolObjectTypeInput.GetValue(), new Vector3(0, 5f, 0));
                yield return new WaitForSeconds(0.2f);
            }
            OnEventHandled?.Invoke();
        }
        
        public void HandleGetFromPool()
        {
            //to have better visual effect need a delay between retrieving items
            StartCoroutine(GetFromPoolCoroutine());
        }

        public void HandleReturnToPool()
        {
            var count = 0;
            var poolableObjects = FindObjectsOfType<PoolableObject>().Where(p => p.Type == _poolObjectTypeInput.GetValue());
            while(count < _numberOfObjectsInput.GetValue() && count < poolableObjects.Count())
            {
                poolableObjects.ElementAt(count).Disable();
                count++;
            }
            OnEventHandled?.Invoke();
        }
    }
}

