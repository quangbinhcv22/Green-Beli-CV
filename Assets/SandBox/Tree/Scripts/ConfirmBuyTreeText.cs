using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class ConfirmBuyTreeText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";
    [SerializeField] private string textDefault;

    private const int OneTree = 1;
    private void OnEnable()
    {
        UpdateView();
    }
    
  

    private void UpdateView()
    {
        var treeData = EventManager.GetData(EventName.Select.TreeQuantity);
        if (treeData is null) return;
        var trees = (int) treeData;

        if(trees > OneTree) text.SetText(string.Format(textFormat,trees));
        else if (trees == OneTree) text.SetText(string.Format(textDefault));
    }
}
