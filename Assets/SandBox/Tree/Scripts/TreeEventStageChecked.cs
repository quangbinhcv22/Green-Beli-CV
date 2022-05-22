using GEvent;
using GNetwork;
using TigerForge;
using UIFlow;
using UnityEngine;

public class TreeEventStageChecked : MonoBehaviour
{
    [SerializeField] private UIRequest buyTreeScreenRequest;
    [SerializeField] private UIRequest timeOutScreenRequest;
    [SerializeField] private UIRequest soldOutScreenRequest;
    [SerializeField] private UIRequest notInWhiteListScreenRequest;


    private void Awake() => EventManager.StartListening(EventName.Server.BuyTreeStage, CheckWhitelisted);


    private void CheckWhitelisted()
    {
        if (CheckWhiteListBuyTreeService.Response is null) return;

        var nullableBuyTreeStage = EventManager.GetData(EventName.Server.BuyTreeStage);
        if (nullableBuyTreeStage is BuyTreeStage buyTreeStage)
        {
            switch (buyTreeStage)
            {
                case BuyTreeStage.WhiteListEnd:
                    buyTreeScreenRequest.SendRequest();
                    break;
                case BuyTreeStage.SoldOut:
                    soldOutScreenRequest.SendRequest();
                    break;
                case BuyTreeStage.TimeOut:
                    timeOutScreenRequest.SendRequest();
                    break;
                case BuyTreeStage.WhiteListStart:
                    if (CheckWhiteListBuyTreeService.Response.canOpen) buyTreeScreenRequest.SendRequest();
                    else notInWhiteListScreenRequest.SendRequest();
                    break;
            }
        }
    }
}