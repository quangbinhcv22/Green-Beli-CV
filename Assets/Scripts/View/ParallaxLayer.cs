using UnityEngine;
using UnityEngine.Assertions;

namespace GView
{
    public class ParallaxLayer : MonoBehaviour
    {
        [SerializeField] private float multiplier = 0.0f;
        [SerializeField] private bool horizontalOnly = true;

        private Transform cameraTransform;

        private Vector3 startCameraPosition;
        private Vector3 startPosition;

        private Vector3 CameraDistanceFromStart => cameraTransform.position - startCameraPosition;


        private void Start()
        {
            var mainCamera = Camera.main;
            Assert.IsNotNull(mainCamera);

            cameraTransform = mainCamera.transform;

            startCameraPosition = cameraTransform.position;
            startPosition = transform.position;
        }

        private void LateUpdate()
        {
            var newPosition = startPosition;

            if (horizontalOnly) newPosition.x += CameraDistanceFromStart.x * multiplier;
            else newPosition += CameraDistanceFromStart * multiplier;

            transform.position = newPosition;
        }
    }
}