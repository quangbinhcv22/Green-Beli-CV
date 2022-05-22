using TMPro;
using UnityEngine;

namespace GRBESystem.Widgets.TextInputField
{
    [RequireComponent(typeof(TMP_InputField))]
    public class OnDisableTextInputResetter : MonoBehaviour
    {
        [SerializeField] private string stringDefault;

        private TMP_InputField _input;

        private void Awake()
        {
            _input = GetComponent<TMP_InputField>();
        }

        private void OnDisable()
        {
            _input.text = stringDefault;
        }
    }
}