using TMPro;
using UnityEngine;
using GRBEGame.UI.DataView;
using UI.ArtVisual;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    public class LeaderboardPvpRank : MonoBehaviour, IMemberView<LeaderboardPvpData>
    {
        [SerializeField] private LeaderboardPvpCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string defaultString;
        
        [SerializeField] [Space] private Image highRankImage;
        [SerializeField] private LuckyGreenbieRankArtSet artSet;
        [SerializeField] private Sprite defaultSprite;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            UpdateTextDefault();
            UpdateImageDefault();
        }

        private void UpdateTextDefault()
        {
            text.SetText(defaultString);
        }
        
        private void UpdateImageDefault()
        {
            highRankImage.gameObject.SetActive(false);
            highRankImage.sprite = defaultSprite;
        }

        public void UpdateView(LeaderboardPvpData data)
        {
            if (data.rank <= artSet.GetMaxCount())
            {
                UpdateTextDefault();
                highRankImage.gameObject.SetActive(true);
                highRankImage.sprite = artSet.GetRankIcon(data.rank);
                return;
            }

            UpdateImageDefault();
            text.SetText(string.Format(textFormat, data.rank));
        }
    }
}
