using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(200)]

public class InsufficientTreesText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";

    private void OnEnable()
    {
        UpdateView();
    }
    

    private void UpdateView()
    {
        var nullableTrees = EventManager.GetData(EventName.Server.RemainingTree);

        if (nullableTrees is int trees)
        {
            text.SetText(string.Format(textFormat,trees));
        }
    }
}
