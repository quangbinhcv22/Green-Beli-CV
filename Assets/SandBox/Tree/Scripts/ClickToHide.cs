using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToHide : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private float delay;

    private void Awake() => button.GetComponent<Button>().onClick.AddListener(Setup);

    private void Setup() => Invoke(nameof(HideObject), delay);

    private void HideObject() => button.SetActive(false);
}
