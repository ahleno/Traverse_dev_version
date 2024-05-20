using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class shortWoodStickCollisionHandler : MonoBehaviour
{
    public Rigidbody rigidbody;
    public int force_up;
    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("충돌");
        // Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name =="LongWoodStick")
        {
            if (cnt == 0)
            {
                Debug.Log("최초 충돌, 상단 힘");
                Vector3 attackUP = Vector3.up * force_up;
                Debug.Log(attackUP);
                rigidbody.AddForce(attackUP, ForceMode.Impulse);
                cnt++;
            }
            else
            {
                Debug.Log("2타 이후 충돌");
            }
        }
        
    }
}

/*
 * if(collision.gameObject.name == "LongWoodStick")
        {
            Debug.Log("충돌");
            if (cnt >= 2) return; // 단일 타겟
            if(cnt==0)
            {
                //충돌 에너지 적용
                rigidbody.AddForce(Vector3.up * force_up, ForceMode.Impulse);
                Debug.Log("충돌1");
            }
            else
            {
                
                Vector3 accel = OVRInput.GetLocalControllerAcceleration(OVRInput.Controller.Hands); 
                Vector3 stickForce = accel * rigidbody.mass;

                rigidbody.AddForceAtPosition(stickForce, transform.position, ForceMode.Force);
                Debug.Log("충돌2");
            }
            cnt++;
        }
 * 
 */