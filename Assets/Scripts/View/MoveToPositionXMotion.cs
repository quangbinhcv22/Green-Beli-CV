using DG.Tweening;
using UnityEngine;

namespace View
{
    public class MoveToPositionXMotion : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer objectImage;
        [SerializeField] private DirectionType directionType;
        [SerializeField] private float duration;
        [SerializeField] private bool isRepeat;

        [SerializeField] [Space] private float leftPoint;
        [SerializeField] private float rightPoint;

        private Transform _starTransform;
        private DirectionType _type;
        private bool _isUpdated;


        private void OnEnable() => StartMotion(directionType);

        private void OnDisable() => StopMotion();

        public void StartMotion(DirectionType type)
        {
            _starTransform = objectImage.transform;
            _isUpdated = false;
            _type = type;
            
            TimeManager.Instance.AddEvent(Motion);
        }

        public void StopMotion()
        {
            _isUpdated = true;
            objectImage.DOKill();
            objectImage.transform.position = _starTransform.position;
            
            if(TimeManager.Instance != null)
                TimeManager.Instance.RemoveEvent(Motion);
        }

        private void Motion(int timer)
        {
            if(_isUpdated) return;
            
            _isUpdated = true;
            objectImage.transform
                .DOMoveX(_type is DirectionType.LeftToRight ? rightPoint : leftPoint, duration)
                .SetEase(Ease.Linear).onComplete += SetUpRepeat;
        }

        private void SetUpRepeat()
        {
            _isUpdated = isRepeat is false;
            objectImage.DOKill();
            SetNewPosition();
        }

        private void SetNewPosition()
        {
            objectImage.transform.position =
                new Vector3(_type is DirectionType.LeftToRight ? leftPoint : rightPoint,
                    objectImage.transform.position.y, objectImage.transform.position.z);
        }
    }

    public enum DirectionType
    {
        LeftToRight = 0,
        RightToLeft = 1,
    }
}
