using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

namespace Service.Client.SelectHero
{
    [CreateAssetMenu(fileName = "SelectHeroClientService", menuName = "ScriptableObjects/Service/Client/SelectHero")]
    public class SelectHeroClientService : ScriptableObject, IClientService<HeroResponse>
    {
        private UnityAction _onResponse;

        public string GetEventName()
        {
            return EventName.PlayerEvent.SelectHero;
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

        public void RemoveListenerEmitEvent(UnityAction callback)
        {
            _onResponse -= callback;
        }

        public void EmitData(HeroResponse data)
        {
            EventManager.EmitEventData(GetEventName(), data);
        }

        public HeroResponse GetEventEmitData()
        {
            return EventManager.GetData<HeroResponse>(GetEventName());
        }
    }
}