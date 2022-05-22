using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.PasswordInput
{
    [DefaultExecutionOrder(200)]
    public class InputFieldContentTypeSetter : MonoBehaviour
    {
        public UnityAction<TMP_InputField.ContentType> onContentTypeChanged;

        [SerializeField] private TMP_InputField inputField;

        public void SetContentType(TMP_InputField.ContentType contentType)
        {
            inputField.SetContentType(contentType);
            InvokeOnContentTypeChanged();
        }

        private void Awake()
        {
            InvokeOnContentTypeChanged();
        }

        private void InvokeOnContentTypeChanged()
        {
            onContentTypeChanged?.Invoke(inputField.contentType);
        }
    }

    public static class TMPInputFieldExtension
    {
        public static void SetContentType(this TMP_InputField inputField, TMP_InputField.ContentType contentType)
        {
            inputField.contentType = contentType;
            inputField.ForceLabelUpdate();
        }
    }
}