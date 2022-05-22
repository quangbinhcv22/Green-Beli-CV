using System;
using System.Collections.Generic;
using GNetwork;
using GRBEGame.Resources;
using QB.ViewData;
using UnityEngine;

[Serializable]
public class Tree
{
    [DataView("id")] public int id;
    [DataView("state")] public int state;

    [SerializeField] private int faction;
    [DataView("element")] public Element Element => (Element) faction;

    [DataView("health")] public int healthPoint;
    [DataView("healthMax")] public const int HealthMax = 7;
    [DataView("intervalTimeUpdateHealthPoint")] public string intervalTimeUpdateHealthPoint;

    [DataView("fruits")] public List<Fruits> fruits;
    [DataView("plantHeroIds")] public long plantHeroIds;
    [DataView("numberLimitFertilizing")] public int numberLimitFertilizing;
    [DataView("numberFruitPerFertilizing")] public int numberFruitPerFertilizing;

    [DataView("fruitRate")] public int fruitRate;

    public int totalFruits;
    
    [SerializeField] private string status;

    [DataView("status")]
    public TreeStatus Status
    {
        get
        {
            return status switch
            {
                "Active" => TreeStatus.Active,
                _ => TreeStatus.Inactive,
            };
        }

        set => status = value.ToString();
    }
}