namespace GRBEGame.UI.DataView
{
    public interface IMemberView<T>
    {
        public void UpdateDefault();
        public void UpdateView(T data);
    }
}