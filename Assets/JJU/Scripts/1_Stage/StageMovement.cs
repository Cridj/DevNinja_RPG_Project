using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour
{
    public LineRenderer lineRenderer;

    //Set speed
    public float speed;
    //Increasing value for lerp
    float moveSpeed;
    //Linerenderer's position index
    int indexNum;


    void Update()
    {
        if(lineRenderer.GetPosition(indexNum) == null || lineRenderer.GetPosition(indexNum + 1) == null)
        {
            return;
        }
        indexNum = Mathf.FloorToInt(moveSpeed);
        moveSpeed += speed / Vector2.Distance(lineRenderer.GetPosition(indexNum), lineRenderer.GetPosition(indexNum + 1));
        //and lerp
        transform.position = Vector2.Lerp(lineRenderer.GetPosition(indexNum), 
                                          lineRenderer.GetPosition(indexNum + 1), moveSpeed - indexNum);
    }
}
