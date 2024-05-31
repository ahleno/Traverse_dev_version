using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;


public class shortWoodStickCollisionHandler : MonoBehaviour
{
    public Rigidbody rigidbody;
    public int force_up;
    public int force_forward;
    private int cnt = 0;
    public int gravity;
    public int torqueValue;

    private bool isAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("충돌");
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "LongWoodStick")
        {
            Rigidbody longStickRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            isAttacked = true;
            //Debug.Log(isAttacked);
            if (cnt == 0)
            {
                Debug.Log("최초 충돌, 상단 힘");
                Vector3 attackUP = Vector3.up * force_up;
                Vector3 torqueVector = new Vector3(torqueValue, Random.Range(1,3), 0);
                Debug.Log(attackUP);
                rigidbody.AddForce(attackUP, ForceMode.Impulse);
                rigidbody.AddTorque(torqueVector);
                cnt++;
            }
            else
            {
                //rigidbody.AddForce(longStickRigidbody.velocity, ForceMode.Impulse);
                rigidbody.AddForce(Vector3.forward * force_forward, ForceMode.Impulse);
                Debug.Log("추후 충돌, 자유 힘");
                Debug.Log(longStickRigidbody.velocity);
            }


        }


    }
}
