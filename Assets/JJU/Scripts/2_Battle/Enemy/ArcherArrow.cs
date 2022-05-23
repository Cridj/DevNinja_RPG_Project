using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    public GameObject archer;
    public GameObject target;
    public bool bShot;
    public Vector3 startPos;


    public float speed = 20f;
    public float launchHeight = 1.5f;

    public Vector3 movePosition;

    private float archerX;
    private float targetX;
    private float nextX;
    private float dist;
    private float baseY;
    private float height;

    private void Awake()
    {
        startPos = transform.position;
    }

    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        if (!bShot)
            return;
        archerX = archer.transform.position.x;
        targetX = target.transform.position.x;
        dist = targetX - archerX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(archer.transform.position.y, target.transform.position.y, (nextX - archerX) / dist);
        height = launchHeight * (nextX - archerX) * (nextX - targetX) / (-0.25f * dist * dist);

        movePosition = new Vector3(nextX, baseY + height, transform.position.z);

        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;

        if (movePosition == target.transform.position)
        {
            //Destroy(gameObject);
            transform.position = startPos;
            gameObject.SetActive(false);

        }
    }

    public static Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }
}

