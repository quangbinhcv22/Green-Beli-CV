using System;
using System.Collections.Generic;
using GRBEGame.Define;
using TMPro;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GRBEGame.UI.OpenPvpChest
{
    public class PvpChestRewardItem : MonoBehaviour
    {
        [SerializeField] private List<QB.Collection.KeyValuePair<PvpRewardType, GameObject>> rewards;
        [SerializeField] private TMP_Text gFruitText;
        [SerializeField] private TMP_Text numberFragmentText;
        [SerializeField, Space] private Image fragmentIcon;
        [SerializeField] private FragmentArtSet fragmentArtSet;
        [SerializeField] [Space] private Button claimButton;


        public void UpdateView(PvpRewardType type,int numberReward = default, int typeFragment = default)
        {
            rewards.ForEach(reward =>
            {
                reward.value.SetActive(reward.key == type);
            });
            if (typeFragment > (int) default)
                fragmentIcon.sprite = fragmentArtSet.GetFragmentIcon((FragmentType) typeFragment);

            gFruitText.SetText(numberReward.ToString("N0"));
            numberFragmentText.SetText(numberReward.ToString("N0"));
        }

        public void AddOnClickListenerClaimButton(UnityAction callback)
        {
            claimButton.onClick.AddListener(callback);
        }

        public void SetActiveState(PvpRewardActiveState pvpRewardActiveState)
        {
            switch (pvpRewardActiveState)
            {
                case PvpRewardActiveState.Claim:
                    rewards.ForEach(reward => { reward.value.SetActive(false); });
                    claimButton.gameObject.SetActive(true);
                    break;
                case PvpRewardActiveState.Claimed:
                    claimButton.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pvpRewardActiveState), pvpRewardActiveState, null);
            }
        }
    }

    [System.Serializable]
    public enum PvpRewardActiveState
    {
        Claim = 0,
        Claimed = 1,
    }

    [System.Serializable]
    public enum PvpRewardType
    {
        GFruit = 0,
        Fragment = 1,
    }
}
