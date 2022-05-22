using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreeQuantity : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField, Space] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";

    [SerializeField] private float defaultValue;

    private void Awake()
    {
        UpdateDefault();
        slider.onValueChanged.AddListener(_ => OnValueChanged());
    }

    private void UpdateDefault() => text.SetText(string.Format(textFormat, defaultValue));

    private void OnValueChanged() =>
        text.SetText(string.Format(textFormat, (int) slider.value));
}