using System.Linq;
using GEvent;
using GRBESystem.Entity;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.Widget;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel.UpdateEnergyWhenSkipAll
{
   public class EnergySkipAllUpdater : MonoBehaviour
   {
      [SerializeField] private ProcessBar energyBar;


      private void Awake()
      {
         EventManager.StartListening(EventName.Server.SkipAllGame, UpdateEnergyInfo);
      }

      private void UpdateEnergyInfo()
      {
         var skipAllResponse = SkipAllGameServerService.Response;
         if (skipAllResponse.IsError || skipAllResponse.data == null) return;

         var energyLoser = NetworkService.playerInfo.energyInfo.EnergyNeedForBattle() * skipAllResponse.data.Count();
         NetworkService.playerInfo.energyInfo.Current -= energyLoser;

         UpdateView();
      }

      private void UpdateView()
      {
         var energyInfo = EventManager.GetData<EnergyInfo>(EventName.Client.Energy.UpdateEnergy);
         energyBar.UpdateView(NetworkService.playerInfo.energyInfo.Current, energyInfo.Capacity);
      }
   }
}
