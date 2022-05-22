using System.Collections.Generic;
using GEvent;
using Manager.Resource.Assets;
using TigerForge;
using UnityEngine;

namespace Extensions.Initialization
{
    public class DecorPool : MonoBehaviour
    {
        [SerializeField] private List<DecoGameObject> decoGameObjects;

        private List<DecoGameObject> _decoGameObjects;
        

        private void Awake()
        {
            _decoGameObjects = new List<DecoGameObject>();
            EventManager.StartListening(EventName.UI.Select<DecoGameObjectId>(), OnDecorSelect);
        }

        private void OnDecorSelect()
        {
            var data = EventManager.GetData(EventName.UI.Select<DecoGameObjectId>());
            if (data is null || (DecoGameObjectId) data is DecoGameObjectId.None) return;

            var id = (DecoGameObjectId) data;
            if (id is DecoGameObjectId.Hide)
            {
                HideAllDecor();
                return;
            }

            CreateDecorIfNull(id);
            foreach (var decor in _decoGameObjects)
                decor.gameObject.SetActive(decor.decoGameObjectId == id);
        }

        private void CreateDecorIfNull(DecoGameObjectId id)
        {
            if(GetDecorObject(id) != null) return;

            var decor = decoGameObjects.Find(item => item.decoGameObjectId == id);
            if(decor is null) return;
            
            _decoGameObjects.Add(Instantiate(decor, transform));
        }

        private void HideAllDecor()
        {
            _decoGameObjects.ForEach(item => item.gameObject.SetActive(default));
        }

        private DecoGameObject GetDecorObject(DecoGameObjectId id)
        {
            return _decoGameObjects.Find(item => item.decoGameObjectId == id);
        }
    }
}
