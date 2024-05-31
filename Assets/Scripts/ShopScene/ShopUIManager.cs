using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public Text currentYeopjeonText;
    public GameManagerTest gameManagerTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentYeopjeonText.text = gameManagerTest.yeopjeon.ToString();     
    }

    public void MoveToMainScene(){
        SceneManager.LoadScene("MainScene");
    }
}
