using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void PlaySinglePlayer()
    {
        // 개인 플레이 모드 로직 추가
        Debug.Log("Single Player Mode Selected");
        // 예: SceneManager.LoadScene("SinglePlayerScene");
    }

    public void PlayMultiplayer()
    {
        // 멀티 플레이 모드 로직 추가
        Debug.Log("Multiplayer Mode Selected");
        // 예: SceneManager.LoadScene("MultiplayerScene");
    }
}
