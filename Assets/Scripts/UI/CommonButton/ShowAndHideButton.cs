using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CommonButton
{
    [RequireComponent(typeof(Button))]
    public class ShowAndHideButton : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(ShowOrHide);
        }

        private void OnEnable()
        {
            target.gameObject.SetActive(false);
        }

        private void ShowOrHide()
        {
            if (target is null) return;

            var isShowing = target.gameObject.activeSelf;
            var isShowNow = !isShowing;

            target.gameObject.SetActive(isShowNow);
        }
    }
}