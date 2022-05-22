using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class TreesOpened : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(IsTreesOpened);
    }

    private void IsTreesOpened()
    {
        EventManager.EmitEvent(EventName.Select.AllTreesOpened);
    }
}
