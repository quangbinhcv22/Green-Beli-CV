using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DelayActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private float delay = 0.5f;


    private void OnEnable()
    {
        HideTargets();
        StartCoroutine(ShowTargets());
    }

    private void OnDisable()
    {
        StopCoroutine(ShowTargets());
    }


    private void ActiveTargets(bool isActive) => targets.ForEach(member => member.SetActive(isActive));
    private void HideTargets() => ActiveTargets(false);

    private IEnumerator ShowTargets()
    {
        yield return new WaitForSeconds(delay);
        ActiveTargets(true);
    }
}