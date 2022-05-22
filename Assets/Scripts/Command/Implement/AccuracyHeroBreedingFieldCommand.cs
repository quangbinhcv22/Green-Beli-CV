using System.Collections.Generic;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;

namespace Command.Implement
{
    public class AccuracyHeroBreedingFieldCommand : ICommand
    {
        public void Execute()
        {
            var accuracyHeroes = EventManager.GetData<List<HeroResponse>>(EventName.PlayerEvent.LastBreedingHeroes);

            foreach (var accuracyHero in accuracyHeroes)
            {
                var modifiedHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(accuracyHero.GetID());
                if(modifiedHero.breeding.Equals(accuracyHero.breeding)) return;

                modifiedHero.breeding = accuracyHero.breeding;
                NetworkService.Instance.services.getHeroList.ModifyHero(modifiedHero);
            }
        }
    }
}
