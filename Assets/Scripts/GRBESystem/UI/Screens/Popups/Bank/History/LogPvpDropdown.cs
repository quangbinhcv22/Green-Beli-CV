using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Bank.History
{
    [RequireComponent(typeof(Dropdown))]
    public class LogPvpDropdown : MonoBehaviour
    {
        [SerializeField] private GameObject logPvp;
        [SerializeField] private GameObject logMystery;
        
        private void Awake()
        {
            GetComponent<Dropdown>().onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(int arg0)
        {
            logPvp.gameObject.SetActive(arg0 == (int)Type.Pvp);
            logMystery.gameObject.SetActive(arg0 == (int)Type.Mystery);
        }
    }

    [System.Serializable]
    public enum Type
    {
        Pvp = 0,
        Mystery = 1,
    }
}
