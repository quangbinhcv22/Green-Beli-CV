using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Match.Widgets.Motion
{
    public class CircularMotion : MonoBehaviour
    {
        private enum DirectionMotion
        {
            LeftToRight = -1,
            RightToLeft = 1,
        }

        [SerializeField] private float angle = 0;
        [SerializeField] private float speed = 1;
        [SerializeField] private float radius = 5;
        [SerializeField] private DirectionMotion directionMotion = DirectionMotion.LeftToRight;

        private RectTransform _rectTransform;
        private Vector3 _positionStart;

        private bool _isActiveMotion = true;


        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _positionStart = _rectTransform.position;
        }

        private void OnEnable()
        {
            SetActiveMotion(true);
        }

        private void Update()
        {
            if (_isActiveMotion is false) return;
            
            angle = GetNewAngle();
            _rectTransform.position = GetNewPosition();
        }

        public void StopMotion()
        {
            SetActiveMotion(default);
            _rectTransform.position = _positionStart;
        }

        public void StartMotion()
        {
            SetActiveMotion(true);
        }

        private void SetActiveMotion(bool isActive)
        {
            _isActiveMotion = isActive;
        }

        private float GetNewAngle()
        {
            return angle + speed * (int)directionMotion * Time.deltaTime;
        }

        private Vector3 GetNewPosition()
        {
            return _positionStart + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        }
    }
}