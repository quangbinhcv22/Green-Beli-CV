using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.ConvertPvpKey
{
    [RequireComponent(typeof(Button))]
    public class ButtonByPvpKeySetter : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TypeSetter stage;


        private void Awake() => button ??= GetComponent<Button>();

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.ConvertPvpKeyToReward, UpdateView);
        }

        private void UpdateView()
        {
            switch (stage)
            {
                case TypeSetter.Active:
                    button.gameObject.SetActive(HasPvpKey());
                    break;
                case TypeSetter.Interactable:
                    button.interactable = HasPvpKey();
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }

        private bool HasPvpKey()
        {
            return NetworkService.Instance.IsNotLogged() is false &&
                   NetworkService.Instance.services.login.MessageResponse.data.numberPVPKey > (int) default;
        }
    }

    [System.Serializable]
    public enum TypeSetter
    {
        Active = 0,
        Interactable = 1,
    }
}
