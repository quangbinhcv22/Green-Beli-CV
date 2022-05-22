using UnityEngine;

namespace UI.Widget.Toast
{
    [CreateAssetMenu(fileName = "GreenBeliToastDataSet", menuName = "ScriptableObjects/GreenBeliToastDataSet")]
    public class GreenBeliToastDataSet : UnityEngine.ScriptableObject
    {
        public ToastData heroHasChanged;

        public ToastData anotherTransactionBeingProcessed;

        public ToastData successfullyDeposited;
        public ToastData depositFailed;

        public ToastData successfullyWithdrawn;
        public ToastData withdrawalFailed;
        
        public ToastData successfullyRestoreLevelsHero;
        public ToastData restoreLevelsHeroFailed;
        
        public ToastData treeHasChanged;
    }
}