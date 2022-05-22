using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.Widgets.KeyBoard
{
    [RequireComponent(typeof(Button))]
    public class DeleteLastCharForTextInputButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(DeleteLastChar);
        }

        private void DeleteLastChar()
        {
            if(string.IsNullOrEmpty(input.text)) return;
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
        
        public void SetInput(TMP_InputField newInputField)
        {
            input = newInputField;
        }
    }
}