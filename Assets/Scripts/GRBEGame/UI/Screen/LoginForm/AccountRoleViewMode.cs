namespace GRBEGame.UI.Screen.LoginForm
{
    [System.Serializable]
    public class AccountRoleViewMode
    {
        public AccountRole accountRole;
    }

    public enum AccountRole
    {
        None = 0,
        Master = 1,
        Slave = 2,
    }
}