using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/Config", fileName = nameof(NetworkAPIConfig))]
    public class NetworkAPIConfig : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> services;
        [SerializeField] [Space] private ScriptableObject serviceToAdd;

        private List<ScriptableObject> Services => services;

        public Dictionary<Type, ScriptableObject> ToByTypeDictionary()
        {
            return services.ToDictionary(service => service.GetType(), service => service);
        }

        public Dictionary<string, IServerAPI> ToByIdDictionary()
        {
            var dictionary = new Dictionary<string, IServerAPI>();

            foreach (var service in services)
            {
                if (service is IServerAPI serverAPI)
                {
                    dictionary.Add(serverAPI.APIName, serverAPI);
                }
            }

            return dictionary;
        }

        public void AddNewService()
        {
            if (serviceToAdd is null)
            {
                Debug.LogError($"Nothing API to add");
            }
            else if (serviceToAdd is IServerAPI)
            {
                if (services.Contains(serviceToAdd))
                {
                    Debug.Log($"API <color=pink>{serviceToAdd.name}</color> already exist");
                }
                else
                {
                    Debug.Log($"Add API <color=pink>{serviceToAdd.name}</color>");

                    services.Add(serviceToAdd);

                    services = services.Union(services).ToList();
                    services = services.OrderBy(service => service.name).ToList();

                    serviceToAdd = null;
                }
            }
            else
            {
                Debug.LogError(
                    $"Add API failed: <color=yellow>{serviceToAdd.name}</color> is not <color=yellow>{nameof(IServerAPI)}</color>");
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(NetworkAPIConfig))]
    public class NetworkAPIConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (NetworkAPIConfig) target;

            GUILayout.Space(10);
            if (GUILayout.Button("Add new service", GUILayout.Height(40)))
            {
                script.AddNewService();
            }
        }
    }
#endif
}