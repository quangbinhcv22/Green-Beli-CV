using GEvent;
using GRBESystem.UI.Screens.Popups.Setting.Avatar;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBEGame.UI.Widget.PlayerInfo
{
    public class PlayerSelfAvatar : MonoBehaviour
    {
        [SerializeField] private HeroVisual heroCoreView;

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.UI.Select<AvatarData>(), UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StartListening(EventName.UI.Select<AvatarData>(), UpdateView);
        }

        private void UpdateView()
        {
            var avatarDataBoxing = EventManager.GetData(EventName.UI.Select<AvatarData>());
            if (avatarDataBoxing is null) return;

            var avatarData = (AvatarData)avatarDataBoxing;

            if (avatarData.selfAvatarID is (long)default) heroCoreView.UpdateDefault();
            else heroCoreView.UpdateView(avatarData.selfAvatarID);
        }
    }
}