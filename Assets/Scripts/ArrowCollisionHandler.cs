using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollisionHandler : MonoBehaviour
{

    public GameObject arrowFactory;

    public GameObject spawnPosition;

    void Awake()
    {
        arrowFactory = Resources.Load<GameObject>("Prefab/Arrow"); // "ArrowPrefab"은 프리팹의 경로
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.name == "TargetPoint")
            {
                GameController.instance.GetScore();
            }

            else if (other.gameObject.name == "Plane")
            {

            }
        }
    }
}