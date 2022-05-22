using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Extensions.UXGameObject
{
    public class FloatingGameObject : MonoBehaviour
    {
        [SerializeField, Space] private bool isFloating = true;
        [SerializeField] private float distanceFloating = 0.2f;
        [SerializeField] private float durationFloating = 1.25f;
        [SerializeField] private Ease easeFloating = Ease.InOutSine;

        private bool _isAllowHovering;

        private Vector3 _positionStart;
        //private float positionYStart;

        private void OnEnable()
        {
            if (this.isFloating == false) return;
            StartFloating();
        }

        private void OnDisable()
        {
            if (this.isFloating == false) return;
            StopFloating();
        }

        private async void StartFloating()
        {
            this._isAllowHovering = true;
            this._positionStart = this.transform.position;
            //this._positionStart = this.GetComponent<RectTransform>().position;

            while (this._isAllowHovering)
            {
                this.transform.DOMoveY(this._positionStart.y - this.distanceFloating, this.durationFloating)
                    .SetEase(this.easeFloating);

                await UniTask.Delay(TimeSpan.FromSeconds(this.durationFloating));

                this.transform.DOMoveY(this._positionStart.y + this.distanceFloating, this.durationFloating)
                    .SetEase(this.easeFloating);

                await UniTask.Delay(TimeSpan.FromSeconds(this.durationFloating));

                // this.GetComponent<RectTransform>().DOMoveY(this._positionStart.y - this.distanceFloating, this.durationFloating)
                //     .SetEase(this.easeFloating);
                //
                // await UniTask.Delay(TimeSpan.FromSeconds(this.durationFloating));
                //
                // this.GetComponent<RectTransform>().DOMoveY(this._positionStart.y + this.distanceFloating, this.durationFloating)
                //     .SetEase(this.easeFloating);
                //
                // await UniTask.Delay(TimeSpan.FromSeconds(this.durationFloating));
            }
        }

        private void StopFloating()
        {
            this._isAllowHovering = false;
            this.transform.position = this._positionStart;
            //this.GetComponent<RectTransform>().position = this._positionStart;
        }
    }
}