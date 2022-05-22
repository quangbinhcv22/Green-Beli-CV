using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
[RequireComponent(typeof(Button))]
public class SelectTree : MonoBehaviour
{
    [SerializeField] private TreeCellView owner;

    private void Awake() => GetComponent<Button>().onClick.AddListener(SelectedTree);
        
    private void SelectedTree() =>
        EventManager.EmitEventData(EventName.Select.Tree, owner.Tree);
    
}