using GRBEGame.UI.ConfirmPopup;

namespace GEvent
{
    public static partial class EventName
    {
        public static class Mechanism
        {
            public const string Confirm = "Confirm";
            public static string ConfirmValue(ConfirmID confirmID) => $"ConfirmValue{confirmID}";
        }
    }
}