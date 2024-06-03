using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomData : MonoBehaviour
{
    private TMP_Text RoomInfoText;
    private RoomInfo room_Info;

    public TMP_InputField userIdText;
    public RoomInfo RoomInfo
    {
        get{
            return room_Info;
        }
        set{
            room_Info = value;
            //ex) room_03 (1/2)
            RoomInfoText.text = $"{room_Info.Name} ({room_Info.PlayerCount}/{room_Info.MaxPlayers})";
            //버튼의 클릭 이벤트에 함수를 연결
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(room_Info.Name));
        }
    }
    void Awake()
    {
        RoomInfoText = GetComponentInChildren<TMP_Text>();
        userIdText = GameObject.Find("InputField (TMP) - Nickname").GetComponent<TMP_InputField>();
    }
    void OnEnterRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 2;

        PhotonNetwork.NickName = userIdText.text;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
    }
}
