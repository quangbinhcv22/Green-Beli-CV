using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using UIFlow.InGame;
using UnityEngine;
using UnityEngine.Events;

namespace UIFlow
{
    public class UIObject : MonoBehaviour
    {
        #region Identification

        public UIId Id { get; private set; }
        public UIType Type { get; private set; }
        public int Layer { get; private set; }

        public object Data { get; private set; }

        public void SetConfig(UIConfig config, int layer)
        {
            Id = config.id;
            Type = config.type;
            Layer = layer;
        }

        private UIAnimator _animator;
        private List<UIId> _useScreens;

        private void UseScreens(UIAction action, bool haveAnimation)
        {
            if (_useScreens is null) return;

            var uiRequest = new UIRequest {action = action, haveAnimation = haveAnimation};

            foreach (var screen in _useScreens)
            {
                uiRequest.id = screen;
                EventManager.EmitEventData(EventName.UI.RequestScreen(), uiRequest);
            }
        }

        #endregion


        #region Events

        // public UnityEvent OnOpening;
        // public UnityAction<object> OnOpened;
        public UnityEvent onOpening;
        // public UnityEvent onClosing;
        // public UnityAction OnClosed;

        #endregion


        #region Methods

        private void Awake()
        {
            _animator = GetComponent<UIAnimator>();
            
            var uiUser = GetComponent<UIUser>();
            if (uiUser) _useScreens = uiUser.targets;
        }


        public void Open(UIRequest request) => Open(request.data, request.haveAnimation);

        public void Open(object data = null, bool haveAnimation = true)
        {
            onOpening?.Invoke();

            Data = data;

            ActiveUIs.Add(Id);
            gameObject.SetActive(true);


            UseScreens(UIAction.Open, haveAnimation);


            if (_animator)
            {
                if (haveAnimation)
                {
                    _animator.Open();
                }
                else
                {
                    _animator.OnlyOpen();
                }
            }
        }

        public void Close(UIRequest request) => Close(request.haveAnimation);

        public void Close(bool haveAnimation = true)
        {
            // onClosing?.Invoke();


            UseScreens(UIAction.Close, haveAnimation);


            if (haveAnimation)
            {
                if (gameObject.activeSelf is false) return;
                StartCoroutine(CloseRoutine());
            }
            else
            {
                FinalClose();
            }

            IEnumerator CloseRoutine()
            {
                if (_animator)
                {
                    _animator.Close();
                    yield return new WaitForSeconds(_animator.Config.delayClose);
                }

                FinalClose();
            }
            
            void FinalClose()
            {
                gameObject.SetActive(false);
                ActiveUIs.Remove(Id);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            UseScreens(UIAction.Close, haveAnimation: false);
        }

        #endregion
    }
}