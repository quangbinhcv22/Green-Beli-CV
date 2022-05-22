using System;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public class ServiceActivator : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> services;

        private void Awake()
        {
            foreach (var serverService in services)
            {
                ((IService)serverService).Active();
            }
        }

        private void OnValidate()
        {
            for (int i = 0; i < services.Count; i++)
            {
                try
                {
                    var service = (IService)services[i];
                }
                catch (InvalidCastException)
                {
                    services.Remove(services[i]);
                }
            }
        }
    }
}
