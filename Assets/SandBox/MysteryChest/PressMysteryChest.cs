using System.Collections;
using DG.Tweening;
using GEvent;
using GTween;
using TigerForge;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressMysteryChest : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Button chestButton;
    [SerializeField] private Transform lid;
    [SerializeField] private Transform chest;
    [SerializeField] private Image chestOpened;
    [SerializeField] private GameObject chestClose;
    [SerializeField] private Image block;
    [SerializeField] private ClickButtonConfig clickButtonConfig;
    [SerializeField] private FadeConfig fadeConfig;
    [SerializeField] private AudioClip open;
    //[SerializeField] private AudioClip clip;
    
    private readonly TweenSession _tweenSession = new TweenSession();

    private void OnEnable()
    {
        block.raycastTarget = false;
        chestOpened.gameObject.SetActive(false);
        chestClose.gameObject.SetActive(true);
        chestButton.onClick.AddListener(ShakeChest);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.EmitEvent(EventName.Server.PressMysteryChest);
        _tweenSession.Cancel();
        
        _tweenSession.Start(
            chest.DOScale(clickButtonConfig.chestIn.scale, clickButtonConfig.chestIn.duration).SetEase(clickButtonConfig.chestIn.ease),
            lid.DOScale(clickButtonConfig.chestIn.scale, clickButtonConfig.chestIn.duration).SetEase(clickButtonConfig.chestIn.ease)
        );
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        block.raycastTarget = true;
        //open.PlayOneShot(clip);
        EventManager.EmitEventData(EventName.WidgetEvent.PLAY_SOUND_ACTION,open);
        _tweenSession.Cancel();

        _tweenSession.Start(
            chest.DOScale(clickButtonConfig.chestOut.scale, clickButtonConfig.chestOut.duration).SetEase(clickButtonConfig.chestOut.ease),
            lid.DOScale(clickButtonConfig.chestOut.scale, clickButtonConfig.chestOut.duration).SetEase(clickButtonConfig.chestOut.ease).OnComplete(ShakeChest)
        );
    }

    private bool IsDisableButton => chestButton.interactable == false;

    private void ShakeChest()
    {
        _tweenSession.Cancel();

        _tweenSession.Start(
            chest.DOShakePosition(clickButtonConfig.shake.durationShake, clickButtonConfig.shake.strength,clickButtonConfig.shake.vibrato,
                clickButtonConfig.shake.randomness).SetEase(clickButtonConfig.shake.ease),
            lid.DOShakePosition(clickButtonConfig.shake.durationShake, clickButtonConfig.shake.strength,clickButtonConfig.shake.vibrato,
                clickButtonConfig.shake.randomness).SetEase(clickButtonConfig.shake.ease).OnComplete(LidAnimation)
        );
    }

    private void LidAnimation()
    {
        
        _tweenSession.Cancel();

        _tweenSession.Start(
            lid.DOScale(clickButtonConfig.shake.scale, clickButtonConfig.shake.duration).SetEase(clickButtonConfig.shake.ease),
            lid.DOLocalMoveY(lid.transform.localPosition.y + clickButtonConfig.shake.up, clickButtonConfig.shake.duration).SetEase(clickButtonConfig.shake.ease).OnComplete(OpenChest)
        );
    }
    
    private void OpenChest()
    {
        chestClose.SetActive(false);
        StartCoroutine(HideChest());
    }

    private IEnumerator HideChest()
    {
        chestOpened.gameObject.SetActive(true);
        _tweenSession.Cancel();
        
        _tweenSession.Start(
            chestOpened.DOFade(fadeConfig.normal.visible, fadeConfig.normal.duration).SetEase(fadeConfig.normal.ease)
        );
        yield return new WaitForSeconds(1);
        EventManager.EmitEvent(EventName.Server.MysteryChestDecor);
        _tweenSession.Cancel();
        
        _tweenSession.Start(
                chestOpened.DOFade(fadeConfig.invincible.visible, fadeConfig.normal.duration).SetEase(fadeConfig.invincible.ease)
        );
        
    }
}
