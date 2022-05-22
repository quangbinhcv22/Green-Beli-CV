using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

namespace Service.Client.ViewRewardDateDetail
{
    [CreateAssetMenu(fileName = "ViewRewardDateDetailClientService", menuName = "ScriptableObjects/Service/Client/ViewRewardDateDetail")]
    public class ViewRewardDateDetailClientService : ScriptableObject, IClientService<ViewRewardDateDetailRequest>
    {
        private UnityAction _onResponse;

        public string GetEventName()
        {
            return EventName.PlayerEvent.ViewRewardDateDetail;
        }

        public void Active()
        {
            EventManager.StartListening(GetEventName(), OnCallback);
        }
        
        private void OnCallback()
        {
            _onResponse?.Invoke();
        }

        public void AddListenerResponse(UnityAction callback)
        {
            _onResponse += callback;
        }

        public void EmitData(ViewRewardDateDetailRequest data)
        {
            EventManager.EmitEventData(GetEventName(), data);
        }

        public ViewRewardDateDetailRequest GetEventEmitData()
        {
            return EventManager.GetData<ViewRewardDateDetailRequest>(GetEventName());
        }

        public void RemoveListenerEmitEvent(UnityAction callback)
        {
            _onResponse -= callback;
        }
    }

    [System.Serializable]
    public struct ViewRewardDateDetailRequest
    {
        public string dateKey;
        public int siblingIndex;
    }
}