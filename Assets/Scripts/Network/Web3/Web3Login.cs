using System;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Network.Web3
{
    public class Web3Login
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void Web3Connect();

        [DllImport("__Internal")]
        private static extern string ConnectAccount();

        [DllImport("__Internal")]
        private static extern void SetConnectAccount(string value);

        public async UniTask<string> GetAccount()
        {
            Web3Connect();

            return await OnConnected();
        }

        private async UniTask<string> OnConnected()
        {
            var address = ConnectAccount();
            while (address.Equals(String.Empty))
            {
                await new WaitForSeconds(1f);
                address = ConnectAccount();
            }

            // reset login message
            SetConnectAccount("");

            return address;
        }
#else
        public async UniTask<string> GetAccount()
        {
            await UniTask.WaitForEndOfFrame();
#if UNITY_EDITOR
            return BlockChainConfig.testAddress;
#else
            return string.Empty;
#endif
        }
#endif
    }
}