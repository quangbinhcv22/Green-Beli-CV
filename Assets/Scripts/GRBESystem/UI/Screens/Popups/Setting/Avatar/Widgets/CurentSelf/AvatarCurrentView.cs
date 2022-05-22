using GEvent;
using Network.Service;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Setting.Avatar.Widgets.CurentSelf
{
    public class AvatarCurrentView : MonoBehaviour
    {
        [SerializeField] private HeroVisual avatar;

        private bool _isUpdateViewFirst;
        
        
        private void OnEnable()
        {
            _isUpdateViewFirst = true;
            
            UpdateView();
            EventManager.StartListening(EventName.UI.Select<AvatarData>(), UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<AvatarData>(), UpdateView);
            
            if (NetworkService.Instance == null || NetworkService.Instance.IsNotLogged() ||
                EventManager.GetData(EventName.UI.Select<AvatarData>()) == null) return;
            
            var avatarData = (AvatarData)EventManager.GetData(EventName.UI.Select<AvatarData>());
            avatarData.selectAvatarID = avatarData.selfAvatarID;

            EventManager.EmitEventData(EventName.UI.Select<AvatarData>(), avatarData);
        }

        private void UpdateView()
        {
            var data = EventManager.GetData(EventName.UI.Select<AvatarData>());
            if(NetworkService.Instance.IsNotLogged() || data is null) return;

            var avatarData = (AvatarData) data;
            var avatarID = _isUpdateViewFirst ? avatarData.selfAvatarID : avatarData.selectAvatarID;

            _isUpdateViewFirst = false;
            avatar.UpdateView(avatarID);
        }
    }
}
