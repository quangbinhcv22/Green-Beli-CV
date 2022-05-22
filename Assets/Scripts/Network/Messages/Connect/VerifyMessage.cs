namespace Network.Messages
{
    public struct VerifySignatureRequest
    {
        public string address;
        public string signature;
    }

    public struct VerifySignatureResponse
    {
        public string token;
    }
}