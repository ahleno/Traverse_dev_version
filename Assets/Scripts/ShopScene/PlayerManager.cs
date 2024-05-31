using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform headTransform;
    public Transform chestTransform;
    public GameManagerTest gameManagerTest;
    public GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wear(int itemNum,bool signal){
        if(signal){
            if(itemNum <=2){
            instance = Instantiate(gameManagerTest.itemPrefabs[itemNum],headTransform.position,Quaternion.identity);
            instance.transform.SetParent(gameObject.transform);
            } else {
            instance = Instantiate(gameManagerTest.itemPrefabs[itemNum],chestTransform.position,Quaternion.identity);
            instance.transform.SetParent(gameObject.transform);    
            }
        } else {
            Destroy(instance);
        }   
    }
}
