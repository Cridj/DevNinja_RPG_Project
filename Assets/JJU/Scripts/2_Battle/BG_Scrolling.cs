using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scrolling : MonoBehaviour
{
    public Transform target; // �ΰ��� ����� ���ΰ� ���θ� Ÿ��
    public float scrollRange = 2f;
    public float moveSpeed = 3.0f;
    public Vector3 moveDirection = Vector3.left;

    private void Update()
    {
        if (BattleManager.I.bScrolling)
        {
            // ����� moveDirection �������� moveSpeed�� �ӵ��� �̵�
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            //����� ������ ������ ����� ��ġ �缳��
            if (transform.position.x <= -scrollRange)
            {
                transform.position = target.position + Vector3.right * scrollRange;
            }
        }
    }
}
