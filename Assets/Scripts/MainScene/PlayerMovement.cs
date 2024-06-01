using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToShopScene(){
        SceneManager.LoadScene("ShopScene");
        Debug.Log("상점 들어감");
    }
    public void MoveToTuhoScene(){
        SceneManager.LoadScene("Tuho");
        Debug.Log("투호 들어감");
    }
    public void MoveToJachigiScene(){
        SceneManager.LoadScene("Jachigi");
        Debug.Log("자치기 들어감");
    }
}
