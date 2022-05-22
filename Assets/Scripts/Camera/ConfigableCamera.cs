using TigerForge;
using UnityEngine;
using UnityEngine.Assertions;

namespace GCamera
{
    [RequireComponent(typeof(Camera))]
    public class ConfigableCamera : MonoBehaviour
    {
        [SerializeField] private CameraConfig defaultConfig;

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            ConfigDefault();
        }

        private void OnEnable()
        {
            EventManager.StartListening("ConfigCamera", OnConfig);
        }

        private void OnConfig()
        {
            var configData = EventManager.GetData("ConfigCamera");

            if (configData is CameraConfig config) config.ApplyTo(_camera);
            else ConfigDefault();
        }

        private void ConfigDefault()
        {
            defaultConfig.ApplyTo(_camera);
        }

        private void OnValidate()
        {
            Assert.IsNotNull(defaultConfig);
        }
    }
}