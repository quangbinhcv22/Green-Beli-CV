using Newtonsoft.Json;

namespace Network.Messages
{
    public struct RequestData
    {
        public object data;
        public string id;


        public string ConvertToJson()
        {
            return data == null
                ? JsonConvert.SerializeObject(this.ConvertToNoneDataRequest())
                : JsonConvert.SerializeObject(this);
        }
        
        private NoneDataRequest ConvertToNoneDataRequest()
        {
            return new NoneDataRequest() { id = this.id };
        }
    }

    public struct NoneDataRequest
    {
        public string id;
    }
}