using UnityEngine;

namespace GCamera
{
    [CreateAssetMenu(menuName = "Preset/CameraConfig", fileName = nameof(CameraConfig))]
    public class CameraConfig : ScriptableObject
    {
        public Vector3 position;
        public float lenSize;

        public void ApplyTo(UnityEngine.Camera camera)
        {
            if (camera is null) return;

            camera.gameObject.transform.position = position;
            camera.orthographicSize = lenSize;
        }
    }
}