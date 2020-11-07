using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObjectPool;

namespace Demo.UserInput
{
    public class PoolObjectTypeDropdown : MonoBehaviour
    {
        [SerializeField]
        private Dropdown _dropdown;

        public PoolObjectType GetValue()
        {
            return (PoolObjectType)_dropdown.value;
        }
    }
}
