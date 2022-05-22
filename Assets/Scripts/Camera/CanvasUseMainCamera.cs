using UnityEngine;
using UnityEngine.Assertions;

namespace GCamera
{
    public class CanvasUseMainCamera : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }

        private void OnValidate()
        {
            Assert.IsNotNull(canvas);
        }
    }
}