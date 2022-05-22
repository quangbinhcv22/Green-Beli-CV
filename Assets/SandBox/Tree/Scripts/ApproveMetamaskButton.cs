using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Manager.Inventory;
using Network.Web3;
using UIFlow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ApproveMetamaskButton : MonoBehaviour
{
    [SerializeField] private MoneyType moneyType;
    [SerializeField] private Button button;

    
    private Web3Controller _web3Controller;

    private string account;

    private string abiTreeNFT;

    private bool _firstUpdate;
    
    public ApproveMetamaskButton(Web3Controller web3Controller, string account)
    {
        this._web3Controller = web3Controller;

        this.account = account;

        abiTreeNFT = _web3Controller.GetAbi("PlantTreeNFT");
    }

    private void Awake() => button.onClick.AddListener(Setup);

    private async void Setup()
    {
        if (_firstUpdate)
        {
            button.gameObject.SetActive(false);
            return;
        }
        UIRequest.ShowDelayPanel.SendRequest();

        switch (moneyType)
        {
            case MoneyType.BUsd:
                var approveBusd = await _web3Controller.BusdToken.CheckAllowance();
                if (approveBusd <= 0)
                {
                    _web3Controller.BusdToken.Approve();
                    UIRequest.HideDelayPanel.SendRequest();
                }
                UIRequest.HideDelayPanel.SendRequest();
                break;
            case MoneyType.GMeta:
                var approveGmeta = await _web3Controller.GmetaToken.CheckAllowance();
                if (approveGmeta <= 0)
                {
                    _web3Controller.GmetaToken.Approve();
                    UIRequest.HideDelayPanel.SendRequest();
                }
                UIRequest.HideDelayPanel.SendRequest();
                break;
            default:
                UIRequest.HideDelayPanel.SendRequest();
                break;
        }
        _firstUpdate = true;
    }
}