using GEvent;
using Network.Messages.LoadGame;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Energy.Detail.Widgets.InfoPanel
{
    public class EnergyInfoPanel : MonoBehaviour
    {
        [SerializeField, Space] private Transform starOfHeroParent;
        [SerializeField] private Transform recharge4HoursParent;
        [SerializeField] private Transform consumeEnergyPvEParent;

        [SerializeField, Space] private Color defaultTextColor;
        [SerializeField] private Color yellowTextColor;

        [SerializeField, Space] private EnergyExchangeView exchangeViewPrefab;
        
        private bool _isHadContent = false;

        
        private void OnEnable()
        {
            if(_isHadContent) return;
            
            UpdateView();
            EventManager.StartListening(EventName.Server.LoadGameConfig, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadGameConfig, UpdateView);
        }

        private void UpdateView()
        {
            if (NetworkService.Instance == null || NetworkService.Instance.IsNotLogged() ||
                EventManager.GetData(EventName.Server.LoadGameConfig) == null) return;

            var loadGameResponse = (LoadGameConfigResponse) EventManager.GetData(EventName.Server.LoadGameConfig);
            if (loadGameResponse.Equals(null) || loadGameResponse.energy?.star_energy == null) return;

            foreach (var dataEnergyExchange in loadGameResponse.energy.star_energy)
            {
                UpdateExchangeViews(starOfHeroParent, dataEnergyExchange.star.ToString(), defaultTextColor);
                UpdateExchangeViews(recharge4HoursParent, dataEnergyExchange.energy_gen_per_time.ToString(), yellowTextColor);
                UpdateExchangeViews(consumeEnergyPvEParent, dataEnergyExchange.energy_per_pve.ToString(),
                    yellowTextColor);
            }

            _isHadContent = true;
        }

        private void UpdateExchangeViews(Transform parent, string content, Color color = default)
        {
            var exchangeView = Instantiate(exchangeViewPrefab, parent);
            exchangeView.contentText.text = content;
            exchangeView.contentText.color = color;
        }
    }
}