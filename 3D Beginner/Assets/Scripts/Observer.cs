using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    private bool isPlayerInRange;
    public GameEnding gameEnding;

    private void OnTriggerEnter(Collider other) // 플레이어가 가고일의 시선범위에 들어왔을때 isPlayerInRange의 값을 true로 만들어주는 메서드
    {
        if(other.transform == player)
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other) // 플레이어가 가고일의 시선범위에 벗어났을때 isPlayerInRagne의 값을 false로 만들어주는 메서드
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if(isPlayerInRange == true) // isPlayerInRange의 값이 true일 때만 조건문이 실행
        {
            Vector3 direction = player.position - transform.position + Vector3.up; // 방향
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
