using System.Collections;
using System.Collections.Generic;
using GEvent;
using Network.Messages.GetHeroList;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectedPlantHero : MonoBehaviour
{
    [SerializeField] private HeroVisual owner;

    private void Awake() => GetComponent<Button>().onClick.AddListener(SelectedHero);
        
    private void SelectedHero() =>
        EventManager.EmitEventData(EventName.Select.PlantHero, owner.Hero.GetID());
}
