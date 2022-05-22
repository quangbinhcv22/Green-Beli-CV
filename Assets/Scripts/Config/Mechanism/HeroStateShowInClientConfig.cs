using System.Collections.Generic;
using Network.Messages.GetHeroList;
using UnityEngine;

namespace Config.Mechanism
{
    [CreateAssetMenu(fileName = nameof(HeroStateShowInClientConfig), menuName = "ScriptableObject/Config/Mechanism/HeroStateShowInClient")]
    public class HeroStateShowInClientConfig : ScriptableObject
    {
        public List<HeroState> heroStateShowInClient;

    }
}