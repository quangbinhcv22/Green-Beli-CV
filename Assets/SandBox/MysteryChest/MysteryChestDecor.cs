using DG.Tweening;
using GEvent;
using GTween;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MysteryChestDecor : MonoBehaviour
{
    [SerializeField] private Image light1;
    [SerializeField] private Image light2;
    [SerializeField] private Image line;
    [SerializeField] private Image title;
    [SerializeField] private TMP_Text text;
    [SerializeField] private FadeConfig fadeConfig;
    [SerializeField] private AudioClip spawn;

    private readonly TweenSession _tweenSession = new TweenSession();
   
    private void OnEnable()
    {
        HideDecor();
        EventManager.StartListening(EventName.Server.MysteryChestDecor,UpdateViewDecor);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.MysteryChestDecor,UpdateViewDecor);
    }

    private void UpdateViewDecor()
    {
        EventManager.EmitEventData(EventName.WidgetEvent.PLAY_SOUND_ACTION,spawn);
        //spawn.Play();
        _tweenSession.Cancel();
        
        _tweenSession.Start(
            light1.DOFade( fadeConfig.normal.visible, fadeConfig.normal.duration).SetEase(fadeConfig.normal.ease),
            light2.DOFade( fadeConfig.normal.visible, fadeConfig.normal.duration).SetEase(fadeConfig.normal.ease),
            line.DOFade( fadeConfig.normal.visible, fadeConfig.normal.duration).SetEase(fadeConfig.normal.ease),
            title.DOFade( fadeConfig.normal.visible, fadeConfig.normal.duration).SetEase(fadeConfig.normal.ease),
            text.DOFade( fadeConfig.normal.visible, fadeConfig.normal.duration).SetEase(fadeConfig.normal.ease)
        );
    }

    private void HideDecor()
    {
        _tweenSession.Cancel();
        
        _tweenSession.Start(
            light1.DOFade( fadeConfig.invincible.visible, fadeConfig.invincible.duration).SetEase(fadeConfig.invincible.ease),
            light2.DOFade( fadeConfig.invincible.visible, fadeConfig.invincible.duration).SetEase(fadeConfig.invincible.ease),
            line.DOFade( fadeConfig.invincible.visible, fadeConfig.invincible.duration).SetEase(fadeConfig.invincible.ease),
            title.DOFade( fadeConfig.invincible.visible, fadeConfig.invincible.duration).SetEase(fadeConfig.invincible.ease),
            text.DOFade( fadeConfig.invincible.visible, fadeConfig.invincible.duration).SetEase(fadeConfig.invincible.ease)
        );
    }
}
