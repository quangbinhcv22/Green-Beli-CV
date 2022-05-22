using QB.Collection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PasswordInput
{
    public class InputFieldContentTypeToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private InputFieldContentTypeSetter inputFieldContentTypeSetter;
        [SerializeField] private DefaultableDictionary<bool, TMP_InputField.ContentType> contentTypeConfig;

        private void Start()
        {
            toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn)
        {
            inputFieldContentTypeSetter.SetContentType(contentTypeConfig[isOn]);
        }
    }
}