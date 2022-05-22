using UnityEngine;
using DG.Tweening;
using GEvent;
using GTween;
using TigerForge;
using UnityEngine.EventSystems;

public class HoverMysteryChest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform lid;
    [SerializeField] private Transform chest;
    [SerializeField] private HoverButtonConfig hoverButtonConfig;

    private readonly TweenSession _tweenSession = new TweenSession();
    private float _startPosition;


    private void OnEnable()
    {
        _startPosition = lid.localPosition.y;
        TweenCancel();
    }

    private void TweenCancel()
    {
        _tweenSession.Cancel();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TweenCancel();

        _tweenSession.Start(
            lid.DOLocalMoveY(_startPosition + hoverButtonConfig.chestIn.up, hoverButtonConfig.chestIn.duration)
                .SetEase(hoverButtonConfig.chestIn.ease),
            lid.DOScale(hoverButtonConfig.chestIn.scale, hoverButtonConfig.chestIn.duration)
                .SetEase(hoverButtonConfig.chestIn.ease),
            chest.DOScale(hoverButtonConfig.chestIn.scale, hoverButtonConfig.chestIn.duration)
                .SetEase(hoverButtonConfig.chestIn.ease).OnComplete(DoMove)
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TweenCancel();

        _tweenSession.Start(
            lid.DOLocalMoveY(_startPosition, hoverButtonConfig.moveY.duration).SetEase(hoverButtonConfig.moveY.ease),
            lid.DOScale(hoverButtonConfig.chestOut.scale, hoverButtonConfig.chestOut.duration)
                .SetEase(hoverButtonConfig.chestOut.ease),
            chest.DOScale(hoverButtonConfig.chestOut.scale, hoverButtonConfig.chestOut.duration)
                .SetEase(hoverButtonConfig.chestOut.ease)
        );
    }

    private void DoMove()
    {
        _tweenSession.Cancel();

        _tweenSession.Start(
            lid.DOLocalMoveY(_startPosition, hoverButtonConfig.moveY.duration).SetEase(hoverButtonConfig.moveY.ease),
            lid.DOScale(hoverButtonConfig.chestOut.scale, hoverButtonConfig.chestOut.duration)
                .SetEase(hoverButtonConfig.chestOut.ease),
            chest.DOScale(hoverButtonConfig.chestOut.scale, hoverButtonConfig.chestOut.duration)
                .SetEase(hoverButtonConfig.chestOut.ease)
        );
    }


    private void DropChest()
    {
        // _tweenSession.Start(chestImage.
        //     DOLocalMoveY(chestImage.localPosition.y - endDropValue, durationDrop).SetEase(ease));
    }
}