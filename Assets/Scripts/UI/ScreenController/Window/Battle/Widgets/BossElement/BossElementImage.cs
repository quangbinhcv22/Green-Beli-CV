using GRBEGame.Resources;
using GRBESystem.Entity.Element;
using Network.Service.Implement;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenController.Window.Battle.Widgets.BossElement
{
    public class BossElementImage : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private ElementArtSet artConfig;

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var startGameResponse = StartGameServerService.Response;
            if (startGameResponse.IsError) return;

            image.sprite = artConfig.GetSprite((Element) startGameResponse.data.boss.faction);
        }
    }
}