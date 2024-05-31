using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JachigiGameManager : MonoBehaviour
{
    public GameObject shortWoodStick;  // Assign this in the inspector

    private Rigidbody rb;              // Rigidbody component of the object
    private Vector3 initialPosition;
    private Quaternion initialRotation;



    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = shortWoodStick.GetComponent<Rigidbody>();

        // 돌아갈 위치 
        initialPosition = shortWoodStick.transform.position;
        initialRotation = shortWoodStick.transform.rotation;
    }

    void Update()
    {
        
        if (true)// (shortWoodStick.isAttacked)
        {
            // Check if the velocity magnitude (speed) is 0
            if (rb.velocity.magnitude == 0)
            {
                // Calculate the distance from the origin (0,0,0)
                float distance = Vector3.Distance(shortWoodStick.transform.position, initialPosition);
                // Log the distances
                Debug.Log("Distance from the initial position: " + distance);
                // 점수 갱신 

                // 위치 초기화
                ResetPosition();
            }
        }
    }
    private void ResetPosition()
    {
        // 초기 상태로 만듬
        shortWoodStick.transform.position = initialPosition;
        shortWoodStick.transform.rotation = initialRotation;
        // shortWoodStick.isAttacked = false;
        rb.velocity = Vector3.zero;

    }
}
