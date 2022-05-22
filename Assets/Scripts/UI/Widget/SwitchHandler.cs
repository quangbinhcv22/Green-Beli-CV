using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Widget
{
    public class SwitchHandler : MonoBehaviour
    {
        private const string PATH_BACKGROUND_IMAGE = "Demo/SwitchBackground";
        private const string PATH_HANDLER_IMAGE = "Demo/SwitchHandler";

        public bool interactable = true;
        [SerializeField] private bool isOn = true;

        public Image background;
        public RectTransform handler;

        [Header("Animation")] [SerializeField] private float durationTweenTurnOnOff = 0.25f;
        [SerializeField] Ease easeWhenOn = Ease.OutQuad;
        [SerializeField] Ease easeWhenOff = Ease.OutQuad;

        [Header("Color")] [SerializeField] private Color colorBackgroundOn = new Color(0.278f, 0.694f, 0.882f);
        [SerializeField] private Color colorBackgroundOff = new Color(0.792f, 0.875f, 0.953f);

        [SerializeField, Space] private Sprite handlerOnSprite;
        [SerializeField] private Sprite handlerOffSprite;

        [Space] public UnityEvent<bool> onValueChanged;

        private float _handlerPositionXWhenOn;
        private float _handlerPositionXWhenOff;

        public bool IsOn
        {
            get => this.isOn;

            set
            {
                this.isOn = value;

                ChangeBackgroundColor();
                ChangeHandlerSprite();

                if (this.isOn)
                {
                    this.handler.DOLocalMoveX(this._handlerPositionXWhenOn, this.durationTweenTurnOnOff)
                        .SetEase(this.easeWhenOn);
                }
                else
                {
                    this.handler.DOLocalMoveX(this._handlerPositionXWhenOff, this.durationTweenTurnOnOff)
                        .SetEase(this.easeWhenOff);
                }

                this.onValueChanged?.Invoke(this.isOn);
            }
        }

        private void ChangeBackgroundColor()
        {
            this.background.color = this.IsOn ? this.colorBackgroundOn : this.colorBackgroundOff;
        }

        private void ChangeHandlerSprite()
        {
            this.handler.GetComponent<Image>().sprite = this.IsOn ? this.handlerOnSprite : this.handlerOffSprite;
        }

        private void OnValidate()
        {
            if (this.background != null)
            {
                ChangeBackgroundColor();
            }
        }

        void Awake()
        {
            this._handlerPositionXWhenOn = Math.Abs(this.handler.localPosition.x);
            this._handlerPositionXWhenOff = -this._handlerPositionXWhenOn;

            this.background.GetComponent<Button>().onClick.AddListener(ChangeValued);
            this.handler.GetComponent<Image>().raycastTarget = false;
        }

        private void ChangeValued()
        {
            this.IsOn = !this.IsOn;
        }
    }
}