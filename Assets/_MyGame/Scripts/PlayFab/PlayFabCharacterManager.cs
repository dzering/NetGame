using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Core;
using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;

public class PlayFabCharacterManager : MonoBehaviour
{
    [SerializeField] private List<SlotCharacterUI> _slots;
    [SerializeField] private CreateNewCharacterPanelUI _createNewCharacterPanel;
    
    private readonly List<CharacterResult> _characters = new();
    private string _characterName;
    private string _itemId = "character_token";
    private void Start()
    {
        _createNewCharacterPanel.gameObject.SetActive(false);
        _createNewCharacterPanel.SubscribeOnCreateButton(CreateNewCharacter);
        _createNewCharacterPanel.SubscribeOnInputFieldChanged(OnNameChanged);
        
        GetCharacters();
        foreach (var item in _slots)
        {
            item.SubscrideOnButtonCreateCharacter(OpenCreateNewCharacterPanel);
            item.SubscribeOnButtonSelectCharacter(OpenCharacter);
        }
    }
    private void OnDestroy()
    {
        _createNewCharacterPanel.UnsubscribeOnCreateButton(CreateNewCharacter);
        
        foreach (var item in _slots)
        {
                item.UnsubscribeOnButtonSelectCharacter(OpenCharacter);
                item.UnsubscribeOnButtonCreateCharacter(OpenCreateNewCharacterPanel);
        }
    }

    private void OpenCreateNewCharacterPanel()
    {
        _createNewCharacterPanel.gameObject.SetActive((true));
    }
    
    private void CreateNewCharacter()
    {
        CreateCharacterWithItemId(_itemId);
        CloseCreateCharacterPanel();
    }

    private void OpenCharacter()
    {
        
    }
    
    private void GetCharacters()
    {
        PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(),
            result =>
            {
                Debug.Log($"Characters owned: + {result.Characters.Count}");
                if(_characters.Count>0)
                    _characters.Clear();

                foreach (var character in result.Characters)
                {
                    _characters.Add(character);
                }
                ShowCharacterInSlots(_characters);
            }, Debug.LogError);
    }

    private void ShowCharacterInSlots(List<CharacterResult> characters)
    {
        PlayFabClientAPI.GetCharacterStatistics(new GetCharacterStatisticsRequest()
        {
            CharacterId = characters.First().CharacterId
        }, result =>
            {
                var level = result.CharacterStatistics["Level"].ToString();
                var XP = result.CharacterStatistics["XP"].ToString();
                var Gold = result.CharacterStatistics["Gold"].ToString();
                
                _slots.First().ShowFillSlot(level, XP, Gold);
            }, 
            OnError);
    }

    private void OnError(PlayFabError obj)
    {
        throw new NotImplementedException();
    }

    public void OnNameChanged(string changedName)
    {
        _characterName = changedName;
    }

    public void CreateCharacterWithItemId(string itemId)
    {
        PlayFabClientAPI.GrantCharacterToUser(
            new GrantCharacterToUserRequest
            {
                CharacterName = _characterName,
                ItemId = itemId,
                
            }, result =>
            {
                UpgradeCharacterStatistic(result.CharacterId);
            }, Debug.LogError);
    }

    private void UpgradeCharacterStatistic(string resultCharacterId)
    {
        PlayFabClientAPI.UpdateCharacterStatistics(
            new UpdateCharacterStatisticsRequest
            {
                CharacterId = resultCharacterId,
                CharacterStatistics = new Dictionary<string, int>
                {
                    {"Level", 1},
                    {"XP", 22},
                    {"Gold", 100}
                }
                
            }, result =>
            {
                Debug.Log($"Initial stats set, telling client to update character list");
                
            }, Debug.LogError);
    }

    private void CloseCreateCharacterPanel()
    {
        _createNewCharacterPanel.gameObject.SetActive(false);
    }
}
