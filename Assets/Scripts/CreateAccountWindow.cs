using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class CreateAccountWindow : AccountDataWindowBase
{
    [SerializeField]
    private TMP_InputField _mailField;

    [SerializeField]
    private Button _createAccountButton;

    private string _mail;

    protected override void SubscriptionsElementUI()
    {
        base.SubscriptionsElementUI();

        _mailField.onValueChanged.AddListener(UpdateMail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void UpdateMail(string mail)
    {
        _mail = mail;
    }

    private void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest 
        {
            Username = _username,
            Email = _mail,
            Password = _password
        },  result =>
        {
            Debug.Log($"Success: {_username}");
        }, error =>
        {
            Debug.Log($"Fail: {error.ErrorMessage}");
        });
    }
}
