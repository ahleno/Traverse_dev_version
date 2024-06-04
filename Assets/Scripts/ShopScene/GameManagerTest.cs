using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : MonoBehaviour
{
    public int yeopjeon;

    public int[] itemMoneyList;
    public GameObject[] itemPrefabs;
    public int[] wearingItems; // 0번 모자, 1번 옷
    public List<int> boughtItems = new List<int>();
    public PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy(int itemNum){
        if(boughtItems.FindIndex(x => x == itemNum) == -1){
            if(yeopjeon - itemMoneyList[itemNum] >= 0){
                yeopjeon = yeopjeon - itemMoneyList[itemNum];
                boughtItems.Add(itemNum);
            } else {
                Debug.Log("잔액 부족");
            }
        } else {
            Debug.Log("이미 구매했습니다.");
        }
        
    }

    public void WearOnOff(int itemNum){
        if(boughtItems.FindIndex(x => x == itemNum) != -1){
            if(itemNum > 2){ // 선택한 아이템이 모자인 경우
                if(wearingItems[0] == -1){ // 모자 자리가 비었을때
                    wearingItems[0] = itemNum;
                    playerManager.Wear(itemNum,true);
                } else { // 모자 이미 착용중 일때
                    if(wearingItems[0] == itemNum){ // 선택한 아이템이 착용중이 아이템과 같을 때 = 해제
                        wearingItems[0] = -1;
                        playerManager.Wear(itemNum,false);
                    } else { // 다를 때 = 해제하고 장착
                        playerManager.Wear(itemNum,false);
                        wearingItems[0] = itemNum;
                        playerManager.Wear(itemNum,true);
                    }
                }
            } else {
                if(wearingItems[1] == -1){ 
                    wearingItems[1] = itemNum;
                    playerManager.Wear(itemNum,true);
                } else { 
                    if(wearingItems[1] == itemNum){ 
                        wearingItems[1] = -1;
                        playerManager.Wear(itemNum,false);
                    } else {
                        playerManager.Wear(itemNum,false);
                        wearingItems[1] = itemNum;
                        playerManager.Wear(itemNum,true);
                    }
                }
            }
        } else {
            Debug.Log("보유 중인 아이템이 아닙니다");
        }
        
    }
}
