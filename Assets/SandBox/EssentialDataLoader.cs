using System.Collections.Generic;
using GEvent;
using GNetwork;
using GScreen;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

public class EssentialDataLoader : MonoBehaviour
{
    private void Awake()
    {
        EventManager.StartListening(EventName.Server.LoginByMetamask, LoadEssentialData);
    }

    private void LoadEssentialData()
    {
        if (NetworkService.Instance.services.login.MessageResponse.IsError) return;

        ResetLoadingReport();

        LoadGameConfig();
        LoadHeroList();
        LoadRewardHistoryAll();
        LoadTreeList();
    }

    private void ResetLoadingReport()
    {
        var loadingReporter = LoadingPanelReporter.Instance;

        loadingReporter.AddTask(EventName.Server.LoadGameConfig);
        loadingReporter.AddTask(EventName.Server.GetListHero);
        loadingReporter.AddTask(EventName.Server.GetRewardHistoryAll);
        loadingReporter.AddTask(EventName.Server.GetListTree);

        loadingReporter.OnLoaded += () => EventManager.EmitEvent(EventName.Client.LoadEssentialData);
    }

    private void LoadGameConfig()
    {
        EventManager.StartListening(EventName.Server.LoadGameConfig, OnLoadGameConfig);
        NetworkService.Instance.services.loadGameConfig.SendRequest();
    }

    private void OnLoadGameConfig()
    {
        EventManager.StopListening(EventName.Server.LoadGameConfig, OnLoadGameConfig);
        LoadingPanelReporter.Instance.RemoveTask(EventName.Server.LoadGameConfig);
    }

    private void LoadHeroList()
    {
        EventManager.StartListening(EventName.Server.GetListHero, OnLoadHeroList);
        NetworkService.Instance.services.getHeroList.SendRequest(GameMode.PVP);
        
        EventManager.EmitEventData(EventName.Client.Battle.BattleMode, BattleMode.PvP);
    }
    
    private void OnLoadHeroList()
    {
        EventManager.StopListening(EventName.Server.GetListHero, OnLoadHeroList);

        EventManager.EmitEventData(EventName.PlayerEvent.BattleHeroes,
            NetworkService.Instance.services.getHeroList.HeroResponses.GetStandardBattleHeroId());
        EventManager.EmitEventData(EventName.PlayerEvent.BreedingHeroes,
            GameManager.Instance.breedingConfig.NonHeroIds);

        LoadingPanelReporter.Instance.RemoveTask(EventName.Server.GetListHero);
    }
    
    private void LoadRewardHistoryAll()
    {
        EventManager.StartListening(EventName.Server.GetRewardHistoryAll, OnLoadRewardHistoryAll);
        NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
    }

    private void OnLoadRewardHistoryAll()
    {
        EventManager.StopListening(EventName.Server.GetRewardHistoryAll, OnLoadRewardHistoryAll);
        LoadingPanelReporter.Instance.RemoveTask(EventName.Server.GetRewardHistoryAll);
    }

    private void LoadTreeList()
    {
        EventManager.StartListening(EventName.Server.GetListTree,OnLoadTreeList);
        GetListTreeServerService.SendRequest();
    }

    private void OnLoadTreeList()
    {
        EventManager.StopListening(EventName.Server.GetListTree,OnLoadTreeList);
        LoadingPanelReporter.Instance.RemoveTask(EventName.Server.GetListTree);
    }
}