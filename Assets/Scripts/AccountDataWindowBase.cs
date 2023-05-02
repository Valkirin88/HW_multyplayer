using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountDataWindowBase : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _usernameField;

    [SerializeField]
    private TMP_InputField _passwordField;

    protected string _username; 
    protected string _password;

    private void Start()
    {
        SubscriptionsElementUI();
    }

    protected virtual void SubscriptionsElementUI()
    {
        _usernameField.onValueChanged.AddListener(UpdateUsername);
        _passwordField.onValueChanged.AddListener(UpdatePassword);
    }

    private void UpdateUsername(string password)
    {
        _password = password;
    }

    private void UpdatePassword(string username)
    {
        _username = username;
    }
}
