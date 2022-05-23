using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;

public class StageScene : HSingleton<StageScene>
{
    public PathFinding PathFinding;
    public Point targetPoint;

    public Point selectedPoint;
    public GameObject MapInfoPanel;
    public GameObject[] points;

    /// <summary>
    /// 스테이지 맵 정보 온오프 여부
    /// </summary>
    public bool bStageMapInfo;

    private void Awake()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
    }

    public void OnDisableInfoPanel()
    {
        MapInfoPanel.SetActive(false);
        foreach(var point in points)
        {
            point.GetComponent<Point>().bClicked = false;
        }
    }

    public void OnStartButon()
    {
        selectedPoint.StartPointEvent();              
    }
}
