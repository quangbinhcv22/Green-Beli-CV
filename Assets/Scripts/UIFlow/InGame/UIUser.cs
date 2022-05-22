using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIFlow.InGame
{
    public class UIUser : MonoBehaviour
    {
        public List<UIId> targets;
        // public List<UIUserSetting> useScreens;

        // private void OnEnable() => targets.ForEach(_ => UIUserRegister.RegisteredUseUI(this));
        // private void OnDisable() => targets.ForEach(_ => UIUserRegister.UnRegisteredUseUI(this));
    }

    [Serializable]
    public class UIUserSetting
    {
        public UIId id;
        public bool haveAnimation;
    }

    public static class UIUserRegister
    {
        private static readonly Dictionary<UIId, List<UIUser>> UIUsers = new Dictionary<UIId, List<UIUser>>();

        public static void RegisteredUseUI(UIUser user)
        {
            // foreach (var uiId in user.targets)
            // {
            //     if (UIUsers.ContainsKey(uiId))
            //     {
            //         UIUsers[uiId].Add(user);
            //         return;
            //     }
            //
            //     UIUsers.Add(uiId, new List<UIUser> {user});
            //
            //     var openRequest = new UIRequest {action = UIAction.Open, id = uiId, haveAnimation = false};
            //     openRequest.SendRequest();
            // }
        }

        public static void UnRegisteredUseUI(UIUser user)
        {
            // foreach (var uiId in user.targets)
            // {
            //     if (UIUsers.ContainsKey(uiId) is false) return;
            //     UIUsers[uiId].Remove(user);
            //
            //     if (UIUsers[uiId].Any()) return;
            //
            //     UIUsers.Remove(uiId);
            //
            //     var closeRequest = new UIRequest {action = UIAction.Close, id = uiId, haveAnimation = false};
            //     closeRequest.SendRequest();
            // }
        }
    }
}