using System.Collections;
using DG.Tweening;
using GTween;
using SandBox.MysteryChest;
using UnityEngine;

public class MysteryChestRewardAnimation : MonoBehaviour
{
    [SerializeField] private MysteryItemViewer mysteryItemViewer;

    [SerializeField] private float duration;
    [SerializeField] private float angle;
    [SerializeField] private int loops;
    [SerializeField] private Ease ease;
    [SerializeField] private LoopType loopType;
    [SerializeField] private AudioSource audio;
    
    private readonly TweenSession _tweenSession = new TweenSession();
    
    private void OnEnable()
    {
        mysteryItemViewer.transform.DOLocalRotate(new Vector3(0,angle,0), duration).SetEase(ease).SetLoops(loops,loopType);
    }

    private IEnumerator SoundEffect()
    {
        for (var i = 0; i < loops; i++)
        {
            audio.Play();
            yield return new WaitForSeconds(1);
        }
    }
}
