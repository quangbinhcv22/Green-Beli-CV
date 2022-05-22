using System.Collections.Generic;

namespace GRBESystem.UI.Screens.Popups.Setting.Avatar
{
    [System.Serializable]
    public struct AvatarData
    {
        public long selfAvatarID;
        public long selectAvatarID;
        public List<long> avatarIDs;

        public bool IsCanChangeAvatar => selfAvatarID != selectAvatarID;
    }
}
