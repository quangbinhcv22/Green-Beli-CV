using System.Collections.Generic;
using Extensions.Initialization.Request;
using UnityEngine;

namespace GRBESystem.Model.HeroModel.Presenter
{
    [CreateAssetMenu(fileName = FileName, menuName = MenuName)]
    public class HeroesPresentConfig : ScriptableObject
    {
        private const string FileName = nameof(HeroesPresentConfig);
        private const string MenuName = "ScriptableObject/PresentConfig/Heroes";
        
        [SerializeField] private List<ShowHeroModelRequest> showHeroModelRequests;

        public ShowHeroModelRequest GetShowModelRequest(int index)
        {
            return showHeroModelRequests.Count > index ? showHeroModelRequests[index] : new ShowHeroModelRequest();
        }
    }
}