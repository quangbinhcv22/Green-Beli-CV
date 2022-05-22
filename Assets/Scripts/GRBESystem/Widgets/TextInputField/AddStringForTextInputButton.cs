using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.Widgets.KeyBoard
{
    [RequireComponent(typeof(Button))]
    public class AddStringForTextInputButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private string stringAdd;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(AddString);
        }

        private void AddString()
        {
            input.text += stringAdd;
            input.onValueChanged?.Invoke(stringAdd);
        }

        public void SetInput(TMP_InputField newInputField)
        {
            input = newInputField;
        }
        
        public void SetStringAdd(string newAddString)
        {
            stringAdd = newAddString;
        }
    }
}