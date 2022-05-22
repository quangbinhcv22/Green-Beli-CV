namespace Network.Messages
{
    public interface IDeserializeResponseMessage<D>
    {
        public MessageResponse<D> DeserializeResponseMessage(string message);
    }
}