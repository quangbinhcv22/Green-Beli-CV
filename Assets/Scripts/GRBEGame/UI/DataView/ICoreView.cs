namespace GRBEGame.UI.DataView
{
    public interface ICoreView<T>
    {
        public void UpdateDefault();
        public void UpdateView(T data);
        public void AddCallBackUpdateView(IMemberView<T> memberView);
    }
}