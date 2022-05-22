using System.Linq;
using GEvent;
using GNetwork;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class TreeStatsPanel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private GameObject featurePanel;

    [SerializeField] private Sprite show;
    [SerializeField] private Sprite hide;

    private void OnEnable()
    {
        button.image.sprite = show;
        button.onClick.AddListener(OnShowStatsPanel);
        EventManager.StartListening(EventName.Select.TreeStats, ChangeButtonImage);
        EventManager.StartListening(EventName.Select.AllTreesOpened, OnShowStatsPanel);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.TreeStats, ChangeButtonImage);
        EventManager.StopListening(EventName.Select.AllTreesOpened, OnShowStatsPanel);
    }

    private void ChangeButtonImage()
    {
        button.image.sprite = hide;
    }

    private void OnShowStatsPanel()
    {
        EventManager.EmitEvent(EventName.Select.TreeFeature);
        button.image.sprite = show;

        if (GetListTreeServerService.Response.IsError) return;

        if (GetListTreeServerService.Response.data.Any())
        {
            statsPanel.gameObject.SetActive(true);
        }

        featurePanel.SetActive(false);
    }
}