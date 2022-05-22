using QB.Collection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PasswordInput
{
    [DefaultExecutionOrder(100)]
    public class InputFieldContentTypeImageReflector : MonoBehaviour
    {
        [SerializeField] private InputFieldContentTypeSetter inputFieldContentTypeSetter;
        [SerializeField] private Image reflectImage;
        [SerializeField] private DefaultableDictionary<TMP_InputField.ContentType, Sprite> reflectSprites;

        private void Awake()
        {
            inputFieldContentTypeSetter.onContentTypeChanged += OnContentTypeChanged;
        }

        private void OnContentTypeChanged(TMP_InputField.ContentType contentType)
        {
            reflectImage.sprite = reflectSprites[contentType];
        }
    }
}