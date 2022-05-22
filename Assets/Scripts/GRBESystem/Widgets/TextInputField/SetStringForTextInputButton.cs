using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.Widgets.KeyBoard
{
    [RequireComponent(typeof(Button))]
    public class SetStringForTextInputButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private string stringSet;
        

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SetString);
        }

        private void SetString()
        {
            input.text = stringSet;
        }

        public void SetInput(TMP_InputField newInputField)
        {
            input = newInputField;
        }
        
        public void SetStringSet(string newString)
        {
            stringSet = newString;
        }
    }
}