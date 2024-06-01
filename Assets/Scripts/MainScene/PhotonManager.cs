using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using Unity.IO.LowLevel.Unsafe;
using TMPro;
using Photon.Pun.UtilityScripts;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 일단 한번 해본거------------------------------------------------------
    // private readonly string version = "1.0f";
    // private string userId = "Test";
    // void Awake()
    // {
    //     //같은 룸의 유저들에게 자동으로 씬 로딩
    //     PhotonNetwork.AutomaticallySyncScene = true;
    //     //같은 버전의 유저끼리 접속 허용
    //     PhotonNetwork.GameVersion = version;
    //     //유저 아이디  할당
    //     PhotonNetwork.NickName = userId;
    //     //포톤 서버와 통신 횟수 설정. 초당 30회
    //     Debug.Log(PhotonNetwork.SendRate);
    //     //서버 접속
    //     PhotonNetwork.ConnectUsingSettings();
    // }
    // //포톤서버에 접속 후 호출되는 콜백 함수
    // public override void OnConnectedToMaster()
    // {
    //     Debug.Log("Connected to Master!");
    //     Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
    //     PhotonNetwork.JoinLobby();
    // }
    // //로비에 접속 후 호출되는 콜백 함수
    // public override void OnJoinedLobby()
    // {
    //     Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
    //     PhotonNetwork.JoinRandomRoom();

    // }
    // //랜덤룸 입장이 실패했을때
    // public override void OnJoinRandomFailed(short returnCode, string message)
    // {
    //     Debug.Log($"JoinRandom Filed {returnCode}:{message}");
    //     //룸의 속성 정의
    //     RoomOptions ro = new RoomOptions();
    //     ro.MaxPlayers = 20;
    //     ro.IsOpen = true;
    //     ro.IsVisible = true;

    //     //룸 생성
    //     PhotonNetwork.CreateRoom("My Room", ro);

    // }
    // //룸 생성이 완료된 후 호출되는 콜백 함수
    // public override void OnCreatedRoom()
    // {
    //     Debug.Log($"Created Room");
    //     Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    // }
    // public override void OnJoinedRoom()
    // {
    //     Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
    //     Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

    //     //룸에 접속한 사용자 정보 확인
    //     foreach(var player in PhotonNetwork.CurrentRoom.Players)
    //     {
    //         Debug.Log($"룸에 접속한 사용자정보확인 = {player.Value.NickName},{player.Value.ActorNumber}");
            
    //     }
    // }
    // -----------------------------------------------------------------------------
    private readonly string gameVersion = "v1.0";
    private string userId = "default";
    public TMP_InputField userIdText;
    public TMP_InputField roomNameText;

    //룸 목록 저장
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();
    //룸을 표시할 프리팹
    public GameObject roomPrefab;
    public Transform scrollContent;

    private void Awake()
    {
        //방장이 혼자 씬을 로딩하면 나머지 사람들은 자동으로 싱크가 됨
        PhotonNetwork.AutomaticallySyncScene = true;
        //게임 버전 지정
        PhotonNetwork.GameVersion = gameVersion;
        //서버 접속
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("awake");

    }
    void Start()
    {
        Debug.Log($"1. {userId}가 포톤서버에 접속");
        //로비에 접속
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("2. 로비 접속");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("랜덤 룸 접속 실패");

        //룸 속성 설정
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 2;

        roomNameText.text = $"Room_{Random.Range(1, 100):000}";

        //룸을 생성 > 자동 입장됨
        PhotonNetwork.CreateRoom("room_1", ro);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("3. 방 생성 완료");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("4. 방 입장 완료");
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level_1");
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach(var room in roomList)
        {
            //룸이 삭제된 경우
            if(room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            //룸 정보가 갱신(변경)된 경우
            else
            {
                //룸이 처음 생성된 경우
                if(roomDict.ContainsKey(room.Name) == false)
                {
                    GameObject _room = Instantiate(roomPrefab, scrollContent);
                    _room.GetComponent<RoomData>().RoomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                //룸 정보를 갱신하는 경우
                else
                {
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
            }
        }
        Debug.Log("OnRoomListUpdate");
    }
    #region UI_BUTTON_CALLBACK
    // Random 버튼 클릭
    public void OnRandomBtm()
    {
        //ID 인풋필드가 비어있으면
        if(string.IsNullOrEmpty(userIdText.text))
        {
            //랜덤 아이디 부여
            userId = $"USER_{Random.Range(0,100):100}";
            userIdText.text = userId;
        }
        PlayerPrefs.SetString("USER_ID", userIdText.text);
        PhotonNetwork.NickName = userIdText.text;
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("랜덤룸 들어감");

    }
    //Room버튼 클릭 시 (룸 생성)
    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 2;

        if(string.IsNullOrEmpty(roomNameText.text))
        {
            //랜덤 룸 이름 부여
            roomNameText.text = $"Room_{Random.Range(1,100):100}";
        }
        PhotonNetwork.CreateRoom(roomNameText.text,ro);
        Debug.Log("룸 들어감");
    }
    #endregion
}
