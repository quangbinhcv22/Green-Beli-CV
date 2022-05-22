using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank
{
    public class StringReplacerForInputField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private List<string> blockedString;
        [SerializeField] private string replacedString;

        private string _lastValue;
        
        private void Awake()
        {
            inputField.onValueChanged.AddListener(ReplaceString);
        }

        private void ReplaceString(string value)
        {
            if(string.IsNullOrEmpty(value)) return;
            if (_lastValue == inputField.text) return;
            
            inputField.text = GetReplaceString(value);
            _lastValue = inputField.text;
        }

        private string GetReplaceString(string value)
        {
            blockedString.ForEach(member=>
            {
                value = value.Replace(member, replacedString);
            });
            
            return value;
        }
    }
}