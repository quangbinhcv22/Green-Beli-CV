using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;

namespace UIFlow
{
    public static class ActiveUIs
    {
        private static readonly List<UIId> UIs = new List<UIId>();

        public static void Add(UIId uiId)
        {
            if (IsActive(uiId) is false) UIs.Add(uiId);
            EventManager.EmitEvent(EventName.UI.ActiveUIsChanged());
        }

        public static void Remove(UIId uiId)
        {
            if (IsActive(uiId)) UIs.Remove(uiId);
            EventManager.EmitEvent(EventName.UI.ActiveUIsChanged());
        }

        public static bool IsActive(UIId uiId) => UIs.Contains(uiId);
    }

    public class UIFlowController : MonoBehaviour
    {
        [SerializeField] private UIFlowConfig flowConfig;

        private void Awake()
        {
            CreateFrames();
            flowConfig.RegisterEvents();

            void CreateFrames() => flowConfig.framePrefabs.ForEach(frame => Instantiate(frame));
        }

        private void Start()
        {
            flowConfig.requests.onAwake.SendRequest();
        }

        private void OnValidate()
        {
            if (flowConfig is null)
                Debug.Log(
                    $"<color=yellow>{nameof(flowConfig)}</color> is <color=yellow>null</color> (<color=cyan>{nameof(UIFlowController)}</color> of <color=cyan>{name}</color>)",
                    gameObject);
        }
    }
}




// public class MainHallAutoOpenUI : MonoBehaviour
// {
//     [SerializeField] private UIRequest convertPvpKeyRequest;
//     private bool _isConvertPvpKey;
//
//     private void Awake()
//     {
//         EventManager.StartListening(EventName.Server.CONVERT_PVP_KEY_TO_REWARD, () => { _isConvertPvpKey = true; });
//     }
//     
//     private void OpenConvertPvpKeyPopup()
//     {
//         if (_isConvertPvpKey is false) return;
//
//         _isConvertPvpKey = default;
//         convertPvpKeyRequest.SendRequest();
//     }
// }