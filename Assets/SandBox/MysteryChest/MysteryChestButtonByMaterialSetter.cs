using GEvent;
using Manager.Inventory;
using Manager.UseFeaturesPermission;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(Button))]
    public class MysteryChestButtonByMaterialSetter : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TypeSetter stage;
        [SerializeField] private NumberChestOpenRequester numberChestRequest;
        
        
        private void Awake() => button ??= GetComponent<Button>();

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.GetMysteryChestInfo, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StartListening(EventName.Server.GetMysteryChestInfo, UpdateView);
        }

        private void UpdateView()
        {
            if (GetMysteryChestInfoServerService.Response.IsError)
                return;

            switch (stage)
            {
                case TypeSetter.Active:
                    button.gameObject.SetActive(CanOpenChest() && PermissionUseFeature.CanUse(FeatureId.MysteryChest) &&
                                                GetMysteryChestInfoServerService.Data.numberRemainLandFragment >
                                                (int) default);
                    break;
                case TypeSetter.Interactable:
                    button.interactable = CanOpenChest() && PermissionUseFeature.CanUse(FeatureId.MysteryChest) &&
                                          GetMysteryChestInfoServerService.Data.numberRemainLandFragment >
                                          (int) default;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }

        private bool CanOpenChest()
        {
            if (NetworkService.Instance.IsNotLogged() ||
                GetPriceOpenChest(TokenType.GFruit) == default) return false;

            return NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit) >= GetPriceOpenChest(TokenType.GFruit);
        }

        private int GetPriceOpenChest(TokenType tokenType)
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            return loadGameResponse.IsError
                ? default
                : numberChestRequest.numberChest * tokenType switch
                {
                    TokenType.GFruit => loadGameResponse.data.mysteryChest.price.gfruit,
                    TokenType.Energy => loadGameResponse.data.mysteryChest.price.energy,
                    _ => loadGameResponse.data.mysteryChest.price.gfruit,
                };
        }
    }

    [System.Serializable]
    public enum TokenType
    {
        Energy = 0,
        GFruit = 1,
    }
    
    [System.Serializable]
    public enum TypeSetter
    {
        Active = 0,
        Interactable = 1,
    }
}
