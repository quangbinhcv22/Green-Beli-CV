using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Log;
using QB.ViewData;
using TMPro;
using UnityEngine;

public class DataObjectDeactivate : MonoBehaviour, IDataMemberView
{
    [SerializeField] [Space] private DataCoreView coreView;
    [SerializeField] private List<string> memberNames;
    
    
    private void Awake()
    {
        coreView.AddCallbackUpdate(this);
    }

    public void UpdateDefault()
    {
        gameObject.SetActive(true);
    }

    public void UpdateView(object data)
    {
        try
        {
            gameObject.SetActive(memberNames.Select(data.Get).ToArray().Contains("Active"));
        }
        catch (UnableAccessDataViewException e)
        {
            GLogger.LogError(e.Message);
            UpdateDefault();
        }
    }

    private void OnValidate()
    {
        if (coreView is null)
        {
            Debug.Log($"<color=yellow>{nameof(coreView)}</color> is null", gameObject);
        }
    }
}
