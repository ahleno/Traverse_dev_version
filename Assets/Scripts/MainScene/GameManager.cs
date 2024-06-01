using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake()
    {
        // 싱글톤 패턴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 예시: PlayerPrefs에 값을 저장하는 메서드
    public void SaveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // 예시: PlayerPrefs에서 값을 불러오는 메서드
    public int LoadData(string key)
    {
        return PlayerPrefs.GetInt(key, 0); // 키가 없으면 기본값 0을 반환
    }



    //사용할 때 예시
    // GameManager gameManager = FindObjectOfType<GameManager>();
        
    //     // 데이터 저장 예시
    //     gameManager.SaveData("PlayerScore", 100);
        
    //     // 데이터 불러오기 예시
    //     int playerScore = gameManager.LoadData("PlayerScore");
    //     Debug.Log("Player Score: " + playerScore);
}
