using System.Collections.Generic;
using GEvent;
using GRBESystem.UI.Screens.Popups.Setting.Avatar;
using GRBESystem.UI.Screens.Popups.Setting.General;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace Manager.Avatar
{
    public class AvatarManager : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.GetListHero, SetUpData);
            EventManager.StartListening(EventName.UI.Select<AvatarData>(), UpdateData);
        }

        private static long AvatarID => AvatarSettingStorage.LoadLocal().avatarID;
        
        private void EmitEventData(AvatarData avatarData) =>
            EventManager.EmitEventData(EventName.UI.Select<AvatarData>(), avatarData);

        private void SetUpData()
        {
            if (NetworkService.Instance.services.getHeroList == null)
            {
                EmitEventData(new AvatarData());
                return;
            }
            var getHeroListServerService = NetworkService.Instance.services.getHeroList;
            var selfAvatarID = getHeroListServerService.ContainHero(AvatarID)
                ? AvatarID : getHeroListServerService.GetHeroAvatarId();

            var avatarData = new AvatarData
            {
                selfAvatarID = selfAvatarID,
                selectAvatarID = selfAvatarID,
                avatarIDs = new List<long>()
            };
            getHeroListServerService.HeroResponses.ForEach(hero => avatarData.avatarIDs.Add(hero.GetID()));
            EmitEventData(avatarData);
        }
        
        private void UpdateData()
        {
            var data = EventManager.GetData(EventName.UI.Select<AvatarData>());
            if (data is null) return;

            var avatarData = (AvatarData) data;
            if (avatarData.IsCanChangeAvatar ||
                avatarData.IsCanChangeAvatar is false && AvatarID == avatarData.selfAvatarID)
                return;
                
            var storage = new AvatarSettingStorage {avatarID = avatarData.selfAvatarID};
            storage.SaveLocal();
        }
    }
}
