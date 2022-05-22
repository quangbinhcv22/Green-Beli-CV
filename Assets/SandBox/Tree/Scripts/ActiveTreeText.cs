using GEvent;
using GNetwork;
using TigerForge;
using TMPro;
using UnityEngine;

public class ActiveTreeText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    
    private void OnEnable() => SetUp();    
    

    private void SetUp()
    {
        if (ActiveTreeServerService.Response.IsError) return;
        
    }
}
