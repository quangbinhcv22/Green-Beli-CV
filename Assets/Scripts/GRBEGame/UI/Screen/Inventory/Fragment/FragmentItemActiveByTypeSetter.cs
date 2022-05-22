using System.Collections.Generic;
using GRBEGame.UI.Screen.Inventory;
using GRBEGame.UI.Screen.Inventory.Fragment;
using UnityEngine;

public class FragmentItemActiveByTypeSetter : MonoBehaviour, IMemberView<FragmentItemInfo>
{
    [SerializeField] private FragmentItemCoreView coreView;
    [SerializeField] private List<int> types;


    private void Awake()
    {
        coreView.AddCallBackUpdateView(this);
    }

    public void UpdateDefault()
    {
        SetActive(false);
    }

    public void UpdateView(FragmentItemInfo data)
    {
        var isHasType = false;
        types.ForEach(type =>
        {
            if (data.type == type) isHasType = true;
        });
        SetActive(isHasType);
    }

    private void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
