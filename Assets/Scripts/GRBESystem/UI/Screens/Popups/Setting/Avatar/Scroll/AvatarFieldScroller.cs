using System.Collections.Generic;
using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Popups.Setting.Avatar.Widgets.CellView;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Setting.Avatar.Scroll
{
    public class AvatarFieldScroller : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private AvatarInformationCellView avatarInformationCellViewPrefab;

        private readonly List<AvatarInformationCellView> _contents = new List<AvatarInformationCellView>();
        private bool _isHasContent = false;


        private void OnEnable()
        {
            SetData();
            EventManager.StartListening(EventName.UI.Select<AvatarData>(), SetData);
            EventManager.StartListening(EventName.Server.GetListHero, ListHeroUpdated);
        }

        private void ListHeroUpdated()
        {
            _isHasContent = false;
            SetData();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<AvatarData>(), SetData);
            EventManager.StopListening(EventName.Server.GetListHero, ListHeroUpdated);
        }

        private void SetData()
        {
            var data = EventManager.GetData(EventName.UI.Select<AvatarData>());
            if(data is null) return;
            
            ReLoadData((AvatarData) data);
        }

        private void ReLoadData(AvatarData avatarData)
        {
            if(_isHasContent) return;
            if (_contents.Any()) _contents.ForEach(content => content.gameObject.SetActive(false));
            
            var avatarIDs = avatarData.avatarIDs;
            _isHasContent = avatarIDs.Any();
            
            for (int i = 0; i < avatarIDs.Count; i++)
            {
                if (i >= _contents.Count) _contents.Add(Instantiate(avatarInformationCellViewPrefab, scrollRect.content));

                var id = avatarIDs[i];
                _contents[i].gameObject.SetActive(true);

                _contents[i].heroID = id;
                _contents[i].UpdateView();
                _contents[i].SetSelected(avatarData.selfAvatarID == id);
                _contents[i].AddListener(() => SendEventSelectAvatar(id));
            }
        }

        private void SendEventSelectAvatar(long selectedID)
        {
            var data = EventManager.GetData(EventName.UI.Select<AvatarData>());
            if (NetworkService.Instance.IsNotLogged() || data is null) return;

            var avatarData = (AvatarData) data;
            avatarData.selectAvatarID = selectedID;
            
            EventManager.EmitEventData(EventName.UI.Select<AvatarData>(), avatarData);
        }
    }
}