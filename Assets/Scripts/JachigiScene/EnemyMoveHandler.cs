using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHandler : MonoBehaviour
{
    public float distance = 5f;  // 움직일 최대 거리
    public float speed = 2f;     // 움직임의 속도

    private float originalZ;     // 시작 위치
    private float targetZ;       // 목표 위치

    private void Start()
    {
        originalZ = transform.position.z;
        targetZ = originalZ + distance;  // 시작 위치에서 거리만큼 더한 위치를 목표 위치로 설정
    }

    private void Update()
    {
        // 현재 위치를 계산 (시간에 따라 변화)
        float newZ = Mathf.PingPong(Time.time * speed, distance) + originalZ;

        // newX 값을 사용하여 오브젝트의 위치 업데이트
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}