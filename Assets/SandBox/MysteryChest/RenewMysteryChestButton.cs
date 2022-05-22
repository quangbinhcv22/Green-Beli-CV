using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class RenewMysteryChestButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(IsRenewMysteryChest);
    }

    private void IsRenewMysteryChest()
    {
        EventManager.EmitEvent(EventName.Server.RenewMysteryChest);
    }
}
