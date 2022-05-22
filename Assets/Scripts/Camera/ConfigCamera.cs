using System;
using TigerForge;
using UnityEngine;

namespace GCamera
{
    [DefaultExecutionOrder(-1)]
    public class ConfigCamera : MonoBehaviour
    {
        [SerializeField] private CameraConfig config;
        [SerializeField] private bool autoConfig = true;

        private void OnEnable()
        {
            if (autoConfig) Config();
        }
        
        private void OnDisable()
        {
            if (autoConfig) ResetConfig();
        }

        public void Config()
        {
            EventManager.EmitEventData("ConfigCamera", config);
        }

        public void ResetConfig()
        {
            EventManager.EmitEventData("ConfigCamera", data: null);
        }
    }
}