using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [Header("Panel Player")]
    [HideInInspector] public string _strPlayerName;
    public GameObject _goPnlLogin;
    public InputField _inpPlayerName;
    public GameObject _myPlayerPrefab;

    [Header("Panel Room")]
    public GameObject _goPnlRoom;
    public InputField _inpRoomName;

    //------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        _strPlayerName = "Player" + Random.Range(100, 999);
        _inpPlayerName.text = _strPlayerName;

        _inpRoomName.text = "Room" + Random.Range(100, 999);

        _goPnlLogin.gameObject.SetActive(true);
        _goPnlRoom.gameObject.SetActive(false);
    }

    public void BtnLogin()
    {
        if (_inpPlayerName.text != "")
        {
            PhotonNetwork.NickName = _inpPlayerName.text;
        }
        else
        {
            PhotonNetwork.NickName = _strPlayerName;
        }
        PhotonNetwork.ConnectUsingSettings();
        _goPnlLogin.gameObject.SetActive(false);
    }

    public void BtnPartidasRapidas()
    {
        PhotonNetwork.JoinLobby();
    }

    public void BtnCriarSala()
    {
        string _strRoomNameTemp_ = _inpRoomName.text;
        RoomOptions _roomOptionsTemp_ = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(_strRoomNameTemp_, _roomOptionsTemp_, TypedLobby.Default);
    }


    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        Debug.Log("Server: " + PhotonNetwork.CloudRegion + " Ping: "+ PhotonNetwork.GetPing());
        _goPnlRoom.gameObject.SetActive(true);

        //PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        string _roomTemp_ = "Room" + Random.Range(100, 999);
        PhotonNetwork.CreateRoom(_roomTemp_);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Debug.Log("Room Name: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Players in Room: " + PhotonNetwork.CurrentRoom.PlayerCount);

        _goPnlLogin.gameObject.SetActive(false);
        _goPnlRoom.gameObject.SetActive(false);

        PhotonNetwork.Instantiate(_myPlayerPrefab.name, _myPlayerPrefab.transform.position, _myPlayerPrefab.transform.rotation,0);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected: " + cause);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
