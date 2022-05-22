using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckRemainingTreesButton : MonoBehaviour
{
    [SerializeField] private Button button;
    private void Awake() => button.onClick.AddListener(Setup);

    private void Setup() => CheckRemainingTreesService.SendRequest();
}