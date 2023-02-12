using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LoadingWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private AccountWindowBase[] _accountWindowBase;

    private void Start()
    {
        foreach (var item in _accountWindowBase)
        {
            item.OnLoadingStarted += ShowText;
            item.OnLoadingFinished += ShowResultText;
        }
        _loadingText.gameObject.SetActive(false);
    }

    private void ShowResultText(bool obj)
    {
        if (obj)
            _loadingText.gameObject.SetActive(false);
        else
        {
            _loadingText.text = "FAIL CONNECTION";
        }
              
    }

    private void ShowText()
    {
        _loadingText.gameObject.SetActive(true);
        StartCoroutine(Blinking());
        _loadingText.text = "LOADING";
    }

    private void OnDestroy()
    {
        foreach (var item in _accountWindowBase)
        {
            item.OnLoadingStarted -= ShowText;
            item.OnLoadingFinished -= ShowResultText;
        }

        StopCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        Color color = _loadingText.color;

        float alphaStart = color.a;
        float currentAlpha = alphaStart;

        float delta = alphaStart * 0.7f;

        float a = -1;
        while (true)
        {
            if (currentAlpha <= alphaStart - delta)
                a = 1;
            if (currentAlpha >= alphaStart)
                a = -1;

            currentAlpha = currentAlpha + a * delta / 100;

            color.a = currentAlpha;
            _loadingText.color = color;


        yield return null;

        }
    }
}
