using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class MysteryChestSound : MonoBehaviour
{
    private AudioSource audio;
    private void OnEnable()
    {
        EventManager.StartListening("SoundEffect",RenewMysteryChest);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening("SoundEffect",RenewMysteryChest);
    }
    
    private void  RenewMysteryChest()
    {
        var soundEffect = EventManager.GetData("SoundEffect");
        
    }
}
