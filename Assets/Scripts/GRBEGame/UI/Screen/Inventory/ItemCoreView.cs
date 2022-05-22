namespace GRBEGame.UI.Screen.Inventory
{
    public interface ICoreView<T>
    {
        public void UpdateDefault();
        public void UpdateView(T data);
        public void AddCallBackUpdateView(IMemberView<T> memberView);
    }

    public interface IMemberView<T>
    {
        public void UpdateDefault();
        public void UpdateView(T data);
    }
}
