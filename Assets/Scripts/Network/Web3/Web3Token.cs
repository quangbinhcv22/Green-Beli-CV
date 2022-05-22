using System;
using System.Numerics;
using Cysharp.Threading.Tasks;
using Log;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Web3
{
    public abstract class Web3Token
    {
        private string Contract { get; }

        protected string Account { get; }

        public string Abi { get; }
    
        protected int Decimals { get; set; }
    
        protected string Name { get; set; }

        protected readonly Web3Controller _web3Controller;

        protected Web3Token(Web3Controller web3Controller, string contract, string account, string abi)
        {
            this._web3Controller = web3Controller;
            this.Contract = contract;
            this.Account = account;
            this.Abi = abi;
            this.Decimals = 18;
        }

        public void SwapToken()
        {
            Application.OpenURL(string.Format(BlockChainConfig.urlSwapToken, BlockChainConfig.busdTokenContract,
                Contract));
        }

        public async UniTask<int> GetToken()
        {
            return await GetToken(Account, Abi, Contract);
        }
    
        private async UniTask<int> GetToken(string account, string abi, string tokenContract)
        {
            GLogger.LogLog($"Get Token: {account} => {tokenContract}");
            if (account.Length == 0)
            {
                return 0;
            }

            // smart contract method to call
            string method = "balanceOf";
            // array of arguments for contract
            string[] obj = {account};
            string args = JsonConvert.SerializeObject(obj);

            // display response in game
            try
            {
                string response = await EVM.Call(BlockChainConfig.chain, BlockChainConfig.network, tokenContract, abi,
                    method, args, BlockChainConfig.rpc);
                GLogger.LogLog($"Get Token Response: {Name} - {response}");
                return Web3Convert.EtherToInt(response, Decimals);
            }
            catch (Exception e)
            {
                GLogger.LogLog("Something is wrong: " + e);
                return 0;
            }
        }

        public async UniTask<BigInteger> CheckAllowance()
        {
            return await CheckAllowance(Account, Abi, Contract);
        } 
    
        private async UniTask<BigInteger> CheckAllowance(string account, string abi, string contract)
        {
            // smart contract method to call
            string method = "allowance";
            // array of arguments for contract
            object[] obj = {account, BlockChainConfig.approveContract};
            string args = JsonConvert.SerializeObject(obj);

            // display response in game
            try
            {
                // connects to user's browser wallet to call a transaction
                string response = await EVM.Call(BlockChainConfig.chain, BlockChainConfig.network,
                    contract, abi, method, args, BlockChainConfig.rpc);

                GLogger.LogLog($"Allowance: {response}");

                var result = BigInteger.TryParse(response, out var limit);
                if (result == false)
                {
                    //throw new Exception($"error {method}: " + response);
                    _web3Controller.ShowToastPanel("Got Error Allowance");
                    return -1;
                }

                return limit;
            }
            catch
            {
                _web3Controller.ShowToastPanel("Got Error Allowance");
                return -1;
            }
        }

        public void Approve()
        {
            Approve(Abi, Contract).Forget();
        }
    
        private async UniTaskVoid Approve(string abi, string contract)
        {
#if UNITY_WEBGL
            // smart contract method to call
            string method = "approve";
            // array of arguments for contract
            object[] obj =
            {
                BlockChainConfig.approveContract, Web3Convert.IntToEther(int.MaxValue).ToString()
            };
            string args = JsonConvert.SerializeObject(obj);

            // display response in game
            try
            {
                string response = await Web3GL.SendContract(method, abi, contract, args, "0");
                GLogger.LogLog(response);
            }
            catch (Exception e)
            {
                GLogger.LogLog("Something is wrong: " + e);
                _web3Controller.ShowToastPanel("Got Error Approve");
            }
#else
            await UniTask.Yield();
#endif
        }

        public abstract void Deposit(int number);

        public abstract void WithDraw(int number);
    }
}
