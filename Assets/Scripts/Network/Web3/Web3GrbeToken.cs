namespace Network.Web3
{
    public class Web3GrbeToken : Web3Token
    {
        public Web3GrbeToken(Web3Controller web3Controller, string contract, string account, string abi) : base(web3Controller, contract, account, abi)
        {
            // do something
            Name = "GRBE";
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
