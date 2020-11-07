using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UserInput
{
    public class IntInputField : MonoBehaviour
    {
        [SerializeField]
        private InputField _input;

        public int GetValue()
        {
            if(int.TryParse(_input.text, out var value))
            {
                return value;
            }
            return 0;
        }
    }
}
