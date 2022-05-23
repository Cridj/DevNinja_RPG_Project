using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;

public class CamShake : MonoBehaviour
{
    //ī�޶� ����
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
        //ī�޶� ��鸱 ��ġ��
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
