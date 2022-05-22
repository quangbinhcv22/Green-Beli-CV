using System.Collections;
using DG.Tweening;
using GEvent;
using Manager.Resource;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.BlendCamera
{
    public class BlendCameraHandler : MonoBehaviour
    {
        [SerializeField, Space] private Vector3 positionTarget = new Vector3(0, 2, -10);
        [SerializeField] private float durationTweenPosition = 0.85f;
        [SerializeField] private Ease easeTweenPosition = Ease.Linear;

        [SerializeField, Space] private float sizeCameraTarget = 3f;
        [SerializeField] private float durationTweenSize = 0.85f;
        [SerializeField] private Ease easeTweenSize = Ease.Linear;

        [SerializeField, Space] private float durationShake = 5f;
        [SerializeField] private Vector3 strengthShake = new Vector3(0.4f, 0.75f);
        [SerializeField] private int vibratoShake = 10;
        [SerializeField] private float randomnessShake = 2;

        private void Awake()
        {
            EventManager.StartListening(EventName.ScreenEvent.Battle.END_BATTLE, OneEndBattle);
        }

        private void OneEndBattle()
        {
            // var blendCamera = EventData.CameraEvent.GetMainCamera();
            //
            // BlendCamera(blendCamera);
            // StartCoroutine(Shake(blendCamera));
        }

        private void BlendCamera(Camera blendCamera)
        {
            blendCamera.transform.DOMove(this.positionTarget, durationTweenPosition).SetEase(easeTweenPosition);
            blendCamera.DOOrthoSize(sizeCameraTarget, durationTweenSize).SetEase(easeTweenSize);
        }

        private IEnumerator Shake(Camera blendCamera)
        {
            yield return new WaitForSeconds(durationTweenSize);
            blendCamera.DOShakeRotation(durationShake, strengthShake, vibratoShake, randomnessShake);
        }
    }
}