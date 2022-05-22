using SandBox.DataInformation;
using UnityEngine;

[CreateAssetMenu(menuName = "Preset/MaintainPopup", fileName = nameof(MaintainPopupPreset))]
public class MaintainPopupPreset : ScriptableObject
{
    public InfoPopupData2 data;
}