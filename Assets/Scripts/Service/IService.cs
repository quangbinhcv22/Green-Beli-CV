using Network.Messages;
using UnityEngine.Events;

namespace Service
{
    public interface IService
    {
        public string GetEventName();
        public void Active();
        public void AddListenerResponse(UnityAction callback);
    }
    
    
    public interface IClientService<E> : IService
    {
        public void EmitData(E data);
        public E GetEventEmitData();
        public void RemoveListenerEmitEvent(UnityAction callback);
    }


    public interface IServerService<R> : IService
    {
        public ResponseData<R> GetDeserializeResponseData(string message);
        public R GetResponse();
    }

    public interface IServerService<R, C> : IServerService<R>
    {
        public C GetClientData();
        public void SetClientDataOnResponse();
    }
}