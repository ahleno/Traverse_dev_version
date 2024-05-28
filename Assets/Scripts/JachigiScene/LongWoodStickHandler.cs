using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWoodStickHandler : MonoBehaviour
{
    public Rigidbody rigidbody;
    private Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;

        lastPosition = transform.position;
        rigidbody.velocity = velocity;
    }
}
