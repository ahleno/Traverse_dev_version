using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotWoodStickCollisionHandler : MonoBehaviour
{
    Rigidbody rigidbody;
    int force_up;
    private int cnt;
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
        if(collision.gameObject.name == "LongWoodStick")
        {
            if(cnt==0)
            {
                //충돌 에너지 적용
                rigidbody.AddForce(Vector3.up * force_up, ForceMode.Impulse);
                cnt++;
            }
            else
            {
                //collision의 가속도 가져와서 적용하기 
                rigidbody.AddRelativeForce();
            }

           
        }
    }
}
