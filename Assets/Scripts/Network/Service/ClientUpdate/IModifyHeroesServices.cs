using System.Collections.Generic;
using Network.Messages.GetHeroList;

namespace Network.Service.ClientUpdate
{
    public interface IModifyHeroesServices
    {
        List<HeroResponse> GetModifiedHero();
    }
}