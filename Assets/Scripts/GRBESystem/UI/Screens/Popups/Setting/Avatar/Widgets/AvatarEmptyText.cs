using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Popups.Setting.Avatar;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

public class AvatarEmptyText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string emptyListString;
    
    
    private void OnEnable()
    {
        UpdateView();
        EventManager.StartListening(EventName.UI.Select<AvatarData>(), UpdateView);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.UI.Select<AvatarData>(), UpdateView);
    }

    private void UpdateView()
    {
        var data = EventManager.GetData(EventName.UI.Select<AvatarData>());
        if (NetworkService.Instance.IsNotLogged() || data is null) return;
        var avatars = (AvatarData) data;
     
        text.SetText(avatars.avatarIDs.Any() ? string.Empty : emptyListString);
        SetActive(avatars.avatarIDs.Any() is false);
    }

    private void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
