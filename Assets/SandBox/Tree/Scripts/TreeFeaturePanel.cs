using System.Linq;
using GEvent;
using GNetwork;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class TreeFeaturePanel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject featurePanel;
    [SerializeField] private GameObject statsPanel;
    
    [SerializeField] private Sprite show;
    [SerializeField] private Sprite hide;

    private void Awake()
    {
        featurePanel.gameObject.SetActive(false);
        button.image.sprite = hide;
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnShowFeaturePanel);
        EventManager.StartListening(EventName.Select.TreeFeature,ChangeButtonImage);
        EventManager.StartListening(EventName.Select.AllTreesOpened,OnShowFeaturePanel);
        ChangeButtonImage();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.TreeFeature,ChangeButtonImage);
        EventManager.StopListening(EventName.Select.AllTreesOpened,OnShowFeaturePanel);
    }

    private void ChangeButtonImage()
    {
        button.image.sprite = hide;
    }

    private void OnShowFeaturePanel()
    {
        EventManager.EmitEvent(EventName.Select.TreeStats);
        button.image.sprite = show;
        if (GetListTreeServerService.Response.data.Any())
        {
            featurePanel.gameObject.SetActive(true);
        }
        
        statsPanel.gameObject.SetActive(false);
    }
}
