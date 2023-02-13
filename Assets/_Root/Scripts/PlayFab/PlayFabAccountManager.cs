using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;


public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetInfo, OnError);
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogSuccess, OnError);
    }

    private void OnGetCatalogSuccess(GetCatalogItemsResult result)
    {
        Debug.Log("CatalogSuccess");
        ShowCatalog(result.Catalog);
    }

    private void ShowCatalog(List<CatalogItem> resultCatalog)
    {
        foreach (var item in resultCatalog)
        {
            Debug.Log($"{item.ItemId}");
        }
    }

    private void OnGetInfo(GetAccountInfoResult obj)
    {
        titleText.text = $"Player id: {obj.AccountInfo.PlayFabId}\n" +
            $"Username: {obj.AccountInfo.Username}\n" +
            $"Created: {obj.AccountInfo.Created}";
    }

    private void OnError(PlayFabError obj)
    {
        var errorMessage = obj.GenerateErrorReport();
        Debug.Log(errorMessage);
    }
}
