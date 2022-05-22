using Log;
using QB.ViewData;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-200)]
[RequireComponent(typeof(TMP_Text))]
public class DataDetailView : MonoBehaviour, IDataMemberView
{
    [SerializeField] [Space] private DataCoreView coreView;
    [SerializeField] private ScriptableObject detailSet;
    [SerializeField] private string memberName;
    [SerializeField] [Space] [TextArea] private string format = "{0}";
    [SerializeField] [TextArea] private string textDefault;
    
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        coreView.AddCallbackUpdate(this);
    }

    public void UpdateDefault()
    {
        _text.SetText(textDefault);
    }

    public void UpdateView(object data)
    {
        try
        {
            _text.SetText(string.Format(format, ((ILocallizeSet) detailSet)?.GetDetail(data.Get(memberName))));
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