using System;
using System.Collections.Generic;
using System.Linq;
using Network.Messages;
using QB.Collection;
using UnityEngine;

namespace Localization.Nation
{
    [CreateAssetMenu(fileName = nameof(NationConfig), menuName = "GreenBeli/Localization/Nation")]
    public class NationConfig : ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<string, NationInfo> supportedNations;

        public NationInfo GetNation(string nationId) => supportedNations[nationId];
        public List<string> AllNationIds => supportedNations.customPairs.Select(pair => pair.key).ToList();
    }

    [Serializable]
    public class NationInfo
    {
        public Sprite art;
        public string fullName;
    }
}