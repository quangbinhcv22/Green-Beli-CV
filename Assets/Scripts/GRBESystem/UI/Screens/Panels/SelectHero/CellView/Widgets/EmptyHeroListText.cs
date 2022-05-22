using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

public class EmptyHeroListText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string emptyHeroListString;
    
    private SelectHeroMode _selectHeroMode;
    
    
    private void OnEnable()
    {
        OnSelectHeroModeChanged();
        
        EventManager.StartListening(EventName.Server.GetListHero, OnSelectHeroModeChanged);
        EventManager.StartListening(EventName.Server.RestoreHeroLevel, OnSelectHeroModeChanged);
        EventManager.StartListening(EventName.Server.BreedingHero, OnSelectHeroModeChanged);
        EventManager.StartListening(EventName.Server.FusionHero, OnSelectHeroModeChanged);
        
        EventManager.StartListening(EventName.PlayerEvent.SelectHeroMode, OnSelectHeroModeChanged);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.GetListHero, OnSelectHeroModeChanged);
        EventManager.StopListening(EventName.Server.RestoreHeroLevel, OnSelectHeroModeChanged);
        EventManager.StopListening(EventName.Server.BreedingHero, OnSelectHeroModeChanged);
        EventManager.StopListening(EventName.Server.FusionHero, OnSelectHeroModeChanged);
        
        EventManager.StopListening(EventName.PlayerEvent.SelectHeroMode, OnSelectHeroModeChanged);
    }

    private void OnSelectHeroModeChanged()
    {
        var data = EventManager.GetData(EventName.PlayerEvent.SelectHeroMode);
        if(NetworkService.Instance.IsNotLogged() || data is null) return;

        _selectHeroMode = (SelectHeroMode) data;
        UpdateView();
    }

    private void UpdateView()
    {
        if (NetworkService.Instance is null) return;

        IHeroSelectSlotInteractableCondition interactableCondition = _selectHeroMode switch
        {
            SelectHeroMode.Breeding => new InteractableBasedBreedingCondition(),
            SelectHeroMode.Fusion => new InteractableBasedFusionCondition(),
            SelectHeroMode.RestoreLevels => new InteractableBasedRestoreLevelsCondition(),
            _ => new InteractableAlwaysTrueCondition()
        };
        var notFilterHeroes = NetworkService.Instance.services.getHeroList.HeroResponses;
        var heroList = notFilterHeroes.Where(interactableCondition.CanInteractable).ToList();
        
        text.SetText(heroList.Any() ? string.Empty : emptyHeroListString);
        SetActive(heroList.Any() is false);
    }

    private void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
