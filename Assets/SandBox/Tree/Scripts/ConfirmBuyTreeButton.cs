using GEvent;
using Network.Web3;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ConfirmBuyTreeButton : MonoBehaviour
{
    private void Awake() => GetComponent<Button>().onClick.AddListener(ConfirmBuyTree);

    private void ConfirmBuyTree()
    {
        var nullableTreeQuantity = EventManager.GetData(EventName.Select.TreeQuantity);
        if (nullableTreeQuantity is int treeQuantity) Web3Controller.Instance.RealTree.OpenTree(treeQuantity);
    }
}