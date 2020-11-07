using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObjectPool;

namespace Demo 
{
    public class PoolCountPresenter : MonoBehaviour
    {
        [SerializeField]
        private Text _uiText;

        public void SetCounters(Dictionary<PoolObjectType, int> counters)
        {
            _uiText.text = "Objects in pool:";
            foreach(var counter in counters )
            {
                _uiText.text += "\n" + $"{counter.Key}: {counter.Value}";
            }            
        }
    }
}