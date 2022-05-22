using GEvent;
using Network.Service;
using TigerForge;

namespace Manager.Inventory
{
    public class GFRUITToken : Money
    {
        public GFRUITToken() : base((int)MoneyType.GFruit)
        {
            EventManager.StartListening(EventName.Server.TokenHasChanged, OnTokenHasChangedResponse);
            EventManager.StartListening(EventName.Server.ClaimRewardByDate, OnClaimRewardByDataResponse);
        }

        private void OnTokenHasChangedResponse()
        {
            var response = NetworkService.Instance.services.tokenHasChanged.MessageResponse;
            if (string.IsNullOrEmpty(response.error) == false) return;

            Set(response.data.gfrToken);
        }

        private void OnClaimRewardByDataResponse()
        {
            var response = NetworkService.Instance.services.claimRewardByDate.Response;
            if (string.IsNullOrEmpty(response.error) == false) return;

            Add(response.data);
        }
    }
}