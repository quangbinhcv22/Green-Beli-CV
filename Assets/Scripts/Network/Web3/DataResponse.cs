using System;

namespace Network.Web3
{
    public struct DataResponse<T>
    {
        public int error_code;
        public T data;
    }

    public struct RewardResponse
    {
        public string owner;
        public BigNumber reward;
        public int time;
        public string salt;
        public int v;
        public string r;
        public string s;
    }
    
    public struct BreedingResponse
    {
        public string owner;
        public long parent_1_code;
        public long parent_2_code;
        public BigNumber gfruit_price;
        public BigNumber grbe_price;
        public long mint_code;
        public float boosted;
        public int create_time;
        public string salt;
        public int v;
        public string r;
        public string s;
    }
    
    public struct FusionResponse
    {
        public string owner;
        public long main_code;
        public long support_code;
        public BigNumber gfruit_price;
        public BigNumber grbe_price;
        public int create_time;
        public string salt;
        public int v;
        public string r;
        public string s;
    }
    
    public struct OpenTreeResponse
    {
        public string owner;
        public int[] code;
        public string tokenA;
        public string tokenB;
        public BigNumber amountA;
        public BigNumber amountB;
        public string salt;
        public int v;
        public string r;
        public string s;
    }

    [Serializable]
    public struct WithDrawTokenInfo
    {
        public float fee;
    }

    public struct BigNumber
    {
        public string type;
        public string hex;
    }
}