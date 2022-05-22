using GEvent;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Nation
{
    public class ResetNationSelection : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.EmitEventData(EventName.UI.Select<NationSelection>(), null);
        }
    }
}