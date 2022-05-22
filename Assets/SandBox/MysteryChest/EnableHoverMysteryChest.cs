using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;

public class EnableHoverMysteryChest : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.Server.PressMysteryChest, DisableHover);
        EventManager.StartListening(EventName.Server.MysteryChestDecor,EnableHover);
        EnableHover();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.PressMysteryChest, DisableHover);
        EventManager.StopListening(EventName.Server.MysteryChestDecor,EnableHover);

    }

    private void EnableHover()
    {
        chest.GetComponent<HoverMysteryChest>().enabled = true;
    }
    private void DisableHover()
    {
        chest.GetComponent<HoverMysteryChest>().enabled = false;
    }
}
