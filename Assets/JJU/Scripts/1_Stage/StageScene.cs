using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MHomiLibrary;
using TMPro;
using UnityEngine.UI;

public class StageScene : HSingleton<StageScene>
{
    public PathFinding PathFinding;
    public Point targetPoint;

    public Point selectedPoint;
    public Animator MapInfoAnim;
    public GameObject[] points;
    public StageInfoManager stageInfoManager;

    public TextMeshProUGUI goldText;

    public Image shopImage;

    public Animator FadeAnimator;

    /// <summary>
    /// 스테이지 맵 정보 온오프 여부
    /// </summary>
    public bool bStageMapInfo;

    private void Awake()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
    }

    private void Start()
    {
        GoldTextUpdate();
        if(DataManager.Instance.m_PlayerData.nStack < 5)
        {
            shopImage.color = Color.gray;
        }
        else
        {
            shopImage.color = Color.white;
        }
    }

    public void GoldTextUpdate()
    {
        goldText.text = DataManager.Instance.m_PlayerData.nCoin.ToString();
    }



    public void OnDisableInfoPanel()
    {
        MapInfoAnim.SetBool("Click", false);
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
