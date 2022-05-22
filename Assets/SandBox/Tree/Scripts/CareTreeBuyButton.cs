using System;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CareTreeBuyButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private UpgradeEnergyMode currency;
    [SerializeField] private TMP_Text text;

    private void Awake() => button.onClick.AddListener(UpdateView);

    private void UpdateView() =>
        EventManager.EmitEventData(EventName.Select.CareTreeTotalPrice,new CareTree(){currency = currency,total = text.text});
}
[Serializable]
public class CareTree
{
    public UpgradeEnergyMode currency;
    public string total;
}
