using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string tuhoGameVersion = "v1.0_tuho";
    private readonly string jachigiGameVersion = "v1.0_jachigi";

    private string gameVersion;
    private string selectedGameMode; // 선택된 게임 모드 변수 추가

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
        gameVersion = tuhoGameVersion; // 초기값은 투호 게임 버전으로 설정
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

        roomNameText.text = $"{selectedGameMode}_Room_{Random.Range(1, 100):000}"; // 게임 모드에 따라 방 이름 설정

        //룸을 생성 > 자동 입장됨
        PhotonNetwork.CreateRoom(roomNameText.text, ro);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("3. 방 생성 완료");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("4. 방 입장 완료");
        if (PhotonNetwork.IsMasterClient)
        {
            if (gameVersion == tuhoGameVersion)
            {
                PhotonNetwork.LoadLevel("Tuho_Level");//씬 이름 넣기
            }
            else if (gameVersion == jachigiGameVersion)
            {
                PhotonNetwork.LoadLevel("Jachigi_Level");
            }
        }
        Debug.Log($"{selectedGameMode} 방 입장");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach (var room in roomList)
        {
            //룸이 삭제된 경우
            if (room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            //룸 정보가 갱신(변경)된 경우
            else
            {
                //룸이 처음 생성된 경우
                if (roomDict.ContainsKey(room.Name) == false)
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
        if (string.IsNullOrEmpty(userIdText.text))
        {
            //랜덤 아이디 부여
            userId = $"USER_{Random.Range(0, 100):100}";
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

        if (string.IsNullOrEmpty(roomNameText.text))
        {
            //랜덤 룸 이름 부여
            roomNameText.text = $"{selectedGameMode}_Room_{Random.Range(1, 100):100}";
        }
        PhotonNetwork.CreateRoom(roomNameText.text, ro);
        Debug.Log("룸 들어감");
    }

    // 투호 모드 선택 버튼 클릭
    public void OnSelectTuhoMode()
    {
        gameVersion = tuhoGameVersion;
        selectedGameMode = "Tuho";
        Debug.Log($"게임모드 : {selectedGameMode}");
    }

    // 자치기 모드 선택 버튼 클릭
    public void OnSelectJachigiMode()
    {
        gameVersion = jachigiGameVersion;
        selectedGameMode = "Jachigi";
        Debug.Log($"게임모드 : {selectedGameMode}");
    }
    #endregion
}
