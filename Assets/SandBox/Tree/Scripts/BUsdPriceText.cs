using Network.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Network.Messages.LoadGame;
using TigerForge;

[DefaultExecutionOrder(100)]
public class BUsdPriceText : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private long bUsd;
    [SerializeField, Space] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";

    private void Awake()
    {
        Setup();
        text.SetText(string.Format(textFormat, bUsd));
        slider.onValueChanged.AddListener(_ => OnValueChanged());
    }

    private void Setup()
    {
        var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
        if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError) return;

        var percentValueCurrent = loadGameResponse.data.tree.price;

        bUsd = percentValueCurrent[(int) LoadGameConfigResponse.TreeConfig.TreeCoinType.BUsd].quantity;
    }

    private void OnValueChanged()
    {
        var price = bUsd * (int) slider.value;
        text.SetText(string.Format(textFormat, price));
    }
}