using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget
{
    public class ProcessBar : MonoBehaviour
    {
        private const int VALUE_ZERO = 0;

        [SerializeField] private Image currentValueBar;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private string textFormat = "{0:N0}/{1:N0}";

        [SerializeField, Space] public float durationTweenOnValueChange = 0.5f;
        [SerializeField] private Ease easeTweenOnValueChange = Ease.OutQuad;

        [SerializeField, Space] private bool isHideOnValueZero = true;

        public void UpdateView(float currentValue, float maxValue)
        {
            if (isHideOnValueZero) gameObject.SetActive(true);

            var nonNegativeCurrentValue = Mathf.Clamp(currentValue, VALUE_ZERO, maxValue);
            var percentValueCurrent = nonNegativeCurrentValue / maxValue;

            TweenFillAmountValueBar(percentValueCurrent);
            SetProcessText(nonNegativeCurrentValue, maxValue);
        }

        public void SetProcessText(string content)
        {
            valueText.text = $"{content}";
        }

        public void ResetView()
        {
            currentValueBar.fillAmount = default;
        }

        private void TweenFillAmountValueBar(float targetValue)
        {
            currentValueBar.DOFillAmount(targetValue, durationTweenOnValueChange).SetEase(easeTweenOnValueChange);
        }

        private void SetProcessText(float currentValue, float maxValue)
        {
            if(valueText) valueText.SetText(string.Format(textFormat, currentValue, maxValue));
        }

        private IEnumerator SetActiveFalseAfter(float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            gameObject.SetActive(false);
        }
    }
}