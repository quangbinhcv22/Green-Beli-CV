#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using GRBESystem.UI.Screens.Windows.Leaderboard;
using Network.Messages;
using Network.Messages.BreedingHero;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UIFlow;
using UnityEngine;

public class Test : MonoBehaviour
{
    // public List<OpenPvpChestResponse> a;
    // public string heroListA;
    // public MessageResponse<List<HeroResponse>> heroList;
    // public MessageResponse<BreedingHeroResponse> response;
    // public string responseB;
    // public MessageResponse<FusionSuccessResponse> fusion;
    

    private void Awake()
    {
    }

    private void OnValidate()
    {
        // heroList = JsonUtility.FromJson<MessageResponse<List<HeroResponse>>>(heroListA);
        // response.data.hero = heroList.data.First();

        // fusion.data.mainHero = response.data.hero;
        // responseB = JsonUtility.ToJson(fusion);
        // fusion.data.mainHero 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CheckWhiteListBuyTreeService.SendRequest();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // NetworkService.Instance.services.calculateMysteryChestRate.SendRequest(new MysteryChestRateRequest
            //     {heroIds = new List<long>() {124112411111}});
            
        }
    }

    
}
#endif