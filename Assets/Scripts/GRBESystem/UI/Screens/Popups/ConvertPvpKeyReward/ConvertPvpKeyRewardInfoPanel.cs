using GEvent;
using GRBEGame.Define;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.ConvertPvpKeyReward
{
    public class ConvertPvpKeyRewardInfoPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text infoText;
        [SerializeField] private string formatString1;
        [SerializeField] [Space] private TMP_Text gFruitText;
        [SerializeField] private string formatString2;
        [SerializeField] [Space] private TMP_Text fragmentCountText;
        [SerializeField] private string formatString3; 
        [SerializeField] private FragmentArtSet fragmentArtSet;
        [SerializeField] private Image fragmentImage;
        [SerializeField] [Space] private GameObject gFruitObject;
        [SerializeField] private GameObject fragmentObject;
        [SerializeField] private Transform simpleTransform;

        private Transform _gFruitTransform;
        private Transform _fragmentTransform;


        private void Awake()
        {
            _gFruitTransform = gFruitObject.transform;
            _fragmentTransform = fragmentObject.transform;
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.ConvertPvpKeyToReward, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.ConvertPvpKeyToReward, UpdateView);
        }

        private void UpdateView()
        {
            if(NetworkService.Instance.IsNotLogged() || ConvertPvpKeyToRewardServerService.Response.IsError) return;

            var totalGFruit = ConvertPvpKeyToRewardServerService.TotalGFruit();
            var totalFragment = ConvertPvpKeyToRewardServerService.TotalFragment();

            if (totalGFruit > (int) default && totalFragment > (int) default)
                MultiReward();
            else
                SimpleReward(totalGFruit > (int) default ? gFruitObject : fragmentObject);

            infoText.SetText(string.Format(formatString1, ConvertPvpKeyToRewardServerService.Data.Count));
            gFruitText.SetText(string.Format(formatString2, totalGFruit));
            fragmentCountText.SetText(string.Format(formatString3, totalFragment));
            fragmentImage.sprite = fragmentArtSet.GetFragmentIcon(FragmentType.Land);
        }

        private void MultiReward()
        {
            gFruitObject.SetActive(true);
            gFruitObject.transform.position = _gFruitTransform.position;
            
            fragmentObject.SetActive(true);
            fragmentObject.transform.position = _fragmentTransform.position;
        }

        private void SimpleReward(Object rewardObject)
        {
            gFruitObject.SetActive(gFruitObject == rewardObject);
            gFruitObject.transform.position =
                gFruitObject.activeSelf ? simpleTransform.position : _gFruitTransform.position;
            
            fragmentObject.SetActive(fragmentObject == rewardObject);
            fragmentObject.transform.position =
                fragmentObject.activeSelf ? simpleTransform.position : _fragmentTransform.position;
        }
    }
}
