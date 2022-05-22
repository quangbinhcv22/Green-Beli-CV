using TMPro;
using UnityEngine;


namespace GRBESystem.Widgets.TextInputField
{
    public class TextInputStringLengthLimiter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private int stringLengthLimit;
        
        private void Awake()
        {
            input.onValueChanged.AddListener(LimitCharacter);
        }

        private void LimitCharacter(string value)
        {
            if (value.Length > stringLengthLimit)
            {
                input.text = input.text.Substring(0, stringLengthLimit);
            }
        }
    }
}