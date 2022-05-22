using System.Collections.Generic;
using GEvent;
using GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator;
using Manager.Game;
using Manager.UseFeaturesPermission;
using TigerForge;
using UI.Widget.Reflector;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Fusion.FusionButton
{
    public class FusionButtonInteractiveSetter : MonoBehaviour, ICoinCostUpdateViewer
    {
        [SerializeField] private ButtonInteractReflector buttonReflector;
        [SerializeField] private CoinCostCalculator costCalculator;

        private CoinCost _coinCost;


        private void Awake()
        {
            buttonReflector.SetInteractCondition(CanFusion);
            costCalculator.AddCallbackUpdateView(this);

            EventManager.StartListening(EventName.PlayerEvent.FusionHeroes, () => UpdateView(new CoinCost()));
        }

        private bool CanFusion()
        {
            if (GameManager.Instance is null || PermissionUseFeature.CanUse(FeatureId.Fusion) is false) return false;

            var selectedHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.FusionHeroes);
            var isFullSelection = GameManager.Instance.selectHeroConfig.IsFullSelection(selectedHeroIds,
                GameManager.Instance.selectHeroConfig.StandardFusionHeroCount);

            return isFullSelection && _coinCost.IsEnoughCoinCost();
        }

        public void UpdateView(CoinCost coinCost)
        {
            _coinCost = coinCost;
            buttonReflector.ReflectInteract();
        }
    }
}