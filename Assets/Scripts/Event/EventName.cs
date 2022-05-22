using Network.Messages.ErrorCase;
using QuangBinh.UIFramework.Screen;

namespace GEvent
{
    public static partial class EventName
    {



        public static class Client
        {
            public const string LoadEssentialData = "LoadEssentialData";
            public const string MainHeroOldable = "main_hero_oldable";
            public const string EventSystem = "event_system"; // data: EventSystem
            public const string EventPvpKeyUpdate = "EVENT_PVP_KEY_UPDATE";
            public const string SelectSpine = "Select_Spine"; // data: SpineName
            public const string UnSelectSpine = "Un_Select_Spine"; // data: SpineName

            public static class Login
            {
                public const string TermAgreed = "event_term_agreed";
            }

            public static class Battle
            {
                public const string BattleData = "ClientDataBattleChanged"; //data: BattleClientData
                public const string OpenSkipBattleConfirmPanel = "open_skip_battle_confirm_panel";
                public const string OpenSkipAllBattleConfirmPanel = "open_skip_all_battle_confirm_panel";
                public const string BattleMode = "BattleMode";
                public const string NextPvpBattleByTicketError = "NextPvpBattleError";
                public const string NextPvpBattleInTimeLimitError = "NextPvpBattleInTimeLimitError";

                public const string PvpRoom = "PvpRoom";
            }

            public static class Energy
            {
                public const string UpdateEnergy = "update_energy";
                public const string UpgradeEnergyCapacity = "UPGRADE_ENERGY_CAPACITY";

                public const string OpenUpgradeEnergyReconfirmPanel = "open_upgrade_energy_reconfirm_panel";
                public const string ReconfirmUpgradeEnergy = "reconfirm_upgrade_energy";
            }

            public static class LuckyMen
            {
                public const string OpenLuckyMenConfirmPanel = "open_lucky_men_confirm_panel";
            }

            public static class Inventory
            {
                public const string FakeAssembleFragment = "FAKE_ASSEMBLE_FRAGMENT";
                public const string FakePackFragment = "FAKE_PACK_FRAGMENT";
                public const string FakeUnbox = "FAKE_UNBOX";
                public const string FakeUnpack = "FAKE_UNPACKs";
            }
        }


        public static class PlayerEvent
        {
            //public const string PLAYER_INFO = "player_info";
            public const string SelectHero = "select_hero";
            public const string BattleHeroes = "BattleHeroes"; //data: List<long> (id)
            public const string BreedingHeroes = "BreedingHeroes"; //data: List<long> (id)
            public const string LastBreedingHeroes = "LastBreedingHeroes"; //data: List<HeroResponse>
            public const string FusionHeroes = "FusionHeroes"; //data: List<long> (id)
            public const string LastFusionHero = "LastFusionHero"; //data: HeroResponse
            public const string RestoreLevelsHero = "RestoreLevelsHero";

            public const string LoseTurnBattle = "LoseTurnBattle";

            public const string ConfirmRestoreLevels = "ConfirmRestoreLevels";

            public const string SelectTicket = "SelectTicket"; //data: long
            public const string SelectedTickets = "SelectedTickets"; //data: List<long>
            public const string EndCountdownLottery = "EndCountdownLottery";
            public const string EndOpenBuyLottery = "EndOpenBuyLottery";

            public const string ViewRewardDateDetail = "view_reward_date_detail";
            public const string CollapseRewardDateDetail = "collapse_view_detail";

            public const string PlayerInfoChange = "player_info_change"; //data: PlayerClientInfo

            public const string BlockRaycastCollider = "block_raycast_collider";
            public const string SelectHeroMode = "HeroSelectSlotInteractableMode";

            public const string UseFeaturesPermission = "UseFeaturesPermission";
        }

        public static class ScreenEvent
        {
            public const string RequestOpenScreen = "RequestOpenScreen"; //data: OpenScreenRequest
            public const string RequestCloseScreen = "RequestCloseScreen"; //data: CloseScreenRequest
            public const string RequestCloseCurrentPopup = "RequestCloseCurrentPopup"; //data: none
            public const string ScreenOpeningID = "ScreenOpeningId"; //data: ScreenId
            public const string RequestOpenCustomTextPopup = "request_open_custom_text_popup";

            public const string ShowToastPanel = "show_toast_panel"; //data: ToastData

            // public const string ShowDelayResponsePanel = "show_delay_response_panel";
            // public const string HideDelayResponsePanel = "hide_delay_response_panel";


            public static string GetScreenDataEventName(ScreenID screenID)
            {
                const string suffixes = "ClientScreenData";
                return $"{screenID}{suffixes}";
            }


            public static class Battle
            {
                public const string ENTER_SCREEN = "enter_battle_screen";
                public const string EXIT_SCREEN = "exit_battle_screen";

                public const string START_BATTLE = "start_battle";
                public const string END_BATTLE = "end_battle";

                public const string HERO_ATTACK = "hero_attack";
                public const string BOSS_ANIMATION = "boss_animation";
                public const string PLAY_EFFECT_ATTACK = "play_effect_attack";

                //public const string CHANGE_PHRASE_BATTLE = "change_phrase_battle";
                public const string CHANGE_BOSS_HEALTH = "change_boss_health"; //data: HealthBarBossData

                public const string SELECTING_CARD = "CLIENT_SLECTING_CARD"; //data: int
            }
        }

        public static class WidgetEvent
        {
            public const string CHANGE_THEME = "change_theme";
            public const string PLAY_SOUND_EFFECT = "play_sound_effect";
            public const string PLAY_SOUND_ACTION = "play_sound_action";
        }

        public static class CameraEvent
        {
            public const string MAIN_CAMERA = "main_camera";
        }


        public static class Model
        {
            public const string CreateHeroModel = "CreateHeroModel"; //data: CreateHeroModelRequest
            public const string ShowHeroModel = "ShowHeroModel"; //data: ShowHeroModelRequest


            public const string ShowBossModel = "ShowBossModel"; //data: ShowBossModelRequest

            public const string HideAllModels = "HideAllModels"; //none data
            public const string HidingAllModels = "HidingAllModels"; //data: bool

            public const string HeroModelPool = "HERO_MODEL_POOL"; //data: OptimizeHeroModelPool
        }
    }
}