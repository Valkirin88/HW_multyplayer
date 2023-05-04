using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _titleLabel;

    [SerializeField]
    private GameObject _newCharacterCreaterPanel;

    [SerializeField]
    private Button _createCharacterButton;

    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private List<SlotCharacterWidget> _slots;

    private string _characterName;


    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);

        GetCharacters();

        foreach (var slot in _slots)
            slot.SlotButton.onClick.AddListener(OpenCreateNewCharacter);

        _inputField.onValueChanged.AddListener(OnNameChanged);
        _createCharacterButton.onClick.AddListener(CreateCharacter);
    }

    private void CreateCharacter()
    {
        PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
        {
            CharacterName = _characterName,
            ItemId = "character_token"
        },
        result =>
        {
            UpdateCharacterStatisticsRequest(result.CharacterId);
        }, OnError
        );
    }

    private void UpdateCharacterStatisticsRequest(string characterId)
    {
        PlayFabClientAPI.UpdateCharacterStatistics(new UpdateCharacterStatisticsRequest 
        {
            CharacterId = characterId,
            CharacterStatistics = new Dictionary<string, int>
            {
                {"Level", 1 },
                {"Gold", 0 }
            }
        },
        result =>
        {
            Debug.Log("Complete!!!");
            CloseCreateNewCharacter();
            GetCharacters();
        }, OnError);
    }

    private void OnNameChanged(string changedName)
    {
        _characterName = changedName;
    }

    private void OpenCreateNewCharacter()
    {
        _newCharacterCreaterPanel.SetActive(true);
    }

    private void CloseCreateNewCharacter()
    {
        _newCharacterCreaterPanel.SetActive(false);
    }

    private void GetCharacters()
    {
        PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(),
            result =>
            {
                Debug.Log($"Character count: {result.Characters.Count}");
                ShowCharactersInSlot(result.Characters);
               
            }, OnError);
    }

    private void ShowCharactersInSlot(List<CharacterResult> characters)
    {
        if(characters.Count == 0)
        {
            foreach(var slot in _slots)
                slot.ShowEmptySlot();
        }
        else if(characters.Count >0 && characters.Count <_slots.Count)
        {
            PlayFabClientAPI.GetCharacterStatistics(new GetCharacterStatisticsRequest 
            {
                CharacterId = characters.First().CharacterId
            },
              result =>
              {
                    var level = result.CharacterStatistics["Level"].ToString();
                    var gold = result.CharacterStatistics["Gold"].ToString();

                  _slots.First().ShowInfoCharacterSlot(characters.First().CharacterName, level, gold);
              }, OnError);
            
          
        }
        else
        {
            Debug.LogError($"Add slots for characters.");
        }
    }

    private void OnGetAccount(GetAccountInfoResult result)
    {
        _titleLabel.text = $"Playfab id: {result.AccountInfo.PlayFabId}, {result.AccountInfo.Created}";

    }

    private void OnError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.Log(errorMessage);
    }
}
