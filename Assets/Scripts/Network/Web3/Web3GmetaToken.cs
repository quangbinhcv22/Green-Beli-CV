namespace Network.Web3
{
    public class Web3GmetaToken : Web3Token
    {
        public Web3GmetaToken(Web3Controller web3Controller, string contract, string account, string abi) : base(web3Controller, contract, account, abi)
        {
            // do something
            Decimals = 9;
            Name = "GMETA";
        }

        public override void Deposit(int number)
        {
            throw new System.NotImplementedException();
        }

        public override void WithDraw(int number)
        {
            throw new System.NotImplementedException();
        }
    }
}
