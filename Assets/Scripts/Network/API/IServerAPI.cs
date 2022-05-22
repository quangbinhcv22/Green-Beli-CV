namespace GNetwork
{
    public interface IServerAPI
    {
        public string APIName { get; }
        public void OnResponse(string message);
    }
}