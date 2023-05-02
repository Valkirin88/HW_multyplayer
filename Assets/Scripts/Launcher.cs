using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Button _connectButton;
    [SerializeField]
    private Button _disconnectButton;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _connectButton.onClick.AddListener(Connect);
        _disconnectButton.onClick.AddListener(Disconnect);
        _disconnectButton.enabled = false;
    }

   
    private void Disconnect()
    {
        PhotonNetwork.Disconnect();

        _connectButton.enabled = true;
        _disconnectButton.enabled = false;
        Debug.Log("Disconnected");
    }

    private void Connect()
    {
        if (PhotonNetwork.IsConnected)
            return;

        PhotonNetwork.ConnectUsingSettings();
        _connectButton.enabled = false;
        _disconnectButton.enabled = true;
        Debug.Log("Connected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectToMaster");
    }
}
