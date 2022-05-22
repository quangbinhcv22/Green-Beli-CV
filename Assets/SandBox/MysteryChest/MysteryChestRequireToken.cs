using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace SandBox.MysteryChest
{
    public class MysteryChestRequireToken : MonoBehaviour
    {
        [SerializeField] private TMP_Text simpleEnergyText;
        [SerializeField] private TMP_Text simpleGFruitText;
        [SerializeField] [Space] private TMP_Text multiEnergyText;
        [SerializeField] private TMP_Text multiGFruitText;
        [SerializeField] [Space] private string stringDefault;
        [SerializeField] private string stringFormat = "{0}";


        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.LoadGameConfig, UpdateView);
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.services.loadGameConfig.Response.IsError)
                SetTextDefault();

            var data = NetworkService.Instance.services.loadGameConfig.Response.data.mysteryChest;
            
            simpleEnergyText.SetText(string.Format(stringFormat, data.price.energy));
            simpleGFruitText.SetText(string.Format(stringFormat, data.price.gfruit));
            multiEnergyText.SetText(string.Format(stringFormat, data.price.energy * 10));
            multiGFruitText.SetText(string.Format(stringFormat, data.price.gfruit * 10));
        }

        private void SetTextDefault()
        {
            simpleEnergyText.SetText(stringDefault);
            simpleGFruitText.SetText(stringDefault);
            multiEnergyText.SetText(stringDefault);
            multiGFruitText.SetText(stringDefault);
        }
    }
}
