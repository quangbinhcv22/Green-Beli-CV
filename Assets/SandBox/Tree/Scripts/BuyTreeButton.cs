using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;


[DefaultExecutionOrder(150)]
public class BuyTreeButton : MonoBehaviour
{
    private const int SOLD_OUT = 0;

    [SerializeField] private Button button;
    [SerializeField] private UIRequest buyTreePopup;
    [SerializeField] private UIRequest soldOutTreePopup;
    [SerializeField] private UIRequest insufficientTreePopup;

    private int trees;

    private void Awake() => button.onClick.AddListener(Setup);

    private void Setup()
    {
        switch (IsAvaiableTrees())
        {
            case TreesStage.BuyTree:
                buyTreePopup.SendRequest();
                break;
            case TreesStage.Insufficient:
                insufficientTreePopup.SendRequest();
                break;
            case TreesStage.SoldOut:
            default:
                soldOutTreePopup.SendRequest();
                break;
        }
    }

    private TreesStage IsAvaiableTrees()
    {
        var nullableTrees = EventManager.GetData(EventName.Server.RemainingTree);
        var nullableTreeQuantity = EventManager.GetData(EventName.Select.TreeQuantity);

        trees = (int) nullableTrees;
        EventManager.EmitEventData(EventName.Select.RemainingTree, trees);

        if (nullableTreeQuantity is int treeQuantity)
        {
            if (trees >= treeQuantity) return TreesStage.BuyTree;
            if (trees <= SOLD_OUT) return TreesStage.SoldOut;
        }

        return TreesStage.Insufficient;
    }
}

[Serializable]
public enum TreesStage
{
    BuyTree,
    SoldOut,
    Insufficient
}