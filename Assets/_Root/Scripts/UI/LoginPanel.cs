using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayFabClient
{
    public class LoginPanel
    {
        public event Action onConnect;
        public event Action onDisconnect;

        private LoginPanelUI _panelView;

        public LoginPanel(LoginPanelUI panelView, string panelName)
        {
            _panelView = panelView;
            _panelView.Init(Connect, panelName);
            _panelView.InitDisconnect(Disconnect);
            _panelView.gameObject.SetActive(true);
        }

        public void Connect()
        {
            onConnect?.Invoke();
        }

        public void Disconnect()
        {
            onDisconnect?.Invoke();
        }

        public void UpdateStatus(bool isSuccess)
        {
            if (isSuccess)
                _panelView.ChangeButtonView(Color.green, "Connected");
            else
                _panelView.ChangeButtonView(Color.red, "Not Connected");
        }
    }
}

