using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions.Text
{
    public class VersatileText : MonoBehaviour
    {
        [SerializeField] public TMP_Text outputText;
        [SerializeField] private Image icon;

        [SerializeField, Space] private Color normalDamageColor;
        [SerializeField] private Color criticalDamageColor;

        void OnEnable()
        {
            outputText = outputText == null ? GetComponent<TMP_Text>() : outputText;
        }

        public void DisplayText(string content)
        {
            outputText.text = content;
        }

        public void DisplayNumber(float number)
        {
            outputText.text = $"{number:N0}";
        }

        public void DisplayTime(TimeSpan timeSpan)
        {
            outputText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }

        public void DisplayTime(float minutes, float seconds)
        {
            outputText.text = $"{minutes:D2}:{seconds:D2}";
        }

        public async void DisplayBlendNumber(float startNumber, float endNumber, float duration = 1f,
            int numberOfUpdates = 30, string format = "N0", string culture = "es-ES")
        {
            TimeSpan delayBetweenTimeSpan = TimeSpan.FromSeconds(duration / numberOfUpdates);

            float splitRatioNumber = Mathf.Pow(numberOfUpdates, -1) * (endNumber - startNumber);
            float nowNumber = startNumber;

            DisplayNumber(nowNumber);

            while (nowNumber < endNumber)
            {
                await UniTask.Delay(delayBetweenTimeSpan);
                nowNumber += splitRatioNumber;

                if (endNumber - nowNumber < splitRatioNumber)
                {
                    nowNumber = endNumber;
                }

                DisplayNumber(nowNumber);
            }
        }

        public void DisplayBlendNumber(float endNumber, float duration = 1f, int numberOfUpdates = 30)
        {
            float startNumber;

            try
            {
                startNumber = int.Parse(outputText.text);
            }
            catch (FormatException)
            {
                startNumber = 0;
            }

            DisplayBlendNumber(startNumber, endNumber, duration, numberOfUpdates);
        }

        public void FloatingText(Vector3 distance, float duration, float numberDisplay, bool haveIcon)
        {
            gameObject.SetActive(true);
            var positionStart = transform.localPosition;
            DisplayNumber(numberDisplay);

            var target = positionStart + distance;
            transform.DOLocalMove(target, duration).OnComplete(() =>
            {
                gameObject.SetActive(false);
                transform.localPosition = positionStart;
            });

            icon.gameObject.SetActive(haveIcon);
            TintDamageText(haveIcon);
        }

        private void TintDamageText(bool isCritical)
        {
            outputText.color = isCritical ? criticalDamageColor : normalDamageColor;
        }
    }
}