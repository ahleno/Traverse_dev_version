using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform headTransform;
    public Transform rootTransform;
    public GameManagerTest gameManagerTest;
    public GameObject instanceHead;
    public GameObject instanceClothes;
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
            if(itemNum > 2){
                instanceHead = Instantiate(gameManagerTest.itemPrefabs[itemNum],headTransform.position+Vector3.up*0.04f,Quaternion.identity);
                instanceHead.transform.SetParent(gameObject.transform);
            } else {
                instanceClothes = Instantiate(gameManagerTest.itemPrefabs[itemNum],rootTransform.position+Vector3.down*0.65f+Vector3.left*0.05f,Quaternion.Euler(new Vector3(0,180,0)));
                instanceClothes.transform.SetParent(gameObject.transform);    
            }
        } else {
            if(itemNum > 2){
                Destroy(instanceHead);
            } else {
                Destroy(instanceClothes);
            }
        }   
    }
}
