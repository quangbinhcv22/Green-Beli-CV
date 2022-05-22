using GRBEGame.UI.Screen.Inventory;
using GRBEGame.UI.Screen.Inventory.Fragment;
using Network.Service.Implement;
using UnityEngine;

namespace SandBox.MysteryChest
{
    public class MysteryItemViewer : MonoBehaviour
    {
        [SerializeField] private FragmentItemCoreView missCoreView;
        [SerializeField] private FragmentItemCoreView beLiCoreView;
        [SerializeField] private FragmentItemCoreView gFruitCoreView;
        [SerializeField] private FragmentItemCoreView fragmentCoreView;

        public void UpdateView(OpenMysteryChestResponse response)
        {
            missCoreView.UpdateDefault();
            beLiCoreView.UpdateDefault();
            gFruitCoreView.UpdateDefault();
            fragmentCoreView.UpdateDefault();
            
            switch (response.type)
            {
                case RewardMysteryType.GFRUIT:
                    gFruitCoreView.UpdateView(new FragmentItemInfo(default, response.number));
                    break;
                case RewardMysteryType.BELI:
                    beLiCoreView.UpdateView(new FragmentItemInfo(default, response.number));
                    break;
                case RewardMysteryType.LAND_FRAGMENT:
                    fragmentCoreView.UpdateView(new FragmentItemInfo((int) response.type, response.number));
                    break;
                default:
                    missCoreView.UpdateView(new FragmentItemInfo(default, default));
                    break;
            }
        }
    }
}
