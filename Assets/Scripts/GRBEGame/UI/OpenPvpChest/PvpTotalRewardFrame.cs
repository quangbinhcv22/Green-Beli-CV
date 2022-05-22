using System.Collections.Generic;
using GRBEGame.Define;
using GRBEGame.UI.Screen.Inventory;
using GRBEGame.UI.Screen.Inventory.Fragment;
using GRBEGame.UI.Screen.Inventory.Material;
using UnityEngine;

namespace GRBEGame.UI.OpenPvpChest
{
    public class PvpTotalRewardFrame : MonoBehaviour
    {
        [SerializeField] private FragmentItemCoreView gFruitFragment;
        [SerializeField] private List<MaterialCoreView> materials;
        
        
        public void UpdateView(int gFruit, List<MaterialInfo> materialInfos)
        {
            gFruitFragment.UpdateView(new FragmentItemInfo((int) FragmentType.GFruit, gFruit));
            gFruitFragment.gameObject.SetActive(gFruit > (int) default);
            
            for (int i = default; i < materials.Count; i++)
            {
                materials[i].gameObject.SetActive(i < materialInfos.Count);
                if(i < materialInfos.Count)
                    materials[i].UpdateView(materialInfos[i]);
            }
        }
    }
}
