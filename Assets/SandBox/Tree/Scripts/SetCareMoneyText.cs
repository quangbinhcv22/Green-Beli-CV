using Network.Service;
using TMPro;
using UnityEngine;

public class SetCareMoneyText : MonoBehaviour
{
    [SerializeField] private CareTreeType careTreeType;
    [SerializeField, Space] private TMP_Text beliText;
    [SerializeField, Space] private TMP_Text gFruitText;
    [SerializeField] private string textFormat = "{0}";
    
    private void OnEnable() => SetUp(careTreeType);
    
    private void SetUp(CareTreeType careTreeType)
    {
        var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
        if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError) return;

        var tree = loadGameResponse.data.tree.general;
        
        switch (careTreeType)
        {
            case CareTreeType.Fertilizing:
                beliText.SetText(string.Format(textFormat,tree.beLiCostPerFertilizing));
                gFruitText.SetText(string.Format(textFormat,tree.gFruitCostPerFertilizing));
                break;
            case CareTreeType.Water:
                beliText.SetText(string.Format(textFormat,tree.beLiCostPerWatering));
                gFruitText.SetText(string.Format(textFormat,tree.gFruitCostPerWatering));
                break;
        }
    }
}
