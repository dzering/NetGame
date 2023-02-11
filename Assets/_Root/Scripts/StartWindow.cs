using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartWindow : MonoBehaviour
{
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Button _signInButton;

    [SerializeField] private Canvas _startCanvas;
    [SerializeField] private Canvas _createAccountCanvas;
    [SerializeField] private Canvas _signInCanvas;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        _signInButton.onClick.AddListener(OpenSignInWindow);
    }

    private void OpenSignInWindow()
    {
        _signInCanvas.enabled= true;
        _createAccountCanvas.enabled= false;
    }

    private void OpenCreateAccountWindow()
    {
        _signInCanvas.enabled= false;
        _createAccountCanvas.enabled= true;
    }
}
