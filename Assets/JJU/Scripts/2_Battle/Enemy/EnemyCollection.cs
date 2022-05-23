using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollection : Collection<Enemy>
{
    public void ClearChild()
    {
        GameObject[] enemies =  GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            Destroy(enemy);
        }   
    }
}
