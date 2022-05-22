using System;
using System.Collections;
using UnityEngine;

namespace UI.Widget.Toast
{
    public class ToastPanel : MonoBehaviour
    {
        [SerializeField, Space] private ToastPanelConfig toastPanelConfig;
        [SerializeField] private ToastContentFrame toastContentFrame;

        private void OnEnable()
        {
            StartCoroutine(FadeAfterSeconds());
        }
        
        private void OnDisable()
        {
            StopCoroutine(FadeAfterSeconds());
        }
        
        private IEnumerator FadeAfterSeconds()
        {
            toastContentFrame.MoveFade(ToastFade.FadeIn, toastPanelConfig.moveDuration);
            yield return new WaitForSeconds(toastPanelConfig.delayHide);
            
            toastContentFrame.MoveFade(ToastFade.FadeOut, toastPanelConfig.moveDuration);
            yield return new WaitForSeconds(toastPanelConfig.moveDuration);
            
            SetActive(false);
        }
        
        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}