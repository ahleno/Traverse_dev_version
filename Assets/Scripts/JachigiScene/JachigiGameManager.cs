using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JachigiGameManager : MonoBehaviour
{
    public static JachigiGameManager gm;
    public Text totalScoreText;
    public Text maxScoreText;
    public Text chanceCntText;
    
    public GameObject shortWoodStick; 

    private shortWoodStickCollisionHandler shortWoodStickCollisionHandler;
    private Rigidbody rb;             
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private int chanceNum;
    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
    }

    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = shortWoodStick.GetComponent<Rigidbody>();
        shortWoodStickCollisionHandler = shortWoodStick.GetComponent<shortWoodStickCollisionHandler>();
        // 돌아갈 위치 
        initialPosition = shortWoodStick.transform.position;
        initialRotation = shortWoodStick.transform.rotation;

        // Playerfabs에서 최고 점수 불러오기 
        // maxScoreText.text = PlayerPrefs.GetInt("JachigiHighScore").ToString();
        totalScoreText.text = "0";
        chanceNum = shortWoodStickCollisionHandler.cnt;
        chanceCntText.text = chanceNum.ToString();
    }

    void Update()
    {
        if (shortWoodStickCollisionHandler.isAttacked && (chanceNum > 0))
        {
            // Check if the velocity magnitude (speed) is 0
            if (rb.velocity.magnitude == 0)
            {
                // Calculate the distance from the origin (0,0,0)
                float distance = Vector3.Distance(shortWoodStick.transform.position, initialPosition);
                // Log the distances
                Debug.Log("점수 : " + distance);
                // 점수 갱신 
                String newScore = Math.Round((float.Parse(totalScoreText.text)+ distance),1).ToString();
                totalScoreText.text = newScore;
                // 위치 초기화
                ResetPosition();
            }
        }

        // 점수 갱신
        /*
         * 
         * if (int.Parse(totalScoreText.text) > int.Parse(maxScoreText.text))
        {
            PlayerPrefs.SetInt("JachigiHighScore",int.Parse(totalScoreText.text));
            maxScoreText.text = totalScoreText.text;
            PlayerPrefs.Save();
            Debug.Log("======점수 갱신 및 저장======");
        }
         */


    }
    private void ResetPosition()
    {
        // 초기 상태로 만듬
        shortWoodStick.transform.position = initialPosition;
        shortWoodStick.transform.rotation = initialRotation;
        shortWoodStickCollisionHandler.isAttacked = false;
        shortWoodStickCollisionHandler.cnt = 0;
        // shortWoodStick.isAttacked = false;
        rb.velocity = Vector3.zero;
        chanceNum--;
        chanceCntText.text = chanceNum.ToString();


    }
}
