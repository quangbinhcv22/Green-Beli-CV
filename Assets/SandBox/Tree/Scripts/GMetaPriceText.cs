using Network.Messages.LoadGame;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class GMetaPriceText : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private long gMeta;
    [SerializeField, Space] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";

    private const long OneBillion = 1000000000;
    private long _singlePrice;

    private void Awake()
    {
        Setup();
        _singlePrice = gMeta / OneBillion;
        text.SetText(string.Format(textFormat, _singlePrice));
        slider.onValueChanged.AddListener(_ => OnValueChanged());
    }

    private void Setup()
    {
        var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
        if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError) return;

        var percentValueCurrent = loadGameResponse.data.tree.price;

        gMeta = percentValueCurrent[(int) LoadGameConfigResponse.TreeConfig.TreeCoinType.GMeta].quantity;
    }

    private void OnValueChanged()
    {
        var price = _singlePrice * (int) slider.value;
        text.SetText(string.Format(textFormat, price));
    }
}