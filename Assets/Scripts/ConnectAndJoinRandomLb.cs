using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

public class ConnectAndJoinRandomLb : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{
    [SerializeField]
    private ServerSettings _serverSettings;

    [SerializeField]
    private TMP_Text _stateUiText;

    [SerializeField]
    private Button _closeRoomButton;

    [SerializeField]
    private TMP_Text _roomName;

    [SerializeField]
    private TMP_Text _lobbyList;

    [SerializeField]
    private Button _copyRoomNameButton;

    private LoadBalancingClient _lbc;

    private void Start()
    {
        _lbc = new LoadBalancingClient();
        _lbc.AddCallbackTarget(this);

        _closeRoomButton.onClick.AddListener(CloseRoom);
        _closeRoomButton.gameObject.SetActive(false);
        _copyRoomNameButton.onClick.AddListener(CopyRoomName);
        _copyRoomNameButton.gameObject.SetActive(false);
       
        _lbc.ConnectUsingSettings(_serverSettings.AppSettings);
    }

    private void OnDestroy()
    {
        _lbc.RemoveCallbackTarget(this);
    }

    private void Update()
    {
        if (_lbc == null)
            return;

        _lbc.Service();

        var state = _lbc.State.ToString();
        _stateUiText.text = state;
    }

    public void OnConnected()
    {

    }

    public void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        var roomOptions = new RoomOptions { MaxPlayers = 12 };
        var enterRoomParams = new EnterRoomParams { RoomName = "NewRoom" };
        _lbc.OpCreateRoom(enterRoomParams);
        //_lbc.OpJoinLobby(new TypedLobby("lobby", LobbyType.Default));

    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreateRoom");
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {

    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {

    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {

    }

    public void OnDisconnected(DisconnectCause cause)
    {

    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {

    }

    public void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        _roomName.text = _lbc.CurrentRoom.Name.ToString();
        _closeRoomButton.gameObject.SetActive(true);
        _copyRoomNameButton.gameObject.SetActive(true);
        

    }

    private void CloseRoom()
    {
        _lbc.CurrentRoom.IsVisible = false;
        Debug.Log($"Is room visible {_lbc.CurrentRoom.IsVisible}");
    }

    private void CopyRoomName()
    {
        EditorGUIUtility.systemCopyBuffer = _lbc.CurrentRoom.Name.ToString();
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
        _lbc.OpCreateRoom(new EnterRoomParams());
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
     
    }

    public void OnLeftLobby()
    {

    }

    public void OnLeftRoom()
    {

    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {

    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {

    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");

        foreach (var room in roomList)
            _lobbyList.text = string.Concat(_lobbyList.text, room.ToString());
    }
}
