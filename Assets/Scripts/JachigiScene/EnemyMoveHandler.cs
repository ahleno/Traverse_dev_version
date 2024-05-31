using UnityEngine;
public class EnemyMoveHandler : MonoBehaviour
{
    public float minDistance = 3f;  // 움직일 최소 거리
    public float maxDistance = 5f;  // 움직일 최대 거리
    public float minSpeed = 1f;     // 최소 속도
    public float maxSpeed = 3f;     // 최대 속도
    private float originalZ;        // 시작 위치 (X축 기준)
    private float distance;         // 이번 이동에 사용할 거리
    private float speed;            // 이번 이동에 사용할 속도

    private void Start()
    {
        originalZ = transform.position.z;
        ResetMovement();
    }

    private void Update()
    {
        // Mathf.PingPong 함수를 사용하여 X축 방향으로 좌우 반복 움직임 구현
        float newZ = Mathf.PingPong(Time.time * speed, distance * 2) - distance + originalZ;

        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    private void ResetMovement()
    {
        // 움직임 거리와 속도를 랜덤하게 결정
        distance = Random.Range(minDistance, maxDistance);
        speed = Random.Range(minSpeed, maxSpeed);

        originalZ = transform.position.z;
    }
}