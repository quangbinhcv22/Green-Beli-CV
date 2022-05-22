using GEvent;
using GNetwork;
using TigerForge;
using TMPro;
using UnityEngine;

public class TreeTotalQuantity : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";

    private void OnEnable()
    {
        EventManager.StartListening(EventName.Server.GetListTree,UpdateView);
        UpdateView();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.GetListTree,UpdateView);
    }
    
    private void UpdateView()
    {
        text.SetText(string.Format(textFormat, GetListTreeServerService.StageOneResponse.Count));
    }
}
