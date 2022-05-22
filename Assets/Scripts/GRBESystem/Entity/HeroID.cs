using Network.Messages.GetHeroList;

namespace GRBESystem.Entity
{
    public static class HeroID
    {
        public const long DEFAULT_HERO_ID = 111111111111;

        public static long ConvertFromBodyPartsResponse(HeroResponse.BodyPart[] bodyParts)
        {
            var heroIdString = string.Empty;

            foreach (var bodyPart in bodyParts)
            {
                heroIdString += $"{bodyPart.faction}{bodyPart.skinId}";
            }

            return long.Parse(heroIdString);
        }

        // public static bool IsIdValid(long id)
        // {
        //     
        // }
    }
}