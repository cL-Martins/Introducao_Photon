using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [Header("Panel Player")]
    public GameObject goLogin;
    public InputField inpPlayerName;
    [HideInInspector] public string strPlayerName;

    [Header("Panel Room")]
    public GameObject goRoom;
    public InputField inpRoomName;


    // Start is called before the first frame update
    void Start()
    {
        strPlayerName = "Player" + Random.Range(100, 999);
        inpPlayerName.text = strPlayerName;

        inpRoomName.text = "Room" + Random.Range(100, 999);

        goLogin.gameObject.SetActive(true);
        goRoom.gameObject.SetActive(false);
    }

    public void BtnLogin()
    {
        if (inpPlayerName.text != "")
        {
            PhotonNetwork.NickName = inpPlayerName.text;
        }
        else
        {
            PhotonNetwork.NickName = strPlayerName;
        }
        PhotonNetwork.ConnectUsingSettings();
        goLogin.gameObject.SetActive(false);
    }

    public void BtnPartidasRapidas()
    {
        PhotonNetwork.JoinLobby();
    }

    public void BtnCriarSala()
    {
        string tempStrRoomName = inpRoomName.text;
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(tempStrRoomName, roomOptions, TypedLobby.Default);
    }



    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        Debug.Log("Server: " + PhotonNetwork.CloudRegion + " Ping: "+ PhotonNetwork.GetPing());
        goRoom.gameObject.SetActive(true);

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
        string roomTemp = "Room" + Random.Range(100, 999);
        PhotonNetwork.CreateRoom(roomTemp);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Debug.Log("Room Name: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Players in Room: " + PhotonNetwork.CurrentRoom.PlayerCount);
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
