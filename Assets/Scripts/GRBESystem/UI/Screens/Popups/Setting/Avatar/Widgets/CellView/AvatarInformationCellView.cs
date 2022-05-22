using GEvent;
using Network.Service;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Setting.Avatar.Widgets.CellView
{
    public class AvatarInformationCellView : MonoBehaviour
    {
        public long heroID;

        [SerializeField] private Button button;
        [SerializeField] private Image selectedBackground;
        [SerializeField] private Sprite selectedSprite;
        [SerializeField] private Sprite unselectedSprite;
        [SerializeField] private HeroVisual heroAvatar;
        
        
        private void OnEnable()
        {
            SetData();
            EventManager.StartListening(EventName.UI.Select<AvatarData>(), SetData);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<AvatarData>(), SetData);
        }

        private void SetData()
        {
            var data = EventManager.GetData(EventName.UI.Select<AvatarData>());
            if(NetworkService.Instance.IsNotLogged() || data is null) return;

            var avatarData = (AvatarData) data;
            var isSelected = (avatarData.IsCanChangeAvatar && avatarData.selectAvatarID == heroID) ||
                             (avatarData.IsCanChangeAvatar == false && avatarData.selfAvatarID == heroID);
            SetSelected(isSelected);
        }

        public void SetSelected(bool isSelect)
        {
            selectedBackground.sprite = isSelect ? selectedSprite : unselectedSprite;
            selectedBackground.gameObject.SetActive(selectedBackground.sprite != null);
        }

        public void UpdateView()
        {
            heroAvatar.UpdateView(heroID);
        }

        public void AddListener(UnityAction callbackOnClick)
        {
            button.onClick.AddListener(callbackOnClick);
        }
    }
}