namespace Network.Messages
{
    [System.Serializable]
    public struct MessageResponse<T>
    {
        public string id;
        public T data;
        public string error;

        public bool IsError => string.IsNullOrEmpty(error) is false || data is null;
    }
}