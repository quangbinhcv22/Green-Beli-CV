using SandBox.DataInformation;
using UnityEngine;

namespace SandBox.Tree.InfomartionPopup
{
    [CreateAssetMenu(menuName = "Preset/InfoPopup", fileName = nameof(InfoPopupPreset))]
    public class InfoPopupPreset : ScriptableObject
    {
        public InfoPopupData2 data;
    }
}

