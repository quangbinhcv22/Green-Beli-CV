using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Setting.Avatar.Widgets.Confirm
{
    public class ConfirmAvatarButton : MonoBehaviour
    {
        [SerializeField] private Button confirmButton;
        
        private void Awake()
        {
            const bool defaultInteractable = false;
            SetInteractable(defaultInteractable);
            
            confirmButton.onClick.AddListener(ChangeAvatar);
        }

        private void OnEnable()
        {
            SetInteractable(default);
            EventManager.StartListening(EventName.UI.Select<AvatarData>(), InteractableWhenListeningEvent);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<AvatarData>(), InteractableWhenListeningEvent);
        }

        private void InteractableWhenListeningEvent()
        {
            var data = EventManager.GetData(EventName.UI.Select<AvatarData>());
            if(NetworkService.Instance.IsNotLogged() || data is null) return;

            var avatarData = (AvatarData) data;
            SetInteractable(avatarData.IsCanChangeAvatar);
        }

        private void SetInteractable(bool enable)
        {
            if(enable == confirmButton.interactable) return;
            confirmButton.interactable = enable;
        }

        void ChangeAvatar()
        {
            var data = EventManager.GetData(EventName.UI.Select<AvatarData>());
            if(NetworkService.Instance.IsNotLogged() || data is null) return;
            
            var avatarData = (AvatarData) data;
            if(avatarData.IsCanChangeAvatar is false) return;

            avatarData.selfAvatarID = avatarData.selectAvatarID;
            EventManager.EmitEventData(EventName.UI.Select<AvatarData>(), avatarData);
        }
    }
}
