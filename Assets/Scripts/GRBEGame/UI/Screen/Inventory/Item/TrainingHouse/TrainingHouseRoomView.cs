using Network.Service.Implement;
using TMPro;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class TrainingHouseRoomView : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text starText;
        [SerializeField] private StarRarityArtSet starRarityArtSet;

        
        public void UpdateView(TrainingHouseRoom trainingHouseRoom)
        {
            background.sprite = starRarityArtSet.GetIcon(trainingHouseRoom.rarity);
            starText.SetText(trainingHouseRoom.star.ToString());
        }
    }
}
