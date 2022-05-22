using UnityEngine;
using DG.Tweening;

public class TreeNftFeature : MonoBehaviour
{
    [SerializeField] private GameObject treeNavigator;
   
    public void ActiveTreeNavigator()
    {
        treeNavigator.SetActive(treeNavigator.activeInHierarchy != true);
    }
}
