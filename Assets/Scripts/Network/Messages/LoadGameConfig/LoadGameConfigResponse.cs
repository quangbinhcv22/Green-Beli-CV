using System;
using System.Collections.Generic;
using System.Linq;
using GRBESystem.Entity.Element;
using GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator;
using Network.Messages.GetHeroList;
using Network.Service;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Messages.LoadGame
{
    [Serializable]
    public class LoadGameConfigResponse
    {
        public int energyRecoverIntervalSec;
        [JsonProperty("server_time")]
        public string serverTime;
        [JsonProperty("support_hero")]
        public SubHeroMutual supportHero;
        public Game game;
        public Nation nation;
        public TreeConfig tree;
        [JsonProperty("level_capacity_star")]
        public LevelCapacityStar levelCapacityStar;
        public Lottery lottery;
        public Pvp pvp;
        [JsonProperty("restore_Level")]
        public RestoreLevel restoreLevel;
        public Inventory inventory;
        public Fusion fusion;
        [JsonProperty("mystery_chest")]
        public MysteryChest mysteryChest;
        public Pve pve;
        public Energy energy;
        public Breeding breeding;
        
        
        [System.Serializable]
        public class SubHeroMutual
        {
            public List<MutalGenerationRule> mutal_generation_rule;
            public List<BuffRarityStar> buff_rarity_star;

            [System.Serializable]
            public struct MutalGenerationRule
            {
                public int main;
                public string main_class;
                public int support;
                public string support_class;
            }

            [System.Serializable]
            public struct BuffRarityStar
            {
                public int star;
                public float rarity_1;
                public float rarity_2;
                public float rarity_3;
                public float rarity_4;
                public float rarity_5;
                public float rarity_6;
            }


            public float GetMutualBuff(int star, int rarity)
            {
                if (buff_rarity_star is null) return 0;

                var starInfo = buff_rarity_star.Find(starBuff => starBuff.star.Equals(star));
                return rarity switch
                {
                    1 => starInfo.rarity_1,
                    2 => starInfo.rarity_2,
                    3 => starInfo.rarity_3,
                    4 => starInfo.rarity_4,
                    5 => starInfo.rarity_5,
                    6 => starInfo.rarity_6,
                    _ => 0
                };
            }

            public bool IsSubHeroMutual(HeroElement mainHeroElement, HeroElement subHeroElement)
            {
                var mainHeroMutualElement =
                    (HeroElement) mutal_generation_rule.Find(rule => rule.main.Equals((int) mainHeroElement)).support;
                return mainHeroMutualElement.Equals(subHeroElement);
            }

            public float GetSubHeroMutualFactor(HeroElement mainHeroElement, HeroElement subHeroElement)
            {
                return IsSubHeroMutual(mainHeroElement, subHeroElement) ? 1f : 0.5f;
            }
        }

        [Serializable]
        public class Game
        {
            [JsonProperty("lose_streak_buff")]
            public float loseStreakBuff;
        }
        
        [Serializable]
        public class Nation
        {
            public List<Country> nation;

            [JsonProperty("limit-day-update-nation")]
            public int limitDayUpdateNation;

            [Serializable]
            public class Country
            {
                public string name;
                public string code;
            }
        }

        [Serializable]
        public class TreeConfig
        {
            public General general;
            [JsonProperty("fruit_rare")]
            public List<FruitRare> fruitRare;
            [JsonProperty("hero_fruit_rate")]
            public List<HeroFruitRate> heroFruitRate;

            public List<Price> price;

            [Serializable]
            public class General
            {
                
                [JsonProperty("beli_cost_per_watering")]
                public string beLiCostPerWatering;

                [JsonProperty("gfruit_cost_to_restore_dying_tree")]
                public string gFruitCostToRestoreDyingTree;

                [JsonProperty("max_health_point")] 
                public int maxHealthPoint;

                [JsonProperty("number_hero_to_active_tree")]
                public int numberHeroToActiveTree;

                [JsonProperty("gfruit_cost_per_watering")]
                public string gFruitCostPerWatering;

                [JsonProperty("gfruit_cost_per_fertilizing")]
                public string gFruitCostPerFertilizing;

                [JsonProperty("number_fruit_per_fertilizing")]
                public int numberFruitPerFertilizing;

                [JsonProperty("interval_time_update_health_point")]
                public string intervalTimeUpdateHealthPoint;

                [JsonProperty("limit_fertilizing_per_day")]
                public int limitFertilizingPerDay;

                [JsonProperty("number_fruit_per_watering")]
                public string numberFruitPerWatering;

                [JsonProperty("beli_cost_per_fertilizing")]
                public string beLiCostPerFertilizing;
                
                [JsonProperty("fruit_rate")]
                public string fruitRate;

                [JsonProperty("number_fragment_to_active_tree")]
                public string numberFragmentToActiveTree;
            }
            
            [Serializable]
            public class Price
            {
                public TreeCoinType money;
                public long quantity;
            }
            public enum TreeCoinType
            {
                GMeta = 0,
                BUsd = 1,
                Beli = 2,
                Gfruit = 3
            }
            
            [Serializable]
            public class FruitRare
            {
                public string rate;
                public string rarity;
            }
            
            public class HeroFruitRate
            {
                public string star;
                public string rarity1;
                public string rarity2;
                public string rarity3;
                public string rarity4;
                public string rarity5;
                public string rarity6;
            }
        }
        
        [System.Serializable]
        public struct LevelCapacityStar
        {
            [JsonProperty("level_capacity_star")]
            public List<LevelCapacityStarMember> levelCapacityStar;

            [System.Serializable]
            public struct LevelCapacityStarMember
            {
                public string star;
                public string level;
            }

            public int GetMaxLevelAtStar(int star)
            {
                var starIndex = levelCapacityStar.FindIndex(capacityStar => capacityStar.star.Equals($"{star}"));

                return starIndex < (int) default ? default : int.Parse(levelCapacityStar[starIndex].level);
            }
        }

        [Serializable]
        public class Breeding
        {
            public List<Price> price;

            [Serializable]
            public class Price
            {
                public string times;
                public string gfruit;
                public string grbe;
            }

            public CoinCost GetBreedingCost(int breedingTime)
            {
                if (breedingTime is (int) default) return new CoinCost();

                var queryPrice = price.Find(cost => cost.times == breedingTime.ToString());
                return new CoinCost() {gFruit = int.Parse(queryPrice.gfruit), grbe = int.Parse(queryPrice.grbe)};
            }
        }

        [Serializable]
        public class Pve
        {
            [JsonProperty("pve_reward_rate")]
            public List<RewardRate> pveRewardRate;

            [Serializable]
            public class RewardRate
            {
                public string reward;
                public float rate;
                [JsonProperty("star_1")]
                public string star1;
                [JsonProperty("star_2")]
                public string star2;
                [JsonProperty("star_3")]
                public string star3;
                [JsonProperty("star_4")]
                public string star4;
                [JsonProperty("star_5")]
                public string star5;
                [JsonProperty("star_6")]
                public string star6;
            }

            public RewardRate GetRewardGFruit() => pveRewardRate.Find(item => item.reward == "beli");
        }

        [Serializable]
        public class Pvp
        {
            public int ticket_price;
            public int ticketOriginPrice = 600;

            public List<PvpticketRequirePvpkeyReward> pvpticket_require_pvpkey_reward;
            public List<LeaderboardReward> leaderboard_reward;
            public List<OpenBoxReward> open_box_reward;

            [Serializable]
            public class PvpticketRequirePvpkeyReward
            {
                public int key_reward;
                public int star;
                public int pvp_ticket_require;
            }

            [Serializable]
            public class LeaderboardReward
            {
                public string top;
                public float rate_gfruit_reward_per_prize;
            }

            [Serializable]
            public class OpenBoxReward
            {
                public int amount;
                public int id;
                public double rate_drop;
            }

            private const int GFruitType = 0;

            public double GetTotalRateOpenPvpChestGFruit()
            {
                var total = (double) default;
                open_box_reward.ForEach(item => total += item.id is GFruitType ? item.rate_drop : default);
                return total;
            }

            public double GetTotalRateOpenPvpChestFragment()
            {
                var total = (double) default;
                open_box_reward.ForEach(item => total += item.id is GFruitType ? default : item.rate_drop);
                return total;
            }

            public long GetMinValue(int type)
            {
                var items = open_box_reward.Where(item => item.id == type)?.ToList();
                if (items.Count is (int) default) return default;

                var amount = items.First().amount;
                items.ForEach(item =>
                {
                    if (amount > item.amount)
                        amount = item.amount;
                });

                return amount;
            }

            public long GetMaxValue(int type)
            {
                var items = open_box_reward.Where(item => item.id == type)?.ToList();
                if (items.Count is (int) default) return default;

                var amount = items.First().amount;
                items.ForEach(item =>
                {
                    if (amount < item.amount)
                        amount = item.amount;
                });

                return amount;
            }

            public int PvpTicketRequireToBattle()
            {
                const int defaultValue = -1;

                var mainHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetMainHero();
                if (NetworkService.Instance.IsNotLogged() || mainHero.GetID() == default) return defaultValue;

                return pvpticket_require_pvpkey_reward.Find(item => item.star == mainHero.star).pvp_ticket_require;
            }
        }


        [Serializable]
        public class Energy
        {
            public List<StarEnergy> star_energy;
            public List<Capacity> capacity;


            [System.Serializable]
            public struct StarEnergy
            {
                public int energy_per_pve;
                public int energy_gen_per_time;
                public int star;
            }

            [System.Serializable]
            public struct Capacity
            {
                public int level;
                public long upgrade_cost;
                public int capacity;
            }


            public int GetEnergyPerPve(int star)
            {
                var starEnergy = star_energy.Find(x => x.star == star);
                return starEnergy.energy_per_pve;
            }

            public int GetEnergyPerFourHour(int star)
            {
                var starEnergy = star_energy.Find(x => x.star == star);
                return starEnergy.energy_gen_per_time;
            }

            public int GetCapacity(int level)
            {
                var starEnergy = capacity.Find(x => x.level == level);
                return starEnergy.capacity;
            }

            public long GetUpgradeCost(int level)
            {
                var starEnergy = capacity.Find(x => x.level == level);
                return starEnergy.upgrade_cost;
            }
        }


        [System.Serializable]
        public class Inventory
        {
            [JsonProperty("fragment_information")]
            public List<FragmentInventoryInfo> fragmentInformation;
            [JsonProperty("material_infomation")]
            public List<MaterialInventoryInfo> materialInformation;
            [JsonProperty("combine_box_price")]
            public List<CombineBoxPrice> combineBoxPrice;
            [JsonProperty("unbox_price")]
            public List<UnboxPrice> unboxPrice;
            [JsonProperty("land_pack_price")]
            public List<LandPackPrice> landPackPrice;
            [JsonProperty("land_unpack_price")]
            public List<LandUnpackPrice> landUnpackPrice;

            [System.Serializable]
            public class FragmentInventoryInfo
            {
                [JsonProperty("amout_of_fragment_to_combine_box")]
                public int amountOfFragmentToCombineBox;
                [JsonProperty("fragment_name")]
                public string fragmentName;
                [JsonProperty("fragment_type")]
                public int fragmentType;
                public string description;
            }
            
            [System.Serializable]
            public class MaterialInventoryInfo
            {
                [JsonProperty("material_type")]
                public int materialType;
                [JsonProperty("material_name")]
                public string materialName;
                public string description;
            }

            [System.Serializable]
            public class CombineBoxPrice
            {
                [JsonProperty("gfruit")]
                public long gFruit;
                public int id;
                public long grbe;
            }

            [System.Serializable]
            public class UnboxPrice
            {
                [JsonProperty("gfruit")]
                public long gFruit;
                public int id;
                public long grbe;
            }

            [System.Serializable]
            public class LandPackPrice
            {
                [JsonProperty("gfruit")]
                public long gFruit;
                public int id;
                public long grbe;
            }

            [System.Serializable]
            public class LandUnpackPrice
            {
                [JsonProperty("gfruit")]
                public long gFruit;
                public int id;
                public long grbe;
            }

            public FragmentInventoryInfo GetFragmentInventory(int type)
            {
                return fragmentInformation.Find(item => item.fragmentType == type) ?? new FragmentInventoryInfo();
            }

            public MaterialInventoryInfo GetMaterialInventoryInfo(int type)
            {
                return materialInformation.Find(item => item.materialType == type) ?? new MaterialInventoryInfo();
            }

            public CombineBoxPrice GetPriceToAssemble(int type)
            {
                return combineBoxPrice.Find(item => item.id == type) ?? new CombineBoxPrice();
            }

            public LandPackPrice GetPriceToPack(int type)
            {
                return landPackPrice.Find(item => item.id == type) ?? new LandPackPrice();
            }

            public UnboxPrice GetPriceToUnbox(int type)
            {
                return unboxPrice.Find(item => item.id == type) ?? new UnboxPrice();
            }

            public LandUnpackPrice GetPriceToUnpack(int type)
            {
                return landUnpackPrice.Find(item => item.id == type) ?? new LandUnpackPrice();
            }
        }


        [Serializable]
        public class RestoreLevel
        {
            public List<Price> restore_level_price;

            [Serializable]
            public class Price
            {
                public int star;
                public int max_level;
                public long gfruits_per_level;
                public int min_level;
            }
        }


        [Serializable]
        public class Lottery
        {
            private const int FragmentTypeIndex = 0;
            private const int FragmentCountIndex = 1;


            public int price;
            public int reward_interval_time;
            public int reward_interval_close_time;

            [JsonProperty("daily_reward_structure")]
            public List<DailyRewardStructure> dailyRewardStructure;


            [Serializable]
            public class DailyRewardStructure
            {
                public float reward;
                public string top;
            }

            public int GetMaxTopCount()
            {
                var tops = dailyRewardStructure.Last().top.Split('-');
                return int.Parse(tops.Last());
            }

            public long GetGFruitReward(int totalGFruit, int top)
            {
                var rate = (float) default;
                foreach (var structure in dailyRewardStructure)
                {
                    var tops = structure.top.Split('-');
                    if (top == int.Parse(tops.First()))
                    {
                        rate = structure.reward;
                        break;
                    }
                    if (top >= int.Parse(tops.First()) && top <= int.Parse(tops.Last()))
                    {
                        rate = structure.reward;
                        break;
                    }
                }

                return (long) (totalGFruit * rate);
            }

            public int GetTotalPriceTickets(int ticketCount)
            {
                return (int) (ticketCount * price);
            }


            public int GetRewardScheduleSeconds()
            {
                const int secondPerMilliseconds = 1000;
                return reward_interval_time / secondPerMilliseconds;
            }

            public DateTime GetPreviousLotterySessionDate()
            {
                return DateTime.UtcNow.AddMilliseconds(-reward_interval_time);
            }

            public bool IsOpenBuy()
            {
                const int nextCycleCount = 1;
                var cycleSeconds = GetRewardScheduleSeconds();
                var currentSeconds = TimeManager.Instance.Seconds;

                var nextCycleSeconds =
                    ((Mathf.Floor((float) currentSeconds / cycleSeconds) + nextCycleCount) * cycleSeconds -
                     currentSeconds);
                var currentToCloseTime = TimeSpan.FromSeconds(nextCycleSeconds).Duration();

                return currentToCloseTime > GetOpenTime();
            }

            public TimeSpan GetOpenTime()
            {
                return TimeSpan.FromMilliseconds(reward_interval_close_time);
            }
        }

        [Serializable]
        public class MysteryChest
        {
            public List<StarBuffRate> star_buff_rate;
            public List<GenerationBuffRate> generation_buff_rate;
            public Price price;
            public List<RateDropFragmentDefault> rate_drop_fragment_default;
            public List<RarityBuffRate> rarity_buff_rate;

            public int limit_number_land_fragment;
            public int limit_lucky_point_to_claim;


            [Serializable]
            public struct Price
            {
                public int gfruit;
                public int energy;
            }

            [Serializable]
            public class StarBuffRate
            {
                public string star;
                public string star_buff_rate;
            }

            [Serializable]
            public class GenerationBuffRate
            {
                public string generation;
                public string generation_buff_rate;
            }

            [Serializable]
            public class RateDropFragmentDefault
            {
                public string id_fragment;
                public string rate_drop;
            }

            [Serializable]
            public class RarityBuffRate
            {
                public string rarity;
                public string rarity_buff_rate;
            }
        }


        [Serializable]
        public class Fusion
        {
            public List<Price> price;

            [Serializable]
            public class Price
            {
                public string star;
                public string quantity_level_up_body_part;
                public string max_level;
                public string GFRUIT;
                public string GRBE;

                public Price()
                {
                    const string valueDefault = "0";

                    GRBE = valueDefault;
                    GFRUIT = valueDefault;
                    max_level = valueDefault;
                    star = valueDefault;
                    quantity_level_up_body_part = valueDefault;
                }
            }


            public Price GetNextPriceMemberAtStar(int star)
            {
                var nextStart = star + 1;
                var queryIndex = price.FindIndex(priceMember => priceMember.star.Equals(nextStart.ToString()));

                return queryIndex < (int) default ? new Price() : price[queryIndex];
            }

            public CoinCost GetCostNextStar(int star)
            {
                var queryPrice = GetNextPriceMemberAtStar(star);
                return new CoinCost() {grbe = int.Parse(queryPrice.GRBE), gFruit = int.Parse(queryPrice.GFRUIT)};
            }

            public int GetNextLevelUpBodyPartCount(int star)
            {
                return int.Parse(GetNextPriceMemberAtStar(star).quantity_level_up_body_part);
            }
        }
    }
}