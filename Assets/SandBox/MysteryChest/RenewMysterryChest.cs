using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GEvent;
using TigerForge;
using UnityEngine;

public class RenewMysterryChest : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject lid;
    [SerializeField] private AudioClip buy;
    
    private Vector3 _lidPosition;
    private Vector3 _bodyPosition;

    private void Awake()
    {
        _lidPosition = lid.transform.localPosition;
        _bodyPosition = body.transform.localPosition;
    }

    private void OnEnable()
    {
        SoundEffect();
        EventManager.StartListening(EventName.Server.SoundEffect,SoundEffect);
        EventManager.StartListening(EventName.Server.RenewMysteryChest,UpdateView);
        UpdateView();
    }
    
    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.SoundEffect,SoundEffect);
        EventManager.StopListening(EventName.Server.RenewMysteryChest,UpdateView);
    }

    private void SoundEffect()
    {
        EventManager.EmitEventData(EventName.WidgetEvent.PLAY_SOUND_ACTION,buy);
        //buy.Play();
    }
    private void UpdateView()
    {
        lid.transform.DOLocalMove(_lidPosition, 0.01f);
        lid.transform.DOScale(1, 0.01f);
        body.transform.DOLocalMove(_bodyPosition, 0.01f);
        body.transform.DOScale(1, 0.01f);
    }
}
