namespace Service.Server
{
    public interface IConvertedResponseClientData<R>
    {
        public void SetDataFromResponse(R response);
    }
}