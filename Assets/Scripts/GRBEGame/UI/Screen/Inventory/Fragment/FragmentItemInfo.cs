using Network.Messages.Battle;
using Network.Service;
using Network.Service.Implement;

namespace GRBEGame.UI.Screen.Inventory
{
    [System.Serializable]
    public class FragmentItemInfo
    {
        public int type;
        public int count;
        public int numberOfRequestsToCombine;

        public FragmentItemInfo(int type, int count)
        {
            this.type = type;
            this.count = count;

            SetNumberOfRequestsToCombine();
        }

        public FragmentItemInfo(FragmentResponse fragmentResponse)
        {
            type = fragmentResponse.type;
            count = fragmentResponse.number;

            SetNumberOfRequestsToCombine();
        }

        public FragmentItemInfo(EndGameResponse.PlayerInfo.RewardFragment fragment) : this(fragment.type, fragment.number)
        {
        }

        private void SetNumberOfRequestsToCombine()
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (loadGameResponse.IsError) return;
            
            numberOfRequestsToCombine = loadGameResponse.data.inventory.GetFragmentInventory(type).amountOfFragmentToCombineBox;
        }
    }
}