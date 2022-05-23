using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathControll : MonoBehaviour
{
    public Point[] path1;
    public Point[] path2;

    private void Awake()
    {
      
    }

    //private void Start()
    //{
    //    int i = 0;
    //    foreach (Point p in path1)
    //    {
    //        if (p.bOpen)
    //        {
    //            GameInstance.Instance.gamdData.path1.pathOpen[i] = true;
    //        }
    //        if (p.bClear)
    //        {
    //            GameInstance.Instance.gamdData.path1.pathClear[i] = true;
    //        }
    //        i++;
    //    }
    //    i = 0;
    //    foreach (Point p in path2)
    //    {
    //        if (p.bOpen)
    //        {
    //            GameInstance.Instance.gamdData.path2.pathOpen[i] = true;
    //        }
    //        if (p.bClear)
    //        {
    //            GameInstance.Instance.gamdData.path2.pathClear[i] = true;
    //        }
    //        i++;
    //    }
    //}
}
