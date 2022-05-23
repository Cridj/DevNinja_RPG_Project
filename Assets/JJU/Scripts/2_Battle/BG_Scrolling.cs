using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scrolling : MonoBehaviour
{
    public Transform target; // 두개의 배경이 서로가 서로를 타겟
    public float scrollRange = 2f;
    public float moveSpeed = 3.0f;
    public Vector3 moveDirection = Vector3.left;

    private void Update()
    {
        if (BattleManager.I.bScrolling)
        {
            // 배경이 moveDirection 방향으로 moveSpeed의 속도로 이동
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            //배경이 설정된 범위를 벗어나면 위치 재설정
            if (transform.position.x <= -scrollRange)
            {
                transform.position = target.position + Vector3.right * scrollRange;
            }
        }
    }
}
