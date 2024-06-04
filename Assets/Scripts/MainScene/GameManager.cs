using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 현재 씬에서 GameManager 인스턴스를 찾기
                instance = FindObjectOfType<GameManager>();

                // 인스턴스를 찾지 못한 경우 새로운 GameManager 오브젝트 생성
                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(GameManager).ToString());
                    instance = singleton.AddComponent<GameManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        // 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 중복된 인스턴스가 생성된 경우 파괴
        }
    }
    public void SaveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
        Debug.Log($"Save Data = {key} : {value}");
    }
    void Start()
    {
        //게임 시작 시 엽전 0으로 초기화
        SaveData("yeopjeon", 0);
        Debug.Log($"yeopjeon = {PlayerPrefs.GetInt("yeopjeon", 0)}");

    }
}
