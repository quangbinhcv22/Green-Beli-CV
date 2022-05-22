using DG.Tweening;
using GNetwork;
using GRBEGame.UI.DataView;
using Network.Service;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class TreeHealthProcess : MonoBehaviour, IMemberView<Tree>
{
    [SerializeField] private TreeCellView treeCellView;

    [SerializeField] private Image healthValue;
    //[SerializeField] private ProcessBar processBar;
    
    private void Awake() => treeCellView.AddCallbackUpdate(this);

    public void UpdateDefault()
    {
        //processBar.UpdateView(default,default);
    }

    public void UpdateView(Tree tree)
    {
        var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
        if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError) return;
        
        var percentValueCurrent = (float)tree.healthPoint / loadGameResponse.data.tree.general.maxHealthPoint;
        TweenFillAmountValueBar(percentValueCurrent);
    }
    
    private void TweenFillAmountValueBar(float targetValue)
    {
        healthValue.DOFillAmount(targetValue, 1).SetEase(Ease.Linear);
    }

    private void OnValidate()
    {
        if (treeCellView is null) Debug.Log($"I'm <color=yellow>{name}</color>, <color=yellow>Tree Cell View</color> is <color=yellow>null</color>!", gameObject);
        //if (processBar is null) Debug.Log($"I'm <color=yellow>{name}</color>, <color=yellow>Process Bar</color> is <color=yellow>null</color>!", gameObject);
    }
}

