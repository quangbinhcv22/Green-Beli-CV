using System;
using GEvent;
using Extensions.Initialization.Request;
using Network.Messages.GetHeroList;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.ModelsPresenter
{
    public class BattleHeroModelsPresenter : MonoBehaviour
    {
        [SerializeField] private Vector3 ownerHeroPosition;
        [SerializeField] private Vector3 opinionHeroPosition;
        
        private bool _isFirstLoaded;
        private bool _haveRequest;
        
        
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.StartGame, PresentModels);
        }

        private void OnEnable()
        {
            if (_isFirstLoaded is false || _haveRequest)
                PresentModels();
        }

        void PresentModels()
        {
            if(StartGameServerService.Response.IsError) return;
            if (isActiveAndEnabled is false)
            {
                _haveRequest = true;
                return;
            }
                
            _isFirstLoaded = true;
            
            EventManager.EmitEventData(EventName.Model.ShowHeroModel,
                data: new ShowHeroModelRequest()
                {
                    heroId = StartGameServerService.Data.GetSelfInfo().selectedHeros
                        .GetMainHero().GetID(),
                    position = ownerHeroPosition,
                    isFlip = false,
                    scale = Vector3.one,
                });

            EventManager.EmitEventData(EventName.Model.ShowHeroModel,
                data: new ShowHeroModelRequest()
                {
                    heroId = StartGameServerService.Data.GetOpponentInfo()
                        .selectedHeros
                        .GetMainHero().GetID(),
                    position = opinionHeroPosition,
                    isFlip = true,
                    scale = Vector3.one,
                });
        }
    }
}