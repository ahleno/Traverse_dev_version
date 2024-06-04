using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
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

        if (collision.gameObject.name == "shortWoodStick")
        {
            Rigidbody shortStickRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            shortStickRigidbody.velocity = new Vector3(0,0,0);
            
        }
    }
}
