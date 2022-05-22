using GEvent;
using Manager.Resource.Assets;
using TigerForge;
using UnityEngine;

namespace SandBox.Decor
{
    public class DecorUser : MonoBehaviour
    {
        [SerializeField] private DecoGameObjectId decorID;


        private void OnEnable()
        {
            EventManager.EmitEventData(EventName.UI.Select<DecoGameObjectId>(), decorID);
        }

        private void OnDisable()
        {
            EventManager.EmitEventData(EventName.UI.Select<DecoGameObjectId>(), DecoGameObjectId.Hide);
        }
    }
}
