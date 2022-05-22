using GEvent;
using Log;
using TigerForge;
using UnityEngine;

namespace Manager.Inventory
{
    public class InventoryExample
    {
        private global::Inventory inventory;
        
        public InventoryExample()
        {
            inventory = new global::Inventory();
            
            EventManager.StartListening(EventName.Inventory.Add, MoneyAdd);
            EventManager.StartListening(EventName.Inventory.Sub, MoneySub);
            EventManager.StartListening(EventName.Inventory.Change, MoneyChange);
            

            inventory.AddMoney(MoneyType.GFruit, 100);
            inventory.SubMoney(MoneyType.GFruit, 60);
        }
        
        private void MoneyChange()
        {
            var data = EventManager.GetData(EventName.Inventory.Change);
            GLogger.LogLog("Data Change: " + data);
        }
        
        private void MoneyAdd()
        {
            var data = EventManager.GetData(EventName.Inventory.Change);
            GLogger.LogLog("Data Add: " + data);
        }
        
        private void MoneySub()
        {
            var data = EventManager.GetData(EventName.Inventory.Change);
            GLogger.LogLog("Data Sub: " + data);
        }
    }
}