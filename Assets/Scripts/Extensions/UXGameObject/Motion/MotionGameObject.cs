using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Extensions.UXGameObject.Motion
{
    [System.Serializable]
    public class MotionConfig
    {
        [Space] public Transform positionTransformStart;
        public Transform positionTransformTarget;

        [Space] public float delay;
        public float duration = 1;
        public Ease ease;
    }

    public class MotionGameObject : MonoBehaviour
    {
        [SerializeField, Space] private GameObject motionObject;

        [SerializeField, Space] private MotionConfig motionConfigShow;
        [SerializeField] private MotionConfig motionConfigHide;

        public void MoveToShow()
        {
            StartCoroutine(StartMotion(this.motionConfigShow));
        }

        public void MoveToHide()
        {
            StartCoroutine(StartMotion(this.motionConfigHide));
        }

        private IEnumerator StartMotion(MotionConfig motionConfig)
        {
            yield return new WaitForSeconds(motionConfig.delay);
            this.motionObject.transform.position = motionConfig.positionTransformStart.position;

            this.motionObject.transform.DOMove(
                motionConfig.positionTransformTarget.position,
                motionConfig.duration).SetEase(motionConfig.ease);
        }
    }
}