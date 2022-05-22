using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GCamera
{
    public class CameraDragger : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        private enum Position
        {
            X = 0,
            Y = 1,
        }

        [SerializeField] private CameraDragConfig dragConfig;

        private Camera _camera;

        private Vector2 _startDragPosition;
        private Vector2 _lastDragPosition;
        private TweenerCore<Vector3, Vector3, VectorOptions> _currentTween;


        private Vector3 GetNewCameraPosition
        {
            get
            {
                var oldCameraPosition = _camera.transform.position;
                return new Vector3(NewCameraPosition(Position.X), NewCameraPosition(Position.Y), oldCameraPosition.z);


                Vector2 GetDragDirection() => _lastDragPosition - _startDragPosition;

                float NewCameraPosition(Position position)
                {
                    var oldPosition = _camera.transform.position;
                    var oldPositionValue = position == Position.X ? oldPosition.x : oldPosition.y;

                    var dragDirection = GetDragDirection();
                    var dragDirectionValue = position == Position.X ? dragDirection.x : dragDirection.y;

                    var minValue = position == Position.X ? -dragConfig.boundX : -dragConfig.boundY;
                    var maxValue = position == Position.X ? dragConfig.boundX : dragConfig.boundY;

                    var newCameraUnClampPosition = oldPositionValue - dragDirectionValue * dragConfig.speed;
                    var newCameraPositionX = Mathf.Clamp(newCameraUnClampPosition, minValue, maxValue);

                    return newCameraPositionX;
                }
            }
        }

        private void OnEnable() => _camera = Camera.main;

        private void OnDisable() => KillCurrentTween();

        public void OnPointerDown(PointerEventData eventData)
        {
            _startDragPosition = eventData.position;
        }


        public void OnDrag(PointerEventData eventData)
        {
            _lastDragPosition = eventData.position;

            KillCurrentTween();
            StartNewTween();

            _startDragPosition = eventData.position;
        }

        private void KillCurrentTween()
        {
            if (_currentTween != null) DOTween.Kill(_currentTween.id);
        }

        private void StartNewTween()
        {
            _currentTween = _camera.transform.DOMove(GetNewCameraPosition, dragConfig.duration).SetEase(dragConfig.ease);
            _currentTween.id = Guid.NewGuid();
        }
    }
}