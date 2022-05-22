using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class CareTreeButton : MonoBehaviour
{
    // [SerializeField] private Button button;
    //
    // private void Awake() => button.onClick.AddListener(Setup);
    //
    // private void OnEnable()
    // {
    //     EventManager.StartListening(EventName.Select.Tree, Setup);
    //     Setup();
    // }
    //
    // private void OnDisable() => EventManager.StopListening(EventName.Select.Tree, Setup);
    //
    // private void Setup()
    // {
    //     var nullableTree = EventManager.GetData(EventName.Select.Tree);
    //
    //     if (nullableTree is Tree tree)
    //     {
    //         EventManager.EmitEventData(EventName.Select.CareTreeSelected, tree.id);
    //     }
    // }
}
