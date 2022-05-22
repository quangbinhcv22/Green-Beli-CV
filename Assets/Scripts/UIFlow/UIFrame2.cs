using System.Collections.Generic;
using System.Linq;
using UIFlow.InGame;
using UnityEngine;
using UnityEngine.Assertions;

namespace UIFlow
{
    public class UIFrame2 : MonoBehaviour
    {
        [SerializeField] private UIFrameSetting setting;

        private readonly List<UIObject> _createdUis = new List<UIObject>();

        private void Awake()
        {
            Assert.IsNotNull(setting);
        }

        public void Request(UIRequest request)
        {
            switch (request.action)
            {
                case UIAction.Open:
                    Open(request);
                    break;
                case UIAction.OpenNew:
                    OpenNew(request);
                    break;
                case UIAction.Close:
                    Close(request);
                    break;
                case UIAction.Switch:
                    Switch(request);
                    break;
                case UIAction.New:
                    Create(request);
                    break;
                case UIAction.Destroy:
                    Destroy(request);
                    break;
            }
        }


        private void Open(UIRequest request)
        {
            var targetUI = _createdUis.GetUI(request.id) ?? Create(request);
            (targetUI)?.Open(request);
        }

        private void OpenNew(UIRequest request)
        {
            var targetUI = _createdUis.GetUINotActive(request.id) ?? Create(request);
            (targetUI)?.Open(request);
        }

        private void Close(UIRequest request)
        {
            if (request.id is UIId.All)
            {
                CloseAll();
                return;
            }

            foreach (var ui in _createdUis.GetActiveUIs(request.id))
            {
                ui.Close(request);
                ui.GetComponent<UIUser>()?.targets.ForEach(target => Close(new UIRequest{id = target, haveAnimation = request.haveAnimation}));
            }
        }

        private void CloseAll()
        {
            foreach (var ui in _createdUis)
            {
                if (ui.gameObject.activeInHierarchy) ui.gameObject.SetActive(false);
            }
        }

        private void Switch(UIRequest request)
        {
            var switchedScreen = _createdUis.GetUI(request.id) ?? Create(request);
            if (switchedScreen is null) return;

            var hidedOnSwitchUIs = _createdUis.GetHidedOnSwitchUIs(switchedScreen.Type);

            hidedOnSwitchUIs.ForEach(ui => ActiveUIs.Remove(ui.Id));
            hidedOnSwitchUIs.ForEach(ui => ui.Hide());
            switchedScreen.Open(request.data, request.haveAnimation);
        }

        private void Destroy(UIRequest request)
        {
            ActiveUIs.Remove(request.id);

            _createdUis.GetUIs(request.id).ForEach(RemoveScreen);

            void RemoveScreen(UIObject ui)
            {
                _createdUis.Remove(ui);
                Destroy(ui.gameObject);
            }
        }


        private UIObject Create(UIRequest request)
        {
            var newUi = setting.CreateUI(request.id, transform);
            if (newUi is null) return null;

            _createdUis.Add(newUi);
            SortUILayerBasedOnOrder();

            return newUi;
        }


        private void SortUILayerBasedOnOrder()
        {
            var orderlyScreens = _createdUis.OrderBy(ui => ui.Layer).ToList();
            for (var i = 0; i < orderlyScreens.Count; i++) orderlyScreens[i].transform.SetSiblingIndex(i);
        }
    }

    public static class UIListExtension
    {
        public static UIObject GetUI(this IEnumerable<UIObject> uis, UIId uiId)
        {
            return uis.FirstOrDefault(ui => ui.Id.Equals(uiId));
        }

        public static List<UIObject> GetUIs(this IEnumerable<UIObject> uis, UIId uiId)
        {
            return uis.Where(ui => ui.Id.Equals(uiId)).ToList();
        }

        public static List<UIObject> GetActiveUIs(this IEnumerable<UIObject> uis, UIId uiId)
        {
            return uis.Where(ui => ui.Id.Equals(uiId) && IsActive(ui)).ToList();
        }

        public static UIObject GetUINotActive(this IEnumerable<UIObject> uis, UIId uiId)
        {
            return uis.FirstOrDefault(ui => ui.Id.Equals(uiId) && !IsActive(ui));
        }

        public static List<UIObject> GetHidedOnSwitchUIs(this IEnumerable<UIObject> uis, UIType targetUIType)
        {
            return uis.ToList().FindAll(ui => ui.Type == targetUIType && IsActive(ui)).ToList();
        }

        private static bool IsActive(Component ui) => ui.gameObject.activeInHierarchy;
    }
}