using GEvent;
using GNetwork;
using TigerForge;
using UI.Widget.Toast;
using UnityEngine;

namespace SandBox.Tree.Scripts
{
    public class PlantToEarnServiceHandler : MonoBehaviour
    {

        private void OnEnable()
        {
            Setup();
        }

        private void Setup()
        {
            GetListTreeServerService.SendRequest();
        }
    }
}
