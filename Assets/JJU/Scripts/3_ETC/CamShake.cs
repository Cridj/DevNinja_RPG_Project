using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;

public class CamShake : MonoBehaviour
{
    //카메라 흔들기
    public float ShakeAmount;
    public float ShakeTime;
    Vector3 initialPosition;

    public void VibrateForTime(float time)
    {
        ShakeTime = time;
    }

    private void Start()
    {
        initialPosition = transform.position;
        //카메라 흔들릴 위치값
    }

    private void Update()
    {

        if (ShakeTime > 0)
        {
            transform.position = new Vector3(Random.insideUnitSphere.x, 
                                             Random.insideUnitSphere.y * 
                                             0.5f, 0f) * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPosition;
        }
    }
}
