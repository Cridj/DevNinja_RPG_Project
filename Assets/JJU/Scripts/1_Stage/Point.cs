using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

enum POINT_TYPE { BATTLE, SP_BATTLE, ITEM, SHOP, HEALLING, BOSS }

public class Point : MonoBehaviour
{
    [SerializeField]
    POINT_TYPE type = new POINT_TYPE();

    public ENEMY_TYPE[] enemies;
    public ENEMY_TYPE eliteEnemy;

    public Sprite notOpenSprite;
    public Sprite openSprite;
    public Sprite clearSprite;

    public Image spriteImage;

    [SerializeField]
    public GameObject[] egdes;

    public GraphNode<Point> node;

    public Vector3 worldPos;
    public bool bClicked;


    [Header("변수들")]
    
    public int stageNum;

    /// <summary>
    /// 가로위치
    /// </summary>
    public float fX;

    /// <summary>
    /// 세로위치
    /// </summary>
    public float fY;

    /// <summary>
    /// 해당 스테이지가 오픈되어 있는지 체크
    /// </summary>
    public bool bOpen;

    /// <summary>
    /// 해당 포인트 클리어여부
    /// </summary>
    public bool bClear;

    /// <summary>
    /// 해당스테이지에 위치해 있는지 체크
    /// </summary>
    public bool bStay;

    /// <summary>
    /// 이동할 씬
    /// </summary>
    public string moveSceneName;



    private void Awake()
    {
        InitVertex();
        worldPos = Camera.main.ScreenToWorldPoint(transform.position);
        worldPos.x = (float)(Math.Truncate(worldPos.x * 100) / 100);
        worldPos.y = (float)(Math.Truncate(worldPos.y * 100) / 100);

    }

    private void Start()
    {
        bOpen = GameInstance.Instance.gamdData.pathData[stageNum].pathOpen;
        bClear = GameInstance.Instance.gamdData.pathData[stageNum].pathClear;
        fX = (float)(Math.Truncate(transform.position.x * 100) / 100);
        fY = (float)(Math.Truncate(transform.position.y * 100) / 100);

        if (bOpen && !bClear)
        {
            spriteImage.sprite = openSprite;

        }
        else if (bClear)
        {
            spriteImage.sprite = clearSprite;
        }
        else
        {
            spriteImage.sprite = notOpenSprite;
        }
    }

    public void InitVertex()
    {
        node = new GraphNode<Point>();
        node.Data = this;

         
        //foreach (GameObject point in egdes)
        //{
        //    if (node != null)
        //    {
        //        node._neighbors.Add(
        //        point.GetComponent<Point>().node);
        //    }

        //}
    }



    //public void ClearAndOpenNextPoint()
    //{
    //    this.bClear = true;
    //    foreach(Point p in NextPoint)
    //    {
    //        p.bOpen = true;
    //    }
    //}


    public void ClickButton()
    {
        if (!bOpen || bClear)
            return;
        print("버튼눌림");
        //StageScene.I.MapInfoPanel.SetActive(true);

        if (!bClicked)
        {
            StageScene.I.selectedPoint = this;
            StageScene.I.MapInfoPanel.SetActive(true);
            foreach (var point in StageScene.I.points)
            {
                Point pointComp =point.GetComponent<Point>();
                if (pointComp.bClicked == true)
                {                   
                    pointComp.bClicked = false;
                    print("클릭해제");
                }
            }
            bClicked = true;
            return;
        }    
        
        //StartPointEvent();
    }


    /// <summary>
    /// 해당 포인트로 이동했을때 실행되는 함수
    /// </summary>
    public void StartPointEvent()
    {
        GameInstance.Instance.nowStage = stageNum;
        switch (enemies.Length)
        {
            case 1:
                GameInstance.Instance.enemies = new ENEMY_TYPE[enemies.Length];
                GameInstance.Instance.enemies[0] = enemies[0];
                GameInstance.Instance.eliteEnemy = eliteEnemy;
                break;

            case 2:
                GameInstance.Instance.enemies = new ENEMY_TYPE[enemies.Length];
                GameInstance.Instance.enemies[0] = enemies[0];
                GameInstance.Instance.enemies[1] = enemies[1];
                GameInstance.Instance.eliteEnemy = eliteEnemy;
                break;

            case 3:
                GameInstance.Instance.enemies = new ENEMY_TYPE[enemies.Length];
                GameInstance.Instance.enemies[0] = enemies[0];
                GameInstance.Instance.enemies[1] = enemies[1];
                GameInstance.Instance.enemies[2] = enemies[2];
                GameInstance.Instance.eliteEnemy = eliteEnemy;
                break;
        }

        //GameInstance.Instance.gamdData.InPoint = this;
        switch (type)
        {
            case POINT_TYPE.BATTLE:
                //배틀 스테이지로 이동하기
                BattleRoom();
                break;

            case POINT_TYPE.SP_BATTLE:
                //특수 스테이지로 이동
                SpecialRoom();
                break;

            case POINT_TYPE.ITEM:
                //아이템 3개중에 한개를 정해서 가져가기
                ItemRoom();
                break;

            case POINT_TYPE.SHOP:
                //상점 이용
                ShopRoom();
                break;

            case POINT_TYPE.HEALLING:
                //체력을 일부 회복하는 룸
                HealingRoom();
                break;

            case POINT_TYPE.BOSS:
                //보스와 전투가 벌어지는 룸
                BossRoom();
                break;

        }
    }

    ////////////////////////////////////////////////////////////////
    //---------------------사용자 정의함수------------------------//
    ////////////////////////////////////////////////////////////////
    

    private void BattleRoom()
    {
        SceneLoad.LoadScene("Battle");
    }

    private void SpecialRoom()
    {
        print("스페셜룸");
    }

    private void ItemRoom()
    {
        print("아이템룸");
    }

    private void ShopRoom()
    {
        print("상점룸");
    }

    private void HealingRoom()
    {
        print("힐링 룸");
    }

    private void BossRoom()
    {
        print("보스룸");
    }
}
