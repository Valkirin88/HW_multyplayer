using UnityEngine.UI;
using UnityEngine;
using System;
using PlayFab;
using PlayFab.ClientModels;

public class SignInWindow : AccountDataWindowBase
{
    [SerializeField]
    private Button _signInButton;

    protected override void SubscriptionsElementUI()
    {
        base.SubscriptionsElementUI();

        _signInButton.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _password,
        }, result =>
        {
            Debug.Log($"Success: {_username}");
        }, error =>
        {
            Debug.Log($"Fail: {error.ErrorMessage}");
        }
        );
    }
}
